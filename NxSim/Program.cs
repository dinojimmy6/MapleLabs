using System;
using System.Threading;

namespace Game1
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var game = new NxSim();
            if(args.Length > 0)
            {
                PipeClient pc = new PipeClient(args[0], game);
                Thread t = new Thread(
                    new ParameterizedThreadStart(pc.Listen)
                );
                t.Start();
            }
            game.Run();
        }
    }
}
