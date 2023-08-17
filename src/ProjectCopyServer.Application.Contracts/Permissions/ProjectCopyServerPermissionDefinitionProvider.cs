using ProjectCopyServer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ProjectCopyServer.Permissions;

public class ProjectCopyServerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProjectCopyServerPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ProjectCopyServerPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProjectCopyServerResource>(name);
    }
}
