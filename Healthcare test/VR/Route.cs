using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare_test.VR
{
    public class Route
    {
        public string id;
        public List<Node> nodes;

        public Route(string id, List<Node> nodes)
        {
            this.id = id;
            this.nodes = nodes;
        }
    }
}
