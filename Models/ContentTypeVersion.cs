using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.Utility.Extensions;
using Orchard.ContentManagement.MetaData.Models;

namespace Windsong.VersionManager.Models
{
    public class ContentTypeVersion
    {
        public int Version { get; set; }
        [StringLength(128)]
        public string Name { get; private set; }
        [Required, StringLength(1024)]
        public string DisplayName { get; private set; }
        public IEnumerable<ContentTypePartDefinition> Parts { get; private set; }
        public SettingsDictionary Settings { get; private set; }

        public ContentTypeVersion(ContentTypeDefinition type)
        {
            Name = type.Name;
            DisplayName = type.DisplayName;
            Parts = type.Parts.ToReadOnlyCollection();
            Settings = type.Settings;
        }
    }
}