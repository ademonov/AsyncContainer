using System.Threading;
using AsyncContainer.Interfaces;

namespace AsyncContainer.Implementations
{
    public class HeavyComponent3 : IComponent3
    {
        public void Initialize(int initializationDelaySeconds)
        {
            Thread.Sleep(1000 * initializationDelaySeconds); // really blocks the thread
        }
    }
}