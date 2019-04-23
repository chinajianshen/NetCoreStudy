using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transfer8Pro.Entity;
using Transfer8Pro.Utils;

namespace Transfer8Pro.Core.Infrastructure
{
    public class BuildInstanceObject
    {
        /// <summary>
        /// 获取SQL查询条件开始和结束时间
        /// </summary>
        /// <param name="cycleType"></param>
        /// <returns></returns>
        public virtual ISqlQueryTime GetSqlQueryTimeStragety(CycleTypes cycleType)
        {
            ISqlQueryTime sqlQueryTime = null;
            switch (cycleType)
            {
                case  CycleTypes.M:
                    sqlQueryTime = AutoFacContainer.ResolveNamed<ISqlQueryTime>(typeof(MonthSqlQueryTime).Name);
                    break;
                case  CycleTypes.W:
                    sqlQueryTime = AutoFacContainer.ResolveNamed<ISqlQueryTime>(typeof(WeekSqlQueryTime).Name);
                    break;
                case  CycleTypes.D:
                    sqlQueryTime = AutoFacContainer.ResolveNamed<ISqlQueryTime>(typeof(DaySqlQueryTime).Name);
                    break;
                default:
                    throw new ArgumentNullException("GetSqlQueryTimeStragety()方法，参数cycleType为空");                    
            }
          
            return sqlQueryTime;
        }

        /// <summary>
        /// 获取文件名信息  1一般文件 2压缩文件  3 T8一般文件 4 T8压缩文件
        /// 如果配置键OpenBookSystemType=1 日采集系统 
        /// 如果配置键OpenBookSystemType=2 传8系统 周销售、周在架、月销售和月在架按老传8格式传输，其它都按日采集格式传输
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual AFileName GetGenerateFileNameStragety(int type,TaskEntity task)
        {
            AFileName aFileName = null;
            int systype =  Common.DecryptConfigKey("OpenBookSystemType").ToInt();
                       
            switch (type)
            {
                case 1:
                    aFileName = AutoFacContainer.ResolveNamed<AFileName>(typeof(GeneralFileName).Name);
                    break;
                case 2:
                    aFileName = AutoFacContainer.ResolveNamed<AFileName>(typeof(CompressFileName).Name);
                    break;
                case 3:                    
                    if ((task.CycleType == CycleTypes.M || task.CycleType == CycleTypes.W) && (task.DataType == DataTypes.Sale || task.DataType == DataTypes.Stock))
                    {
                        aFileName = AutoFacContainer.ResolveNamed<AFileName>(typeof(T8GeneralFileName).Name);
                    }
                    else
                    {
                        aFileName = AutoFacContainer.ResolveNamed<AFileName>(typeof(GeneralFileName).Name);
                    }
                    break;
                case 4:
                    if ((task.CycleType == CycleTypes.M || task.CycleType == CycleTypes.W) && (task.DataType == DataTypes.Sale || task.DataType == DataTypes.Stock))
                    {
                        aFileName = AutoFacContainer.ResolveNamed<AFileName>(typeof(T8CompressFileName).Name);
                    }
                    else
                    {
                        aFileName = AutoFacContainer.ResolveNamed<AFileName>(typeof(CompressFileName).Name);
                    }                      
                    break;
                default:
                    throw new ArgumentNullException("GetSqlQueryTimeStragety()方法，参数type值错误");
            }  
            return aFileName;
        }      
    }
}
