namespace Hamster.TouchPuzzle {
    public class PaperChild : PaperHuman {

        protected override int GetBBKeyGive() {
            return (int)ESaveKey.CHILD_FOOD;
        }

        protected override int GetBBKeyTake() {
            return (int)ESaveKey.CHILD_MATCHES;
        }
    }
}
