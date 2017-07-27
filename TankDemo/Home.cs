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
        List<Wall> homeList = new List<Wall>();

        public void creteHome(int mapHeight, int mapWidth)
        {
            int startX = (int)(mapWidth - 1.5 * Wall.WALL_SIZE);
            int startY = mapHeight - 2 * Wall.WALL_SIZE;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Wall wall = new Wall();
                    wall.setX(startX + i * Wall.WALL_SIZE);
                    wall.setY(startY);
                    if (i == 1 && j == 1)
                    {
                        wall.setType(5);
                    }
                    else
                    {
                        wall.setType(4);
                    }
                    homeList.Add(wall);
                }
                startY += Wall.WALL_SIZE;
            }
        }
        public void paintHome(Graphics g)
        {
            foreach (Wall wall in homeList)
            {
                switch (wall.getType())
                {
                    case 4:
                        g.FillRectangle(new SolidBrush(Color.Black), wall.getX(), wall.getY(), Wall.WALL_SIZE , Wall.WALL_SIZE);
                        break;
                    case 5:
                        g.FillRectangle(new SolidBrush(Color.White), wall.getX(), wall.getY(), Wall.WALL_SIZE, Wall.WALL_SIZE);
                        break;
                    default:
                        break;

                }
            }
            //在窗口上显示字符串
            //Font f = new Font("宋体", 34);
            //Brush b;
            //b = new SolidBrush(Color.Red);
            //g.DrawString("家", f, b, home.getX() + 10, home.getY() + 20);
            //g.Dispose(); 
            
        }
    }
}
