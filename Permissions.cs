using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;

namespace Windsong.VersionManager
{
    public class Permissions : IPermissionProvider
    {

        public static readonly Permission SetReadOnlyStateForContent = new Permission { Description = "Manage Read Only state for any content item", Name = "SetReadOnlyStateForContent" };
        public static readonly Permission SetReadOnlyStateForOwnContent = new Permission { Description = "Lock or Unlock own content", Name = "SetReadOnlyStateForOwnContent", ImpliedBy = new[] { SetReadOnlyStateForContent } };

        public static readonly Permission MetaListContent = new Permission { ImpliedBy = new[] { SetReadOnlyStateForOwnContent } };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                SetReadOnlyStateForOwnContent,
                SetReadOnlyStateForContent
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {SetReadOnlyStateForContent}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {SetReadOnlyStateForContent}
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = new[] {SetReadOnlyStateForOwnContent}
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = new[] {SetReadOnlyStateForOwnContent}
                }
            };
        }

    }
}