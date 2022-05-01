namespace Hamster.TouchPuzzle {
    public class PaperWoman : PaperChild {

        protected override void OnPickItem(ItemPicker item) {
            base.OnPickItem(item);

            if (item == _targetPicker) 
                OriginItem.SetActive(true);
        }

        protected override int GetBBKeyGive() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EPropID.Necklace, 0, 0);
        }

        protected override int GetBBKeyTake() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EPropID.Sandalwood, 0, 0);
        }

    }
}
