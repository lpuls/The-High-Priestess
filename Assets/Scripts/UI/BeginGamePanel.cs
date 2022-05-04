using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Hamster.TouchPuzzle {
    public class BeginGamePanel : MonoBehaviour {

        public Text Title = null;
        public Button NewGame = null;
        public Button OldGame = null;

        public void Start() {
            Blackboard bb = World.GetWorld().GetManager<Blackboard>();
            if (bb.TryGetValue((int)ESaveKey.PASS_GAME, out int _)) {
                Title.color = Color.red;
            }

            bool exitSave = bb.TryGetValue((int)ESaveKey.EXIT_SAVE, out int _);
            OldGame.gameObject.SetActive(exitSave);
            OldGame.onClick.AddListener(OnClickOldGame);

            NewGame.onClick.AddListener(OnClickNewGame);
        }

        private void OnClickNewGame() {
            World.GetWorld().GetManager<SaveHelper>().Delete();
            Blackboard bb = World.GetWorld().GetManager<Blackboard>();
            bb.Clean();
            bb.SetValue((int)ESaveKey.EXIT_SAVE, 1);
            World.GetWorld().GetManager<SaveHelper>().Save();

            OnClickOldGame();
        }

        private void OnClickOldGame() {
            World.GetWorld<TouchPuzzeWorld>().EnterGame();
        }

    }
}