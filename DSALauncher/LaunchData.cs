using System.Collections.Generic;


namespace DSALauncher
{
    /// <summary>
    /// Stores a pdf path and all it's keywords + pdf page ogffset.
    /// </summary>
    public class LaunchData
    {
        public List<string> Keywords { get; set; }
        public string PdfPath { get; set; }
        public int Offset { get; set; }
    }
}
