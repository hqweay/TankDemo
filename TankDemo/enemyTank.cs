using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankDemo
{
    class enemyTank
    {
        public int x;
        public int y;
        Image image;//敌方坦克图片
        public int speed;
        public int width;
        public int height;
        public int direct;

        public enemyTank(Random r)
        {
           
            x = r.Next(0, 1000);
            y = 0;
            speed = 20;
            image = TankDemo.Properties.Resources.tankD;
            width = image.Width;
            height = image.Height;
            createDirect(r);

        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, x, y);
        }


        //-------------------------------------------------------------------------------------------------\\
        //-----------------------------敌人移动的逻辑怎么写

        public void Move(MapTest map, Random r)
        {
            int go = r.Next(4);

            if (this.x < map.getPlayer().getX())
            {
                this.x++;
            }

            else if (this.x > map.getPlayer().getX())
            {
                this.x--;
            }

            if (this.y > map.getPlayer().getY())
            {
                this.y--;
            }

            if (this.y < map.getPlayer().getY())
            {
                this.y++;
            }

        }

        public void createDirect(Random r)
        {
            //   Random r = new Random();
            int newdirect = r.Next(0, 4);//产生0—3的数
            while (this.direct == newdirect)
                newdirect = r.Next(0, 4);//产生0—3的数
            this.direct = newdirect; //0--上,1--下,2--左,3--右
            switch (direct)
            {
                case 0: y -= speed;
                    break;
                case 1:
                    y += speed;
                    break;
                case 3:
                    x -= speed;
                    break;
                case 4:
                    x += speed;
                    break;
            }
        }
            
    

            
        
        
    }
}
