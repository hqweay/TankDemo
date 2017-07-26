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
        //墙的集合
        List<Wall> wallList = new List<Wall>();
        //水晶
        Home home = new Home();

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

            


            InitializeComponent();
        }

        private void MapTest_Load(object sender, EventArgs e)
        {
            this.crateWall();
            Graphics g = Graphics.FromHwnd(Handle);
            this.drawHome(g);
            this.Paint(this.CreateGraphics());
        }

        public void Paint(Graphics g)
        {


            //绘制Home

           
            foreach (Wall wall in wallList)
            {
                switch (wall.getType())
                {
                    case 0:
                        g.FillRectangle(new SolidBrush(Color.Green), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 1:
                        g.FillRectangle(new SolidBrush(Color.Red), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 2:
                        g.FillRectangle(new SolidBrush(Color.Yellow), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 3:
                        g.FillRectangle(new SolidBrush(Color.Blue), wall.getX() * 40, wall.getY() * 40, Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    default:
                        break;
                }
            }

            

        }

        public void crateWall()
        {
            int mapHeight = getMapHeight();
            int mapWidth = getMapWidth();
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;

            Random ran = new Random();
            while(wallList.Count() != 100)
            {

                int x = ran.Next(mapSizeWidth);
                int y = ran.Next(mapSizeHeight);
                int type = ran.Next(4);
                Wall wall = new Wall();
                wall.setX(x);
                wall.setY(y);
                wall.setType(type);
                if (isInSelf(wall) || isInHome(wall))
                {
                    continue;
                }
                wallList.Add(wall);
                }
        }

        private void drawHome(Graphics g)
        {
            home.setX(this.getMapWidth() / 2 - Wall.WALL_SIZE);
            home.setY(this.getMapHeight() - Wall.WALL_SIZE * 2);
            home.Paint(g);

            //在窗口上显示字符串
            Font f = new Font("宋体", 34);
            Brush b;
            b = new SolidBrush(Color.Red);
            g.DrawString("家", f, b, home.getX() + 10, home.getY() + 20);
            g.Dispose(); 

        }

        /// <summary>
        /// 这里设置为private
        ///因为出错 参数类型权限与函数权限
        ///Wall中某些参数是private的
        /// </summary>
        /// <param name="wallSelf"></param>
        /// <returns></returns>
        private Boolean isInSelf(Wall wallSelf)
        {
            foreach (Wall wall in wallList)
            {
                if (wallSelf.getX() == wall.getX() && wallSelf.getY() == wall.getY())
                {
                    return true;
                }
            }
            return false;
        }

        private Boolean isInHome(Wall wallSelf)
        {
            foreach (Wall wall in wallList)
            {
                if (wall.getX() * 40 > home.getX() &&wall.getX() *40 < home.getX() - 40&&wall.getY() *40 > home.getY() && wall.getY() < this.getMapHeight() - 40)
                {
                    return true;
                }
            }
            return false;
        }
        public int getMapHeight()
        {
            return this.Height - Wall.WALL_SIZE;
        }
        public int getMapWidth()
        {
            return this.Width;
        }

        
    }
}
