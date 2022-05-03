namespace Hamster.TouchPuzzle {
    public class GoToFiledTriggerWithEventBB : GoToFieldTrigger {
        public ESaveKey EventKey = ESaveKey.None;
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
            return (int)EventKey;
            // return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EventKey, ID, Value);
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
