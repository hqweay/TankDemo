using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    public class enemyBullet : Bullet
    {
        int speed = 10;
        public void crashHome()
        {
            for (int i = 0; i < MapTest.homeList.Count; i++)   //遍历墙的集合
            {
                if (this.getRectangle().IntersectsWith(new Rectangle(MapTest.homeList[i].getX(), MapTest.wallList[i].getY(), 40, 40)))
                {

                    MapTest.wallList.Remove(MapTest.homeList[i]);   //remove 这面墙

                }
            }
        }

        public enemyBullet()
        {

        }
    }
}
