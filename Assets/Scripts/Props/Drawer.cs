using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Drawer : Props {

        private bool _isOpen = false;
        private Animator _animator = null;

        private void Awake() {
            _animator = GetComponent<Animator>();
        }

        public override void OnClick(int propID) {
            _isOpen = !_isOpen;
            _animator.SetBool("Open", _isOpen);
        }
    }
}
