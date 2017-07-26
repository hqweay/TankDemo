using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    class Home
    {
        private int X;
        private int Y;
        public int getX()
        {
            return X;
        }
        public int getY()
        {
            return Y;
        }
        public void setX(int X){
            this.X = X;
        }
        public void setY(int Y)
        {
            this.Y = Y;
        }
        public void Paint(Graphics g)
        {
            
            g.FillRectangle(new SolidBrush(Color.Black), this.getX(), this.getY(), Wall.WALL_SIZE * 2, Wall.WALL_SIZE * 2);
        }
    }
}
