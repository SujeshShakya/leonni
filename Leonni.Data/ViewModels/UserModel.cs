using System;
using System.Collections.Generic;


namespace Leonni.Models.ViewModels
{
    public class UserView : BaseViewModel
    {
        public List<UserModel> UserLists { get; set; }
        public long CurrentPage { get; set; }
        public long TotalPages { get; set; }
        public long TotalItems { get; set; }
        public long ItemsPerPage { get; set; }
    }
    public class UserModel : BaseViewModel
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string[] UserRoles { get; set; }
        public string[] Permissions { get; set; }
        public bool IsChecked { get; set; }
        public bool IsLocked { get; set; }
        public List<RoleModel> Roles { get; set; }
        public DateTime CreatedDate { get; set; }

       
       
    }
    public class UserEditModel : UserModel
    {
 
    }
}
