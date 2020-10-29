using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public class GameData : MonoBehaviour {
        public int chairCount;
        public int unitCount;
        // todo: move to configuration (make scriptable object)
        public const float ChangeChairProbability = 0.45f;
        public const float ChairRadius = 1.5f;
        public const float UnitVelocity = 1f;
    }
}