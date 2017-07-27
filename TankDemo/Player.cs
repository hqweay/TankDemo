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
    class Player:TankMe
    {
        /// <summary>
        /// 构造函数 初始化Player
        /// </summary>
        public Player(int mapHeight, int mapWidth)
        {
            this.image = Properties.Resources.p2tankU;
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
     //       g.DrawImage(image,this.getX(), this.getY());
            g.FillEllipse(new SolidBrush(Color.Green), this.getX(), this.getY(), TankMe.TANK_SIZE, TankMe.TANK_SIZE);
            g.DrawRectangle(new Pen(new SolidBrush(Color.LightSalmon)), this.getX() + 20, this.getY() + 20, 2, 2);
        }
       
        public void Move(Graphics g, MapTest map)
        {
            int condition = this.condition;
            switch (condition)
            {
                case 0:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), TankMe.TANK_SIZE, TankMe.TANK_SIZE);
                    this.setY(this.getY() - 20);
                    this.Paint(g);
                    break;
                case 1:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), TankMe.TANK_SIZE, TankMe.TANK_SIZE);
                    this.setY(this.getY() + 20);
                    this.Paint(g);
                    break;
                case 2:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), TankMe.TANK_SIZE, TankMe.TANK_SIZE);
                    this.setX(this.getX() - 20);
                    this.Paint(g);
                    break;
                case 3:
                    g.FillEllipse(new SolidBrush(Color.White), this.getX(), this.getY(), TankMe.TANK_SIZE, TankMe.TANK_SIZE);
                    this.setX(this.getX() + 20);
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
