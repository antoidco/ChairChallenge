using UnityEngine;

namespace DefaultNamespace {
    public class Chair {
        public Chair(GameObject unityRef) {
            View = unityRef;
        }
        public GameObject View;
    }
}