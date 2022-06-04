using System.Collections;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Vase : Props {

        public Props Key = null;

        private Animator _animtor = null;
        private bool _hasBlood = false;

        public override void Init(IField field) {
            base.Init(field);

            _animtor = GetComponent<Animator>();

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasBloodKey(), out int _)) {
                _animtor.Play("FloatUp", 0, 1);
                Key.enabled = true;
            }
            else {
                Key.enabled = false;
            }
        }

        public override void OnClick(int propID) {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();

            if (EPropID.Blood == (EPropID)propID) {
                _animtor.Play("FloatUp");
                if (null != Key)
                    Key.enabled = true;

                _hasBlood = true;

                world.RemoveCurrentUsingItem();
                world.Blackboard.SetValue(GetTakedKey(), 1);
                world.Blackboard.SetValue(GetHasBloodKey(), 1);
            }
            else if (!_hasBlood) {
                _animtor.Play("Float", 0, 0);
                world.ShowMessage(CommonString.VASE_ROOT);
            }
        }

        private int GetHasBloodKey() {
            return (int)ESaveKey.VASE_HAS_BLOOD;
        }

        private int GetTakedKey() {
            return (int)ESaveKey.VASE_KEY_BE_TAKE;
        }
      
    }
}