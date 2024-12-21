using System.ComponentModel;

using NewLife.Configuration;

namespace Pek.BaoTa;

/// <summary>宝塔参数配置</summary>
[DisplayName("宝塔参数配置")]
[Config("BT")]
internal class BTSettings : Config<BTSettings>
{
    /// <summary>
    /// 宝塔面板地址
    /// </summary>
    [Description("宝塔面板地址")]
    public String? BtPanel { get; set; }

    /// <summary>
    /// 宝塔面板密钥
    /// </summary>
    [Description("宝塔面板密钥")]
    public String? BtKey { get; set; }
}
