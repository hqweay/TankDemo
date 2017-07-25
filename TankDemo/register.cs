using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            //下面是注册信息的验证
            //完成了基本的验证  
            //重复密码  邮箱格式

            Tb_User tb_user = new Tb_User();
            if (text_username.Text == "" || text_password.Text == "" || text_email.Text == "")
            {
                MessageBox.Show("您未填写完整，请填写");
                text_username.Focus();
                return;
            }
            else if (text_repassword.Text == "")
            {
                MessageBox.Show("请重复密码");
                text_repassword.Focus();
                return;
            } else if (text_repassword.Text != text_password.Text)
            {
                MessageBox.Show("重复密码不一致");
                text_repassword.Focus();
                return;
            }
            else
            {
                //无这行的话
                //弹出的框无法显示
                button_confrim.Enabled = true;
            }
            tb_user.UserName = text_username.Text.Trim();
            tb_user.UserPWD = text_password.Text.Trim();

            //邮箱地址应为  xxxx@xx.xx
            //              用正则表达式匹配吧


            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");


            if (r.IsMatch(text_email.Text))
            {
                tb_user.UserEmail = text_email.Text.Trim();
            }
            else
            {
                MessageBox.Show("请输入正确的邮箱");
                text_email.Text = "";
                text_email.Focus();
                return;
            }
           //操作数据库
           //对数据库进行插入数据操作
            SqlConnection con = new SqlConnection("server=B412-008;initial catalog=TankDemo;integrated security=SSPI");
            SqlCommand com = new SqlCommand("insert into userinfor(userName,userPassword,userEmail) values('" + tb_user.UserName + "','" + tb_user.UserPWD + "','" + tb_user.UserEmail + "'" +  ")", con);
            try
            {
                con.Open();
                int i = com.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show(text_username.Text + ",恭喜你注册成功！！");
                    //main lf = new main();
                    //lf.Show();
                    //this.Hide();
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());

            }
            finally
            {
                con.Close();
            }

            this.Close();

        }
    }
}
