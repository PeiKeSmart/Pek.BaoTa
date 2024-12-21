using System.Security.Cryptography;
using System.Text;

using NewLife;

namespace Pek.BaoTa;

public static class BaoTaHelper {
    /// <summary>
    /// 签名实现
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static String GetMd5Hash(String? input)
    {
        if (input.IsNullOrWhiteSpace()) return String.Empty;

        var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        var sb = new StringBuilder();
        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("x2"));
        }

        return sb.ToString();
    }



    public static FormUrlEncodedContent GetFormContent(List<KeyValuePair<String, String>>? data = null)
    {
        var requestTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var requestToken = GetMd5Hash(requestTime + GetMd5Hash(BTSettings.Current.BtKey));

        data ??= [];

        data.AddRange(
        [
            new KeyValuePair<String, String>("request_time", requestTime),
            new KeyValuePair<String, String>("request_token", requestToken)
        ]);
        var formContent = new FormUrlEncodedContent(data);
        return formContent;
    }
}
