using System.Threading;
using AsyncContainer.Interfaces;

namespace AsyncContainer.Implementations
{
    public class HeavyComponent4 : IComponent4
    {
        public HeavyComponent4(IComponent1 componentInstance1, IComponent2 componentInstance2, IComponent3 componentInstance3)
        {
        }

        public void Initialize(int initializationDelaySeconds)
        {
            Thread.Sleep(1000 * initializationDelaySeconds); // really blocks the thread
        }
    }
}