using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.Data.Conventions;
using Orchard.Utility.Extensions;
using Orchard.ContentManagement.MetaData.Models;

namespace Windsong.VersionManager.Models
{
    public class ContentTypeVersion
    {
        public string DisplayName { get; set; }
        public int Version { get; set; }
        //public string ModifiedBy { get; set; }
        public ContentTypeDefinition TypeDefinition { get; set; }
    }
}