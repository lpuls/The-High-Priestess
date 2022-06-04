using UnityEngine;
using UnityEngine.UI;

namespace Hamster.TouchPuzzle {
    public class BeginGamePanel : MonoBehaviour {

        public Text Title = null;
        public Button NewGame = null;
        public Button OldGame = null;
        public GameObject ProductionPersonnelList = null;

        public void Start() {
            UpdateTitleColor();

            Blackboard bb = World.GetWorld().GetManager<Blackboard>();
            
            OldGame.onClick.AddListener(OnClickOldGame);

            NewGame.onClick.AddListener(OnClickNewGame);
        }

        public void OnEnable() {
            UpdateTitleColor();
        }

        private void UpdateTitleColor() {
            Blackboard bb = World.GetWorld().GetManager<Blackboard>();
            if (bb.TryGetValue((int)ESaveKey.PASS_GAME, out int _)) {
                Title.color = Color.red;
            }

            // 无存档或当前存档已经通关则不显示继续游戏
            bool exitSave = bb.TryGetValue((int)ESaveKey.EXIT_SAVE, out int _);
            bool gameWin = bb.TryGetValue((int)ESaveKey.GAME_WIN, out int _);
            OldGame.gameObject.SetActive(exitSave && !gameWin);
        }

        private void OnClickNewGame() {
            SaveHelper saver = World.GetWorld().GetManager<SaveHelper>();

            Blackboard bb = World.GetWorld().GetManager<Blackboard>();
            bool passedGame = bb.HasValue((int)ESaveKey.PASS_GAME);
            
            saver.Delete();
            saver.Reset();

            bb.SetValue((int)ESaveKey.EXIT_SAVE, 1);
            if (passedGame) {
                bb.SetValue((int)ESaveKey.PASS_GAME, 1);
            }

            OnClickOldGame();
        }

        private void OnClickOldGame() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            world.EnterGame();
        }

        public void ShowProductionPersonnelList() {
            ProductionPersonnelList.gameObject.SetActive(true);
        }

        public void HideProductionPersonnelList() {
            ProductionPersonnelList.gameObject.SetActive(false);
        }

    }
}