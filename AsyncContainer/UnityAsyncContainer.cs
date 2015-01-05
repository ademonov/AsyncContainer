using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using AsyncContainer.Implementations;
using AsyncContainer.Interfaces;
using log4net;
using Microsoft.Practices.Unity;

namespace AsyncContainer
{
    public class UnityAsyncContainer : UnityContainer
    {
        private readonly ILog _logger;
        private readonly ConcurrentBag<Task> _registrationTasks;
        private readonly object _syncReg = new object();

        public UnityAsyncContainer(ILog logger)
        {
            _logger = logger;
            _registrationTasks = new ConcurrentBag<Task>();
            this.RegisterInstance(_logger);
        }
        
        public async Task RegisterComponentsAsync()
        {
            _logger.Info("Components async registration started");

            var heavyComponent1Task = RegisterInstanceAsync<IComponent1>(() =>
            {
                var result = new HeavyComponent1();
                result.Initialize(1);
                return result;
            });

            var heavyComponent2Task = RegisterInstanceAsync<IComponent2>(() =>
            {
                var result = new HeavyComponent2();
                result.Initialize(2);
                return result;
            });

            var heavyComponent3Task = RegisterInstanceAsync<IComponent3>(() =>
            {
                var result = new HeavyComponent3();
                result.Initialize(3);
                return result;
            });

            var heavyComponent4Task = RegisterInstanceAsync<IComponent4>(async () =>
            {
                var result = new HeavyComponent4(await heavyComponent1Task, await heavyComponent2Task, await heavyComponent3Task);
                result.Initialize(4);
                return result;
            });

            await FinishRegistrationTasks();
            _logger.Info("Components async registration finished");
        }

        private Task<TInterface> RegisterInstanceAsync<TInterface>(Func<TInterface> registration)
        {
            var result = Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();

                var instance = registration();
                lock (_syncReg)
                {
                    this.RegisterInstance(instance);
                }

                sw.Stop();
                _logger.DebugFormat("Initialization of {0} ({1}) completed in {2}ms.", typeof(TInterface).Name, instance.GetType().Name, sw.ElapsedMilliseconds);
                return instance;
            });

            _registrationTasks.Add(result);
            return result;
        }

        private Task<TInterface> RegisterInstanceAsync<TInterface>(Func<Task<TInterface>> registration)
        {
            return RegisterInstanceAsync(() => registration().Result);
        }

        private async Task FinishRegistrationTasks()
        {
            var sw = Stopwatch.StartNew();
            _logger.Debug("FinishRegistrationTasks started");
            await Task.WhenAll(_registrationTasks);
            _logger.DebugFormat("FinishRegistrationTasks completed in {0}ms", sw.ElapsedMilliseconds);
        }

    }
}