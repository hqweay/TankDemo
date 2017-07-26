using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    /// <summary>
    /// Player 继承自 Tank
    /// </summary>
    class Player:Tank
    {
        /// <summary>
        /// 构造函数 初始化Player
        /// </summary>
        public Player(int mapHeight, int mapWidth)
        {
            int mapSizeWidth = mapWidth / 40;
            int mapSizeHeight = mapHeight / 40;
            this.setX((mapSizeWidth / 2 - 2) * 40);
            this.setY(mapSizeHeight * 40);
            //初始方向为右
            this.condition = 0;
           
            
            this.type = 0;
        }

        public void Paint(Graphics g)
        {
           
            g.FillEllipse(new SolidBrush(Color.Green), this.getX(), this.getY(), Tank.TANK_SIZE, Tank.TANK_SIZE);
 //           g.DrawRectangle(new Pen(new SolidBrush(Color.LightSalmon)), this.getX(), this.getY(), 1, 1);
        }
       
        public void Move(Graphics g)
        {
            int condition = this.condition;
            switch (condition)
            {
                case 0:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), Tank.TANK_SIZE, Tank.TANK_SIZE);
                    this.setY(this.getY() - 1);
                    this.Paint(g);
                    break;
                case 1:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), Tank.TANK_SIZE, Tank.TANK_SIZE);
                    this.setY(this.getY() + 1);
                    this.Paint(g);
                    break;
                case 2:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), Tank.TANK_SIZE, Tank.TANK_SIZE);
                    this.setX(this.getX() - 1);
                    this.Paint(g);
                    break;
                case 3:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), Tank.TANK_SIZE, Tank.TANK_SIZE);
                    this.setX(this.getX() + 1);
                    this.Paint(g);
                    break;
                default:
                    break;
            }
        }

        public void initPlayer()
        {
            while (true)
            {
                Console.WriteLine("ddd");
                
            }
        }
    }
}
