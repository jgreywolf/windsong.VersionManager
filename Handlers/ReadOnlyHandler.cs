using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Localization;
using Windsong.VersionManager.Models;

namespace Windsong.VersionManager.Handlers
{
    public class ReadOnlyHandler : ContentHandler 
    {
        public ReadOnlyHandler()
        {
            T = NullLocalizer.Instance;
            OnPublishing<ContentPart>((context, part) => PublishContentItem(part, context));
            OnUnpublishing<ContentPart>((context, part) => UnpublishContentItem(part, context));
            OnRemoving<ContentPart>((context, part) => RemoveContentItem(part, context));
        }

        public Localizer T { get; set; }

        protected override void Activating(ActivatingContentContext context) {
            context.Builder.Weld<ReadOnlySettings>();
        }

        public void PublishContentItem(ContentPart part, PublishContentContext context) {
            var settings = part.As<ReadOnlySettings>();
            if (settings.ReadOnly) {
                context.Cancel = true;
            }
        }

        public void UnpublishContentItem(ContentPart part, PublishContentContext context) {
            var settings = part.As<ReadOnlySettings>();
            if (settings.ReadOnly)
            {
                context.Cancel = true;
            }
        }

        public void RemoveContentItem(ContentPart part, RemoveContentContext context) {
            var settings = part.As<ReadOnlySettings>();
            if (settings.ReadOnly) {
                //context.
            }
        }
    }
}