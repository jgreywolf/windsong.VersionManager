using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using Windsong.VersionManager.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.Data;
using Windsong.VersionManager.Services;

namespace Windsong.VersionManager.Handlers
{
    public class VersionHandler: ContentHandler {
        private readonly IVersionUtilities _utilities;
        public VersionHandler(IVersionUtilities utilities) {
            _utilities = utilities;
            OnUpdated<ContentPart>((context, part) => UpdateVersionInfo(part, context));
        }

        protected override void Activating(ActivatingContentContext context)
        {
            context.Builder.Weld<VersionInfoSettings>();
        }
        private void UpdateVersionInfo(IContent item, UpdateContentContext context)
        {
            var settings = item.As<VersionInfoSettings>();
            if (!String.IsNullOrWhiteSpace(settings.ModifiedBy)) {
                settings.ModifiedBy = _utilities.GetUser();
            }
        }
    }
}