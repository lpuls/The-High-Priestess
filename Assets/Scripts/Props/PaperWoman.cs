namespace Hamster.TouchPuzzle {
    public class PaperWoman : PaperHuman {

        private bool _setup = false;

        protected override void Awake() {
            // 读取记录值
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetParperWomanKey(), out int value)) {
                _setup = true;
            }
        }

        public override void OnClick(int propID) {
            if (EPropID.Necklace == (EPropID)propID && !_setup) {
                // ... 
            }
        }

        private int GetParperWomanKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanNecklace, 0, 0);
        }

    }
}
