using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTest
{
    public class NetScaler_Login
    {
        public static void test()
        {
            var re = "";
            var headers = new Dictionary<string, string>();
            headers.Add("X-Citrix-IsUsingHTTPS", "Yes");
            var container = new CookieContainer();

            HttpCreator.Create("https://yun.dev.cloud.com/Citrix/storeWeb/", "get", "", "", "", null, container, null, headers, out re);
            var strcook = container.GetCookieHeader(new Uri("https://yun.dev.cloud.com/Citrix/storeWeb/"));

            var referrer = "https://yun.dev.cloud.com/vpn/index.html";
            HttpCreator.Create("https://yun.dev.cloud.com/cgi/login", "post", referrer, "login=10101&passwd=123qwe,.", "", null, container, null, headers, out re);

            strcook = container.GetCookieHeader(new Uri("https://yun.dev.cloud.com/Citrix/storeWeb/"));

            HttpCreator.Create("https://yun.dev.cloud.com/Citrix/storeWeb/Home/Configuration", "post", "", "", "", null, container, null, headers, out re);

            strcook = container.GetCookieHeader(new Uri("https://yun.dev.cloud.com/Citrix/storeWeb/"));

            var match = Regex.Match(strcook, "\\s*CsrfToken\\s*=\\s*(\\.*?);");
            var csrftoken = "";
            if (match.Success)
                csrftoken = match.Groups[1].Value;

            HttpCreator.Create("https://yun.dev.cloud.com/Citrix/storeWeb/Authentication/GetAuthMethods", "post", "", "", "", null, container, null, headers, out re);
            strcook = container.GetCookieHeader(new Uri("https://yun.dev.cloud.com/Citrix/storeWeb/"));

            referrer = "https://yun.dev.cloud.com/Citrix/storeWeb/";
            HttpCreator.Create("https://yun.dev.cloud.com/Citrix/storeWeb/GatewayAuth/Login", "post", referrer, "", "", null, container, null, headers, out re);

            strcook = container.GetCookieHeader(new Uri("https://yun.dev.cloud.com/Citrix/storeWeb/"));
            
            #region MyRegion
            //try
            //{
            //    var headers = new Dictionary<string, string>();
            //    headers.Add("X-Citrix-IsUsingHTTPS", "Yes");
            //    var container = new CookieContainer();

            //    NetScalerLoginHelper.Create($"https://{DataStore.Domain}/Citrix/storeWeb/", "get", "", "", "", container, null, headers);

            //    var referrer = $"https://{DataStore.Domain}/vpn/index.html";
            //    var re = NetScalerLoginHelper.Create($"https://{DataStore.Domain}/cgi/login", "post", referrer, $"login={username}&passwd={password}", "", container, null, headers);

            //    if (re.StatusCode == (HttpStatusCode)480)
            //    {
            //        return new LogResult() { Error = "NetScaler返回480错误，这可能是操作频繁造成的，请于1分钟后重试", Result = false };
            //    }

            //    NetScalerLoginHelper.Create($"https://{DataStore.Domain}/Citrix/storeWeb/Home/Configuration", "post", "", "", "", container, null, headers);

            //    var strcook = container.GetCookieHeader(new Uri($"https://{DataStore.Domain}/Citrix/storeWeb/"));
            //    var match = Regex.Match(strcook, "\\s*CsrfToken\\s*=\\s*(\\.*?);");
            //    var csrftoken = "";
            //    if (match.Success)
            //        csrftoken = match.Groups[1].Value;

            //    NetScalerLoginHelper.Create($"https://{DataStore.Domain}/Citrix/storeWeb/Authentication/GetAuthMethods", "post", "", "", "", container, null, headers);

            //    referrer = $"https://{DataStore.Domain}/Citrix/storeWeb/";
            //    NetScalerLoginHelper.Create($"https://{DataStore.Domain}/Citrix/storeWeb/GatewayAuth/Login", "post", referrer, "", "", container, null, headers);

            //    strcook = container.GetCookieHeader(new Uri($"https://{DataStore.Domain}/Citrix/storeWeb/"));
            //    if (strcook.Contains("CtxsAuthId"))
            //    {
            //        return new LogResult() { Error = "", Result = true };
            //    }
            //}
            //catch { }
            //return new LogResult() { Error = "未知错误", Result = false }; 
            #endregion

            //reference YZYSFAPI.dll
            //var r0 = YZYSFAPI.YZYHelper.Storefront.checkSF("https://yun.dev.cloud.com/Citrix/storeWeb/");
            //var sf = new YZYSFAPI.YZYHelper.Storefront();
            //var r1 = sf.initilize("https://yun.dev.cloud.com/Citrix/storeWeb/");
            ////var r2 = sf.checkConfigration("https://yun.dev.cloud.com/Citrix/storeWeb/");
            ////var r3 = sf.checkNetscalar("https://yun.dev.cloud.com/Citrix/storeWeb/");
            ////var r4 = sf.getAuthMethods("https://yun.dev.cloud.com/Citrix/storeWeb/");
            //var r5 = sf.AuthenticateWithPost("https://yun.dev.cloud.com/Citrix/storeWeb/", "10101", "123qwe,.", "");
        }
    }
}
