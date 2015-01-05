using AsyncContainer.Implementations;
using AsyncContainer.Interfaces;
using log4net;
using Microsoft.Practices.Unity;

namespace AsyncContainer
{
    public class UnityRegularContainer : UnityContainer
    {
        private readonly ILog _logger;

        public UnityRegularContainer(ILog logger)
        {
            _logger = logger;
        }

        public void RegisterComponents()
        {
            _logger.Info("Components registration started");

            var heavyComponent1 = new HeavyComponent1();
            heavyComponent1.Initialize(1);
            this.RegisterInstance<IComponent1>(heavyComponent1);

            var heavyComponent2 = new HeavyComponent2();
            heavyComponent2.Initialize(2);
            this.RegisterInstance<IComponent2>(heavyComponent2);

            var heavyComponent3 = new HeavyComponent3();
            heavyComponent3.Initialize(3);
            this.RegisterInstance<IComponent3>(heavyComponent3);

            var heavyComponent4 = new HeavyComponent4(heavyComponent1, heavyComponent2, heavyComponent3);
            heavyComponent4.Initialize(4);
            this.RegisterInstance<IComponent1>(heavyComponent1);

            _logger.Info("Components registration finished");
        }
    }
}