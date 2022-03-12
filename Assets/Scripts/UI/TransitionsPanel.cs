using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hamster.TouchPuzzle {
    public class TransitionsPanel : MonoBehaviour {
        public float TransitionMaxTime = 0;


        private Image _panel = null;
        private bool _isComplete = false;
        private bool _needUpdate = false;
        private float _transitionTime = 0;
        private float _multiplier = 1.0f;
        private Action _onTransitionComplete = null;

        private void Awake() {
            _panel = GetComponent<Image>();
        }

        public void Execute(Action OnTransitionComplete=null) {
            _multiplier = 1.0f;
            _isComplete = false;
            _needUpdate = true;
            _transitionTime = 0;
            _onTransitionComplete = OnTransitionComplete;
        }

        public void SetComplete() {
            _isComplete = true;

            if (!_needUpdate) {
                _needUpdate = true;
                _multiplier = 1.0f;
                _transitionTime = TransitionMaxTime;
            }
        }

        public void Update() {
            if (_needUpdate && _panel) {
                _transitionTime += _multiplier * Time.deltaTime;
                float t = Mathf.Clamp01(_transitionTime / TransitionMaxTime);
                if (t >= 1 && _multiplier > 0) {
                    _onTransitionComplete?.Invoke();
                    if (_isComplete) {
                        _multiplier = -1.0f;
                        _isComplete = false;
                    }
                    else {
                        _needUpdate = false;  
                    }
                }
                if (t <= 0 && _multiplier < 0) {
                    _needUpdate = false;  
                }
                _panel.color = new Color(0, 0, 0, t);
            }
        }

    }
}
