using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class ClientData
    {
        public String username { get; set; }
        public String password { get; set; }

        public ClientData(String username, String password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
