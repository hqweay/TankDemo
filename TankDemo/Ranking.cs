using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace TankDemo
{
    public partial class Ranking : Form
    {
        Welcome welcome;
        public Ranking()
        {
            InitializeComponent();
        }
        public Ranking(Welcome welcome)
        {
            this.welcome = welcome;
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = Sql.getCon();//链接数据库
            try
            {
                con.Open();
            }
            catch (SqlException err) 
            {
                groupBox1.Controls.Clear();

                Label research = new Label();//设置排名lable
                research.Text = "尝试连接数据库失败\n\n请联系供应商";
                research.Height = 100;
                research.Width = 400;//设置lable宽度
                research.Location = new System.Drawing.Point(50, 50);
                groupBox1.Controls.Add(research);
                con.Close();
            }


            if (con.State == ConnectionState.Open)
            {

                groupBox1.Controls.Clear();
                string sqlstr = "select row_number() over (order by userScore desc) as 'No', userName,userScore from userinfor order by userScore desc";//设置一个No排名标示，SQL查询积分，降序
                SqlDataAdapter sda = new SqlDataAdapter(sqlstr, con);//执行命令
                DataSet ds = new DataSet();
                sda.Fill(ds, "dt");//将查询数据储存
                // DataTable dt = ds.Tables[0];//结果存入DataTable
                if (ds.Tables["dt"].Rows.Count > 0)//判断如果查到数据
                {
                    int lineHeight = 30;//显示行距30
                    int colWidth = 20;//显示每列间距20
                    for (int i = 0; i < ds.Tables["dt"].Rows.Count; i++)//循环展示排名
                    {

                        Label lbNo = new Label();//设置排名lable
                        lbNo.Text = ds.Tables["dt"].Rows[i]["No"].ToString();//将排名付给lable
                        lbNo.Width = 30;//设置lable宽度

                        lbNo.Location = new System.Drawing.Point(colWidth, (i + 1) * lineHeight);
                        groupBox1.Controls.Add(lbNo);//把排名添加到groupbox
                        Label lbUserName = new Label();//设置名字lable
                        lbUserName.Text = ds.Tables["dt"].Rows[i]["userName"].ToString();//用户名赋给namelable
                        lbUserName.Width = 80;//名字lable宽度
                        lbUserName.Location = new System.Drawing.Point(colWidth + lbNo.Location.X + lbNo.Width, (i + 1) * lineHeight);//用户名lable相对于groupbox的位置
                        groupBox1.Controls.Add(lbUserName);//将用户名Lable添加到groupbox
                        Label lbScore = new Label();//设置分数lable
                        lbScore.Text = ds.Tables["dt"].Rows[i]["userScore"].ToString();//分数给Scorelable
                        lbScore.Width = 60;//宽度
                        lbScore.Location = new System.Drawing.Point(colWidth + lbUserName.Location.X + lbUserName.Width, (i + 1) * lineHeight);//位置
                        groupBox1.Controls.Add(lbScore);//加入
                    }
                }
            }
            else
            {
                
            }


            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void Ranking_Load(object sender, EventArgs e)
        {
            Label research = new Label();//设置排名lable
            research.Text = "正在查询成绩排名...\n\n可能会有延迟...\n\n请稍作等待...";
            research.Height = 100;
            research.Width = 400;//设置lable宽度
            research.Location = new System.Drawing.Point(50, 50);
            groupBox1.Controls.Add(research);
        }

        private void Ranking_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.welcome.Show();
            
        }
    }
}
