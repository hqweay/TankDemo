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
            this.setX(mapSizeWidth / 2 - 2);
            this.setY(mapSizeHeight);
            //初始方向为右
            this.condition = 3;
           
            
            this.type = 0;
        }

        public void Paint(Graphics g)
        {
           
            g.FillEllipse(new SolidBrush(Color.Green), this.getX() * 40, this.getY() * 40, Tank.TANK_SIZE, Tank.TANK_SIZE);
 //           g.DrawRectangle(new Pen(new SolidBrush(Color.LightSalmon)), this.getX(), this.getY(), 1, 1);
        }

        
    }
}
