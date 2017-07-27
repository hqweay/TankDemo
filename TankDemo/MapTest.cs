using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static List<Bullet> planeBullets = new List<Bullet>();
//        Player p;
        private Graphics g = null;
        //创建线程用于刷新屏幕
        private Thread threadRefresh = null;

        private Bitmap bmp = null;

        Image imageMapSoil;
        Image imageMapSteel;
        Image imageMapWater;
//        Image imageMapGrass;
        

        public static MapTest GameForm;
        public static tank Gametank;
        public static int elapsedFrames = 0;

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

            imageMapSoil = Properties.Resources.soil;
            imageMapSteel = Properties.Resources.steel;
            imageMapWater = Properties.Resources.water;

            g = this.CreateGraphics();
            //            initMap();
            createMap();
            

            //            GameForm = this;
            //            Gametank=new tank();
            //---------------------------------------
            //双缓冲的一些设置
            bmp = new Bitmap(this.getMapWidth(), this.getMapHeight());
            g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);

            //设置双缓冲画图
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);




        }

        //原来底层重绘每次会清除画布，然后再全部重新绘制，这才是导致闪烁最主要的原因
        protected override void WndProc(ref Message m)
        {
            // 禁掉清除背景消息
            if (m.Msg == 0x0014)
                return;
            base.WndProc(ref m);
        }

        private void RefreshUI()
        {
            {
                g.Clear(Color.Black);
               

                //显示图像
                drawMap();

                Thread.Sleep(10);
            }
        }
        /// <summary>
        /// 绘制地图
        /// </summary>

        protected override void OnPaint(PaintEventArgs e)
        {
            //获得绘画设备
            Graphics g = e.Graphics;




            //绘制内容---坦克

            foreach (Bullet bullet in planeBullets)
            {
                bullet.Draw(this.CreateGraphics());
            }

 
    //         this.drawMap();
       //     this.initMapRan();

        }
        private void MapTest_Load(object sender, EventArgs e)
        {
            //开启刷新页面线程
            threadRefresh = new Thread(new ThreadStart(RefreshUI));
            threadRefresh.Priority = ThreadPriority.Highest;
            threadRefresh.Start();
            threadRefresh.IsBackground = true;
            //    initMap();
            //    this.createMap();
        }
        private void Update(int elapsedFrames)

        {
            //Gametank.Updata(elapsedFrames);


            //foreach (Bullet bullet in planeBullets)
            //{
            //    bullet.update(this);
            //}
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedFrames++;
      //      elapsedFrames += 40;
            Update(elapsedFrames);
       //     this.Refresh();
         }

        #region 创建墙 家 绘制
        public void createMap()
        {
            this.createHome(this.getMapHeight(), this.getMapWidth());
            this.createWall();
        }
        public void drawMap()
        {
            //           this.drawTank(this.CreateGraphics());
            this.drawHome(this.CreateGraphics());
            this.drawWall(this.CreateGraphics());
        }
        public void drawTank(Graphics g)
        {
            Gametank.Draw(g);
        }      
        public void drawWall(Graphics g)
        {
            foreach (Wall wall in wallList)
            {
                switch (wall.getType())
                {
                    case 0:
                        g.DrawImage(imageMapSoil, wall.getX(), wall.getY());
                 //       g.FillRectangle(new SolidBrush(Color.Green), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    case 1:
                        g.DrawImage(imageMapSteel, wall.getX(), wall.getY());
                        break;
                    case 2:
                        g.DrawImage(imageMapWater, wall.getX(), wall.getY());
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
        public void createWall()
        {
            int mapHeight = getMapHeight();
            int mapWidth = getMapWidth();
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;

            Random ran = new Random();
            while (wallList.Count() != 200)
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
        #endregion


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
        private Boolean isInHome(Wall wallSelf)
        {

            if (wallSelf.getX() >= homeList[0].getX() - 40 && wallSelf.getX() <= homeList[2].getX() + 40 && wallSelf.getY() >= homeList[0].getY() - 2 * 40)
            {
                return true;
            }
            return false;
        }
        #region  获得窗口长宽
        public int getMapHeight()
        {
            return this.Height - Wall.WALL_SIZE;
        }
        public int getMapWidth()
        {
            return this.Width;
        }
        #endregion

        private void MapTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //        Application.Exit();//退出整个应用程序
            //退出时关闭所有线程
                  System.Environment.Exit(0);
        }

        private List<Wall> getWallList()
        {
            return this.wallList;
        }
        private List<Wall> getHomeList()
        {
            return this.homeList;
        }
    }
}
