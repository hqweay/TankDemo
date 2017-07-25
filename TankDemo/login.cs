using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    public partial class Login : Form
    {
        public Login()
        {
            string str = System.IO.Directory.GetCurrentDirectory();
       //     MessageBox.Show(str);
           SoundPlayer sp = new SoundPlayer(Properties.Resources.bgm);
            sp.PlayLooping();


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server=B412-008;initial catalog=TankDemo;integrated security=SSPI");
            SqlDataAdapter da = new SqlDataAdapter("select * from userinfor where username='" + text_username.Text.Trim() + "' and userpassword='" + text_password.Text.Trim() + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "userinfor");
            if (ds.Tables["userinfor"].Rows.Count > 0)
            {
                MessageBox.Show("登录成功，转向游戏界面");

                this.Close();
                new Map();

                //下面是游戏界面代码
          //      register form = new register();
         //       form.lbluser.Text = "热烈欢迎游戏玩家：" + text_login_username.Text.Trim() + "";
         //       form.Show();
         //       this.Hide();

            }
            else
            {
                MessageBox.Show("用户名或密码有误，请输入正确的用户和密码！");
                text_username.Text = "";
                text_username.Text = "";
                text_username.Focus();

            }
}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //跳转至注册窗体
            Register formRegister = new Register();
            formRegister.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            text_username.Text= "";
            text_password.Text = "";
        }
    }
}
