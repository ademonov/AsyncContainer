using System.Threading;
using AsyncContainer.Interfaces;

namespace AsyncContainer.Implementations
{
    public class HeavyComponent1 : IComponent1
    {
        public void Initialize(int initializationDelaySeconds)
        {
            Thread.Sleep(1000 * initializationDelaySeconds); // really blocks the thread
        }
    }
}