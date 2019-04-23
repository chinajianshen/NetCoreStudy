using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity.OB
{
    [Serializable]
    public class SystemFunctionEntity
    {
        public SystemFunctionEntity()
        {
            ChildFunList = new List<SystemFunctionEntity>();
        }

        public int FunctionID { get; set; }

        public string FunctionName { get; set; }

        public string FunctionURL { get; set; }

        public int FunctionParentID { get; set; }

        public int FunctionLevel { get; set; }

        public int FunctionIsValid { get; set; }

        public int FunctionOrder { get; set; }

        public DateTime CreateTime { get; set; }

        public List<SystemFunctionEntity> ChildFunList { get; set; }
    }

    
}
