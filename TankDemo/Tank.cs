using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Media;

namespace TankDemo
{
    //枚举玩家坦克状态 移动方向 不动
    public enum MoveDiretion
    {
        Stop, Left, Right, Up, Down
    }


    public class tank
    {
        //坦克的坐标，图片，移动速度，开火状态，宽度，高度
        public int x;
        public int y;
        public int life;
        int speed;
        public bool isShooting = false;
        int width;
        int height;
        Image image_tankright;
        Image image_tankleft;
        Image image_tankup;
        Image image_tankdown;
        
        
        MoveDiretion xMove;
        MoveDiretion yMove;
        MoveDiretion lastMove = MoveDiretion.Stop;
        
        MoveDiretion myMoveControl = MoveDiretion.Stop;

        MoveDiretion bulletDirection = MoveDiretion.Left;

        public tank()
        {

        }
        public tank(Map map)
        {

            image_tankleft = TankDemo.Properties.Resources.tankL;
            image_tankup = TankDemo.Properties.Resources.tankU;
            image_tankdown = TankDemo.Properties.Resources.tankD;
            image_tankright = TankDemo.Properties.Resources.tankR;

            width = image_tankup.Width;
            height = image_tankup.Height;
            x = map.getMapWidth() / 2 - 90;
            y = map.getMapHeight() - 40;
            speed = 10;

            Map.GameForm.KeyDown += new System.Windows.Forms.KeyEventHandler(GameForm_KeyDown);
            Map.GameForm.KeyUp += new System.Windows.Forms.KeyEventHandler(GameForm_KeyUp);
        }

        private void GameForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (myMoveControl == MoveDiretion.Stop)
            {
                switch (e.KeyCode)
                {
                    case System.Windows.Forms.Keys.Up:
                        yMove = MoveDiretion.Up;
                        bulletDirection = MoveDiretion.Up;
                        lastMove = MoveDiretion.Up;
                        myMoveControl = MoveDiretion.Up;
                        break;
                    case System.Windows.Forms.Keys.Down:
                        yMove = MoveDiretion.Down;
                        bulletDirection = MoveDiretion.Down;
                        lastMove = MoveDiretion.Down;
                        myMoveControl = MoveDiretion.Down;

                        break;
                    case System.Windows.Forms.Keys.Left:
                        xMove = MoveDiretion.Left;
                        bulletDirection = MoveDiretion.Left;
                        lastMove = MoveDiretion.Left;
                        myMoveControl = MoveDiretion.Left;

                        break;
                    case System.Windows.Forms.Keys.Right:
                        xMove = MoveDiretion.Right;
                        bulletDirection = MoveDiretion.Right;
                        lastMove = MoveDiretion.Right;
                        myMoveControl = MoveDiretion.Right;

                        break;
                    case System.Windows.Forms.Keys.Space:
                        isShooting = true;

                        break;



                }
            }
        }


        private void GameForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.Down:
                    yMove = MoveDiretion.Stop;
                    myMoveControl = MoveDiretion.Stop;
                    break;
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.Right:
                    xMove = MoveDiretion.Stop;
                    myMoveControl = MoveDiretion.Stop;
                    break;
                case System.Windows.Forms.Keys.Space:

