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
        MoveDiretion bulletDirection ;

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

            image = Properties.Resources.bullet;
            width = image.Width;
            height = image.Height;
            this.bulletDirection = bulletDirection;




        }


        public void Draw(Graphics g)
        {

            g.DrawImage(image, x+10, y+10);



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
            
            
            crashWall();    //判断砖墙及清除的一些操作





        }

        public Rectangle getRectangle()
        {
            return new Rectangle(x,y,width, height);
        }

        public void crashWall()
        {

            for (int i = 0; i < MapTest.wallList.Count; i++)   //遍历墙的集合
            {
                if (this.getRectangle().IntersectsWith(new Rectangle(MapTest.wallList[i].getX(), MapTest.wallList[i].getY(), 40, 40)))
                {
                    if (MapTest.wallList[i].getType() == 0)
                    {
                        MapTest.planeBullets.Remove(this);           //remove 这颗子弹
                        MapTest.wallList.Remove(MapTest.wallList[i]);   //remove 这面墙
                    }
                    if (MapTest.wallList[i].getType() == 1)   //判断是否撞倒铁
                    {
                        MapTest.planeBullets.Remove(this);
                    }

                }
            }

        }   
               
            

            
        


    }
}
