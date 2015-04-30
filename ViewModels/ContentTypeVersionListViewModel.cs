using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.ViewModels
{
    public class ContentTypeVersionListViewModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<ContentTypeVersion> Versions { get; set; }
    }
}