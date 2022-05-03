namespace Hamster.TouchPuzzle {
    public class PaperMan : PaperHuman {

        public override void OnEnterField() {
            base.OnEnterField();

            bool show = false;
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetWomanDeadKey(), out int _)) {
                show = true;
            }
            gameObject.SetActive(show);
        }

        protected override int GetBBKeyGive() {
            return (int)ESaveKey.MAN_MONEY;  // TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManMoney, 0, 0);
        }

        protected override int GetBBKeyTake() {
            return (int)ESaveKey.MAN_SANDALWOOD;  //TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManSandalwood, 0, 0);
        }

        private int GetWomanDeadKey() {
            return (int)ESaveKey.MAN_KILL;  //TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKill, 0, 0);
        }
    }
}
