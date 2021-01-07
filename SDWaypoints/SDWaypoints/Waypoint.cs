using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SDWaypoints
{
    public class Waypoint
    {
        public Waypoint(String name, bool enabled, Vector3 location)
        {
            this.name = name;
            this.enabled = enabled;
            this.x = location.x;
            this.y = location.y;
            this.z = location.z;
        }

        public string name { get; set; }
        public bool enabled { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
}
