namespace Windsong.VersionManager.ViewModels
{
    public class CompareVersionViewModel
    {
        public dynamic Latest { get; set; }
        public dynamic Compare { get; set; }
        public int LatestVersion { get; set; }
        public int CompareVersion { get; set; }
        public string ContentType { get; set; }
        public string Title { get; set; }
    }
}