using System;
using System.Windows.Forms;

namespace CameraCanvas
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the CameraCanvas application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                if (args[0].Equals("/config"))
                {
                    if (args.Length > 1)
                    {
                        //Config is missing!
                        //Application.Run(new Config(args[1]));
                    }
                    else
                    {
                        //Config is missing!
                        //Application.Run(new Config("config.ini")); 
                    }
                }
                else
                {
                    Console.WriteLine(args[0]);
                    Application.Run(new CCMainForm(args[0]));
                }
            }
            else
            {
                Application.Run(new CCMainForm("config.ini"));
            }
        }
    }
}