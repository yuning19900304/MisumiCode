using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Misumi_Client.Common
{
    public class HttpWeb
    {
        public static bool strTelnet = true;
        static HttpWeb _instance = new HttpWeb();
        private WebClient webClient = new WebClient();
        public static HttpWeb Submit
        {
            get { return _instance; }
        }

        #region get
        /// <summary>
        /// Get请求地址，获取返回信息
        /// </summary>
        public string GET(string url)
        {
            return GET(url, Encoding.GetEncoding("utf-8"));
        }

        /// <summary>
        /// Get请求地址，获取返回信息
        /// </summary>
        public string GET(string url, Encoding encoding)
        {
            return GET(url, encoding, 10000);
        }

        /// <summary>
        /// Get请求地址，获取返回信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string GET(string url, Encoding encoding, int time)
        {
            string result = "";
            StreamReader reader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);

                if (time > 0)
                {
                    request.Timeout = time;
                }
                request.Method = "GET";
                request.ContentType = "charset=" + encoding.HeaderName;
                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322)";

                response = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream(), encoding);

                result = reader.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                try
                {
                    //出现异常，GPRS重试
                    string SecondServerOutAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["SecondServerOutAddress"];

                    var matchValue = Regex.Match(url, @"(?<=http://)(.*?)(?=\/)").Value;
                    string URL = url.Replace("http://" + matchValue.ToString(), SecondServerOutAddress);
                    result = webClient.UploadString(URL, "GET");

                }
                catch (Exception eex)
                {
                    throw new Exception("GRPS Send Post Data Error" + "|" + url + "|" + eex.Message, eex);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }
        #endregion

        #region ping网络
        /// <summary>
        /// 是否能 Ping 通指定的主机
        /// </summary>
        /// <param name="ip">ip 地址或主机名或域名</param>
        /// <returns>true 通，false 不通</returns>
        public bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 2000; // Timeout 时间，单位：毫秒
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }
        #endregion

        #region post
        /// <summary>
        /// 向指定URLpost数据，超时时间默认为30秒
        /// </summary>
        /// <returns></returns>
        public string Post(string url, string strParam)
        {
            return Post(url, strParam, System.Text.Encoding.UTF8, 10000);
        }
        public string Post(string url, string strParam, int time)
        {
            return Post(url, strParam, System.Text.Encoding.UTF8, time = 10000);
        }
        public string Post(string url, string strParam, System.Text.Encoding resultEncoding)
        {
            return Post(url, strParam, resultEncoding, 10000);
        }

        public static bool cmdTelnet(string strIP, int strNum)
        {
            strTelnet = false;
            try
            {
                string IP = strIP;
                if (strIP.Contains(":"))
                {
                    IP = strIP.Substring(0, strIP.LastIndexOf(':'));
                }
                TcpClient client = new TcpClient(IP, strNum);
                strTelnet = true;
            }
            catch (SocketException e)
            {
                strTelnet = false;
            }
            return strTelnet;
        }

        /// <summary>
        /// 提供通过POST方法获取页面的方法
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="strParam">POST数据</param>
        /// <param name="encoding">页面使用的编码</param>
        /// <param name="time">超时时间</param>
        /// <returns>获取的页面</returns>
        public string Post(string url, string strParam, Encoding encoding, int time)
        {
            //定义局部变量
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            Stream inputStream = null;
            Stream outputStream = null;
            StreamReader streamReader = null;
            string htmlString = string.Empty;
            //转换POST数据
            byte[] postDataByte = encoding.GetBytes(strParam);
            //建立页面请求
            WriteFile("**************向统合网站请求" + url + "参数是：" + strParam);
            try
            {
                httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            }
            //处理异常
            catch (Exception ex)
            {
                WriteFile(ex.Message);
            }
            //设置代理服务器
            WebProxy proxy = WebProxy.GetDefaultProxy();//获取IE缺省设置
            string ProxyUse = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyUse"];
            //if (proxy.Address != null || ProxyUse.ToLower() == "true")
            //{
            //    string ProxyAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyAddress"];
            //    string ProxyUser = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyUser"];
            //    string ProxyPwd = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyPwd"];
            //    string ProxyDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyDomain"];
            //    proxy = new WebProxy(ProxyAddress, false);
            //    proxy.Credentials = new System.Net.NetworkCredential(ProxyUser, ProxyPwd, ProxyDomain);
            //    httpWebRequest.Proxy = proxy;
            //}
            //if (System.Web.Configuration.WebConfigurationManager.AppSettings["PostTimeOut"] != null && System.Web.Configuration.WebConfigurationManager.AppSettings["PostTimeOut"].ToString() != "")
            //{
            //    time = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["PostTimeOut"].ToString());
            //}
            //指定请求处理方式
            httpWebRequest.Method = "POST";
            //httpWebRequest.ContentType = "charset=" + encoding.HeaderName;
            httpWebRequest.KeepAlive = false;
            httpWebRequest.ProtocolVersion = HttpVersion.Version10;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentLength = postDataByte.Length;
            httpWebRequest.Timeout = time;
            //向服务器传送数据
            try
            {
                inputStream = httpWebRequest.GetRequestStream();
                inputStream.Write(postDataByte, 0, postDataByte.Length);
            }
            //处理异常
            catch (Exception ex)
            {
                WriteFile("**************向服务器发送请求失败: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + url + "参数是：" + strParam + "\n\r" + ex.ToString());
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            //接受服务器返回信息
            try
            {
                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    outputStream = httpWebResponse.GetResponseStream();
                    streamReader = new StreamReader(outputStream, encoding);
                    htmlString = streamReader.ReadToEnd();
                }
            }
            //处理异常
            catch (Exception ex)
            {
                WriteFile("**************接受服务器返回信息失败" + url + "参数是：" + strParam + "\n\r" + ex.ToString());
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
            foreach (Cookie cookie in httpWebResponse.Cookies)
            {
                cookieContainer.Add(cookie);
            }

            WriteFile("统合网站返回: " + htmlString);
            return htmlString;
        }

        /// <summary>
        /// 提供通过POST方法获取页面的方法
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="strParam">POST数据</param>
        /// <param name="encoding">页面使用的编码</param>
        /// <param name="time">超时时间</param>
        /// <returns>获取的页面</returns>
        public string GprsPost(string url, string strParam, Encoding encoding, int time)
        {
            //定义局部变量
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            Stream inputStream = null;
            Stream outputStream = null;
            StreamReader streamReader = null;
            string htmlString = string.Empty;
            //转换POST数据
            byte[] postDataByte = encoding.GetBytes(strParam);
            //建立页面请求
            try
            {
                httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                SetProxy(httpWebRequest);
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("Post page request error", ex);
            }
            //指定请求处理方式
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "charset=" + encoding.HeaderName;
            httpWebRequest.KeepAlive = false;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentLength = postDataByte.Length;
            httpWebRequest.Timeout = time;
            //向服务器传送数据
            try
            {
                inputStream = httpWebRequest.GetRequestStream();
                inputStream.Write(postDataByte, 0, postDataByte.Length);
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("Send Post Data Error" + "|" + url + "|" + ex.Message);
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Close();
                }
            }
            //接受服务器返回信息
            try
            {
                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    outputStream = httpWebResponse.GetResponseStream();
                    streamReader = new StreamReader(outputStream, encoding);
                    htmlString = streamReader.ReadToEnd();
                }
            }
            //处理异常
            catch (Exception ex)
            {
                throw new Exception("Accept the server returns an error page", ex);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
            foreach (Cookie cookie in httpWebResponse.Cookies)
            {
                cookieContainer.Add(cookie);
            }
            return htmlString;
        }
        #endregion


        #region HtmlEncode
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string HtmlEncode(string value)
        {
            return HttpUtility.UrlEncode(HttpUtility.HtmlEncode(value));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string HtmlDecode(string value)
        {
            return HttpUtility.HtmlDecode(HttpUtility.UrlDecode(value));
        }
        #endregion


        //写日志文件
        public static void WriteFile(string input)
        {
            try
            {
                CommonHelper.WriteLog("HTTPWeb", input);
            }
            catch { };
        }


        /// <summary>
        /// 设置HttpWebReqest 是否使用代理
        /// </summary>
        /// <param name="request"></param>
        public void SetProxy(HttpWebRequest request)
        {
            try
            {
                string ProxyUse = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyUse"];
                WriteFile("是否使用代理" + ProxyUse);
                if (ProxyUse.ToLower() == "true")
                {

                    //设置代理服务器
                    WebProxy proxy = WebProxy.GetDefaultProxy(); //获取IE缺省设置

                    if (proxy.Address != null || ProxyUse.ToLower() == "true")
                    {
                        string ProxyAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyAddress"];
                        string ProxyUser = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyUser"];
                        string ProxyPwd = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyPwd"];
                        string ProxyDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyDomain"];
                        WriteFile("代理：" + ProxyAddress + "\t" + ProxyUser + "\t" + ProxyPwd + ProxyDomain + "\t");
                        proxy = new WebProxy(ProxyAddress, false);
                        proxy.Credentials = new System.Net.NetworkCredential(ProxyUser, ProxyPwd, ProxyDomain);
                        request.Proxy = proxy;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteFile("设置代理产生异常：" + ex + "\t");
            }

        }

        /// <summary>
        /// 设置HttpWebReqest 是否使用代理
        /// </summary>
        /// <param name="request"></param>
        public void SetProxy(WebClient webclient)
        {
            try
            {
                string ProxyUse = "false"; //把代理弄掉 System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyUse"];
                WriteFile("是否使用代理" + ProxyUse);
                if (ProxyUse.ToLower() == "true")
                {

                    //设置代理服务器
                    WebProxy proxy = WebProxy.GetDefaultProxy(); //获取IE缺省设置

                    if (proxy.Address != null || ProxyUse.ToLower() == "true")
                    {
                        string ProxyAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyAddress"];
                        string ProxyUser = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyUser"];
                        string ProxyPwd = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyPwd"];
                        string ProxyDomain = System.Web.Configuration.WebConfigurationManager.AppSettings["ProxyDomain"];
                        WriteFile("代理：" + ProxyAddress + "\t" + ProxyUser + "\t" + ProxyPwd + ProxyDomain + "\t");

                        proxy = new WebProxy(ProxyAddress, false);
                        proxy.Credentials = new System.Net.NetworkCredential(ProxyUser, ProxyPwd, ProxyDomain);
                        webclient.Proxy = proxy;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteFile("设置代理产生异常：" + ex + "\t");
            }
        }
    }
}
