namespace Hamster.TouchPuzzle {
    public class Knife : Props {
        public override void OnEnterField() {
            bool needShow = false;

            // 男人有刀且女人有项链
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKnifeKey(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetWomanHasNecklace(), out int _)) {
                needShow = true;
            }

            // 杀死过人，刀就不显示了
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKillKey(), out int _)) {
                needShow = false;
            }

            gameObject.SetActive(needShow);
        }

        public override void OnLeaveField() {
            base.OnLeaveField();

            // 离开时，检查女人是否拥有项链，男人是否拥有刀
            // 如果女人有项链，男人有刀，而还未杀人，则标记女人被杀死

            // 女人已经被杀死，不进行任务处理
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKillKey(), out int _)) {
                return;
            }

            // 男人有刀且女人有项链，在离开场景时标记男人杀人了
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetWomanHasNecklace(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKnifeKey(), out int _)) {
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetManHasKillKey(), 1);
            }
        }

        private int GetManHasKillKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKill, 0, 0);
        }

        private int GetManHasKnifeKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKnife, 0, 0);
        }

        private int GetWomanHasNecklace() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanNecklace, 0, 0);
        }
    }
}
