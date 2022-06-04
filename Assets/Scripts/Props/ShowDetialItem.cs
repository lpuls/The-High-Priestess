namespace Hamster.TouchPuzzle {
    public class ShowDetialItem : Props {

        public string AtlasPath = string.Empty;
        public string SpritePath = string.Empty;
        public string DetailInfo = string.Empty;

        public override void OnClick(int propID) {
            World.GetWorld<TouchPuzzeWorld>().ShowDetialInfo(AtlasPath, SpritePath, DetailInfo);
        }
    }
}
