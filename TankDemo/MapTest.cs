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
        //---------------------
        KeyMouseMessage keyMouse;
        Move move;
        Collider coll = new Collider();

        //各种链表
        public static List<Wall> wallList = new List<Wall>();
        List<Wall> homeList = new List<Wall>();
        List<enemyTank> enemyList = new List<enemyTank>();
        public static List<Bullet> planeBullets = new List<Bullet>();

        //应对双缓存
        private Graphics g = null;
        private Bitmap bmp = null;
        //创建线程用于刷新屏幕
        private Thread threadRefresh = null;
        //敌人坦克线程
        private Thread threadEnemy = null;
        //父窗口的对象，进行该窗口关闭打开父窗口操作
        Welcome welcome;

        Image imageMapSoil;
        Image imageMapSteel;
        Image imageMapWater;
        Image imageMapGrass;
        Image imageHome;
        

        public static MapTest GameForm;
        public static tank Gametank;
        public static int elapsedFrames = 0;

        public MapTest()
        {
            InitializeComponent();

            imageMapSoil = Properties.Resources.soil;
            imageMapSteel = Properties.Resources.steel;
            imageMapWater = Properties.Resources.water;
            imageMapGrass = Properties.Resources.grass;
            imageHome = Properties.Resources.home;

            g = this.CreateGraphics();
            GameForm = this;
            Gametank = new tank(this);
            createAllWall();
            createEnemy();
            

                        
            //---------------------------------------
            //双缓冲的一些设置
            bmp = new Bitmap(this.getMapWidth(), this.getMapHeight());
            g = Graphics.FromImage(bmp);
            //g.Clear(Color.White);

            //设置双缓冲画图
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);


            Point scale, location;

            
            keyMouse = new KeyMouseMessage(this);
            keyMouse.setKeyPreView(true);
            keyMouse.add();

            scale.x = 40;
            scale.y = 40;
            location.x = Gametank.getX();
            location.y = Gametank.getY();

            move = new Move(location, scale);


        }
        #region  含参构造函数 主要是为了返回
        public MapTest(Welcome welcome)
        {
            this.welcome = welcome;
            InitializeComponent();

            imageMapSoil = Properties.Resources.soil;
            imageMapSteel = Properties.Resources.steel;
            imageMapWater = Properties.Resources.water;
            imageMapGrass = Properties.Resources.grass;
            imageHome = Properties.Resources.home;

            g = this.CreateGraphics();
            GameForm = this;
            Gametank = new tank(this);
            createAllWall();
            createEnemy();



            //---------------------------------------
            //双缓冲的一些设置
            bmp = new Bitmap(this.getMapWidth(), this.getMapHeight());
            g = Graphics.FromImage(bmp);
            //进游戏时擦除背景
            //g.Clear(Color.White);

            //设置双缓冲画图
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);


            Point scale, location;


            keyMouse = new KeyMouseMessage(this);
            keyMouse.setKeyPreView(true);
            keyMouse.add();

            scale.x = 40;
            scale.y = 40;
            location.x = Gametank.getX();
            location.y = Gametank.getY();

            move = new Move(location, scale);


        }
        #endregion

        //-------------重绘闪屏的一点经验------------------
        //#hqweay@qq.com
        //写点自己的经验
        //在不同时刻获取到的Graphics对象是不同的
        //也就是同一个窗体可能有多个Graphics对象
        //猜测应该是这样 但不明白为什么
        //---------------------------------------------------------------------------
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
            Point TrankLocation;

            Random r = new Random();
            while(true){
                g.Clear(Color.Black);
                TrankLocation = move.getNewxy();
          //      Gametank.x = (int)TrankLocation.x;
          //      Gametank.y = (int)TrankLocation.y;
                //显示图像
                playerMove();

                for (int i = 0; i < planeBullets.Count; i++)
                {
                    if (Crash.isCrashWall (planeBullets[i]))
                    {
                        planeBullets.Remove(planeBullets[i]);
                    }
                }

                    drawEnemy(g);

                drawAllWall(g);
                
                bulletMove();

   //             enemyMove(g);

                drawMap();
                Thread.Sleep(10);
            }
        }
        public void enemyMove(Graphics g)
        {

            //            Thread thread = new Thread(new ParameterizedThreadStart(showmessage));
            //string o = "hello";
            //thread.Start((object)o);
            //private static void showmessage(object message)
            //{
            //string temp = (string)message;
            //Console.WriteLine(message);
            //}
            //开启敌人坦克
    //        threadEnemy = new Thread(new ParameterizedThreadStart(drawEnemy));
         //   string ob = (String)(g);
     //       threadEnemy.Start(g);  
     //       drawEnemy(g);

   
        }
        private void playerMove()
        {
            Gametank.Draw(g);

     //       if (MapTest.Gametank.isTouchWall())
     //       {
     //           return;
     //       }
            

            Gametank.Updata(elapsedFrames);
        }
        private void bulletMove()
        {
            for (int i = 0; i < planeBullets.Count; i++)
            {
                planeBullets[i].update(this);
            }
                //子弹绘制
                foreach (Bullet bullet in planeBullets)
                {
                    bullet.Draw(g);
                }
        }
        public void drawMap()
        {
            try
            {
                Graphics g = this.CreateGraphics();
                g.DrawImage(bmp, 0, 0);
            }
            catch
            { }
        }

        #region 创造所有墙块 障碍物 家
        public void createAllWall()
        {
            this.createHome(this.getMapHeight(), this.getMapWidth());
            this.createWall();
        }
        #endregion

        /// <summary>
        /// 画的时候要把g传进去不要重新用this.Graphics()
        /// 猜测这就是闪屏的原因
        /// </summary>
        /// <param name="g"></param>
        public void drawAllWall(Graphics g)
        {
            #region   画wall
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
                //        g.FillRectangle(new SolidBrush(Color.Blue), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        g.DrawImage(imageMapGrass, wall.getX(), wall.getY());
                        break;
                    default:
                        break;
                }
            }
            #endregion
            #region 画Home
            foreach (Wall wall in homeList)
            {
                switch (wall.getType())
                {

                    case 4:
                        g.DrawImage(imageMapSoil, wall.getX(), wall.getY());
                        break;
                    case 5:
                        g.DrawImage(imageHome, wall.getX(), wall.getY());
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        public void drawEnemy(Object g)
        {
            Random r = new Random();
            foreach (enemyTank enemy in enemyList)
            {
                enemy.Move(this, r);
                enemy.Draw((Graphics)g);
            }
        }

        #region 按规律create 墙的集合
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
        #endregion

        #region  create wall
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
        #endregion

        #region  create home
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

        #endregion

        #region create enemy

        public void createEnemy()
        {
            Random r = new Random();
            while (enemyList.Count < 6)
            {
                enemyTank enemy = new enemyTank(r);
                enemyList.Add(enemy);
            }
        }

        #endregion

        /// <summary>
        /// 这里设置为private
        ///因为出错 参数类型权限与函数权限
        ///Wall中某些参数是private的
        ///创建墙的判断
        ///是否重合
        ///创建墙调用 判断是否新建墙是否和已知墙重合
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
            return this.Width - 500;
        }
        #endregion
        private void MapTest_Load(object sender, EventArgs e)
        {
            //开启刷新页面线程
            threadRefresh = new Thread(new ThreadStart(RefreshUI));
            threadRefresh.Priority = ThreadPriority.Highest;
            threadRefresh.Start();
            threadRefresh.IsBackground = true;
           
            //
        }
        private void MapTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //        Application.Exit();//退出整个应用程序
            //退出时关闭所有线程
                  System.Environment.Exit(0);
        }

       
        private List<Wall> getHomeList()
        {
            return this.homeList;
        }

        public tank getPlayer()
        {
            return Gametank;
        }

        private void MapTest_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            //    System.Environment.Exit(0);
            welcome.Show();
        }
    }
}
