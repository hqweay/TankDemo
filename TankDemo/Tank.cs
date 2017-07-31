using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TankDemo
{
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

        Image image_tankright;
        Image image_tankleft;
        Image image_tankup;
        Image image_tankdown;
        int speed;
        public bool isShooting = false;
        int width;
        int height;
        MoveDiretion xMove;
        MoveDiretion yMove;
        MoveDiretion lastMove = MoveDiretion.Left;

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
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                    yMove = MoveDiretion.Up;
                    bulletDirection = MoveDiretion.Up;
                    lastMove = MoveDiretion.Up;
                    break;
                case System.Windows.Forms.Keys.Down:
                    yMove = MoveDiretion.Down;
                    bulletDirection = MoveDiretion.Down;
                    lastMove = MoveDiretion.Down;

                    break;
                case System.Windows.Forms.Keys.Left:
                    xMove = MoveDiretion.Left;
                    bulletDirection = MoveDiretion.Left;
                    lastMove = MoveDiretion.Left;

                    break;
                case System.Windows.Forms.Keys.Right:
                    xMove = MoveDiretion.Right;
                    bulletDirection = MoveDiretion.Right;
                    lastMove = MoveDiretion.Right;

                    break;
                case System.Windows.Forms.Keys.Space:
                    isShooting = true;

                    break;



            }
        }


        private void GameForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.Down:
                    yMove = MoveDiretion.Stop;
                    break;
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.Right:
                    xMove = MoveDiretion.Stop;
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

        //        int lastShooTime;
        public void Updata()
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
                Bullet bullet = new Bullet(bulletDirection);
                bullet.X = this.x;
                bullet.Y = this.y;
                Map.planeBullets.Add(bullet);
                bullet.speed = 30;
            }

        }
        public Rectangle getRectangle()
        {
            return new Rectangle(x, y, width, height);
        }
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

        #region  get
        public int getX()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
        #endregion
        public bool isKill()
        {
            //foreach (Bullet enemyBullet in Map.enemyBullets)
            //{
            //    if (enemyBullet.killMyTank())
            //    {
            //        Map.Gametank = null;
            //        return true;
            //    }


            //}
            for (int i = 0; i < Map.enemyBullets.Count; i++)
            {
                if (Map.enemyBullets[i].killMyTank())
                {
                    Map.Gametank = null;
                    return true;
                }
            }
            return false;
        }
        public void setMyTankLocation(Map map)
        {
            this.x = map.getMapWidth() / 2 - 90;
            this.y = map.getMapHeight() - 40;
        }
         public int eatProp()
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

    }
    
}
