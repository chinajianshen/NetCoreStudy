public enum CycleTypes
{
    M = 1,
    W = 2,
    D = 3
}

public enum DataTypes
{
    Sale = 1,
    Stock = 2,
    Book = 3,
    Store = 4,
    Flow = 5,
    Member = 6
}

/// <summary>
/// 任务状态枚举
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// 运行状态
    /// </summary>
    RUN = 1,

    /// <summary>
    /// 停止状态
    /// </summary>
    STOP = 2
}

public enum SystemConfigs
{
    /// <summary>
    /// 数据导出服务
    /// </summary>
    DataExportService = 10,

    /// <summary>
    /// FTP上传服务
    /// </summary>
    FtpUpoladService = 20,

    /// <summary>
    /// 心跳服务
    /// </summary>
    HeartbeatService = 30,

    /// <summary>
    /// 配置同步状态
    /// </summary>
    ConfigSynStatus = 40,

    /// <summary>
    /// 系统版本
    /// </summary>
    SystemVersion = 50,

    /// <summary>
    /// 密钥
    /// </summary>
    EncryptKey = 60,

    ///// <summary>
    ///// 开卷系统类型
    ///// </summary>
    //OpenbookSysType = 70,    
}

/// <summary>
/// 数据库类型
/// </summary>
public enum DbTypes
{
    Sqlserver = 1,
    Oracle = 2,
    MySql = 4,
    Oledb = 8,
}

/// <summary>
/// 文件上传状态
/// </summary>
public enum FtpUploadStatus
{
     上传成功 =1,
     等待上传=2,
     上传中 =4,
     上传失败=8
}

/// <summary>
/// 任务执行状态
/// </summary>
public enum TaskExecutedStatus
{
    成功 =1,
    失败 =2,
    执行中=4
}

/// <summary>
/// POS类型
/// </summary>
public enum PosTypes
{
    自定义 =1,
    晴川=2,
    中启=4,
    益华=8,
    广智=16,
    金高=32,
    智博=64,
    巴颜喀拉=128,
    实图=256
}

/// <summary>
/// 手动执行任务状态
/// </summary>
public enum ManualTaskStatus
{
    已创建=0,
    已执行=1,
    执行中=2,
    执行失败=4
}
