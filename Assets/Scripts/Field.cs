using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {


    public class Field : MonoBehaviour, IField {

        [SerializeField] private EFieldID ID = 0;
        [SerializeField] private EFieldID GoLeftID = 0;
        [SerializeField] private EFieldID GoRightID = 0;
        [SerializeField] private EFieldID GoBackID = 0;

        private List<IProps> _props = new List<IProps>(16);

        public void Init() {
            IProps[] propsArray = GetComponentsInChildren<IProps>();
            for (int i = 0; i < propsArray.Length; i++) {
                IProps p = propsArray[i];
                p.Init(this);
                _props.Add(p);
            }

            gameObject.SetActive(false);
        }

        public void OnDestroy() {
            World.GetWorld<TouchPuzzeWorld>().FieldManager.Unregister(this);
        }

        public void DestoryProp(IProps props) {
            if (_props.Contains(props)) {
                _props.Remove(props);
            }
        }

        public int GetID() {
            return (int)ID;
        }

        public int GetLeftID() {
            return (int)GoLeftID; 
        }

        public int GetRightID() {
            return (int)GoRightID;
        }

        public int GetBackID() {
            return (int)GoBackID;
        }

        public bool IsValidLeftID() {
            return EFieldID.None != GoLeftID; 
        }

        public bool IsValidRightID() {
            return EFieldID.None != GoRightID;
        }

        public bool IsValidBackID() {
            return EFieldID.None != GoBackID;
        }

        public void OnEnter() {
            gameObject.SetActive(true);
            for (int i = 0; i < _props.Count; i++) {
                IProps p = _props[i];
                p.OnEnterField();
            }
        }

        public void OnLeave() {
            for (int i = 0; i < _props.Count; i++) {
                IProps p = _props[i];
                p.OnLeaveField();
            }
            gameObject.SetActive(false);
        }
    }
}
