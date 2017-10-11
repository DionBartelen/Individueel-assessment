using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Road
    {
        public string id;
        public float heightoffset;

        public Road(string id, float heightoffset)
        {
            this.id = id;
            this.heightoffset = heightoffset;
        }
    }
}
