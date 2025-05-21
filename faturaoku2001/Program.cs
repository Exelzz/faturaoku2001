using System;
using System.Windows.Forms;
using faturaoku2001;

namespace faturaoku2001
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }
}
