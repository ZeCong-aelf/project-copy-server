using Volo.Abp.Settings;

namespace ProjectCopyServer.Settings;

public class ProjectCopyServerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ProjectCopyServerSettings.MySetting1));
    }
}
