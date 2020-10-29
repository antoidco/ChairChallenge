using UnityEngine;

namespace DefaultNamespace {
    public class Unit {
        public Unit(GameObject unityRef) {
            View = unityRef;
        }
        public GameObject View;
    }
}