namespace Hamster.TouchPuzzle {
    public class TestProps : Props {

        public int GoToField = 0;

        public override void OnClickDown(int propID) {
        }

        public override void OnClickUp(int propID) {
        }

        public override void OnClick(int propID) {
            World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.Execute(BeginGoTo);
        }

        public void BeginGoTo() {
            World.GetWorld<TouchPuzzeWorld>().FieldManager.GoTo(GoToField);
            World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.SetComplete();
        }
    }
}
