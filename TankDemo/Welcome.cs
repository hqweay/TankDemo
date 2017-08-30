using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("坦克小战由葫芦娃项目组开发\n\n以下排名不分先后：\n\n桂贞林 向新宇 张   波\n罗博伦 李成洪 杨泽宇\n衡清文");
        }

        private void gameSpecial_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map map = new Map(this, 1);
            map.Show();
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void Rank_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("新的风暴已经出现，赶快加入坦克大军一起拯救世界吧！\n操作：在坦克小战的世界里，您将扮演一辆坦克，按方向键进行移动，按空格键发弹。\n道具：在游戏中会随机产生各种有趣的道具，当您获得道具，将会大大增强您的实力！\n胜利规则：在坦克小战的世界里，没有终点，您将一个地图一个地图地与敌人进行作战，直到您的死亡。注意：每消灭完一个地图的敌人，你将进入下一个地图。下一个地图的敌人实力将会大大增强！\n模式：游戏分为两个模式，点开进去一探究竟吧！！");
        }

        private void gameNormal_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map map = new Map(this, 0);
            map.Show();
        }

      

        private void Welcome_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

    }
}
