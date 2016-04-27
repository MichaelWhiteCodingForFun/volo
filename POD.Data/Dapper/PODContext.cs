using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace POD.Data.Dapper
{
    public class PODContext : IDisposable
    {
        public PODContext(string connectionString, Enums.DbEngine engine, bool withTransaction)
        {
            this._connectionString = connectionString;
            this._engine = engine;
            this.OpenDatabase(withTransaction);
        }

        #region Properties
 
        /// <summary>
        /// Currently active Database Engine.
        /// </summary>
        public Enums.DbEngine DbEngine
        {
            get
            {
                return this._engine;
            }
        }
        /// <summary>
        /// Database connection string.
        /// </summary>
        public string ConnectionSring
        {
            get
            {
                return this._connectionString;
            }
        }
        /// <summary>
        /// Represent the DapperExtension database instance. 
        /// This is THE connectino to database used by repository engine.
        /// </summary>
        public DapperExtensions.Database Database
        {
            get
            {
                return this._database;
            }
        }

        #endregion        

        #region Private helper
        private void OpenDatabase(bool withTransaction)
        {
            IDbConnection connection = null;
            DapperExtensionsConfiguration config = null;

            if (_engine == Enums.DbEngine.PostgreSQL)
            {
               
            }
            else
            {
                connection = new SqlConnection(_connectionString);
                config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
            }

            var sqlGenerator = new SqlGeneratorImpl(config);
            this._database = new Database(connection, sqlGenerator);
        }
        #endregion

        #region Private members
        private DapperExtensions.Database _database;
        private string _connectionString;
        private Enums.DbEngine _engine;
      
        #endregion

        #region Dispose
        public void Dispose()
        {
            this.Database.Connection.Close();
            this.Database.Dispose();
        }
        #endregion
    }
}
