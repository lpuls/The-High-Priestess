using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class PaperWomanDead : Props {
        private bool _hasTakeBlood = false;
        private Animator _animator = null;

        public override void Init(IField field) {
            base.Init(field);

            _animator = GetComponent<Animator>();

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetTakeBloodKey(), out int _)) {
                _hasTakeBlood = true;
            }
        }

        public override void OnClick(int propID) {
            if (EPropID.Bowl == (EPropID)propID && !_hasTakeBlood) {
                _hasTakeBlood = true;

                _animator.Play("TakeBlood");

                World.GetWorld<TouchPuzzeWorld>().RemoveCurrentUsingItem();
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetTakeBloodKey(), 1);
            }
        }

        private int GetTakeBloodKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.TakeBlood, 0, 0);
        }

        public void OnAnimTakeBlood() {
            World.GetWorld<TouchPuzzeWorld>().ItemManager.AddItem((int)EPropID.Blood);
        }
    }
}
