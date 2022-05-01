namespace Hamster.TouchPuzzle {
    public class PaperWoman : PaperHuman {

        protected override void OnPickItem(ItemPicker item) {
            base.OnPickItem(item);

            if (item == _targetPicker) 
                OriginItem.SetActive(true);
        }

        protected override int GetBBKeyGive() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanNecklace, 0, 0);
        }

        protected override int GetBBKeyTake() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanSandalwood, 0, 0);
        }

    }
}
