using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    static class GameStart
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
        //    Map map = new Map();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
         //   Map map = new Map();
        //    Application.Run(new Login());
            Application.Run(new MapTest());


        }
    }
}
