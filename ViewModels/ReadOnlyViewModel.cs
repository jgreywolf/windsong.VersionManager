using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Localization;
using Orchard.Security;

namespace Windsong.VersionManager.ViewModels
{
    public class ReadOnlyViewModel
    {
        public bool ReadOnly { get; set; }
        public IUser ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public LocalizedString Message { get; set; }
        public int ContentId { get; set; }
    }
}