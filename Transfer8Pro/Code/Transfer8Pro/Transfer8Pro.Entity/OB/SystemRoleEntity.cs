using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity.OB
{
    [Serializable]
    public class SystemRoleEntity:ParamtersForDBPageEntity
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }
    }

    [Serializable]
    public class SystemRoleRelFunEntity
    {
        public int RoleID { get; set; }
        public int FunctionID { get; set; }
    }
}
