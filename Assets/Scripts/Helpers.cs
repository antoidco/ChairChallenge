using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public static class Helpers {
        public static Vector3 X0Z(this Vector3 vector) {
            return new Vector3(vector.x, 0, vector.z);
        }
    }
}