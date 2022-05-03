namespace Hamster.TouchPuzzle {
    public class Knife : Props {
        public override void OnEnterField() {
            bool needShow = false;

            // 男人有刀且女人有项链
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKnifeKey(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasWomanSandalwood(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasChildMatchesKey(), out int _)) {
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

            // 离开时，检查女人的香是否被拿，小孩的火柴是否被拿，男人是否拥有刀
            // 如果女人的香被拿，小孩的火柴被拿，男人有刀，而还未杀人，则标记女人被杀死

            // 女人已经被杀死，不进行任务处理
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKillKey(), out int _)) {
                return;
            }

            // 男人有刀且女人有项链，在离开场景时标记男人杀人了
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasWomanSandalwood(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKnifeKey(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasChildMatchesKey(), out int _)) {
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetManHasKillKey(), 1);
            }
        }

        private int GetManHasKillKey() {
            return (int)ESaveKey.MAN_KILL;
        }

        private int GetManHasKnifeKey() {
            return (int)ESaveKey.MAN_KNIFE;
        }

        private int GetHasChildMatchesKey() {
            return (int)ESaveKey.CHILD_MATCHES;
        }

        private int GetHasWomanSandalwood() {
            return (int)ESaveKey.WOMAN_SANDALWOOD;
        }
    }
}
