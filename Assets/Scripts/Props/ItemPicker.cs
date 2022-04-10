namespace Hamster.TouchPuzzle {
    public class ItemPicker : Props {

        public ESaveKye SaveKey = ESaveKye.None;
        public EPropID ItemID = EPropID.None;

        public void Awake() {
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.HasValue(GetItemKey())) {
                Destroy(gameObject);
            }
        }

        public override void OnClick(int propID) {
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetItemKey(), 1);
            World.GetWorld<TouchPuzzeWorld>().ItemManager.AddItem((int)ItemID);
            Destroy(gameObject);
        }

        private int GetItemKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)ItemID, (int)SaveKey, 0);
        }
    }
}
