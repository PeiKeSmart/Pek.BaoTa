using NewLife.Log;

using Pek.Webs.Clients;

namespace Pek.BaoTa.Tests;

public class BaoTaTests
{
    public BaoTaTests()
    {
        BTSettings.Current.BtPanel = "https://38.147.191.241:33950";
        BTSettings.Current.BtKey = "h9nqDtbCrD2pJWUaeim2QqdkgNOiCiX7";
        BTSettings.Current.Save();
    }

    [Fact]
    public async Task Test()
    {
        var Client = new WebClient();
        var url = UrlHelper.Combine(BTSettings.Current.BtPanel!, "ssl/cert/get_cert_list");

        XTrace.WriteLine($"获取到的链接：{url}");

        var requestTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var requestToken = BaoTaHelper.GetMd5Hash(requestTime + BaoTaHelper.GetMd5Hash(BTSettings.Current.BtKey));

        var responseData = await Client.Post(url).ContentType(HttpContentType.FormUrlEncoded).IgnoreSsl()
            .Data("request_time", requestTime)
            .Data("request_token", requestToken)
            .Data("p", 1)
            .Data("limit", 10)
            .ResultAsync();

        XTrace.WriteLine($"获取到的数据：{responseData}");
    }
}