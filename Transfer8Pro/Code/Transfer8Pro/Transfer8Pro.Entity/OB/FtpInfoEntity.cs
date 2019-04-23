using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity.OB
{
    [Serializable]
    public class FtpInfoEntity : ParamtersForDBPageEntity
    {
        public int FtpID { get; set; }

        public string FtpUserName { get; set; }

        public string FtpPassword { get; set; }

        public string FtpServerName { get; set; }

        public string FtpFolderName { get; set; }

        public string FtpEncryptKey { get; set; }

        public string SupName { get; set; }

        public string MainFolderName { get; set; }
      
    }
}
