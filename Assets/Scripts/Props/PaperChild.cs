namespace Hamster.TouchPuzzle {
    public class PaperChild : PaperHuman {

        protected override int GetBBKeyGive() {
            return (int)ESaveKey.CHILD_FOOD;
        }

        protected override int GetBBKeyTake() {
            return (int)ESaveKey.CHILD_MATCHES;
        }

        protected override void OnNotTargetProp() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            if (!world.Blackboard.TryGetValue(GetBBKeyGive(), out int _)) {
                world.ShowMessage(CommonString.CHILD_WHISPER);
            }
        }
    }
}
