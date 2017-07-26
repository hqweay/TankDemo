using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    class Wall
    {
        public const int WALL_SIZE = 40;
        private int x;
        private int y;

        //type 代表Wall类型
        //0   1   2   3
        //绿  红  黄   蓝
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
    }
}
