using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Transfer8Pro.Utils
{
   public class RegexExpressionUtil
    {
        /// <summary>
        /// 验证是否是文件路径格式 如 C:\Users
        /// </summary>
        public static Regex ValidateFilePathReg = new Regex(@".*?\w{1}:\.*?", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        ///周 t8sjzch_20051226_20060101_W.db
        ///周在架 t8sjzch_20051226_20060101_WS.db
        ///月 t8sjzch_20160601_20160630_M.db
        ///月在架 t8sjzch_20160525_20160525_S.db
        /// </summary>
        public static Regex T8DataFileFormatReg = new Regex(@"\w{1,}_\d{8}_\d{8}_\w{1,2}.db(.zip)?$", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// SqlServer连接字符串
        /// server=192.168.0.14;database=T8DataTest;uid=sa;pwd=sa.;min pool size=10;max pool size=300;Connection Timeout=10;
        /// </summary>
        public static Regex SqlServerConnReg = new Regex(@"server\s*=\s*(?<server>.*?);\s*database\s*=\s*(?<database>.*?);\s*uid\s*=\s*(?<uid>.*?);\s*pwd\s*=\s*(?<pwd>.*?);", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Oracle连接字符串
        /// User ID=ox;Password=ox_pwd;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.60)(PORT=18991)))(CONNECT_DATA=(SERVICE_NAME=ORCL12)))
        /// </summary>
        public static Regex OracleConnReg = new Regex(@"user\s{1,}id\s*=\s*(?<uid>.*?);\s*password\s*=\s*(?<pwd>.*?);.*?\s*host\s*=\s*(?<host>.*?)\).*?port\s*=\s*(?<port>.*?)\).*?service_name\s*=\s*(?<servicename>.*?)\)", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// MySql连接字符串
        /// server=192.168.3.249;database=test;userid=root;password=123456;port=3306;Charset=utf8;Allow User Variables=True;
        /// </summary>
        public static Regex MySqlConnReg = new Regex(@"server\s*=\s*(?<server>.*?);\s*database\s*=\s*(?<database>.*?);\s*userid\s*=\s*(?<uid>.*?);\s*password\s*=\s*(?<pwd>.*?);\s*port\s*=\s*(?<port>.*?);", RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
