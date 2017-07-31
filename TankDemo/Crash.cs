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
        //子弹碰墙检测
        public static bool isCrashWall(Bullet bullet)
        {

            for (int i = 0; i < Map.wallList.Count; i++)   //遍历墙的集合
            {
                if (bullet.getRectangle().IntersectsWith(new Rectangle(Map.wallList[i].getX(), Map.wallList[i].getY(), 40, 40)))
                {
                    if (Map.wallList[i].getType() == 0)
                    {
                        if (Map.wallList[i].Life == 2)
                        {
                            Map.wallList[i].Life--;
                        }
                        else
                        {
                            Map.wallList.Remove(Map.wallList[i]);
                        }

                    }
                    else if (Map.wallList[i].getType() == 2)                
                    {
        
                        return false;

                    }
                    else if (Map.wallList[i].getType() == 3)      // 3 水
                    {
                        return false;
                    }

                    else if(Map.wallList[i].getType() == 5){
                        Map.Gametank = null;
                    }    
                    return true;
                }
            }
            return false;
        }

        //广义两个矩形
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
