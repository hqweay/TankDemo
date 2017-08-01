using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    class Sql
    {
        /**
           建立一个数据库连接对象  con
           server  =   后跟数据库名称   这里是本地数据库
           initial catalog    =    后跟表名
           integrated security=SSPI     这是数据库连接方式
           至于用户名和密码现在还没有考虑

            
           */
        public static SqlConnection getCon(){
            return new SqlConnection("server=B412-008;initial catalog=TankDemo;integrated security=SSPI");
        //    return new SqlConnection("server=LAPTOP-Q3STI184;initial catalog=TankDemo;integrated security=SSPI");
        }

        /*
        查询一般用SqlDataAdapter
        在注册时因为用的插入 所以用的是SqlCommand

        DataAdapter对象在DataSet与数据之间起桥梁作用
        DataSet，DataAdapter读取数据。 
        */
    }
}
