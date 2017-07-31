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

        public static String userName;

        //




        SoundPlayer sp;
        public Boolean flag;
        public Login()
        {
            //播放音乐
            string str = System.IO.Directory.GetCurrentDirectory();
            sp = new SoundPlayer(Properties.Resources.bgm);
            sp.PlayLooping();


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**
            建立一个数据库连接对象  con
            server  =   后跟数据库名称   这里是本地数据库
            initial catalog    =    后跟表名
            integrated security=SSPI     这是数据库连接方式
            至于用户名和密码现在还没有考虑

            
            */
            //    SqlConnection con = new SqlConnection("server=B412-008;initial catalog=TankDemo;integrated security=SSPI");

            SqlConnection con = Sql.getCon();
            /*
            查询一般用SqlDataAdapter
            在注册时因为用的插入 所以用的是SqlCommand

            DataAdapter对象在DataSet与数据之间起桥梁作用
            DataSet，DataAdapter读取数据。 
            */

            try
            {
                con.Open();
            }
            catch (SqlException err)
            {

                MessageBox.Show("抱歉连接失败，请检查自己的网络连接\n或联系供应商\nQq10086");
                con.Close();
            }



            if (con.State == ConnectionState.Open)
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from userinfor where username='" + text_username.Text.Trim() + "' and userpassword='" + text_password.Text.Trim() + "'", con);
                DataSet ds = new DataSet();
                //使用DataAdapter的Fill方法(填充)，调用SELECT命令
                da.Fill(ds, "userinfor");

                /*
            
                                      这里Count  是计数的意思
                                      查询的结果大于  0   则说明在数据库中有该用户信息 且用户名与密码匹配正确
            
            
                */
                if (ds.Tables["userinfor"].Rows.Count > 0)
                {
                    userName = text_username.Text.ToString();
                    MessageBox.Show("登录成功，转向游戏界面");
                    //
                    //这行代码是为了在游戏界面前成功显示登录界面
                    //
                    //this.Hide();
                    //MapTest map = new MapTest();
                    //map.Show();
                    // this.DialogResult = DialogResult.OK;
                    this.Hide();
                    sp.Stop();
                    Welcome welcome = new Welcome();
                    welcome.Show();
                }
                else
                {
                    MessageBox.Show("用户名或密码有误，请输入正确的用户和密码！");
                    text_password.Text = "";
                    text_username.Focus();

                }
            }
            else
            {

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

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void text_password_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }




        System.Drawing.Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标记是否为左键

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new System.Drawing.Point(-e.X, -e.Y);//Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;             //点击左键按下时标注为true;
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                System.Drawing.Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

    }
}
