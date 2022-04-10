using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Drawer : LockItem {
        public int ID = 0;

        private bool _isOpen = false;
        private Animator _animator = null;

        private void Awake() {
            _animator = GetComponent<Animator>();
            CheckLock(GetDoorKey());
        }

        public override void OnClickUnlocak(int propID) {
            _isOpen = !_isOpen;
            _animator.SetBool("Open", _isOpen);
        }

        public int GetDoorKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.TributeTableLitterDrawer, ID, 0);
        }

    }
}
