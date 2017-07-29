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
            }
            else if (text_repassword.Text != text_password.Text)
            {
                MessageBox.Show("重复密码不一致");
                text_repassword.Focus();
                return;
            }
            else
            {

                //
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
            SqlConnection con = Sql.getCon();

            

            //插入操作返回的结果是 受影响的行数   是一个  int 值
            //而查询操作返回的是一个 集合
            //可以在sql中试试 insert 和 select 操作 观察返回结果
          
                SqlCommand com = new SqlCommand("insert into userinfor(userName,userPassword,userEmail,userScore) values('" + tb_user.UserName + "','" + tb_user.UserPWD + "','" + tb_user.UserEmail + "','" + "0" + "'" + ")", con);


                //判断用户名是否存在
                //引用了前面登录的代码
                SqlDataAdapter da = new SqlDataAdapter("select * from userinfor where username='" + text_username.Text.Trim() + "'", con);
                DataSet ds = new DataSet();
                try
                {
                    da.Fill(ds, "userinfor");
                    if (ds.Tables["userinfor"].Rows.Count > 0)
                    {
                        MessageBox.Show("抱歉，该用户名已存在");
                        return;

                    }
                }
                catch (Exception err){
                    MessageBox.Show("抱歉连接失败，请检查自己的网络连接\n或联系供应商\nQq10086");
                }

                try
                {
                    con.Open();

                    //进行注册，即插入数据前应先判断
                    //判断用户名是否也存在

                    int i = com.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show(text_username.Text + ",恭喜你注册成功！！");
                    }

                }
                catch (Exception er)
                {
        //            MessageBox.Show(er.ToString());
                    MessageBox.Show("抱歉连接失败，请检查自己的网络连接\n或联系供应商\nQq10086");
                    con.Close();
                }

                this.Close();

            }
        
    }
}
