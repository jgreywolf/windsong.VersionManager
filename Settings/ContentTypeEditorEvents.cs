using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Orchard;
using Orchard.AuditTrail.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.Records;
using Orchard.ContentManagement.ViewModels;
using Orchard.ContentTypes.Events;
using Orchard.ContentTypes.Services;
using Orchard.Data;
using Windsong.VersionManager.Models;
using Windsong.VersionManager.Services;

namespace Windsong.VersionManager.Settings
{
    public class ContentTypeEditorEvents : ContentDefinitionEditorEventsBase
    {
        private readonly IVersionManagerService _versionManager;

        public ContentTypeEditorEvents(IVersionManagerService versionManager)
        {
            _versionManager = versionManager;
        }

        public override void TypeEditorUpdated(ContentTypeDefinitionBuilder builder)
        {
            _versionManager.SaveContentTypeVersion(builder.Current); 
            base.TypeEditorUpdated(builder);
        }
    }
}