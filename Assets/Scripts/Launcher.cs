using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.Antoid.ChairChallenge {
    public class Launcher : MonoBehaviour {
        public InputField chairCountField;
        public InputField unitCountField;

        public const int ChairCountDefault = 6;
        public const int UnitCountDefault = 10;

        private bool _executed = false;
        private void Start() {
            chairCountField.text = ChairCountDefault.ToString();
            unitCountField.text = UnitCountDefault.ToString();
        }
        public void OnGameLaunch() {
            if (_executed) return;
            _executed = true;

            int chairCount = Convert.ToInt32(chairCountField.text);
            int unitCount = Convert.ToInt32(unitCountField.text);

            if (chairCount < 0) chairCount = ChairCountDefault;
            if (unitCount < 0) unitCount = UnitCountDefault;

            var gameDataObject = new GameObject("GameData");
            DontDestroyOnLoad(gameDataObject);
            var gameData = gameDataObject.AddComponent<GameData>();
            gameData.chairCount = chairCount;
            gameData.unitCount = unitCount;

            SceneManager.LoadScene(1);
        }
    }
}