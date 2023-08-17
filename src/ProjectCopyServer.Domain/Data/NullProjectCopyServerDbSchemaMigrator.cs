using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Data;

/* This is used if database provider does't define
 * IProjectCopyServerDbSchemaMigrator implementation.
 */
public class NullProjectCopyServerDbSchemaMigrator : IProjectCopyServerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
