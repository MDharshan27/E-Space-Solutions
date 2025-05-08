using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using E___Space_Solution_System.E_Space_Solution;
using E___Space_Solution_System.E_Space_Solution.Admin;
using E___Space_Solution_System.E_Space_Solution.Colony_Superintendent;
using E___Space_Solution_System.E_Space_Solution.Data_Entry_Operator;
using E___Space_Solution_System.E_Space_Solution.Pilot;
using E___Space_Solution_System.E_Space_Solution.System_Administrator;

namespace E___Space_Solution_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
