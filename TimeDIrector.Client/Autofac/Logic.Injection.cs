using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Context;
using TimeDIrector.Client.Models.Repository;
using TimeDIrector.Client.Services;
using TimeDIrector.Client.Services.Interfaces;
using TimeDIrector.Client.ViewModels;

namespace TimeDIrector.Client.Autofac
{
	static partial class Injection
	{
		private static void RegisterLogicTypes(ContainerBuilder builder)
		{
			builder.RegisterType<WebService>().As<IWebService>();
			builder.Register(r => {
				var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "TimeDirectorDateBase.db");
				if (!File.Exists(path))
					Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Data"));
				//else
				//	File.Delete(path);
				return new TimeDirectorDataContext(path);
			}).InstancePerLifetimeScope();
			builder.RegisterType<ProcessMonitorService>().As<IProcessMonitorService>();
			builder.RegisterType<ClientProcessRepository>().As<IClientProcessRepository>();
			builder.RegisterType<QualityTimeRepository>().As<IQualityTimeRepository>();
			builder.RegisterType<MainWindowViewModel>();
		}
	}
}
