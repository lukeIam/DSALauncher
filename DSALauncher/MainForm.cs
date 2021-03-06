﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSALauncher
{
    public partial class MainForm : Form
    {
        // Launcher to open pdf open commands
        private Launcher _launcher;
        // Server instance to allow the companion browser extension to open pdfs.
        private SimpleHttpServer _server;

        /// <summary>
        /// Instantiates a new main form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
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

            string command = input.Text.ToLower().Trim();

            // Exit requested?
            if (command == "exit")
            {
                _server.Stop();
                Application.Exit();
            }

            // Trigger the launch of pdf the requested by the given command
            bool result = _launcher.Launch(command);

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

        private async void MainForm_Load(object senderLoad, EventArgs eLoad)
        {
            await Init();
        }

        /// <summary>
        /// Initializes the whole programm.
        /// </summary>
        /// <returns>async task</returns>
        private async Task Init()
        {
            // Load settings from settings.json file
            Settings settings = null;
            try
            {
                settings = Settings.LoadFromFile("settings.json");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Could not load 'settings.json'", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            // Create a Launcher
            try
            {
                _launcher = new Launcher(settings);
                var notLoadedFiles = _launcher.Init();
                if (notLoadedFiles.Length > 0)
                {
                    MessageBox.Show(string.Join("\n", notLoadedFiles), "Some files where missing", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Could not load launcher", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }

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
                try
                {
                    _server = new SimpleHttpServer(settings.WebserverPort, _launcher);
                    await _server.Start();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Could not start webserver", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
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
