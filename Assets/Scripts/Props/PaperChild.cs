namespace Hamster.TouchPuzzle {
    public class PaperChild : PaperHuman {

        private bool _isGive = false;
        private bool _isTake = false;
        public ItemPicker _targetPicker = null;

        protected override void OnAwake() {
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetBBKeyGive(), out int isGive)) {
                _isGive = true;
            }
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetBBKeyTake(), out int isTake)) {
                _isTake = true;
            }

            if (!_isTake) {
                _animator.Play("RightHandUp", 0, 1);
            }

            _targetPicker.OnPick += OnPickItem;
        }

        public override void Finish() {
            base.Finish();

            _targetPicker.OnPick -= OnPickItem;
        }

        protected override void OnClickByTargetProps(int propID) {
            if (_isTake) {
                return;
            }

            OriginItem.SetActive(true);
            TargetItem.SetActive(true);

            _animator.Play("RightHandUp");
            _isTake = true;
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetBBKeyGive(), 1);
        }

        protected void OnPickItem(ItemPicker item) {
            if (item == _targetPicker) {
                OriginItem.SetActive(false);
                TargetItem.SetActive(false);
                _animator.Play("LeftHand");
            }
        }

        protected int GetBBKeyGive() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Matches, 0, 0);
        }

        protected int GetBBKeyTake() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Matches, 0, 0);
        }
    }
}
