using System.Collections.Generic;
using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public class Chair {
        public GameObject View { get; set; }
        public bool Busy { get; set; }
        public float Radius { get; set; }

        public Chair(GameObject unityRef, float radius) {
            Radius = radius;
            View = unityRef;
            Busy = false;
        }
    }
}