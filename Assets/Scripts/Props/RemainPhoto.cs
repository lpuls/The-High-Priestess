using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class RemainPhoto : Props {

        public int MaxSandalwoodCount = 3;
        public ItemPicker ItemPicker = null;
        private Animator _animator = null;

        public override void Init(IField field) {
            base.Init(field);

            ItemPicker.OnPick += OnPickItemPicker;
            _animator = GetComponent<Animator>();
        }

        private void OnPickItemPicker(ItemPicker obj) {
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetHasTakeCandleKey(), 1);
            _animator.Play("Down");
        }

        public override void OnEnterField() {
            base.OnEnterField();

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasTakeCandleKey(), out int _)) {
                return;
            }

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasEnoughSandalwood(), out int value) && value >= MaxSandalwoodCount) {
                _animator.Play("Give");
            }
        }

        private int GetHasTakeCandleKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.PhotoCandle, 0, 0);
        }

        private int GetHasEnoughSandalwood() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.FurnaceSandalwood, 0, 0);
        }

    }
}
