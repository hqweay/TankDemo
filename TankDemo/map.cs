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
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();
        }

        private void map_Load(object sender, EventArgs e)
        {
            Player p = new Player();
            Graphics g = this.CreateGraphics();

            g.DrawRectangle(new Pen(new SolidBrush(Color.Red)), p.startX, p.startY, p.startX + 30, p.startY + 30);
        }
    }
}
