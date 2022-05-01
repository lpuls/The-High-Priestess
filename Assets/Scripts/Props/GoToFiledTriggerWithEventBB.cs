namespace Hamster.TouchPuzzle {
    public class GoToFiledTriggerWithEventBB : GoToFieldTrigger {
        public EEventKey EventKey = EEventKey.None;
        public int ID = 0;
        public int Value = 0;
        public bool ShowOnHasValue = true;

        public override void Init(IField field) {
            base.Init(field);

            CheckShow();
        }

        public override void OnEnterField() {
            base.OnEnterField();

            CheckShow();
        }

        protected int GetKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EventKey, ID, Value);
        }

        protected void CheckShow() {
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetKey(), out int _)) {
                if (ShowOnHasValue) {
                    gameObject.SetActive(true);
                }
                else {
                    gameObject.SetActive(false);
                }
            }
            else {
                if (!ShowOnHasValue) {
                    gameObject.SetActive(true);
                }
                else {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
