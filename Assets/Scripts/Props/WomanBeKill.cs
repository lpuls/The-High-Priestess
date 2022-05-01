namespace Hamster.TouchPuzzle {
    public class WomanBeKill : Props {
        public override void OnEnterField() {
            base.OnEnterField();

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetManHasKillKey(), out int _)
                && World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetWomanHasNecklace(), out int _)) {
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetPlayerLeavePaperManKey(), 1);
            }
        }

        private int GetManHasKillKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKnife, 0, 0);
        }
        private int GetWomanHasNecklace() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanNecklace, 0, 0);
        }

        private int GetPlayerLeavePaperManKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.PlayerLeaveRight, 0, 0);
        }
    }
}
