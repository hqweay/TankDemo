using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TankDemo
{
    public class Bullet
    {
        public Image image;
        private int x;
        private int y;


        private int width;   //子弹宽度
        private int height;
        public int speed = 4;
        MoveDiretion bulletDirection;

        
        #region Set Get X Y Width Height Get子弹矩形
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

        public int Width
        {
            get { return width; }
            set { Width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public Rectangle getRectangle()
        {
            return new Rectangle(x, y, width, height);
        }
        #endregion


        public Bullet(MoveDiretion bulletDirection)
        {
            image = Properties.Resources.bullet;
            width = image.Width;
            height = image.Height;
            this.bulletDirection = bulletDirection;
        }


        public void Draw(Graphics g)
        {
            g.DrawImage(image, x, y);
        }


        public void Move(Map map)
        {
            switch (bulletDirection)
            {
                case MoveDiretion.Up:
                    y -= speed;
                    break;
                case MoveDiretion.Right:
                    x += speed;
                    break;
                case MoveDiretion.Left:
                    x -= speed;
                    break;
                case MoveDiretion.Down:
                    y += speed;
                    break;

            }
            if (x <= 0)            //向上飞出
            {
                Map.planeBullets.Remove(this);    //移除子弹

            }
            if (y < 0)
            {
                Map.planeBullets.Remove(this);

            }
            if (x >= map.getMapWidth())
            {
                Map.planeBullets.Remove(this);
            }

            if (y >= map.getMapHeight())
            {
                Map.planeBullets.Remove(this);
            }
        }

       
        public Bullet()
        {
        }
        public Boolean killMyTank()
        {         
                if (this.getRectangle().IntersectsWith(Map.Gametank.getRectangle()))
                {
                    Map.enemyBullets.Remove(this);
                    return true;
                }
            return false;
        }
    }
}
