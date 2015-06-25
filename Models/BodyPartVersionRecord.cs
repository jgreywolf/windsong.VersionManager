using Orchard.ContentManagement.Records;
using Orchard.Core.Common.Models;
using Orchard.Data.Conventions;

namespace Windsong.VersionManager.Models {
    public class BodyPartVersionRecord : ContentPartVersionRecord {
        [StringLengthMax]
        public virtual string Text { get; set; }

        public virtual string Format { get; set; }
        public virtual BodyPartRecord BodyPartRecord { get; set; }
        public virtual int ContentItem_Id { get; set; }
        public virtual int ContentItemVersion_Id { get; set; }
    }
}