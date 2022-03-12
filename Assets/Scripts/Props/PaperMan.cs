using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class PaperMan : Props {
        public EPropID TargetProp = EPropID.None;
        public string AnimaTriggerName = string.Empty;
        public GameObject OriginItem = null;
        public GameObject TargetItem = null;

        private Animator _animator = null;

        private void Awake() {
            _animator = GetComponent<Animator>();
            OriginItem.SetActive(false);
            TargetItem.SetActive(false);
        }

        public override void OnClick(int propID) {
            if (propID == (int)TargetProp) {
                _animator.SetBool(AnimaTriggerName, true);
                OriginItem.SetActive(true);
            }
        }

        public void ChengeItem() {
            OriginItem.SetActive(false);
            TargetItem.SetActive(true);
        }
    }
}
