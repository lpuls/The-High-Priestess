namespace Hamster.TouchPuzzle {
    public class GoToFieldTrigger : Props {

        public EFieldID GotoFieldID = EFieldID.None;

        public override void OnClick(int propID) {
            World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.Execute(BeginGoTo);
        }

        public void BeginGoTo() {
            World.GetWorld<TouchPuzzeWorld>().FieldManager.GoTo((int)GotoFieldID);
            World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.SetComplete();
        }
    }
}
