using NewLife.Log;

using Pek.Webs.Clients;

namespace Pek.BaoTa.Tests;

public class BaoTaTests
{
    public BaoTaTests()
    {
        BTSettings.Current.BtPanel = "http://39.108.80.8:8888";
        BTSettings.Current.BtKey = "phzSpGLSykSRaEbgs4RxccwSLHX4iBoW";
        BTSettings.Current.Save();
    }

    [Fact]
    public async Task Test()
    {
        var dic = BaoTaHelper.Urls;

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
            .ResultStringAsync();

        XTrace.WriteLine($"获取到的数据：{responseData}");

        //url = UrlHelper.Combine(BTSettings.Current.BtPanel!, dic["applycertapi"]);
        //XTrace.WriteLine($"获取到的链接：{url}");

        //requestTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        //requestToken = BaoTaHelper.GetMd5Hash(requestTime + BaoTaHelper.GetMd5Hash(BTSettings.Current.BtKey));

        //responseData = await Client.Post(url).ContentType(HttpContentType.FormUrlEncoded).IgnoreSsl()
        //    .Data("request_time", requestTime)
        //    .Data("request_token", requestToken)
        //    .Data("domains", new[] { "www.hlktech.cn", "hlktech.cn" })
        //    .Data("auth_type", "http")
        //    .Data("auth_to", new[] { "www.hlktech.cn", "hlktech.cn" })
        //    .Data("auto_wildcard", 0)
        //    .Data("id", 35)
        //    .ResultAsync();

        //XTrace.WriteLine($"获取到的数据：{responseData}");
    }
}