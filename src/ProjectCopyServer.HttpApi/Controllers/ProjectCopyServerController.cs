using ProjectCopyServer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ProjectCopyServer.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProjectCopyServerController : AbpControllerBase
{
    protected ProjectCopyServerController()
    {
        LocalizationResource = typeof(ProjectCopyServerResource);
    }
}
