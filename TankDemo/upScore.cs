using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankDemo
{
    class upScore
    {

        public static void uploadSorre(string name, int score)
        {

            int SC;
            SqlConnection con = new SqlConnection("server=B412-009;initial catalog=TankDemo;integrated security=SSPI");
            SqlDataAdapter da = new SqlDataAdapter("select * from userinfor where username='" + name + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "userinfor");

            SC = Convert.ToInt32(ds.Tables["userinfor"].Rows[1]["userScore"].ToString());//ds.Tables["userinfor"].Rows[1]["userScore"].ToString();
            if (SC < score)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "update userinfor set userScore='" + score.ToString() + "'";
                
                con.Close();



            }
            else return;
        }
    }
}