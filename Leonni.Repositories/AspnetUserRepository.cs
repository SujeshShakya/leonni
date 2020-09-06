using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models;
using System.Data.Entity;
using Leonni.Interfaces;

namespace Leonni.Repositories
{
    public class AspnetUserRepository : RepositoryBase<aspnet_Users>, IAspnetUserRepository
    {
        public AspnetUserRepository(DbContext context)
            : base(context)
        {

        }
    }
}
