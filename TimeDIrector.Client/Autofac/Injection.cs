using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeDIrector.Client.Autofac
{
	static partial class Injection
	{
		private static readonly IContainer Container;

		static Injection()
		{
			Container = BuildContainer();
		}

		public static T Resolve<T>()
		{
			return Container.Resolve<T>();
		}

		public static T ResolveKeyed<T>(object serviceKey)
		{
			return Container.ResolveKeyed<T>(serviceKey);
		}

		public static ILifetimeScope BeginLifetimeScope()
		{
			return Container.BeginLifetimeScope();
		}

		public static ILifetimeScope BeginLifetimeScope(string str)
		{
			return Container.BeginLifetimeScope(str);
		}

		private static IContainer BuildContainer()
		{
			var builder = new ContainerBuilder();

			RegisterLogicTypes(builder);

			return builder.Build();
		}
	}
}
