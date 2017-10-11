using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Terrain
    {
        public int[] size;
        public int[] heights;
        public List<Node> nodes;
        public List<Road> road;
        public List<Route> route;
        public Boolean textureLoaded = false;
        public Boolean pauze = false;
        public Boolean terainAdded = false;
        public String UuidHead;
        public String UuidMainBike;
        public String UuidCamera;
        public String UuidStatsPanel;
        public String UuidMessagePanel;
        public String UuidGroundPlane;
        public String UuidTerrainNode;
        public String UuidRoute;
        public String UuidRoadNode;

        public Terrain(int[] size, int[] heights)
        {
            this.size = size;
            this.heights = heights;
            nodes = new List<Node>();
            road = new List<Road>();
            route = new List<Route>();
        }

        public override string ToString()
        {
            return "Nodes: " + nodes.Count + "\r\nRoads: " + road.Count() + "\r\nRoutes: " + route.Count();
        }

        public void RouteNameReceived(string name)
        {
            route.Last().id = name;
        }

        public void NodeNameReceived(string uuid)
        {
            nodes.Last().uuid = uuid;
        }
    }
}
