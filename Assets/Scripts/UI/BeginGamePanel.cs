using UnityEngine;
using UnityEngine.UI;

namespace Hamster.TouchPuzzle {
    public class BeginGamePanel : MonoBehaviour {

        public Text Title = null;
        public Button NewGame = null;
        public Button OldGame = null;

        public void Start() {
            UpdateTitleColor();

            Blackboard bb = World.GetWorld().GetManager<Blackboard>();
            bool exitSave = bb.TryGetValue((int)ESaveKey.EXIT_SAVE, out int _);
            OldGame.gameObject.SetActive(exitSave);
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
            World.GetWorld<TouchPuzzeWorld>().EnterGame();
        }

    }
}