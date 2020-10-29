using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    [CreateAssetMenu(menuName = nameof(Configuration))]
    public class Configuration : ScriptableObject {
        public float ChangeChairProbability = 0.45f;
        public float ChairRadius = 1.5f;
        public float UnitVelocity = 1f;
    }
}