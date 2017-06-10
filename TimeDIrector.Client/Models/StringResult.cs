using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Models
{
	sealed class StringResult : BaseResult
	{
		public StringResult(Uri uri) :
			base(uri)
		{
		}

		public string ResultString;
	}
}
