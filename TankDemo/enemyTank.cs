using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TankDemo
{
    public class EnemyTank
    {
        
        public int x;
        public int y;
        Image image;//敌方坦克图片
        public int speed = 1;
        public int width;
        public int height;
        public int direct = 1;

        public  MoveDiretion bulletDirection ;


        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public EnemyTank(int x, int y)
        {
            this.x = x;
            this.y = y;
            image = TankDemo.Properties.Resources.tankD;
            width = image.Width;
            height = image.Height;

        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, this.x, this.y);
        }

        public  void createDirectByMe()
        {
             Random r = new Random();
            int direct;
            do
            {
               direct = r.Next(0, 6);
            } while (direct % 4==this.direct);
            this.direct = direct;
        }
        public int createDirect()
        {
            Random r = new Random();

            for (int i = 0; i < 2; i++)
            {
                direct = r.Next(0, 6);
            }//产生0—3的数 0shang 1 xia 2 左 3 右
            
    
             return direct = direct%4; 
        }

        public void move()
        {
            switch (direct)
            {
                case 0:
                    bulletDirection = MoveDiretion.Up;
                    image = Properties.Resources.tankU;
                    y  = y - speed;   //上移
                    break;
                case 1:
                    bulletDirection = MoveDiretion.Down;
                    image = Properties.Resources.tankD;
                    y = y + speed;    //下移
                    break;
                case 2:
                    bulletDirection = MoveDiretion.Left;
                    image = Properties.Resources.tankL;
                    x = x - speed;     //左移
                    break;
                case 3:
                    bulletDirection = MoveDiretion.Right;
                    image = Properties.Resources.tankR;
                    x = x + speed;     //右移
                    break;
            }

        }


        public Rectangle getRectangle()
        {
            return new Rectangle(this.x, this.y, 40,  40);
        }

        //判断撞碍物
        public bool isCrash()
        {
            for (int j = 0; j < Map.wallList.Count; j++)
            {
                if (Crash.crash(this.getRectangle(), Map.wallList[j].getRectangle()))
                {
                    if (Map.wallList[j].getType() == 3)
                    {
                        return false;
                    }
                    else
                    {

                        switch (this.bulletDirection)
                        {
                            case MoveDiretion.Up:
                                this.y = (y/40 +1) * 40; 
                                break;
                            case MoveDiretion.Down:
                                this.y = y / 40 * 40; 
                                break;
                            case MoveDiretion.Left:
                                this.x = (x/40 +1) * 40;
                                break;
                            case MoveDiretion.Right:
                                this.x = x/40* 40;
                                break;
                            default:
                                break;
                        }
                              //重新规整x坐标
                              //重新规整y坐标
                    }
                    this.speed = 0;         //是碰撞瞬间坦克的速度变为零
                    return true; 
                }

               
            }
            
            return false;
        }
        
        //判断撞边界  这个感觉没问题了
        public bool isCrashBoder(Map map)
       {
        

            //-----------------------this.x<=0
            if (this.x <= 0)
            {

                this.x = 0;
                this.speed = 0;         //是碰撞瞬间坦克的速度变为零
                        
                return true;

            }
            else if (this.x + 40 >= map.getMapWidth())
            {
                this.x = map.getMapWidth() - 40;
                this.speed = 0;
                return true;
            }
            else if (this.y <= 0)
            {
                this.y = 0;
                this.speed = 0;         //是碰撞瞬间坦克的速度变为零
                
                return true;
            }
            else if (this.y + 40>= map.getMapHeight())
            {
                this.y = map.getMapHeight() - 40;
                this.speed = 0;
                return true;
            }
            

            return false;
        }

        public bool isCrahTank()
        {
            for (int i = 0; i < Map.enemyList.Count; i++)
            {
                if (this != Map.enemyList[i] && Crash.crash(this.getRectangle(), Map.enemyList[i].getRectangle()))
                {
                    switch (this.bulletDirection)
                    {
                        case MoveDiretion.Up:
                            this.y = (y / 40 + 1) * 40;
                            break;
                        case MoveDiretion.Down:
                            this.y = y / 40 * 40;
                            break;
                        case MoveDiretion.Left:
                            this.x = (x / 40 + 1) * 40;
                            break;
                        case MoveDiretion.Right:
                            this.x = x / 40 * 40;
                            break;
                        default:
                            break;
                    }
                    this.speed = 0;         //是碰撞瞬间坦克的速度变为零
                    return true;
                }
            }
            return false;
        }

    }
}
