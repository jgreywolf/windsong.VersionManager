using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.UI.Notify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Windsong.VersionManager.Models;
using Windsong.VersionManager.Services;
using Windsong.VersionManager.ViewModels;

namespace Windsong.VersionManager.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IContentManager _contentManager;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IVersionManagerService _versionManager;
        private dynamic Shape { get; set; }

        public AdminController(
            IOrchardServices orchardServices,
            IContentManager contentManager,
            IContentDefinitionManager contentDefinitionManager,
            IVersionManagerService versionManager,
            IShapeFactory shapeFactory)
        {
            Services = orchardServices;
            _contentManager = contentManager;
            _contentDefinitionManager = contentDefinitionManager;
            _versionManager = versionManager;
            Shape = shapeFactory;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public IOrchardServices Services { get; private set; }
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