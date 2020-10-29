using System.Collections.Generic;
using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public class Chair {
        public GameObject View { get; set; }
        public bool Busy { get; set; }

        public Chair(GameObject unityRef) {
            View = unityRef;
            Busy = false;
        }
    }
}