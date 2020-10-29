using System.Collections.Generic;
using Com.Antoid.ChairChallenge;
using UnityEngine;

namespace DefaultNamespace {
    public class SitOnChairSystem {
        private List<Chair> _chairs = new List<Chair>();
        private List<Unit> _units = new List<Unit>();
        
        public bool Active { get; set; }
        public SitOnChairSystem() {
            var chairObjects = GameObject.FindObjectsOfType<ChairComponent>();
            var unitObjects = GameObject.FindObjectsOfType<UnitComponent>();

            foreach (var chairObject in chairObjects) {
                _chairs.Add(new Chair(chairObject.gameObject));
            }
            foreach (var unitObject in unitObjects) {
                _units.Add(new Unit(unitObject.gameObject));
            }
        }
        public void Update() {
            if (Active) {
                
            }
        }
    }
}