                    isShooting = false;
                    break;
            }
        }


        public void Draw(Graphics g)
        {
            if (xMove == MoveDiretion.Stop && yMove == MoveDiretion.Stop)
            {
                switch (lastMove)
                {
                    case MoveDiretion.Right:
                        g.DrawImage(image_tankright, x, y);
                        break;
                    case MoveDiretion.Left:
                        g.DrawImage(image_tankleft, x, y);
                        break;
                    case MoveDiretion.Up:
                        g.DrawImage(image_tankup, x, y);
                        break;
                    case MoveDiretion.Down:
                        g.DrawImage(image_tankdown, x, y);
                        break;
                    case MoveDiretion.Stop:
                         g.DrawImage(image_tankleft, x, y);
                        break;
                }//of switch
            } //of if

            else
            {
                
                    switch (xMove)
                    {
                        case MoveDiretion.Right:
                            g.DrawImage(image_tankright, x, y);
                            break;
                        case MoveDiretion.Left:
                            g.DrawImage(image_tankleft, x, y);
                            break;
                    }
                
                     switch (yMove)
                    {
                        case MoveDiretion.Up:
                            g.DrawImage(image_tankup, x, y);
                            break;
                        case MoveDiretion.Down:
                            g.DrawImage(image_tankdown, x, y);
                            break;
                    }
                

            }

        }

        public void Move()
        {

            if (isTouchWall(lastMove))//检测到碰撞
            {
                return;
            }
            else
            {
                switch (xMove)
                {
                    case MoveDiretion.Left:
                        x -= speed;
                        break;
                    case MoveDiretion.Right:
                        x += speed;
                        break;
                }
                //竖直移动
                switch (yMove)
                {
                    case MoveDiretion.Up:
                        y -= speed;
                        break;
                    case MoveDiretion.Down:
                        y += speed;
                        break;

                }
            }

            if (Map.planeBullets.Count <= 0 && isShooting)
            {
                SoundPlayer p = new SoundPlayer(Properties.Resources.shoot);
                p.Play();
                Bullet bullet = new Bullet(bulletDirection);
                bullet.X = this.x;
                bullet.Y = this.y;
                Map.planeBullets.Add(bullet);
                bullet.speed = 30;
            }

        }
       
        #region 判断玩家是否碰到墙
        public Boolean isTouchWall(MoveDiretion direction)
        {
            foreach (Wall wall in Map.wallList)
            {

                if (this.getRectangle().IntersectsWith(new Rectangle(wall.getX(), wall.getY(), 40, 40)))
                {
                    if (3 == wall.getType())
                    {
                        return false;
                    }

                    switch (direction)
                    {
                        case MoveDiretion.Up:
                            this.y = (this.y / 40 + 1) * 40;
                            break;
                        case MoveDiretion.Down:
                            this.y = (this.y / 40) * 40;
                            break;
                        case MoveDiretion.Left:
                                    this.x = (this.x / 40 + 1) * 40;
                            break;
                        case MoveDiretion.Right:
                            this.x = (this.x / 40) * 40;
                            break;
                        default:
                            break;
                    }
                    return true;

                }

            }

            return false;
        }
        #endregion
        #region 判断玩家是否碰到地图边界
        public void isTouchBorder(Map map)
        {
            if (Map.Gametank.x < 0)
            {
                Map.Gametank.x = 0;
            }
            else if (Map.Gametank.x + 40 > map.getMapWidth())
            {
                Map.Gametank.x = map.getMapWidth() - 40;
            }
            else if (Map.Gametank.y < 0)
            {
                Map.Gametank.y = 0;
            }
            else if (Map.Gametank.y > map.getMapHeight())
            {
                Map.Gametank.y = map.getMapHeight() - 40;
            }

        }
        #endregion

        #region  Get 玩家坦克 X Y坐标  以及Get玩家坦克的矩形 判断撞各种东西会用到
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
        public Rectangle getRectangle()
        {
            return new Rectangle(x, y, width, height);
        }
        #endregion
        #region 判断是否被敌人子弹杀了
        public bool isKillByBullet()
        {
            for (int i = 0; i < Map.enemyBullets.Count; i++)
            {
                if (Map.enemyBullets[i].killMyTank())
                {
                    SoundPlayer p = new SoundPlayer(Properties.Resources.fail);
                    p.Play();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 判断是否吃到道具
        public int isEatProp()
        {
            for(int i = 0; i < Map.propList.Count; i++)
            {
                int type = Map.propList[i].getType();
                 if(Crash.crash(Map.Gametank.getRectangle(), Map.propList[i].getRectangle())){
                        Map.propList.Remove(Map.propList[i]);
                        return type;
                  }
            }
            return -1;
        }
        #endregion
        #region 判断玩家是否撞敌人 若撞则游戏结束
        public Boolean isTouchEnemy()
         {
             for (int i = 0; i < Map.enemyList.Count; i++)
             {
                 if (Crash.crash(Map.Gametank.getRectangle(), Map.enemyList[i].getRectangle()))
                 {
             
                     return true;
                 }
             }
             return false;
         }
        #endregion
        #region 重新加载地图时 玩家回到初始位置
        public void setMyTankLocation(Map map)
        {
            this.x = map.getMapWidth() / 2 - 90;
            this.y = map.getMapHeight() - 40;
        }
        #endregion
        #region 迷雾模式必须的三个重载判断 与墙 与敌人子弹 与敌人
        public Boolean inSmog(Wall wall)
         {
             if (wall.getX() > Map.Gametank.getX() - 300 && wall.getX() < Map.Gametank.getX() + 300 && wall.getY() > Map.Gametank.getY() - 300 && wall.getY() < Map.Gametank.getY() + 300)
             {
                 return false;
             }
             return true;
         }
         public Boolean inSmog(EnemyTank enemy)
         {
             if (enemy.x > Map.Gametank.getX() - 300 && enemy.x < Map.Gametank.getX() + 300 && enemy.y > Map.Gametank.getY() - 300 && enemy.y < Map.Gametank.getY() + 300)
             {
                 return false;
             }
             return true;
         }
         public Boolean inSmog(Bullet enemy)
         {
             if (enemy.X > Map.Gametank.getX() - 300 && enemy.X < Map.Gametank.getX() + 300 && enemy.Y > Map.Gametank.getY() - 300 && enemy.Y < Map.Gametank.getY() + 300)
             {
                 return false;
             }
             return true;
         }
        #endregion

    }

}
