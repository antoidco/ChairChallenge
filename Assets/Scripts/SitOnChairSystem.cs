using System.Collections.Generic;
using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public class SitOnChairSystem {
        private List<Chair> _chairs = new List<Chair>();
        private List<Unit> _units = new List<Unit>();

        public bool Active { get; set; }
        public bool OneFrameTrigger { get; set; } // todo: event

        public SitOnChairSystem(float chairRadius, float unitVelocity, float changeChairProbability) {
            Active = false;
            OneFrameTrigger = false;
            var chairObjects = GameObject.FindObjectsOfType<ChairComponent>();
            var unitObjects = GameObject.FindObjectsOfType<UnitComponent>();

            foreach (var chairObject in chairObjects) {
                _chairs.Add(new Chair(chairObject.gameObject, chairRadius));
            }

            foreach (var unitObject in unitObjects) {
                _units.Add(new Unit(unitObject.gameObject, unitVelocity, changeChairProbability, this));
            }
        }

        public void Update(float deltaTime) {
            if (Active) {
                foreach (var unit in _units) {
                    unit.Think(_chairs, OneFrameTrigger);
                    unit.Act(deltaTime);
                }
            }

            OneFrameTrigger = false;
        }

        #region helpers

        private List<Chair> _freeChairsCache = new List<Chair>();

        public List<Chair> GetFreeChairs() {
            _freeChairsCache.Clear();
            foreach (var chair in _chairs) {
                if (!chair.Busy) {
                    bool skip = false;
                    foreach (var unit in _units) {
                        if ((unit.View.transform.position.X0Z() - chair.View.transform.position.X0Z()).magnitude <
                            chair.Radius) {
                            skip = true;
                            break;
                        }
                    }

                    if (skip) continue;
                    _freeChairsCache.Add(chair);
                }
            }

            return _freeChairsCache;
        }

        #endregion
    }
}