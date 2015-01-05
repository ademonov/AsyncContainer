using System;
using System.Diagnostics;
using log4net;
using log4net.Config;

namespace AsyncContainer
{
	static class Program
	{
		static void Main()
		{
			XmlConfigurator.Configure();
			var logger = LogManager.GetLogger("DebugLog");
            logger.Info("Starting...");

            var sw = Stopwatch.StartNew();
            var regularContainer = new UnityRegularContainer(logger);
            regularContainer.RegisterComponents();
            logger.InfoFormat("UnityRegularContainer registration completed in {0}ms", sw.ElapsedMilliseconds);
            
            sw.Restart();
            var asyncContainer = new UnityAsyncContainer(logger);
            asyncContainer.RegisterComponentsAsync().Wait();
            logger.InfoFormat("UnityAsyncContainer registration completed in {0}ms", sw.ElapsedMilliseconds);

            logger.Info("Done.");
			Console.ReadKey();
		}
	}
}
