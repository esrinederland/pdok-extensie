using System;
using System.Collections.Generic;
using System.Text;

namespace pdok4arcgis {
    public class BoundingBox {
        private double minx;
        private double maxx;
        private double miny;
        private double maxy;

#region "Properties"
        public double Minx {
            get { return minx; }
            set { minx = value; }
        }
        

        public double Maxx {
            get { return maxx; }
            set { maxx = value; }
        }
       

        public double Miny {
            get { return miny; }
            set { miny = value; }
        }
       

        public double Maxy {
            get { return maxy; }
            set { maxy = value; }
        }
#endregion


    }
}
