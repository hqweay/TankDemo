using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        //Home
        List<Wall> homeList = new List<Wall>();
        Player p;

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
            this.initMapRan();

            //---------------------------------------------------------------------//
            //                       创建坦克之类的开战了
            //--------------------------------------------------------------------//

            //        this.drawPlayer(this.CreateGraphics());
            //另开一个线程显示了Player
                     Thread thPlayer = new Thread(initPlayer);
                     thPlayer.Start();
                    
            // thPlayer.

           

        }

        public void initPlayer()
        {
            this.drawPlayer(this.CreateGraphics());
            while (true)
            {
                try
                {
                    p.Move(this.CreateGraphics());
                    Thread.Sleep(30);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
        }
        public void drawPlayer(Graphics g)
        {
            p = new Player(this.getMapHeight(), this.getMapWidth());
            p.Paint(g);
        }
        public void initMapRan()
        {
            ///载入前初始化地图，算是吧
            this.createHome(this.getMapHeight(), this.getMapWidth());
            this.createWall();
            this.drawHome(this.CreateGraphics());
            this.drawWall(this.CreateGraphics());
        }
        public void initMap()
        {
            ///载入前初始化地图，算是吧
            this.createHome(this.getMapHeight(), this.getMapWidth());
            this.createWall2();
            this.drawHome(this.CreateGraphics());
            this.drawWall(this.CreateGraphics());
        }

        /// <summary>
        /// 画墙
        /// </summary>
        /// <param name="g"></param>
        public void drawWall(Graphics g)
        {
            foreach (Wall wall in wallList)
            {
                switch (wall.getType())
                {
                    case 0:
                        g.FillRectangle(new SolidBrush(Color.Green), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 1:
                        g.FillRectangle(new SolidBrush(Color.Red), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 2:
                        g.FillRectangle(new SolidBrush(Color.BurlyWood), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
             //           g.FillRectangle(new SolidBrush(Color.Red), wall.getX() * 40, wall.getY() * 40, 1,1);
                        break;
                    case 3:
                        g.FillRectangle(new SolidBrush(Color.Blue), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    default:
                        break;
                }
            }

            

        }

        public void createWall2()
        {
            
            int mapHeight = getMapHeight();
            int mapWidth = getMapWidth();
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;
            //      --------------------------第一步
            for (int i = 0; i < 10; i++)
            {
                int x = 5;
                int y = 6;
                Wall wall = new Wall();
                wall.setX((x + i) * 40);
                wall.setY(y * 40);
                wall.setType(1);
                wallList.Add(wall);
            }
            //      --------------------------  第二步
            for (int i = 0; i < 20; i++)
            {
                int x = 10;
                int y = 8;
                Wall wall = new Wall();
                wall.setX((x + i) * 40);
                wall.setY(y * 40);
                wall.setType(1);
                wallList.Add(wall);
            }
            //      --------------------------  第三步
            for (int i = 0; i < 30; i++)
            {
                int x = 8;
                int y = 15;
                Wall wall = new Wall();
                wall.setX((x + i) * 40);
                wall.setY(y * 40);
                wall.setType(2);
                wallList.Add(wall);
            }
            //      --------------------------  第四步
            for (int i = 0; i < 10; i++)
            {
                int x = 33;
                int y = 2;
                Wall wall = new Wall();
                wall.setX(x * 40);
                wall.setY((y + i) * 40);
                wall.setType(3);
                wallList.Add(wall);
            }
            


        }
        /// <summary>
        /// 创造墙
        /// </summary>
        public void createWall()
        {
            int mapHeight = getMapHeight();
            int mapWidth = getMapWidth();
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;

            Random ran = new Random();
            while(wallList.Count() != 200)
            {

                int x = ran.Next(mapSizeWidth);
                int y = ran.Next(mapSizeHeight);
                int type = ran.Next(4);
                Wall wall = new Wall();
                wall.setX(x * 40);
                wall.setY(y * 40);
                wall.setType(type);
                if (isInHome(wall) || isInSelf(wall))
                {
                    continue;
                }
                wallList.Add(wall);
                }
        }
        /// <summary>
        /// 创造水晶
        /// </summary>
        /// <param name="mapHeight"></param>
        /// <param name="mapWidth"></param>
        public void createHome(int mapHeight, int mapWidth)
        {
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;
            int startX = (mapSizeWidth / 2 - 1);
            int startY = (mapSizeHeight - 1);
            for (int i = 0; i < 2; i++)
            {
                int temp = 0;
                for (int j = 0; j < 3; j++)
                {
                    
                    Wall wall = new Wall();
                    wall.setX((startX + temp) * 40);
                    wall.setY(startY * 40);
                    temp++;
                    if (i == 1 && j == 1)
                    {
                        wall.setType(5);
                    }
                    else
                    {
                        wall.setType(4);
                    }
                    homeList.Add(wall);
                }
                startY += 1;
            }
        }
        /// <summary>
        /// 画水晶
        /// </summary>
        /// <param name="g"></param>
        public void drawHome(Graphics g)
        {
            foreach (Wall wall in homeList)
            {
                switch (wall.getType())
                {
                        
                    case 4:
                        
                        g.FillRectangle(new SolidBrush(Color.Black), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 5:
                        g.FillRectangle(new SolidBrush(Color.Red), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    default:
                        break;

                }
            }
            //在窗口上显示字符串
            Font f = new Font("宋体", 34);
            Brush b;
            b = new SolidBrush(Color.White);
            g.DrawString("家", f, b, homeList[4].getX() - 10, homeList[4].getY());
            g.Dispose(); 

        }

       

        /// <summary>
        /// 这里设置为private
        ///因为出错 参数类型权限与函数权限
        ///Wall中某些参数是private的
        ///创建墙的判断
        ///是否重合
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
        /// <summary>
        /// 创建墙的判断
        /// 墙是否和水晶重合
        /// </summary>
        /// <param name="wallSelf"></param>
        /// <returns></returns>
        private Boolean isInHome(Wall wallSelf)
        {
         
            if (wallSelf.getX() >= homeList[0].getX() - 40 && wallSelf.getX() <= homeList[2].getX() + 40 && wallSelf.getY() >= homeList[0].getY() - 2 * 40)
            {
                return true;
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


        private void MapTest_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    p.condition = 0;
                    break;
                case Keys.Down:
                    p.condition = 1;
                    break;
                case Keys.Left:
                    p.condition = 2;
                    break;
                case Keys.Right:
                    p.condition = 3;
                    break;
                default:
                    break;
            }
        }
        

        private void MapTest_FormClosed(object sender, FormClosedEventArgs e)
        {
    //        this.Close();
    //        Application.Exit();//退出整个应用程序
        }

        private void MapTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //        Application.Exit();//退出整个应用程序
            //退出时关闭所有线程
            System.Environment.Exit(0);
        }
    
    }
}
