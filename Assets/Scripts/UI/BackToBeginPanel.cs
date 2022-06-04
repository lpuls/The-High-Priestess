using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class BackToBeginPanel : MonoBehaviour {

        public Animator Animator = null;

        public void Show() {
            gameObject.SetActive(true);
            Animator.Play("Show");
            World.GetWorld<TouchPuzzeWorld>().PauseGame(true);
        }

        public void OnClickYes() {
            gameObject.SetActive(false);
            World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.Execute(BeginGoTo);
        }

        public void OnClickNo() {
            gameObject.SetActive(false);
        }

        private void BeginGoTo() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            world.ResetFields();
            world.ShowBeginGamePanel();
            World.GetWorld<TouchPuzzeWorld>().PauseGame(false);
        }

    }
}
