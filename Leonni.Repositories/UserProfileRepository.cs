﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leonni.Models;
using System.Data.Entity;
using Leonni.Interfaces;

namespace Leonni.Repositories
{
    public class UserProfileRepository : GenericRepository<LeonniDbContext, UserProfile>, IUserProfileRepository
    {
  
    }
}
