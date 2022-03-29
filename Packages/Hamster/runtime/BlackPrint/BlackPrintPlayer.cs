using UnityEngine;
using Hamster;

namespace Hamster.BP {
    public class BlackPrintPlayer : MonoBehaviour {
        [SerializeField]
        private BlackPrintInst _instance = null;

        public bool PlayOnAwake = false;
        public bool PlayOnStar = false;

        public void Awake() {
            if (null != _instance)
                _instance.Initialize(gameObject);
            if (PlayOnAwake) {
                Execute();
            }
        }

        public void Start() {
            if (PlayOnStar) {
                Execute();
            }
        }

        public void SetInstance(BlackPrintInst inst) {
            _instance = inst;
            _instance.Initialize(gameObject);
        }

        public BlackPrintInst GetInstance() {
            return _instance;
        }

        public void Execute() {
            if (null != _instance)
                _instance.Execute();
        }
    }
}