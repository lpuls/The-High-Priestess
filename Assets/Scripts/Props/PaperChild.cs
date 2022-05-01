namespace Hamster.TouchPuzzle {
    public class PaperChild : PaperHuman {

        protected override int GetBBKeyGive() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ChildFood, 0, 0);
        }

        protected override int GetBBKeyTake() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ChildMatches, 0, 0);
        }
    }
}
