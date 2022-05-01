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
        }

        public override void OnEnterField() {
            base.OnEnterField();

            // 女人已经被杀死了，不显示杀人前的男人
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetHasKillKey(), out int _)) {
                gameObject.SetActive(false);
            }
        }

        private int GetHasKnifeKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKnife, 0, 0);
        }

        private int GetHasKillKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.ManKill, 0, 0);
        }

        private int GetWomanHasNecklace() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.WomanNecklace, 0, 0);
        }
    }
}
