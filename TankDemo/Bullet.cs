using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TankDemo
{
    public class Bullet
    {
        Image image;
        private int x;
        private int y;


        private int width;   //子弹宽度
        private int height;
        MoveDiretion bulletDirection = MoveDiretion.Up;

        int speed = 4;

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



        public Bullet(MoveDiretion bulletDirection)
        {

  //          image = Resource.planeBullet1_Power1;
            width = image.Width;
            height = image.Height;
            this.bulletDirection = bulletDirection;




        }


        public void Draw(Graphics g)
        {

            g.DrawImage(image, x, y);



        }


        public void update(MapTest map)
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
            if (x <= 0)
            {
                map.getBulletList().Remove(this);
                
            }
            if (y <= 0)
            {
        //       MapTest.planeBullets.Remove(this);
                map.getBulletList().Remove(this);
               
            }

        }


    }
}
