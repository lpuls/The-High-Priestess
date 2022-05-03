using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Drawer : LockItem {
        public int ID = 0;

        private bool _isOpen = false;
        private Animator _animator = null;

        protected override void Awake() {
            base.Awake();

            _animator = GetComponent<Animator>();
            CheckLock(GetLockKey());
            EnableProps(false);
        }

        public override void OnClickUnlocak(int propID) {
            _isOpen = !_isOpen;
            _animator.SetBool("Open", _isOpen);
            EnableProps(_isOpen);
        }

    }
}
