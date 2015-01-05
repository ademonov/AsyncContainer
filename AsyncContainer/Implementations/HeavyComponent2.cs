using System.Threading;
using AsyncContainer.Interfaces;

namespace AsyncContainer.Implementations
{
    public class HeavyComponent2 : IComponent2
    {
        public void Initialize(int initializationDelaySeconds)
        {
            Thread.Sleep(1000 * initializationDelaySeconds); // really blocks the thread
        }
    }
}