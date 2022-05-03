using System.Collections;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Vase : Props {

        public Props Key = null;
        private Animator _animtor = null;

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
                Key.enabled = true;
                World.GetWorld<TouchPuzzeWorld>().RemoveCurrentUsingItem();
            }
            else {
                _animtor.Play("Float", 0, 0);
            }
        }

        private int GetHasBloodKey() {
            return (int)ESaveKey.VASE_HAS_BLOOD;
        }
        

      
    }
}