
using System.Runtime.InteropServices;

namespace Grayscale.P542_Scoreing___.L010____Log
{

    /// <summary>
    /// ＰＣの性能を測ります。
    /// </summary>
    public class PerformanceMetrics
    {

        /// <summary>
        /// メモリの使用量を測るのに使います。
        /// </summary>
        /// <param name="lpbuffer"></param>
        [DllImport("kernel32.dll")]
        extern static void GlobalMemoryStatus(ref MemoryStatus lpbuffer);


        /// <summary>
        /// メモリの使用量を記録するための、決まりきった形です。
        /// </summary>
        private struct MemoryStatus
        {
            public int dwLength;
            public int dwMemoryLoad;
            public int dwTotalPhys, dwAvailPhys;
            public int dwTotalPageFile, dwAvailPageFile;
            public int dwTotalVirtual, dwAvailVirtual;
        }

        private MemoryStatus memoryStatus;


        /// <summary>
        /// コンストラクターです。
        /// </summary>
        public PerformanceMetrics()
        {
            this.memoryStatus = new MemoryStatus();
        }


        /// <summary>
        /// メモリ使用量の1000分率を返します。
        /// 
        /// メモリを 55% 使っていれば、 550 です。
        /// </summary>
        /// <returns></returns>
        public int GetMemoryPermil()
        {
            // メモリ使用率
            GlobalMemoryStatus(ref this.memoryStatus);

            return this.memoryStatus.dwMemoryLoad * 10; // メモリ使用量の1000分率
        }


    }
}
