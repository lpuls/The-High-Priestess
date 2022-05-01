namespace Hamster.TouchPuzzle {
    public class Knife : Props {
        public override void OnEnterField() {
            bool needShow = false;
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKnifeKey(), out int isGive)) {
                needShow = true;
            }
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKillKey(), out int isTake)) {
                needShow = false;
            }

            gameObject.SetActive(needShow);
        }

        private int GetManHasKillKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKill, 0, 0);
        }

        private int GetManHasKnifeKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKnife, 0, 0);
        }
    }
}
