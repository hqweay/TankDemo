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
        public Player()
        {
            //初始方向为右
            this.condition = 3;
            //绘制一个小方块
            this.startX = 300;
            this.startY = 400;
        }
    }
}
