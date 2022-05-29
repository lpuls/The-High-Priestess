namespace Hamster.TouchPuzzle {
    public class PaperWoman : PaperHuman {

        protected override void OnPickItem(ItemPicker item) {
            base.OnPickItem(item);

            if (item == _targetPicker) 
                OriginItem.SetActive(true);
        }

        protected override int GetBBKeyGive() {
            return (int)ESaveKey.WOMAN_NECKLACE;
        }

        protected override int GetBBKeyTake() {
            return (int)ESaveKey.WOMAN_SANDALWOOD;
        }

        protected override void OnNotTargetProp() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            if (!world.Blackboard.TryGetValue(GetBBKeyGive(), out int _)) {
                world.ShowMessage(CommonString.WOMAN_WHISPER);
            }
        }

    }
}
