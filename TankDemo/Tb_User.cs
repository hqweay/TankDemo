using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankDemo
{
    public class Tb_User
    {
        private int userid;
        public int UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        private string username;
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        private string userpwd;
        public string UserPWD
        {
            get { return userpwd; }
            set { userpwd = value; }

        }
        private int usersex;
        public int UserSex
        {
            get { return usersex; }
            set { usersex = value; }

        }
        private string useremail;
        public string UserEmail
        {
            get { return useremail; }
            set { useremail = value; }
        }
        private int userscore;
        public int UserScore
        {
            get { return userscore; }
            set { userscore = value; }
        }
    }
}
