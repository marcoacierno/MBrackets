using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MBrackets
{
    static class Program
    {
        public static Form1 form1;
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form1 = new Form1());
        }
    }
}
