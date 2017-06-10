using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models;

namespace TimeDIrector.Client.Services.Interfaces
{
	interface IWebService
	{
		bool CheckInternetConnection();
		StringResult GET_String(Uri requestUri);
		StringResult POST_Form(Uri requestUri, Dictionary<string, string> postParameters);
		StringResult POST_Json(Uri requestUri, string json);
		string GetParametersString(Dictionary<string, string> parameters);
	}
}
