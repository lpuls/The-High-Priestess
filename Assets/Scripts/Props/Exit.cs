namespace Hamster.TouchPuzzle {
    public class Exit : Props {
        public override void OnClick(int propID) {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            if (world.Blackboard.TryGetValue((int)ESaveKey.EXIT_DOOR_OPEN_LEFT, out int _)
                && world.Blackboard.TryGetValue((int)ESaveKey.EXIT_DOOR_OPEN_RIGHT, out int _)) {
                // 游戏结束
                world.Blackboard.SetValue((int)ESaveKey.PASS_GAME, 1);

                // 显示结束UI并回到开始界面
                World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.Execute(BeginGoTo);
            }
            else {
                world.ShowMessage(CommonString.LOCK_EXIT_DOOR);
            }
        }

        private void BeginGoTo() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            world.ShowEndText();

            // world.BeginGamePanel.gameObject.SetActive(true);
            // world.TransitionsPanel.SetComplete();
        }
    }
}