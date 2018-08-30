using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DSALauncher
{
    /// <summary>
    /// User settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Path to executable of the pdf viewer.
        /// </summary>
        public string PdfViewer { get; set; }

        /// <summary>
        /// Arguments to open a pdf and jump to a page.
        /// </summary>
        public string PdfCommandPage { get; set; }

        /// <summary>
        /// Arguments to open a pdf and trigger a search.
        /// </summary>
        public string PdfCommandSearch { get; set; }

        /// <summary>
        /// A path that will be used to make relative paths absolute.
        /// </summary>
        public string PdfBasePath { get; set; }

        /// <summary>
        /// If true, the application alway stays in foreground.
        /// </summary>
        public bool AlwaysTop { get; set; }

        /// <summary>
        /// A number between 0 and 1 that decides how opaque the inactive window should be.
        /// </summary>
        public double InactiveOpacity { get; set; }

        /// <summary>
        /// If true, a webserver will be started to allow the companion browser extension to open pdfs. 
        /// </summary>
        public bool WebserverActive { get; set; }

        /// <summary>
        /// The port the webserver should use.
        /// The port must unlocked for the user or the user needs administrative rights.
        /// netsh http add urlacl url=http://*:[Port]/ user=DOMAIN\user
        /// </summary>
        public int WebserverPort { get; set; }

        /// <summary>
        /// Fileinfos (pdf paths, keywords, page offsets)
        /// </summary>
        public List<LaunchData> Files { get; set; }

        /// <summary>
        /// Load the user settings form a path.
        /// </summary>
        /// <param name="path">The path to the user settings json file.</param>
        /// <returns>A Settings object with all the read settings.</returns>
        public static Settings LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Config file not found");
            }

            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                Settings settings = (Settings)serializer.Deserialize(file, typeof(Settings));
                return settings;
            }
        }
    }
}
