using System.Diagnostics;
using System.Threading;
using JetBlack.Diagnostics.Monitoring;
using NUnit.Framework;

namespace JetBlack.Diagnostics.Test.Monitoring
{
    [TestFixture]
    public class ElapsedTimeFixture
    {
        [Test]
        public void Test()
        {
            const string categoryName = "TestCategory";
            const string categoryHelp = "Test category help";
            const PerformanceCounterCategoryType categoryType = PerformanceCounterCategoryType.SingleInstance;
            const string counterName = "TestElapsedTime";
            const string counterHelp = "Test elapsed time";

            if (!PerformanceCounterCategory.Exists(categoryName))
            {
                var counterCreationData = new CounterCreationDataCollection(ElapsedTime.CounterCreator.CreateCounterData(counterName, counterHelp));
                var category = PerformanceCounterCategory.Create(categoryName, categoryHelp, categoryType, counterCreationData);
            }
            var elapsedTime = new ElapsedTime(PerformanceCounterFactory.Singleton, categoryName, "TestElapsedTime", false);
            elapsedTime.Reset();

            var count = 0;
            while (++count < 10)
            {
                Thread.Sleep(1000);
                var value = elapsedTime.NextValue();
                Debug.Print("Value = {0}", value);
            }

            elapsedTime.Dispose();
            
            PerformanceCounterCategory.Delete(categoryName);
        }
    }
}
