using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    class Crash
    {
        public static bool isCrashWall(Bullet bullet)
        {

            for (int i = 0; i < MapTest.wallList.Count; i++)   //遍历墙的集合
            {
                if (bullet.getRectangle().IntersectsWith(new Rectangle(MapTest.wallList[i].getX(), MapTest.wallList[i].getY(), 40, 40)))
                {
                    if (MapTest.wallList[i].getType() == 0)
                    {
                        MapTest.wallList.Remove(MapTest.wallList[i]);


                    }
                    if (MapTest.wallList[i].getType() == 2)
                    {
                         return false;

                    }
                    if (MapTest.wallList[i].getType() == 3)
                    {
                        return false;
                    }
                    



                    return true;






                }


            }
            return false;

        }

        public static bool crash(Rectangle rectangle1, Rectangle rectangle2)
        {
            if (rectangle1.IntersectsWith(rectangle2))
            {
                return true;
            }
            return false;

        }
    }
}
