using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.ViewModels
{
    public class ContentItemVersionListViewModel
    {
        public int ContentId { get; set; }
        public ContentItem ContentItem { get; set; }
        public string ContentType { get; set; }
        public string CurrentTitle { get; set; }
        public bool ReadOnly { get; set; }
        public IEnumerable<ContentItemVersion> Versions { get; set; }
    }
}