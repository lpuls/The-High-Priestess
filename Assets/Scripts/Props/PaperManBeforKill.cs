using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class PaperManBeforKill : Props {

        private bool _hasKnife = false;
        private Animator _animator = null;

        public void Awake() {
            _animator = GetComponent<Animator>();

            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasKnifeKey(), out int _)) {
                _hasKnife = true;
            }

        }

        public override void OnClick(int propID) {
            if (EPropID.Knife == (EPropID)propID && !_hasKnife) {
                _hasKnife = true;
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetHasKnifeKey(), 1);
                World.GetWorld<TouchPuzzeWorld>().RemoveCurrentUsingItem();

                _animator.Play("RightHandUp");
            }
            else {
                OnNotTargetProp();
            }
        }

        public override void OnEnterField() {
            base.OnEnterField();

            // 女人已经被杀死了，不显示杀人前的男人
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasKillKey(), out int _)) {
                gameObject.SetActive(false);
            }
        }

        private int GetHasKnifeKey() {
            return (int)ESaveKey.MAN_KNIFE;  // TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKnife, 0, 0);
        }

        private int GetHasKillKey() {
            return (int)ESaveKey.MAN_KILL;  // TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKill, 0, 0);
        }

        private int GetWomanHasNecklace() {
            return (int)ESaveKey.WOMAN_NECKLACE;  // TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanNecklace, 0, 0);
        }

        protected void OnNotTargetProp() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            if (!world.Blackboard.TryGetValue(GetWomanHasNecklace(), out int _)) {
                world.ShowMessage(CommonString.MAN_WHISPER);
            }
            else if (!world.Blackboard.TryGetValue(GetHasKnifeKey(), out int _)) {
                world.ShowMessage(CommonString.MAN_WHISPER_KILL);
            }
        }
    }
}
