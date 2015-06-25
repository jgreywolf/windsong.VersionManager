using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Common.Models;
using Orchard.DisplayManagement.Shapes;
using Orchard.Localization;
using Orchard.Security;
using Windsong.VersionManager.Models;
using Windsong.VersionManager.ViewModels;

namespace Windsong.VersionManager.Drivers
{
    public class ContentVersionInfoDriver : ContentPartDriver<CommonPart>
    {
        protected override DriverResult Editor(CommonPart part, dynamic shapeHelper)
        {
            if (!part.As<ContentItem>().Record.Versions.Any())
            {
                return null;
            }
            return ContentShape("Parts_ContentItem_VersionInfo_Edit",
                () => shapeHelper.Parts_ContentItem_VersionInfo_Edit());
        }
    }
}