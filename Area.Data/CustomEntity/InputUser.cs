using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area.Data.CustomEntity
{
    public class InputUser:User
    {
        public int permission { get; set; }
        public string Password { get; set; }
    }
}
