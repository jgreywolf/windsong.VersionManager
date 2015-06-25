using Orchard.ContentManagement;

namespace Windsong.VersionManager.Models {
    public class BodyPartVersion : ContentPart<BodyPartVersionRecord> {
        public string Text {
            get { return Retrieve(x => x.Text); }
            set { Store(x => x.Text, value); }
        }

        public string Format {
            get { return Retrieve(x => x.Format); }
            set { Store(x => x.Format, value); }
        }

        public int ContentItem_Id { get; set; }
        public int ContentItemVersion_Id { get; set; }

        public BodyPartVersionRecord BodyPartVersionRecord { get; set; }
    }
}
