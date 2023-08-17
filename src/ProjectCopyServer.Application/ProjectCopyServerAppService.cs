using System;
using System.Collections.Generic;
using System.Text;
using ProjectCopyServer.Localization;
using Volo.Abp.Application.Services;

namespace ProjectCopyServer;

/* Inherit your application services from this class.
 */
public abstract class ProjectCopyServerAppService : ApplicationService
{
    protected ProjectCopyServerAppService()
    {
        LocalizationResource = typeof(ProjectCopyServerResource);
    }
}
