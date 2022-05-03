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
            if (EPropID.Blood == (EPropID)propID) {
                _animtor.Play("FloatUp");
                if (null != Key)
                    Key.enabled = true;

                _hasBlood = true;

                World.GetWorld<TouchPuzzeWorld>().RemoveCurrentUsingItem();

                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetTakedKey(), 1);
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetHasBloodKey(), 1);
            }
            else if (!_hasBlood) {
                _animtor.Play("Float", 0, 0);
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