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

        public static void uploadScore(string name, int score)
        {
            int SC;
            SqlConnection con = Sql.getCon();
            SqlDataAdapter da = new SqlDataAdapter("select * from userinfor where userName='" + name + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "userinfor");

            SC = (int)ds.Tables["userinfor"].Rows[0]["userScore"];
            if (SC < score)
            {

                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "update userinfor set userScore ='" + score + "'where userName='" + name + "'";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



            }
            else return;
        }

        internal void ups()
        {
            throw new NotImplementedException();
        }
    }
}
