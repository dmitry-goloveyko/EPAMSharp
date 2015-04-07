using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    public class User
    {
        public String login;
        public String password;

        public User(String login, String password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
