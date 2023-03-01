using CustomerApplication.Helpers.Extensions;
using Microsoft.Extensions.Configuration;

namespace CustomerApplication.DataAccess.DataServices.Dapper;

public abstract class DapperDataBase
{
    public DapperDataBase(IConfiguration configuration, string connectionKey)
    {
        if (!connectionKey.HasValue()) connectionKey = "ConsultingConnection";
        ConnectionString = configuration.GetConnectionString(connectionKey) ?? throw new ArgumentException($"{connectionKey} does not exist in appsettings.json");
    }

    protected string ConnectionString { get; }
}
