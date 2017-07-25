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
        public int startX;
        public int startY;
        //初始方向
        //condition 0123 代表上下左右
        public int condition;

        public void move() { }
        public void fire() { }
    }
}
