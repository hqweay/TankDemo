using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        //    MapTest map = new MapTest();
       //     Application.Run(new MapTest());

            Application.Run(new MapTest());

            //map.Show();
            //Player p = new Player(map.getMapHeight(), map.getMapWidth());
            //p.Paint(map.CreateGraphics());
            //MessageBox.Show("ddd");


        }
    }
}
