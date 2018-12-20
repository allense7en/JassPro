using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JassPro.Web.Models.Helper
{
    public class UIHelper
    {
        /// <summary>
        /// 客户端IP
        /// </summary>
        public static string IP
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理
                    if (result.IndexOf(".") == -1) //没有"."肯定是非IPv4格式
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有","，估计多个代理。取第一个不是内网的IP。
                            result = result.Replace(" ", "").Replace("", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                        && temparyip[i].Substring(0, 3) != "10."
                                        && temparyip[i].Substring(0, 7) != "192.168"
                                        && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i]; //找到不是内网的地址
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式
                            return result;
                        else
                            result = null; //代理中的内容 非IP，取IP
                    }
                }

                string IpAddress = !string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }

        //是否ip格式
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regformat, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
    }
}