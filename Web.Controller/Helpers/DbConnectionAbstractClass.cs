using System.Configuration;

namespace Web.Controller.Helpers
{
    public static class DbConnectionAbstractClass
    {
        public static string ConnectionString { get; } = ConfigurationManager.ConnectionStrings["DbDSN"].ConnectionString;
    }
}
