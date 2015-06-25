using System;
using System.Collections.Generic;
using System.Globalization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Core.Common.Models;
using Orchard.Core.Contents.Settings;
using Orchard.Localization;
using Orchard.Security;
using Windsong.VersionManager.Models;
using Windsong.VersionManager.ViewModels;

namespace Windsong.VersionManager.Drivers
{
    public class ReadOnlyDriver : ContentPartDriver<ContentPart>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMembershipService _membershipService;

        public ReadOnlyDriver(
            IAuthenticationService authenticationService,
            IAuthorizationService authorizationService,
            IMembershipService membershipService) {

            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _membershipService = membershipService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }
        protected override string Prefix
        {
            get { return "ReadOnlySettings"; }
        }

        protected override DriverResult Editor(ContentPart part, dynamic shapeHelper)
        {
            var settings = part.As<ReadOnlySettings>() ?? new ReadOnlySettings { ReadOnly = false };

            var viewModel = new ReadOnlyViewModel
            {
                ReadOnly = settings.ReadOnly,
                ModifiedBy = (!String.IsNullOrWhiteSpace(settings.ModifiedBy) ? _membershipService.GetUser(settings.ModifiedBy) : null),
                ModifiedDate = (!String.IsNullOrWhiteSpace(settings.ModifiedDate) ? DateTime.Parse(settings.ModifiedDate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal) : DateTime.UtcNow),
                ContentId = part.ContentItem.Id
            };

            viewModel.Message = viewModel.ReadOnly == null ? T("Last set: never") :
                T("Last set by {1} at {2}", viewModel.ModifiedBy.UserName, viewModel.ModifiedDate.ToString());

            return ContentShape("PaFDDDrts_ReadOnly_Edit",
                                () => shapeHelper.EditorTemplate(TemplateName: "PaFDDDrts.ReadOnly.Edit", Model: viewModel, Prefix: Prefix));
        }
    }
}