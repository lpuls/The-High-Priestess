namespace Hamster.TouchPuzzle {
    public class PaperMan : PaperHuman {

        private bool _womanDead = false;

        protected override void OnAwake() {
            base.OnAwake();

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetWomanDeadKey(), out int _)) {
                _womanDead = true;
            }
        }

        public override void OnEnterField() {
            base.OnEnterField();

            gameObject.SetActive(_womanDead);
        }

        protected override int GetBBKeyGive() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManMoney, 0, 0);
        }

        protected override int GetBBKeyTake() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManSandalwood, 0, 0);
        }

        private int GetWomanDeadKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKill, 0, 0);
        }
    }
}
