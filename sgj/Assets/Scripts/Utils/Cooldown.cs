using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts.Utils {
    public class Cooldown {
        public bool IsCooling { get; set; }

        public float CoolTime {
            get {
                return _coolTime;
            }
            private set {
                _coolTime = value;
            }
        }

        public float TimeLeft {
            get {
                return CoolTime - _timer;
            }
        }

        private float _timer;
        private float _coolTime;

        public Cooldown() {
        }

        public void StartCooling(float coolTime) {
            if (coolTime < 0) {
                Debug.Log("Wrong cooltime value...");
            } else {
                _timer = 0;
                CoolTime = coolTime;
                IsCooling = true;
            }
        }

        public void Update(float delta) {
            if (IsCooling) {
                _timer += delta;

                if (_timer >= CoolTime) {
                    Reset();
                }
            }
        }

        public void Reset() {
            IsCooling = false;
            _timer = 0;
        }
    }
}
