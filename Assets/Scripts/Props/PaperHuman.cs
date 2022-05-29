using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class PaperHuman : Props {
        public EPropID TargetProp = EPropID.None;
        public string AnimaTriggerName = string.Empty;
        public GameObject OriginItem = null;
        public GameObject TargetItem = null;

        protected bool _isGive = false;
        protected bool _isTake = false;
        public ItemPicker _targetPicker = null;

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
            else {
                OnNotTargetProp();
            }
        }

        protected virtual void OnNotTargetProp() {
        }

        protected virtual void OnClickByTargetProps(int propID) {
            if (_isGive) {
                return;
            }

            OriginItem.SetActive(true);
            TargetItem.SetActive(true);

            _animator.Play("RightHandUp");
            _isGive = true;
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetBBKeyGive(), 1);

            World.GetWorld<TouchPuzzeWorld>().ItemManager.RemoveItem((int)TargetProp);
        }

        protected virtual void OnAwake() {
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetBBKeyGive(), out int isGive)) {
                _isGive = true;
            }
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetBBKeyTake(), out int isTake)) {
                _isTake = true;
            }

            if (_isGive && !_isTake) {
                TargetItem.SetActive(true);
                _animator.Play("RightHandUp", 0, 1);
            }

            _targetPicker.OnPick += OnPickItem;
        }
        
        public override void Finish() {
            base.Finish();

            _targetPicker.OnPick -= OnPickItem;
        }

        protected virtual void OnPickItem(ItemPicker item) {
            if (item == _targetPicker) {
                OriginItem.SetActive(false);
                TargetItem.SetActive(false);

                _animator.Play("LeftHand");

                _isTake = true;
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetBBKeyTake(), 1);

            }
        }

        protected virtual int GetBBKeyGive() {
            return 0;
        }

        protected virtual int GetBBKeyTake() {
            return 0;
        }
    }
}
