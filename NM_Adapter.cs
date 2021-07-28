using System;
using System.Collections;
using System.Collections.Specialized;

using System.Diagnostics;
namespace NetMon
{
    public class NM_Adapter
    {
        internal NM_Adapter(string strName)
        {
            strAdapterName = strName; 
        }
        private long lngDownloadSpeed { get; set; }
        private long lngUploadSpeed { get; set; }
        private long lngDownloadValue { get; set; }
        private long lngUploadValue { get; set; }
        private long lngOldDownloadValue { get; set; }
        private long lngOldUploadValue { get; set; }
        internal string strAdapterName { get { return strAdapterName; } set {} }
        public long DownloadSpeed { get { return lngDownloadSpeed; } }
        public long UploadSpeed { get { return lngUploadSpeed; } }

        public double DownloadSpeedKbps { get { return lngDownloadSpeed / 1024.0; } }
        public double UploadSpeedKbps { get { return lngUploadSpeed / 1024.0; } }
        internal PerformanceCounter pcDownloadCounter;
        internal PerformanceCounter pcUploadCounter; 

        internal void Initialize()
        {
            lngOldDownloadValue = pcDownloadCounter.NextSample().RawValue;
            lngOldUploadValue = pcUploadCounter.NextSample().RawValue;
        }
        internal void Update()
        {
            lngDownloadValue = pcDownloadCounter.NextSample().RawValue;
            lngUploadValue = pcUploadCounter.NextSample().RawValue;

            lngDownloadSpeed = lngDownloadValue - lngOldDownloadValue;
            lngUploadSpeed = lngUploadValue - lngOldUploadValue;

            lngOldDownloadValue = lngDownloadValue;
            lngOldUploadValue = lngUploadValue;
        }

        public override string ToString()
        {
            return this.strAdapterName;
        }
    }
}
