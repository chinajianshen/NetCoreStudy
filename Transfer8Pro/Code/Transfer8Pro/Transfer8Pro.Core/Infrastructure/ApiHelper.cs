using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Transfer8Pro.Core.Service;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Core.Infrastructure
{
    public class ApiHelper
    {
        private static FtpService ftpBll;

        static ApiHelper()
        {
            ftpBll = new FtpService();
        }


        /// <summary>
        /// 生成API签名
        /// </summary>
        public static string Transfer8ProSecret = "transfer8.openbookscan.com.cn";

        /// <summary>
        /// 客户端生成传8/日采集系统签名 
        /// 签名格式3部分
        ///   totalseconds = (DateTime.Now - DateTime.Parse("1970-1-1")).TotalMilliseconds/1000
        /// (1) Base64(RijndaelCrypt(totalseconds))
        /// (2) Base64(HMACSHA256(totalseconds + Transfer8ProSecret))
        /// (3) Base64(Transfer8ProSecret)取前5个和后5个字符     
        /// </summary>      
        /// <returns></returns>
        public static string GenerateApiSignature()
        {
            try
            {
                TimeSpan ts = DateTime.Now - DateTime.Parse("1970-1-1");
                Int64 totalSeconds = (Int64)(ts.TotalMilliseconds / 1000);
                string encryptStr = RijndaelCrypt.Encrypt(totalSeconds.ToString());

                byte[] bytes = Encoding.UTF8.GetBytes(encryptStr);
                string part1 = Convert.ToBase64String(bytes);

                bytes = Encoding.UTF8.GetBytes($"{totalSeconds}{Transfer8ProSecret}");
                var sha = new HMACSHA256(bytes);
                string part2 = Convert.ToBase64String(sha.ComputeHash(bytes));

                encryptStr= RijndaelCrypt.Encrypt($"{totalSeconds}{Transfer8ProSecret}");
                bytes = Encoding.UTF8.GetBytes($"{encryptStr}");
                string base64Str = Convert.ToBase64String(bytes);
                string part3 = $"{base64Str.PreNChar(5)}{base64Str.AfterNChar(8)}".ToUpper();

                return $"{part1}.{part2}.{part3}";
            }
            catch (Exception ex)
            {
                throw new Exception($"GenerateApiSignature()方法异常,异常信息[{ex.Message}][{ex.StackTrace}]");
            }

        }

        /// <summary>
        /// 解析签名
        /// </summary>
        /// <param name="signatrueKey"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool ResolveApiSignature(string signatrueKey, out string msg)
        {
            msg = "signature非法";
            try
            {
                string[] parts = signatrueKey.Split(new char[] { '.' });
                if (parts.Length != 3)
                {
                    return false;
                }
           
                var part1 = RijndaelCrypt.Decrypt(Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]))).ToInt64();
                var origion = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var expired = origion.AddSeconds(part1);
               
                if (expired.AddSeconds(360) < DateTime.Now || expired.AddSeconds(-360) > DateTime.Now)
                {
                    msg = "时间不在有效期内，请校正系统时间";
                    return false;
                }

                byte[] bytes = Encoding.UTF8.GetBytes($"{part1}{Transfer8ProSecret}");
                var sha = new HMACSHA256(bytes);
                string part2 = Convert.ToBase64String(sha.ComputeHash(bytes));

                if (part2 != parts[1])
                {
                    return false;
                }

                string encryptPart3 = RijndaelCrypt.Encrypt($"{part1}{Transfer8ProSecret}");
                bytes = Encoding.UTF8.GetBytes($"{encryptPart3}");
                string base64Str = Convert.ToBase64String(bytes);
                string part3 = $"{base64Str.PreNChar(5)}{base64Str.AfterNChar(8)}".ToUpper();              
                if (part3 != parts[2])
                {
                    return false;
                }

                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.WriteLog($"ResolveApiSignature()方法异常,异常信息[{ex.Message}][{ex.StackTrace}]");
                return false;
            }
        }
    }
}
