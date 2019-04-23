using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Transfer8Pro.Core;
using Transfer8Pro.Core.Service.OB;
using Transfer8Pro.Entity.OB;
using Transfer8Pro.Utils;

namespace Transfer8Pro.WebAPI.Modules
{
    public class HeartbeatModule: BaseModule
    {
        public HeartbeatModule():base("/heartbeat")
        {
            Post["/poll"] = _ =>
            {
                string encryptfname = HttpContext.Current.Request.Form["fname"];
                string encryptstr = HttpContext.Current.Request.Form["encryptstr"];
                string encryptsystemtype = HttpContext.Current.Request.Form["systemtype"];

                int sysType = RijndaelCrypt.Decrypt(encryptsystemtype).ToInt();
                if (sysType != 1 && sysType != 2)
                {
                    return ErrorResult($"参数systemtype值[{encryptsystemtype}]数据错误或被篡改");
                }

                string fname = RijndaelCrypt.Decrypt(encryptfname);
                FtpInfoEntity ftpInfo = new FtpInfoService().Find(fname);
                if (ftpInfo == null)
                {
                    return ErrorResult($"参数fname值[{encryptfname}]系统中不存在");
                }              

                string decryptStr = Common.DecryptData(encryptstr, ftpInfo.FtpEncryptKey);

                string[] heartTimes = decryptStr.Split(new char[] { '_' });

                if (heartTimes.Length != 2)
                {
                    return ErrorResult($"参数encryptstr值[{encryptstr}]数据错误或被篡改");
                }

                DateTime dataHeartbeatTime;
                if (!DateTime.TryParse(heartTimes[0],out dataHeartbeatTime))
                {
                    return ErrorResult($"参数encryptstr值[{encryptstr}]数据错误或被篡改");
                }

                DateTime ftpHeartbeatTime;
                if (!DateTime.TryParse(heartTimes[1],out ftpHeartbeatTime))
                {
                    return ErrorResult($"参数encryptstr值[{encryptstr}]数据错误或被篡改");
                }

                HeartbeatInfoEntity heartbeatInfo = new HeartbeatInfoEntity();
                heartbeatInfo.SystemType = sysType;
                heartbeatInfo.FtpID = ftpInfo.FtpID;
                if (dataHeartbeatTime != DateTime.MinValue)
                {
                    heartbeatInfo.RecentDataHeartbeatTime = dataHeartbeatTime;
                }
                heartbeatInfo.RecentFtpHeartbeatTime = ftpHeartbeatTime;


                bool isSuccess = new HeartbeatInfoService().InsertOrUpdate(heartbeatInfo);
                if (isSuccess)
                {
                    return OkResult();
                }
                else
                {
                    return ErrorResult("心跳更新失败");
                }               
            };
        }
    }
}