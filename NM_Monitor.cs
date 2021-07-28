﻿using System;
using System.Timers;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using NetMon;

namespace NetMon_Ex
{

	public class NM_Monitor
	{
		private Timer tmrTime;
		private ArrayList alAdapters;
		private ArrayList alAdaptersMonitored;

		public NM_Monitor()
		{
			alAdapters = new ArrayList();
			alAdaptersMonitored = new ArrayList();

			LoopAdapters();

			tmrTime = new Timer(1000);
			tmrTime.Elapsed += new ElapsedEventHandler(tmrTime_Elapsed);
		}

		private void LoopAdapters()
		{

            PerformanceCounterCategory pcNetworkInterface = new PerformanceCounterCategory("Network Interface");

			foreach (string tmpName in pcNetworkInterface.GetInstanceNames())
			{

				if (tmpName == "MS TCP Loopback interface")
					continue;

				NM_Adapter netAdapter = new NM_Adapter(tmpName);

				netAdapter.pcDownloadCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", tmpName);
				netAdapter.pcUploadCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", tmpName);

				alAdapters.Add(netAdapter);
			}
		}

		private void tmrTime_Elapsed(object sender, ElapsedEventArgs e)
		{
			foreach (NM_Adapter tmpAdapter in alAdaptersMonitored)
				tmpAdapter.Update();
		}

		public NM_Adapter[] arrAdapters
		{
			get
			{
				return (NM_Adapter[])alAdapters.ToArray(typeof(NM_Adapter));
			}
		}

		public void Start()
		{
			if (this.alAdapters.Count > 0)
			{
				foreach (NM_Adapter currAdapter in alAdapters)

					if (!alAdaptersMonitored.Contains(currAdapter))
					{
						alAdaptersMonitored.Add(currAdapter);
						currAdapter.Initialize();
					}

				tmrTime.Enabled = true;
			}
		}

		public void Start(NM_Adapter nmAdapter)
		{
			if (!alAdaptersMonitored.Contains(nmAdapter))
			{
				alAdaptersMonitored.Add(nmAdapter);
				nmAdapter.Initialize();
			}

			tmrTime.Enabled = true;
		}

		public void Stop()
		{
			alAdaptersMonitored.Clear();
			tmrTime.Enabled = false;
		}

		public void Stop(NM_Adapter currAdapter)
		{
			if (alAdaptersMonitored.Contains(currAdapter))
				alAdaptersMonitored.Remove(currAdapter);

			if (alAdaptersMonitored.Count == 0)
				tmrTime.Enabled = false;
		}
	}
}
