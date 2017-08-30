using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
   public class Wall
    {
        public const int WALL_SIZE = 40;
        private int x;
        private int y;
        private int life;

        //type 代表Wall类型
        //0   1   2   3
        //砖  铁  水  草

        //特别的  5    代表水晶（Home）  

        //   道具  10      11
       //         炸弹
        private int type;

        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }

        public void setX(int x)
        {
            this.x = x;
        }
        public void setY(int y)
        {
            this.y = y;
        }
        public int getType()
        {
            return type;
        }

        public void setType(int type)
        {
            this.type = type;
        }

        public Rectangle getRectangle()
        {
            return new Rectangle(x, y, 40, 40);
        }
        public int Life
        {
            get { return life; }
            set { life = value; }
        }
        
    }
}
