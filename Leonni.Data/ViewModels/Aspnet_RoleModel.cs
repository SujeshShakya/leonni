using System;
using System.Collections.Generic;


namespace Leonni.Models.ViewModels
{
    public class UserRoleView : BaseViewModel
    {
        public List<RoleModel> UserRoles { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }
        public long TotalItems { get; set; }
        public long ItemsPerPage { get; set; }
    }
    public class RoleModel : BaseViewModel
    {
        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public bool isChecked { get; set; }
       // public List<PermissionModel> Permissions { get; set; }

        
    }
}
