#define DRAW_DEBUG_INFO
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Antoid.ChairChallenge {
    public class SitOnChairSystem {
        private List<Chair> _chairs = new List<Chair>();
        private List<Unit> _units = new List<Unit>();
        private List<Chair> _freeChairsCache = new List<Chair>();
        private Configuration _configuration;
#if DRAW_DEBUG_INFO
        private List<Tuple<LineRenderer, Unit>> _drawLines;
#endif

        public bool Active { get; set; }
        public bool OneFrameTrigger { get; set; } // todo: event

        public SitOnChairSystem(float chairRadius, float unitVelocity, float changeChairProbability,
            Configuration configuration) {
            _configuration = configuration;
            Active = false;
            OneFrameTrigger = false;
            var chairComponents = GameObject.FindObjectsOfType<ChairComponent>();
            var unitComponents = GameObject.FindObjectsOfType<UnitComponent>();
#if DRAW_DEBUG_INFO
            _drawLines = new List<Tuple<LineRenderer, Unit>>();
#endif

            foreach (var chairObject in chairComponents) {
                _chairs.Add(new Chair(chairObject.gameObject, chairRadius));
            }

            foreach (var unitObject in unitComponents) {
                var unit = new Unit(unitObject.gameObject, unitVelocity, changeChairProbability, this);
                _units.Add(unit);
#if DRAW_DEBUG_INFO
                var line = new GameObject("Line");
                line.transform.parent = unitObject.gameObject.transform;
                line.transform.position = unitObject.transform.position;
                var renderer = line.AddComponent<LineRenderer>();
                renderer.material = _configuration.GreenText;
                renderer.startWidth = 0.25f;
                renderer.endWidth = 0.25f;
                renderer.SetPositions(new[] {Vector3.zero, Vector3.zero});
                _drawLines.Add(new Tuple<LineRenderer, Unit>(renderer, unit));
#endif
            }
        }

        public void Update(float deltaTime) {
            if (Active) {
                foreach (var unit in _units) {
                    unit.Think(_chairs, OneFrameTrigger);
                    unit.Act(deltaTime);

#if DRAW_DEBUG_INFO
                    DrawThoughts();
#endif
                }
            }

            OneFrameTrigger = false;
        }

        #region helpers

#if DRAW_DEBUG_INFO

        public void DrawThoughts() {
            foreach (var drawLine in _drawLines) {
                var unit = drawLine.Item2;
                if (unit.GoalChair == null) continue;
                var mat = !unit.GoalChair.Busy ? _configuration.GreenText : _configuration.RedText;
                if (unit.GoalChair.Busy &&
                    (unit.View.transform.position.X0Z() - unit.GoalChair.View.transform.position.X0Z()).magnitude <
                    0.95f * unit.GoalChair.Radius) mat = _configuration.BlueText;
                drawLine.Item1.material = mat;
                drawLine.Item1.SetPosition(0, unit.View.transform.position);
                drawLine.Item1.SetPosition(1, unit.GoalChair.View.transform.position);
            }
        }
#endif

        public List<Chair> GetFreeChairs() {
            _freeChairsCache.Clear();
            foreach (var chair in _chairs) {
                if (!chair.Busy) {
                    bool skip = false;
                    bool checkUnitInRange = false;
                    if (checkUnitInRange) {
                        foreach (var unit in _units) {
                            if ((unit.View.transform.position.X0Z() - chair.View.transform.position.X0Z()).magnitude <
                                chair.Radius) {
                                skip = true;
                                break;
                            }
                        }


                        if (skip) continue;
                    }

                    _freeChairsCache.Add(chair);
                }
            }

            return _freeChairsCache;
        }

        #endregion
    }
}