using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity.OB
{
    [Serializable]
    public class SystemUserEntity : ParamtersForDBPageEntity
    {
        public string UserID { get; set; }

        public string UserLoginName { get; set; }
        public string UserName { get; set; }
        public int UserSex { get; set; }

        public string UserTel { get; set; }

        public string UserMobile { get; set; }

        public string UserMail { get; set; }

        public string UserDeptID { get; set; }

        public string UserParentID { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }

    [Serializable]
    public class SystemUserRelRoleEntity
    {
        public string UserID { get; set; }

        public int RoleID { get; set; }
    }

    [Serializable]
    public class SystemUserViewEntity :SystemUserEntity
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string DepartName { get; set; }
    }
}
