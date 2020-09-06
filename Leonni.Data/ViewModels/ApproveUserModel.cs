using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class ApproveUserModel : BaseViewModel
    {
        public List<MembershipUserModel> MembershipUsers { get; set; }
    }
}
