using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Node
    {
        public string name;
        public string uuid;
        public Node parent;
        public int[] aPosition;
        public int scale;
        public int[] aRotation;


        public Node(string name, Node parent, int[] aPosition, int scale, int[] aRotation)
        {
            this.name = name;
            this.parent = parent;
            this.aPosition = aPosition;
            this.scale = scale;
            this.aRotation = aRotation;
        }


    }
}
