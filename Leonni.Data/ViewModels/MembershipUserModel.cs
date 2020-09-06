using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class MembershipUserModel
    {
        public string Email { get; set; }
        public Guid id { get; set; }
        public bool isApproved { get; set; }
    }
}
