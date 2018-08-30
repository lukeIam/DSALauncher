using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSALauncher
{
    public partial class MainForm : Form
    {
        // Launcher to open pdf open commands
        private readonly Launcher _launcher;
        // Server instance to allow the companion browser extension to open pdfs.
        private readonly SimpleHttpServer _server;

        /// <summary>
        /// Instantiates a new main form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Load settings from settings.json file
            var settings = Settings.LoadFromFile("settings.json");
            // Create a Launcher
            _launcher = new Launcher(settings);

            // Autocomple keywords
            var autoCompleteSource = new AutoCompleteStringCollection();
            autoCompleteSource.AddRange(_launcher.GetKeywords());
            input.AutoCompleteCustomSource = autoCompleteSource;
            input.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // AlwaysTop
            this.TopMost = settings.AlwaysTop;
            
            // Inactivity opacity
            double opacity = Math.Max(0, Math.Min(1, settings.InactiveOpacity));
            if (Math.Abs(opacity - 1) > 0.001)
            {
                input.GotFocus += (sender, args) => this.Opacity = 1;
                input.LostFocus += (sender, args) => this.Opacity = opacity;
            }

            // Start webserver if requested
            if (settings.WebserverActive)
            {
                _server = new SimpleHttpServer(settings.WebserverPort, _launcher);
                _server.Start();
            }
        }

        /// <summary>
        /// Handles the keyup event of the imput field (waiting for Enter).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void input_KeyUp(object sender, KeyEventArgs e)
        {
            // Enter triggers the search
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            // Trigger the launch of pdf the requested by the given command
            bool result = _launcher.Launch(input.Text.Trim());

            if (result)
            {
                // Success -> PDF was opened
                input.Text = "";
                ColorChangeInput(Color.LightGreen);
            }
            else
            {
                // Failed -> PDF not found
                ColorChangeInput(Color.LightCoral);
            }
        }

        /// <summary>
        /// Temporally change the imput color of the imput field for an fiven amount of time.
        /// </summary>
        /// <param name="targetColor">The temporarry color.</param>
        /// <param name="delay">The amount of time before changing back to the old color.</param>
        private async void ColorChangeInput(Color targetColor, int delay = 500)
        {
            var oldColor = input.BackColor;
            input.BackColor = targetColor;
            await Task.Delay(delay);
            input.BackColor = oldColor;
        }

        // Allow drag around of the form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void input_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }
}
