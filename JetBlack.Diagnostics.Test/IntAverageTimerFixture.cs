using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace JetBlack.Diagnostics.Test
{
    [TestFixture]
    public class IntAverageTimerFixture
    {
        [Test, Ignore("Example")]
        public void Example()
        {
            var performanceCounterFactory = MockPerformanceCounterFactory.Singleton;

            var averageTimer = new IntAverageTimer(performanceCounterFactory, "ExampleCategory", "TimeSleeping", false);

            var stopwatch = new Stopwatch();
            var random = new Random();
            var i = 0;
            do
            {
                stopwatch.Restart();
                Thread.Sleep(random.Next(100, 200));
                averageTimer.Increment(stopwatch.ElapsedTicks);
            } while (i++ < 10);

        }
    }
}
