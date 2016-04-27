using DapperExtensions;
using POD.Data.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POD.Data.Services
{
    public class BaseDataService
    {
        #region Private Members

        protected PODContext _context;
        protected IDatabase _db;

        #endregion

        #region ctor

        public BaseDataService(PODContext ctx)
        {
            this._context = ctx;
            this._db = ctx.Database;
        }

        #endregion

        #region Properties

        public PODContext Context
        {
            get
            {
                return this._context;
            }
        }

        #endregion


    }
}
