using System.Threading.Tasks;

namespace ProjectCopyServer.Data;

public interface IProjectCopyServerDbSchemaMigrator
{
    Task MigrateAsync();
}
