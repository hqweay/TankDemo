using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace TankDemo
{
    public partial class Map : Form
    {
        public int score = 0;


        public int ENEMY_SPEED = 0;
        public int ENEMYBULLET_SPEED = 0;
        public static List<Wall> wallList = new List<Wall>();
        public static List<EnemyTank> enemyList = new List<EnemyTank>();
        public static List<Bullet> planeBullets = new List<Bullet>();    //我方子弹
        public static List<Bullet> enemyBullets = new List<Bullet>();

        public static List<Wall> propList = new List<Wall>();

        Welcome welcome;
        private Graphics g = null;
        //创建线程用于刷新屏幕
        private Thread threadRefresh = null;

        //敌人坦克线程  暂时没用
        //private Thread threadEnemy = null;

        private Bitmap bmp = null;

        Image imageMapSoil;
        Image imageMapSteel;
        Image imageMapWater;
        Image imageMapGrass;
        Image imageHome;


        public static Map GameForm;
        public static tank Gametank;
        public static int elapsedFrames = 0;

        public Map()
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

            //设置双缓冲画图
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }
        public Map(Welcome welcome)
        {
            this.welcome = welcome;

            InitializeComponent();

            imageMapSoil = Properties.Resources.soil;
            imageMapSteel = Properties.Resources.steel;
            imageMapWater = Properties.Resources.water;
            imageMapGrass = Properties.Resources.grass;
            imageHome = Properties.Resources.home_1;

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
         }
        //---------------------------------------------------------------------------
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
            while (true)
            {
                myTankMove();
                //撞墙的逻辑也在
                enemyMove();
    
                myBulletMove();
                //敌人子弹移动 以及子弹是否撞墙的判断
                enemyBulletMove();
                // bulletCrash();


                if (!myTankIsLiving())
                {
                    break;
                }
                


                //显示图像
                g.Clear(Color.Black);

                drawMyTank();

                drawEnemy();

                drawAllBullet();

                drawAllWall(g);
                drawProp();
                theMessageOne();
                theMessageTwo();
                theMessageThree();

                if (enemyList.Count == 0)
                {
                    MessageBox.Show("准备好了吗 下关提高难度了哦");
                    createNewMap();
                }

                drawMap();

            }


            upScore.uploadScore(Login.userName, score);
            this.BackgroundImage = Properties.Resources.Gameover;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            
                
            MessageBox.Show("恭喜您：\n您战神般的成绩已被记录到数据库\n您可以退出游戏查看\n");
           
        }
        #region
        public void theMessageOne()
        {
            if(score == 30)
            {
                score += 2;
                MessageBox.Show("哇 你简直是太牛逼了 \n你的分数已经高达30分\n我们将赠送你2分以资鼓励！！");
            }
        }
        public void theMessageTwo()
        {
            if (score == 50)
            {
                score++;
                MessageBox.Show("加油 你马上就能赶上最强选手杨泽宇了！！\n这次我们也慷慨地送给你1分");
            }
        }
        public void theMessageThree()
        {
            if (score == 80)
            {
                score++;
                MessageBox.Show("哇！！！！\n你已经无人可挡了快给自己一个巴掌庆祝一下吧");
            }
        }
        #endregion
        #region 一段判断玩家是否还活着的代码 发弹打死老王的代码不在这哦 在 Crash类里 与敌人相碰也是会挂掉的

        public Boolean myTankIsLiving()
        {
            if (Gametank != null)
            {
                if (Gametank.isKillByBullet() || Gametank.isTouchEnemy())
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion 
        #region 当达到某个条件 重新生成敌人和地图 当然 敌人会更强
        public void createNewMap(){
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets.Remove(enemyBullets[i]);
            }
            for (int i = 0; i < planeBullets.Count; i++)
            {
                planeBullets.Remove(planeBullets[i]);
            }
            for (int i = 0; i < wallList.Count; i++)
            {
                wallList.Remove(wallList[i]);
            }
            for (int i = 0; i < propList.Count; i++)
            {
                wallList.Remove(propList[i]);
            }
            
            if(ENEMYBULLET_SPEED < 15){
                ENEMYBULLET_SPEED += 8;
            }
            createAllWall();
            createEnemy();
            Gametank.setMyTankLocation(this);
            Gametank.isShooting = false;
     }
        #endregion
        #region 敌人子弹移动 以及子弹是否撞墙的判断
        public void enemyBulletMove()
        {
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets[i].Move(this);
            }

            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (Crash.isCrashWall(enemyBullets[i]))
                {
                    bool a = enemyBullets.Remove(enemyBullets[i]);
                }
            }
        }
        #endregion
        #region 我的子弹的移动 以及 是否撞墙 是否击中敌人 是否与敌人子弹相撞的逻辑
        private void myBulletMove()
        {
            for (int i = 0; i < planeBullets.Count; i++)
            {
                planeBullets[i].Move(this);
            }

            for (int i = 0; i < planeBullets.Count; i++)
            {
                if (Crash.isCrashWall(planeBullets[i]))
                {
                    planeBullets.Remove(planeBullets[i]);
                    break;
                }
                for (int j = 0; j < enemyList.Count; j++)
                {
                    if (Crash.crash(enemyList[j].getRectangle(), planeBullets[i].getRectangle()))
                    {
                        score += 10;
                        SoundPlayer p = new SoundPlayer(Properties.Resources.boom);
                        p.Play();
                        planeBullets.Remove(planeBullets[i]);
                        enemyList.Remove(enemyList[j]);
                        break;
                    }
                }

                for (int j = 0; j < enemyBullets.Count; j++)
                {
                    if (planeBullets.Count>0&&Crash.crash(planeBullets[i].getRectangle(), enemyBullets[j].getRectangle()))
                    {
                        bool a = planeBullets.Remove(planeBullets[i]);
                        bool b = enemyBullets.Remove(enemyBullets[j]);
                        break;
                    }
                }
            }
        }
        #endregion



        #region 敌人移动 以及敌人是否撞墙 是否超过边界的逻辑
        public void enemyMove()
        {

                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i].isTouchWall() || enemyList[i].isTouchBorder(this)||enemyList[i].isTouchMyTank())
                    {
                        int oldDirection = enemyList[i].direct;       //存储原来的方向
                        while (oldDirection == enemyList[i].createDirect())    //产生一个新方向  直到和原来的方向不同
                        {
                            enemyList[i].createDirect();
                        }
                        enemyList[i].speed = 2 * ENEMY_SPEED;                       //为获得新方向的坦克赋予速度
                    }
                }
                for (int i = 0; i < enemyList.Count; i++)
                {
                    enemyList[i].Move();
                }
        }
        #endregion



        #region 玩家的移动 吃道具 是否超边界 是否撞墙的判断放在玩家的移动里-->撞墙就不移动
        public void myTankMove()
        {
            //吃道具逻辑
            int type = Gametank.isEatProp();
            if (type > 0)
            {
                switch (type)
                {
                    case 10:
                        {
                            if (enemyList.Count > 3)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    enemyList.Remove(enemyList[0]);
                                }
                            }
                        }
                        break;
                    case 11:
                        {
                            if (enemyList.Count > 3)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    enemyList[i].speed = 0;
                                }
                            }
                        }
                        break;
                    case 12:
                        {
                            foreach (Wall wall in wallList)
                            {
                                if(wall.getType() == 1)
                                wall.setType(0);
                            }
                        }
                        break;

                }
            }

            Gametank.isTouchBorder(this);
            Gametank.Move();
        }
        #endregion
        /// 绘制的时候要把g传进去不要重新用this.Graphics()
        /// 猜测这就是闪屏的原因
        #region 绘制地图上的一切
        #region 绘制我的坦克
        private void drawMyTank()
        {
            Gametank.Draw(g);    
        }
        #endregion
        #region 绘制敌人
        public void drawEnemy()
        {
            foreach (EnemyTank enemy in enemyList)
            {
                if (!Gametank.inSmog(enemy))
                {
                    enemy.Draw(g);
                }
            }
        }
        #endregion
        #region 绘制敌我双方子弹 当然它们放在不同的集合
        private void drawAllBullet()
        {
           
            //玩家子弹绘制
            for (int i = 0; i < planeBullets.Count; i++)
            {
                planeBullets[i].Draw(g);
            }
            //敌人子弹绘制           
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (!Gametank.inSmog(enemyBullets[i]))
                {
                    enemyBullets[i].Draw(g);
                }
            }
        }
        #endregion
        #region 绘制地图 准确的说 绘制背景要好些 来自网络 解决闪屏问题
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
        #endregion
        #region   绘制所有的墙 墙分多种  自己的老家要特别特别区分
        public void drawAllWall(Graphics g)
        {

            #region   画wall
        //    foreach (Wall wall in wallList)
        for(int i = 0; i < wallList.Count; i++)
            {
                if (!Gametank.inSmog(wallList[i]))
                {
                    switch (wallList[i].getType())
                    {
                        case 0:
                            g.DrawImage(imageMapSoil, wallList[i].getX(), wallList[i].getY());
                            break;
                        case 1:
                            g.DrawImage(imageMapSteel, wallList[i].getX(), wallList[i].getY());
                            break;
                        case 2:
                            g.DrawImage(imageMapWater, wallList[i].getX(), wallList[i].getY());
                            break;
                        case 3:
                            g.DrawImage(imageMapGrass, wallList[i].getX(), wallList[i].getY());
                            break;
                        case 4:
                            g.DrawImage(imageMapSoil, wallList[i].getX(), wallList[i].getY());
                            break;
                        case 5:
                         //   g.FillRectangle(new SolidBrush(Color.Green), wallList[i].getX(), wallList[i].getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                            g.DrawImage(imageHome, wallList[i].getX(), wallList[i].getY());
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            
        }
        #endregion

        public void drawProp()
        {
            for (int i = 0; i < propList.Count; i++)
            {
                switch (propList[i].getType())
                {
                    case 10:
                        g.DrawImage(Properties.Resources.bomb, propList[i].getX(), propList[i].getY());
                        break;
                    case 11:
                        g.DrawImage(Properties.Resources.timer, propList[i].getX(), propList[i].getY());
                        break;
                    case 12:
                        g.DrawImage(Properties.Resources.star, propList[i].getX(), propList[i].getY());
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion
        #region 创造地图上的一切--->创建对象存在相应链表
        #region create wall 规律
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
        #region  create wall 随机
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
                int y = ran.Next(2, mapSizeHeight);
                int type = ran.Next(4);

                Wall wall = new Wall();
                wall.setX(x * 40);
                wall.setY(y * 40);
                wall.setType(type);
                wall.Life = 2;
                if(isInHome(wall) || isInSelf(wall))
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
            int startY = (mapSizeHeight - 2);
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
                        wall.Life = 2;
                        wall.setType(0);
                    }
                    //     homeList.Add(wall);
                    wallList.Add(wall);
                }
                startY += 1;
            }
        }

        #endregion

        #region create enemy


        public void createEnemy()
        {
            Random r = new Random();
            int i = 400;
            //创造敌人
            while (enemyList.Count < 10)
            {
                EnemyTank enemy = new EnemyTank(i, 0);
                enemy.speed = 2 + ENEMY_SPEED;
                enemyList.Add(enemy);
                i += 80;
            }
            if (ENEMY_SPEED < 10)
            {
                ENEMY_SPEED += 1;
            }
        }

        #endregion
        public void createAllWall()
        {
            this.createHome(this.getMapHeight(), this.getMapWidth());
            this.createWall();         
        }
        #endregion


        /// 这里设置为private
        ///因为出错 参数类型权限与函数权限
        ///Wall中某些参数是private的
        ///创建墙的判断
        ///是否重合
        #region 一些创建地图的逻辑 创建道具的逻辑 创建墙的逻辑 因为随随机创建 不能重合 以及某些特殊位置不能生成墙.......... 
        #region 道具不能创在已有道具的位置
        public Boolean isInProp(Wall propSelf)
        {
            foreach (Wall prop in propList)
            {
                if (propSelf.getX() == prop.getX())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
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
        #region 家周围不能有墙
        private Boolean isInHome(Wall wallSelf)
        {

            if (wallSelf.getX() >= wallList[0].getX() - 40 && wallSelf.getX() <= wallList[2].getX() + 40 && wallSelf.getY() >= wallList[0].getY() - 2 * 40)
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion
        #region  获得窗口长宽
        public int getMapHeight()
        {
            return this.Height - 36;
        }
        public int getMapWidth()
        {
            return this.Width - 16;
        }
        #endregion




        public tank getPlayer()
        {
            return Gametank;
        }


       // 该定时器生产道具
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = r.Next(0, 3);
            int type = r.Next(10, 13);
            if (propList.Count < 3)
            {
                switch (i)
                {
                    case 0:
                        Wall propLeft = new Wall();
                        propLeft.setX(0);
                        propLeft.setY(40);
                        propLeft.setType(type);

                        if (isInProp(propLeft))
                        {
                            break;
                        }

                        propList.Add(propLeft);
                        break;
                    case 1:
                        Wall propCenter = new Wall();
                        propCenter.setX(this.getMapWidth() / 2);
                        propCenter.setY(40);
                        propCenter.setType(type);
                        if (isInProp(propCenter))
                        {
                            break;
                        }
                        propList.Add(propCenter);
                        break;
                    case 2:
                        Wall propRight = new Wall();
                        propRight.setX(this.getMapWidth() - 40);
                        propRight.setY(40);
                        propRight.setType(type);
                        if (isInProp(propRight))
                        {
                            break;
                        }
                        propList.Add(propRight);
                        break;
                }
                
            }
        }
        //该定时器生产敌方子弹
        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            int i = r.Next(0, enemyList.Count);
            if (enemyList.Count > 0)
            {

                Bullet bullet = new Bullet(enemyList[i].bulletDirection);
                bullet.X = enemyList[i].X;
                bullet.Y = enemyList[i].Y;
                bullet.speed = 7 + ENEMYBULLET_SPEED;
                bullet.image = Properties.Resources.bullet_enemy;
                enemyBullets.Add(bullet);
            }
       }
        private void MapTest_Load(object sender, EventArgs e)
        {
            //开启刷新页面线程
            threadRefresh = new Thread(new ThreadStart(RefreshUI));
            threadRefresh.Priority = ThreadPriority.Highest;
            threadRefresh.Start();
            threadRefresh.IsBackground = true;

            //敌方坦克线程
            //threadEnemy = new Thread(new ThreadStart(enemyMoveNewThread));
            //threadEnemy.Priority = ThreadPriority.Highest;
            //threadEnemy.Start();
            //threadEnemy.IsBackground = true;
        }
        private void MapTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            //测试先删
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                enemyBullets.Remove(enemyBullets[i]);
            }
            for (int i = 0; i < planeBullets.Count; i++)
            {
                planeBullets.Remove(planeBullets[i]);
            }
            for (int i = 0; i < wallList.Count; i++)
            {
                wallList.Remove(wallList[i]);
            }
            for (int i = 0; i < propList.Count; i++)
            {
                wallList.Remove(propList[i]);
            }
            welcome.Show();
            
        }
    }
}
