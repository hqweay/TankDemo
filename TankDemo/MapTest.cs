using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    public partial class MapTest : Form
    {
        //分成40 * 40的格子
        //1080*1920   分为27*48个
        //
        //

        List<Wall> wallList = new List<Wall>();

        public MapTest()
        {

            //
            //
            //做Map测试
            //先不要登录界面
            //
            //Login login = new Login();
            //if (login.ShowDialog() == DialogResult.OK)
            //{
            //    login.Close();
            //}

            Random ran = new Random();
            for (int i = 0; i < 20; i++)
            {
               
                int x = ran.Next(48);
                int y = ran.Next(27);
                Wall wall = new Wall();
                wall.setX(x);
                wall.setY(y);
                wallList.Add(wall);
            }


            InitializeComponent();
        }

        private void MapTest_Load(object sender, EventArgs e)
        {
            
            this.Paint(this.CreateGraphics());
        }

        public void Paint(Graphics g)
        {
            foreach(Wall wall in wallList)
            {
                g.FillRectangle(new SolidBrush(Color.Green), wall.getX(), wall.getY(), wall.getX() + Wall.WALL_SIZE, wall.getY() + Wall.WALL_SIZE);
            }
            
        }
    }
}
