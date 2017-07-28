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
        Image image_tankright;
        Image image_tankleft;
        Image image_tankup;
        Image image_tankdown;
        int speed;
        bool isShooting = false;
        int width;
        int height;
        MoveDiretion xMove;
        MoveDiretion yMove;
        MoveDiretion lastMove;

        MoveDiretion bulletDirection;

        public tank()
        {

        }
        public tank(MapTest map)
        {

            image_tankleft = TankDemo.Properties.Resources.tankL;
            image_tankup = TankDemo.Properties.Resources.tankU;
            image_tankdown = TankDemo.Properties.Resources.tankD;           
            image_tankright = TankDemo.Properties.Resources.tankR;

            width = image_tankup.Width;
            height = image_tankup.Height;
            x = map.getMapWidth()/2 - 90;
            y = map.getMapHeight() - 30;
            speed = 20;

            MapTest.GameForm.KeyDown += new System.Windows.Forms.KeyEventHandler(GameForm_KeyDown);
            MapTest.GameForm.KeyUp += new System.Windows.Forms.KeyEventHandler(GameForm_KeyUp);



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
        public void Updata(int frame)
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

            if (isShooting)
            {
                Bullet bullet = new Bullet(bulletDirection);
                bullet.X = this.x;
                bullet.Y = this.y;
                MapTest.planeBullets.Add(bullet);
            }

        }
        public Rectangle getRectangle()
        {
            return new Rectangle(x, y, width, height);
        }
        public Boolean isTouchWall()
        {
            
            for (int i = 0; i < MapTest.wallList.Count; i++)   //遍历墙的集合
            {
                if (this.getRectangle().IntersectsWith(new Rectangle(MapTest.wallList[i].getX(), MapTest.wallList[i].getY(), 40, 40)))
                {
                    return true;

                }        
            }
            return false;
        }

        #region  get 
        public int getX(){
            return this.x;
        }
        public int getY(){
            return this.y;
        }
        #endregion
        
    }
}
