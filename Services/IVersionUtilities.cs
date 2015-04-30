using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;

namespace Windsong.VersionManager.Services
{
    public interface IVersionUtilities : IDependency {
        string GetUser();
    }
}