using System;
using System.Threading.Tasks;
using AsyncContainer.Implementations;
using AsyncContainer.Interfaces;
using Microsoft.Practices.Unity;

namespace AsyncContainer
{
    /// <summary>
    /// 1st version of async registration
    /// </summary>
    /// <returns></returns>
    public class UnityAsyncContainerV1 : UnityContainer
    {
        public async Task RegisterAsync()
        {
            var syncReg = new Object();

            var heavyComponent1Task = Task.Run(() =>
            {
                var heavyComponent1 = new HeavyComponent1();
                heavyComponent1.Initialize(1);
                lock (syncReg)
                {
                    this.RegisterInstance<IComponent1>(heavyComponent1);
                }
                return heavyComponent1;
            });

            var heavyComponent2Task = Task.Run(() =>
            {
                var heavyComponent2 = new HeavyComponent2();
                heavyComponent2.Initialize(2);
                lock (syncReg)
                {
                    this.RegisterInstance<IComponent2>(heavyComponent2);
                }
                return heavyComponent2;
            });

            var heavyComponent3Task = Task.Run(() =>
            {
                var heavyComponent3 = new HeavyComponent3();
                heavyComponent3.Initialize(3);
                lock (syncReg)
                {
                    this.RegisterInstance<IComponent3>(heavyComponent3);
                }
                return heavyComponent3;
            });

            var heavyComponent4Task = Task.Run(async () =>
            {
                var heavyComponent4 = new HeavyComponent4(await heavyComponent1Task, await heavyComponent2Task, await heavyComponent3Task);
                heavyComponent4.Initialize(4);
                lock (syncReg)
                {
                    this.RegisterInstance<IComponent4>(heavyComponent4);
                }
                return heavyComponent4;
            });

            await Task.WhenAll(heavyComponent1Task, heavyComponent2Task, heavyComponent3Task, heavyComponent4Task);
        }

    }
}