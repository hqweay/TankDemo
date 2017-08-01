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
            MessageBox.Show("坦克小战由葫芦娃项目组开发");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map map = new Map(this);
            map.Show();
        }

        private void Welcome_FormClosing(object sender, FormClosingEventArgs e)
        {

            System.Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ranking rank = new Ranking(this);
            rank.Show();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("游戏以闯关的方式进行，击杀敌方坦克10架进入下一关，击杀敌方坦克会有计分。\n操作：方向键移动，空格键发弹。");
        }
    }
}
