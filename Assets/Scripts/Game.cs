using Com.Antoid.ChairChallenge;
using UnityEngine;

namespace com.Antoid.ChairChallenge {
    public class Game : MonoBehaviour {
        public GameObject chairPrefab;
        public GameObject unitPrefab;
        public Configuration Configuration;

        private SitOnChairSystem _sitOnChairSystem;

        private void Start() {
            var gameData = FindObjectOfType<GameData>();
            if (gameData == null) Debug.Log("No GameData in scene");

            PrepareScene(gameData.chairCount, gameData.unitCount);

            _sitOnChairSystem = new SitOnChairSystem(Configuration.ChairRadius, Configuration.UnitVelocity,
                Configuration.ChangeChairProbability);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (!_sitOnChairSystem.Active)
                    _sitOnChairSystem.Active = true;
                else
                    _sitOnChairSystem.OneFrameTrigger = true;
            }

            _sitOnChairSystem.Update(Time.deltaTime);
        }

        private void PrepareScene(int chairs, int units) {
            CreateObjectsInCircle(chairPrefab, chairs, 3);
            CreateObjectsInCircle(unitPrefab, units, 8);
        }

        private void CreateObjectsInCircle(GameObject toCreate, int count, float radius) {
            for (int i = 0; i < count; ++i) {
                var angle = i * 2 * Mathf.PI / count;
                var relativePosition = new Vector3(radius * Mathf.Sin(angle), 0, radius * Mathf.Cos(angle));
                Instantiate(toCreate, relativePosition, Quaternion.identity);
            }
        }
    }
}