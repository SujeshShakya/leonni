using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models;
using System.Data.Entity;
using Leonni.Interfaces;

namespace Leonni.Repositories
{
    public class CategoryRepository : GenericRepository<LeonniDbContext, Category>, ICategoryRepository
    {
        //public CategoryRepository(DbContext context)
        //    : base(context)
        //{

        //}
    }
}
