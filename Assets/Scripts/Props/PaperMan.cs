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

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.HasValue(GetBBKey())) {
                _animator.SetBool(AnimaTriggerName, true);
            }
        }

        public override void OnClick(int propID) {
            if (propID == (int)TargetProp) {
                World.GetWorld<TouchPuzzeWorld>().ItemManager.RemoveItem(propID);
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetBBKey(), 1);
                _animator.SetBool(AnimaTriggerName, true);
            }
        }

        protected virtual void OnAnimationComplete() {
            OriginItem.SetActive(false);
            TargetItem.SetActive(true);
        }

        public int GetBBKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Matches, 0, 0); 
        }
    }

    public class PaperMan : PaperHuman {

        protected override void OnAnimationComplete() {
            OriginItem.SetActive(false);
            TargetItem.SetActive(true);
        }
    }
}
