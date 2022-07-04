using System;
using System.Runtime.InteropServices;

namespace ConsoleAppStayAwake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // source: https://stackoverflow.com/a/24870115

            Console.WriteLine("Hello World!");

            NativeMethods.SetThreadExecutionState(
                NativeMethods.EXECUTION_STATE.ES_CONTINUOUS |
                NativeMethods.EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                NativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                NativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED
            );

            Console.WriteLine("Trying my best not to sleep !");

            Console.ReadLine();
        }

        public static void ResetSystemDefault()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

    internal static partial class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001

            // Legacy flag, should not be used.
            // ES_USER_PRESENT = 0x00000004
        }
    }
}
