using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        public static StreamWriter sw;
        public static AnonymousPipeServerStream pipeServer;
        public static EventWaitHandle ev = new EventWaitHandle(false, EventResetMode.AutoReset, "NxPipe");
        [STAThread]
        static void Main()
        {
            XmlLoader.LoadStrings("wz\\longcoatxmlstr.xml");
            XmlLoader.LoadStrings("wz\\capexmlstr.xml");
            XmlLoader.LoadStrings("wz\\glovexmlstr.xml");
            XmlLoader.LoadStrings("wz\\shoesxmlstr.xml");
            XmlLoader.LoadStrings("wz\\weaponxmlstr.xml");

            SetupPipe();
            var processInfo = new ProcessStartInfo
            {
                FileName = "..\\Engine\\NxSim.exe",
                Arguments = pipeServer.GetClientHandleAsString(),
                WorkingDirectory = System.IO.Path.GetDirectoryName("..\\Engine\\NxSim.exe"),
                UseShellExecute = false
            };

            Console.WriteLine("Starting child process...");
            Process.Start(processInfo);
            
            //using (var process = Process.Start(processInfo))
            //{
                //process.WaitForExit();
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GuiForm());
            
        }

        static void SetupPipe()
        {
            pipeServer = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            sw = new StreamWriter(pipeServer);
            sw.AutoFlush = true;
        }


    }
}
