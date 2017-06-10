using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Models
{
	abstract class BaseResult
	{
		public Uri Uri { get; }

		protected BaseResult(Uri uri)
		{
			Uri = uri;
		}

		public int? StatusCode;
		public WebExceptionStatus? WebExceptionStatus;
		public string OtherFailedStatusError;
	}
}
