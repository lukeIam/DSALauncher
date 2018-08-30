using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DSALauncher
{
    public class Launcher
    {
        // Settings
        private readonly Settings _settings;
        // Mapping keyword -> (pdf path, pdf page offset)
        private readonly Dictionary<string, (string path, int offset)> _keyMapping = new Dictionary<string, (string, int)>();

        /// <summary>
        /// Creates a new instance of the Launcher.
        /// </summary>
        /// <param name="settings">Settings to use.</param>
        public Launcher(Settings settings)
        {
            _settings = settings;
            foreach (LaunchData file in _settings.Files)
            {
                foreach (string keyword in file.Keywords)
                {
                    string path = file.PdfPath;
                    if (!Path.IsPathRooted(path))
                    {
                        path = Path.Combine(_settings.PdfBasePath, path);
                    }
                    _keyMapping.Add(keyword.ToLowerInvariant().Trim(), (path, file.Offset));
                }
            }
        }

        /// <summary>
        /// Opens the pdf file at the requested position.
        /// </summary>
        /// <param name="command">the open command.</param>
        /// <returns>True if successful, False otherwise.</returns>
        public bool Launch(string command)
        {
            string lowerCommand = command.ToLowerInvariant().Trim();

            // Check for direct match
            if (_keyMapping.ContainsKey(lowerCommand))
            {
                LaunchPdfViewer(_keyMapping[lowerCommand]);
                return true;
            }

            var splittedCommand = lowerCommand.Split(' ');

            if (splittedCommand.Length == 1)
            {
                // Check for "keywordPage"
                Regex expr = new Regex(@"^(?'str'[^0-9]*)(?'page'\d+)$");
                var match = expr.Match(lowerCommand);
                if (match.Success && _keyMapping.ContainsKey(match.Groups["str"].Value))
                {
                    LaunchPdfViewer(_keyMapping[match.Groups["str"].Value], Convert.ToInt32(match.Groups["page"].Value));
                    return true;
                }
            }
            else
            {
                string newCommand = string.Join(" ", splittedCommand.Reverse().Skip(1).Reverse());
                if (_keyMapping.ContainsKey(newCommand))
                {
                    string lastCommandPart = splittedCommand[splittedCommand.Length - 1];

                    // Check for "keyword page"
                    if (int.TryParse(lastCommandPart, out int page))
                    {
                        LaunchPdfViewer(_keyMapping[newCommand], page);
                        return true;
                    }

                    // Check for "keyword searchWord"
                    LaunchPdfViewer(_keyMapping[newCommand], lastCommandPart);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns all possible keywords.
        /// </summary>
        /// <returns>Array of all possible keywords.</returns>
        public string[] GetKeywords()
        {
            return _keyMapping.Keys.ToArray();
        }

        /// <summary>
        /// Launch viewer and navigate to a specific page.
        /// </summary>
        /// <param name="pdfInfo">Tuple of pdf path and page offset for this pdf.</param>
        /// <param name="pageNumber">The target page number.</param>
        private void LaunchPdfViewer((string path, int offset) pdfInfo, int pageNumber = 1)
        {
            LaunchExe(string.Format(_settings.PdfCommandPage, pdfInfo.path, pageNumber+pdfInfo.offset));
        }

        /// <summary>
        /// Launch viewer and trigger a search for a given keyword.
        /// </summary>
        /// <param name="pdfInfo">Tuple of pdf path and page offset for this pdf.</param>
        /// <param name="searchKeyword">The keyword to search for.</param>
        private void LaunchPdfViewer((string path, int offset) pdfInfo, string searchKeyword)
        {
            LaunchExe(string.Format(_settings.PdfCommandSearch, pdfInfo.path, searchKeyword));
        }

        /// <summary>
        /// Launches the pdf viewer application with given arguments
        /// </summary>
        /// <param name="arguments">The arguments to use.</param>
        private void LaunchExe(string arguments)
        {
            ProcessStartInfo startInfo =
                new ProcessStartInfo
                {
                    FileName = _settings.PdfViewer,
                    Arguments = arguments,
                    UseShellExecute = false
                };
            (new Process {StartInfo = startInfo}).Start();
        }
    }
}
