using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    /// <summary>
    /// Tank 作为一个父类
    /// 子类有敌人和玩家两类
    /// </summary>
    class Tank
    {
        //坦克有什么属性
        // 初始坐标
        //颜色，图片》》》  外观
        //方法 移动 发子弹 判断是否撞 （墙 敌人 子弹）
        public const int TANK_STEP = 40;
        public const int TANK_SIZE = 40;
        private int startX;
        private int startY;
        //初始方向
        //condition 0123 代表上下左右
        public int condition;
        //   0   1
        //   我  敌
        public int type;


        public void setX(int X)
        {
            this.startX = X;
        }
        public void setY(int Y)
        {
            this.startY = Y;
        }
        public int getX()
        {
            return this.startX;
        }
        public int getY()
        {
            return this.startY;
        }


        public void move() { }
        public void fire() { }
    }
}
