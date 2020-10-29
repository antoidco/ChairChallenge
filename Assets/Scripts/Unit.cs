using System.Collections.Generic;
using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public class Unit {
        public GameObject View { get; set; }
        public Chair GoalChair { get; set; }
        private SitOnChairSystem _chairSystem;
        private Chair _sittingChair;
        private float _changeChairProbability;
        private float _velocity;

        public Unit(GameObject unityRef, float velocity, float changeChairProbability, SitOnChairSystem chairSystem) {
            View = unityRef;
            _chairSystem = chairSystem;
            _sittingChair = null;
            _changeChairProbability = changeChairProbability;
            _velocity = velocity;
            GoalChair = null;
        }

        /// <summary>
        /// Force unit to choose one free chair from given chairs list
        /// </summary>
        /// <param name="chairs">chairs list</param>
        /// <returns>false if no free chairs</returns>
        public bool ChooseChair(List<Chair> chairs) {
            var freeChairs = _chairSystem.GetFreeChairs();

            if (freeChairs.Count < 1) return false;

            GoalChair = freeChairs[Random.Range(0, freeChairs.Count)];
            return true;
        }

        /// <summary>
        /// Unit can chagne goal chair with probabilty of ChangeChairProbability
        /// </summary>
        private void MaybeChooseAnotherChair() {
            if (Random.Range(0, 1f) < _changeChairProbability) {
                var freeChairs = _chairSystem.GetFreeChairs();
                if (freeChairs.Count > 0) {
                    if (_sittingChair != null) {
                        GoalChair.Busy = false;
                        _sittingChair = null;
                    }
                    GoalChair = freeChairs[Random.Range(0, freeChairs.Count)];
                }
            }
        }

        public void Think(List<Chair> chairs, bool triggerFrame) {
            // if unit is not sitting and doesnt know where to sit
            if (_sittingChair == null && GoalChair == null) {
                // unit tries to find chair
                ChooseChair(chairs);
                return;
            }

            // if unit is sitting or on his way to goal chair and trigger event happen
            if (triggerFrame) {
                MaybeChooseAnotherChair();
            }
        }

        public void Act(float deltaTime) {
            var currentPosition = View.transform.position.X0Z();
            var goalChairPosition = GoalChair.View.transform.position.X0Z();

            bool _stopMove = false;
            
            if ((currentPosition - goalChairPosition).magnitude < GoalChair.Radius) {
                if (!GoalChair.Busy) {
                    GoalChair.Busy = true;
                    _sittingChair = GoalChair;
                }
                else {
                    _stopMove = (_sittingChair != GoalChair);
                }
            }
            // if need to move
            if (GoalChair != null && !_stopMove) {
                var goalVector = (goalChairPosition - currentPosition);
                float _clamp = GoalChair.Radius / 100f;
                var delta = goalVector.normalized * (_velocity * deltaTime);
                delta = goalVector.magnitude > _clamp ? delta : Vector3.zero;
                View.transform.position += delta;
            }
        }
    }
}