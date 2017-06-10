using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using TimeDIrector.Client.Models;
using TimeDIrector.Client.Services.Interfaces;

namespace TimeDIrector.Client.Services
{
	sealed class WebService : IWebService
	{
		private readonly Regex _httpCharsetRegex;
		private readonly Uri _msftncsiUri;
		private readonly Encoding _utf8;

		public WebService()
		{
			_httpCharsetRegex = new Regex(@".*;\s*charset\s*=\s*(cp-|cp)?(?<value>[^/]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			_msftncsiUri = new Uri("http://www.msftncsi.com/ncsi.txt"); //https://technet.microsoft.com/en-us/library/cc766017(v=ws.10).aspx
			_utf8 = new UTF8Encoding(false);
		}

		public bool CheckInternetConnection()
		{
			var result = GET_String(_msftncsiUri);
			if (result.StatusCode == 200)
				return result.ResultString == "Microsoft NCSI";

			return false;
		}

		public StringResult GET_String(Uri requestUri)
		{
			var request = CreateRequest(requestUri);
			request.Method = "GET";

			var result = new StringResult(requestUri);
			var response = GetHttpWebResponse(result, request);
			SetStringResult(result, response);

			return result;
		}
	
		public StringResult POST_Form(Uri requestUri, Dictionary<string, string> postParameters)
		{
#if DEBUG
			if (postParameters == null || postParameters.Count == 0)
				throw new ArgumentException(nameof(postParameters));
#endif

			var postData = GetParametersString(postParameters);
			var data = Encoding.ASCII.GetBytes(postData);

			var request = CreateRequest(requestUri);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;

			var result = new StringResult(requestUri);
			if (SetRequestData(result, request, data))
			{
				var response = GetHttpWebResponse(result, request);
				SetStringResult(result, response);
			}

			return result;
		}

		public StringResult POST_Json(Uri requestUri, string json)
		{
#if DEBUG
			if (string.IsNullOrEmpty(json))
				throw new ArgumentException(nameof(json));
#endif

			var data = _utf8.GetBytes(json);

			var request = CreateRequest(requestUri);
			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = data.Length;

			var result = new StringResult(requestUri);
			if (SetRequestData(result, request, data))
			{
				var response = GetHttpWebResponse(result, request);
				SetStringResult(result, response);
			}

			return result;
		}

		public string GetParametersString(Dictionary<string, string> parameters)
		{
#if DEBUG
			if (parameters == null || parameters.Count == 0)
				throw new ArgumentException(nameof(parameters));
#endif

			var sb = new StringBuilder();
			var addAsp = false;
			foreach (var parameter in parameters)
			{
				if (addAsp)
					sb.Append("&");

				sb.Append(HttpUtility.UrlEncode(parameter.Key));

				if (!string.IsNullOrEmpty(parameter.Value))
				{
					sb.Append("=");
					sb.Append(HttpUtility.UrlEncode(parameter.Value));
				}
				addAsp = true;
			}

			return sb.ToString();
		}

		private HttpWebResponse GetHttpWebResponse(BaseResult targetResult, HttpWebRequest request)
		{
			HttpWebResponse response = null;
			try
			{
				response = request.GetResponse() as HttpWebResponse;
			} catch (WebException wex)
			{
				response = wex.Response as HttpWebResponse;
				targetResult.WebExceptionStatus = wex.Status;
			} catch (Exception ex)
			{
				targetResult.OtherFailedStatusError = ex.GetType().Name;
			}

			if (response == null)
				return null;

			var statusCode = (int)response.StatusCode;
			targetResult.StatusCode = statusCode;

			return response;
		}
		private void SetStringResult(StringResult targetResult, HttpWebResponse response)
		{
			if (response == null)
				return;

			string charset = null;

			var contentType = response.Headers["Content-Type"];
			if (!string.IsNullOrEmpty(contentType))
				charset = GetHttpHeaderCharset(contentType);

			var bytes = GetResponseBytes(targetResult, response);
			var encoding = GetEncoding(charset);

			targetResult.ResultString = encoding.GetString(bytes);
		}

		private HttpWebRequest CreateRequest(Uri requestInfo)
		{
			var request = (HttpWebRequest)WebRequest.Create(requestInfo);
			request.Proxy = WebRequest.DefaultWebProxy;
			request.ProtocolVersion = HttpVersion.Version11;
			request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
			request.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			request.Headers.Add("Accept-Encoding", "gzip,deflate");
			request.Headers.Add("Accept-Charset", "utf-8");
			request.Headers.Add("Pragma", "no-cache");
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			request.AllowAutoRedirect = false;
			request.Timeout = 30000;

			return request;
		}

		
		private byte[] GetResponseBytes(BaseResult targetResult, HttpWebResponse response)
		{
			try
			{
				using (var ms = new MemoryStream())
				using (var stream = response.GetResponseStream())
				{
					if (stream == null)
						return ms.ToArray();
					try
					{
						stream.ReadTimeout = 30000;
					} catch
					{
						//ignore
					}

					stream.CopyTo(ms);
					return ms.ToArray();
				}
			} catch (WebException wex)
			{
				targetResult.WebExceptionStatus = wex.Status;
			} catch (Exception ex)
			{
				targetResult.OtherFailedStatusError = ex.GetType().Name;
			}

			return new byte[] { };
		}

		private bool SetRequestData(BaseResult targetResult, HttpWebRequest request, byte[] data)
		{
#if DEBUG
			if (data == null || data.Length == 0)
				throw new ArgumentException(nameof(data));
#endif
			try
			{
				using (var requestStream = request.GetRequestStream())
					requestStream.Write(data, 0, data.Length);

				return true;
			} catch (WebException wex)
			{
				var response = wex.Response as HttpWebResponse;
				if (response != null)
					targetResult.StatusCode = (int)response.StatusCode;

				targetResult.WebExceptionStatus = wex.Status;
			} catch (Exception ex)
			{
				targetResult.OtherFailedStatusError = ex.GetType().Name;
			}

			return false;
		}

		private string GetHttpHeaderCharset(string contentType)
		{
			if (string.IsNullOrEmpty(contentType))
				return null;

			var charsetMatch = _httpCharsetRegex.Match(contentType);
			if (charsetMatch.Success)
				return charsetMatch.Groups["value"].Value;

			return null;
		}
		private Encoding GetEncoding(string charset)
		{
			var encoding = _utf8;

			charset = charset?.ToLower().Trim();
			if (!string.IsNullOrEmpty(charset))
			{
				try
				{
					int codePage;
					if (int.TryParse(charset, out codePage) && codePage.ToString() == charset)
						encoding = Encoding.GetEncoding(codePage);
					else
						encoding = Encoding.GetEncoding(charset);
				} catch (Exception)
				{
					//ignore
				}
			}
			return encoding;
		}
	}
}