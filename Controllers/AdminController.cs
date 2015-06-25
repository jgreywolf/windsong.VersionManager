using System.Globalization;
using System.Net;
using System.Web.Routing;
using Orchard;
using Orchard.AuditTrail.Helpers;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc.Extensions;
using Orchard.Security;
using System;
using System.Web.Mvc;
using Orchard.UI.Notify;
using Windsong.VersionManager.Models;
using Windsong.VersionManager.Services;
using Windsong.VersionManager.ViewModels;
using System.Net.Http;

namespace Windsong.VersionManager.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IContentManager _contentManager;
        private readonly IVersionManagerService _versionManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizer _authorizer;
        private INotifier _notifier;

        public AdminController(
            IAuthorizer authorizer,
            IContentManager contentManager,
            IAuthenticationService authenticationService,
            IVersionManagerService versionManager,
            INotifier notifier)
        {
            _contentManager = contentManager;
            _authorizer = authorizer;
            _authenticationService = authenticationService;
            _versionManager = versionManager;
            _notifier = notifier;
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public ActionResult ContentItemVersions(int id)
        {
            var item = _contentManager.Get(id);
            var itemTitle = item.Has<TitlePart>() ? item.As<TitlePart>().Title : String.Empty;

            var viewModel = new ContentItemVersionListViewModel
            {
                ContentId = id,
                ContentItem = item,
                ContentType = item.ContentType,
                CurrentTitle = itemTitle,
                ReadOnly = item.Has<ReadOnlySettings>() && item.As<ReadOnlySettings>().ReadOnly,
                Versions = _versionManager.GetContentItemVersionList(id)
            };

            return View(viewModel);
        }

        public ActionResult PromoteVersion(int id, int versionId)
        {
            var versionToPromote = _contentManager.Get(id, VersionOptions.Number(versionId));

            if (versionToPromote == null)
                return HttpNotFound();

            if (!_authorizer.Authorize(Orchard.Core.Contents.Permissions.EditContent, versionToPromote))
                return new HttpUnauthorizedResult();

            var newVersionNumber = _versionManager.BuildNewContentItemVersion(versionToPromote);

            if (newVersionNumber == 0)
            {
                _notifier.Error(T("Version {0} of this content CANNOT be promoted to latest, as this content is in a Read Only state", versionId));
            }
            else
            {
                _notifier.Information(T("Version {0} of this content has been promoted to latest (draft) as version {1}", versionId, newVersionNumber));   
            }

            return RedirectToAction("Edit", "Admin", new { area = "", id = id});
        }

        public ActionResult ViewVersion(int id, int versionId)
        {
            var contentItem = _contentManager.Get(id, VersionOptions.Number(versionId));

            if (contentItem == null)
                return HttpNotFound();

            if (!_authorizer.Authorize(Orchard.Core.Contents.Permissions.ViewContent, contentItem))
                return new HttpUnauthorizedResult();

            var model = _contentManager.BuildEditor(contentItem);
            return View(model);
        }

        public ActionResult CompareVersion(int id, int versionId)
        {
            var latest = _contentManager.Get(id, VersionOptions.Latest);
            var compare = _contentManager.Get(id, VersionOptions.Number(versionId));

            if (latest == null || compare == null)
                return HttpNotFound();

            if (!_authorizer.Authorize(Orchard.Core.Contents.Permissions.ViewContent, latest))
                return new HttpUnauthorizedResult();

            var viewModel = new CompareVersionViewModel()
            {
                Latest = _contentManager.BuildEditor(latest),
                Compare = _contentManager.BuildEditor(compare),
                LatestVersion = latest.Version,
                CompareVersion = compare.Version,
                ContentType = latest.ContentType,
                Title = latest.Has<TitlePart>() ? latest.As<TitlePart>().Title : ""
            };
            return View(viewModel);
        }

        public HttpResponseMessage SetReadOnlyState(int contentId, bool isReadOnly)
        {
            var currentUser = _authenticationService.GetAuthenticatedUser();
            var item = _contentManager.Get(contentId, VersionOptions.Latest);
            if (item == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            if (!_authorizer.Authorize(Permissions.SetReadOnlyStateForContent, item)) {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            var settings = item.As<ReadOnlySettings>();
            settings.ReadOnly = isReadOnly;
            settings.ModifiedBy = currentUser.UserName;
            settings.ModifiedDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());

        }
    }
}