namespace Hamster.TouchPuzzle {
    public class ItemPicker : Props {

        public EPropID ID = EPropID.None;

        public override void OnClick(int propID) {
            World.GetWorld<TouchPuzzeWorld>().ItemManager.AddItem((int)ID);
            Destroy(gameObject);
        }
    }
}
