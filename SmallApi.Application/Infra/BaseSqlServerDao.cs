using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace SmallApi.Application.Infra
{
    public class BaseSqlServerDao
    {
        private readonly SqlServerSettings _sqlServerSettings;
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        public string DefaultQuery { get; protected set; }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        public string DefaultKeyQuery { get; protected set; }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        public string DefaultInsert { get; protected set; }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        public string DefaultUpdate { get; protected set; }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        public string DefaultDelete { get; protected set; }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        /// <param name="sqlServerSettings"></param>
        /// <param name="env"></param>
        public BaseSqlServerDao(IOptions<SqlServerSettings> sqlServerSettings, IHostingEnvironment env)
        {
            _sqlServerSettings = sqlServerSettings.Value;
            _env = env;
        }

        internal SqlConnection DbConnection
        {
            get
            {
                string conx = _sqlServerSettings.ConnectionString;

                if (conx.Contains("%CONTENTROOTPATH%"))
                    conx = conx.Replace("%CONTENTROOTPATH%", _env.ContentRootPath);

                return new SqlConnection(conx);
            }
        }
    }
}
