namespace Hamster.TouchPuzzle {
    public class PaperChild : PaperHuman {

        protected bool _isGive = false;
        protected bool _isTake = false;
        public ItemPicker _targetPicker = null;

        protected override void OnAwake() {
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

        protected override void OnClickByTargetProps(int propID) {
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
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Matches, 0, 0);
        }

        protected virtual int GetBBKeyTake() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Matches, 0, 0);
        }
    }
}
