using System;
using NetMon_Ex;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetMon;

namespace NetMon_App
{
    class Program
    {
        static void Main(string[] args)
        {
            NM_Monitor nmMonitor = new NM_Monitor();
            NM_Adapter[] arrAdapters = nmMonitor.arrAdapters;
            if (arrAdapters.Length == 0)
            {
                Console.WriteLine("No network adapters found.");
                return;
            }

            nmMonitor.Start();
            for (int i = 0; i >= 0; i++)
            {
                foreach (NM_Adapter tmpAdapter in arrAdapters)
                {

/*                    Console.WriteLine(tmpAdapter.strAdapterName);
*/
                    Console.WriteLine("Download: " + tmpAdapter.DownloadSpeedKbps.ToString() + " kbps " + Environment.NewLine +
                       "Upload: " + tmpAdapter.UploadSpeedKbps + " kbps as at "+ DateTime.Now);

                    System.Threading.Thread.Sleep(10000);
                }

            }

        
    }
    }
}
