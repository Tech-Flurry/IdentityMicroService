using DataAcess.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcess.Repositories
{
    internal class ApplicationsRepository
    {
        private readonly IDbConnection _db;

        public ApplicationsRepository(IDbConnection db)
        {
            _db = db;
        }

    }
}
