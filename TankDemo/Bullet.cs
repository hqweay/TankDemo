using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TankDemo
{
    public class Bullet
    {
        Image image;         //子弹图像
        private int x;
        private int y;


        private int width;   //子弹宽度
        private int height;  //子弹高度
        MoveDiretion bulletDirection;    //子弹方向

        int speed = 20;     //子弹速度

        public int X         //get set  
        {
            get { return x; }
            set { x = value; }
        }

        public int Y            //get set
        {
            get { return y; }
            set { y = value; }
        }

        public int Width         //  get set
        {
            get { return width; }
            set { Width = value; }
        }
        public int Height          //get set
        {
            get { return height; }
            set { height = value; }
        }


        //子弹构造函数  方向由坦克传入
        public Bullet(MoveDiretion bulletDirection)
        {

            switch (bulletDirection)
            {

                case MoveDiretion.Up:
                    this.bulletDirection = MoveDiretion.Up;     //若方向向上
                    image = Properties.Resources.bullet_up;               //则为设置子弹图片向上，以此类推
                    break;
                case MoveDiretion.Right:
                    this.bulletDirection = MoveDiretion.Right;
                    image = Properties.Resources.bullet_right;
                    break;
                case MoveDiretion.Left:
                    this.bulletDirection = MoveDiretion.Left;
                    image = Properties.Resources.bullet_left;  
                    break;
                case MoveDiretion.Down:
                    this.bulletDirection = MoveDiretion.Down;

                    image = Properties.Resources.bullet_down;  
                    break;

            }


            width = image.Width;         //设置子弹的宽
            height = image.Height;         //设置子弹的高





        }


        //绘制子弹
        public void Draw(Graphics g)
        {

            g.DrawImage(image, x, y);



        }

        //子弹的行为刷新
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

            if (x <= 0)            //向上飞出
            {
                MapTest.planeBullets.Remove(this);    //移除子弹

            }
            if (y <= 0)
            {
                //       MapTest.planeBullets.Remove(this);
                MapTest.planeBullets.Remove(this);

            }
            if (x >= map.getMapWidth())
            {
                MapTest.planeBullets.Remove(this);
            }

            if (y >= map.getMapHeight())
            {
                MapTest.planeBullets.Remove(this);
            }



        }


    }
}
