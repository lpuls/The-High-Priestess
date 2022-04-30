using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class PaperHuman : Props {
        public EPropID TargetProp = EPropID.None;
        public string AnimaTriggerName = string.Empty;
        public GameObject OriginItem = null;
        public GameObject TargetItem = null;

        protected Animator _animator = null;

        protected virtual void Awake() {
            _animator = GetComponent<Animator>();
            OriginItem.SetActive(false);
            TargetItem.SetActive(false);
            OnAwake();
        }

        public override void OnClick(int propID) {
            if (propID == (int)TargetProp) {
                OnClickByTargetProps(propID);
            }
        }

        protected virtual void OnClickByTargetProps(int propID) {
        }

        protected virtual void OnAwake() {
        }
    }

    public class PaperMan : PaperHuman {
    }
}
