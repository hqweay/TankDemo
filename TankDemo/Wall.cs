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
    }
}
