using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetBlack.Diagnostics.Test
{
    public class CacheInstaller : Installer
    {
        public CacheInstaller()
        {
            var installer = new PerformanceCounterInstaller
            {
                CategoryName = "Example User Cache",
                CategoryHelp = "An example cache of users.",
                CategoryType = PerformanceCounterCategoryType.SingleInstance,
                UninstallAction = UninstallAction.Remove
            };
            installer.Counters.AddRange(CacheMonitor.CreateCounterData("UserCache"));

            Installers.Add(installer);
        }
    }
}
