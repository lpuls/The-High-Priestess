using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class CandleStick : Props {
        public int ID = 0;

        public SpriteRenderer Candle = null;
        public SpriteRenderer Fire = null;

        public BoxCollider2D SetupCollider = null;
        public BoxCollider2D UnSetupCollider = null;

        private bool _setUp = false;

        private void Awake() {
            if (null != Candle)
                Candle.enabled = false;
            if (null != Fire)
                Fire.enabled = false;
            if (null != SetupCollider)
                SetupCollider.enabled = false;
            if (null != UnSetupCollider)
                UnSetupCollider.enabled = true;

            // 读取记录值
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetIsSetUpKey(), out int isSetUpValue))
                OnSetUp();
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetIsFireKey(), out int IsFireValue))
                OnFire();
        }

        private int GetIsSetUpKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.CandleStick, ID, 0);
        }

        private int GetIsFireKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.CandleStick, ID, 1);
        }

        private void OnSetUp() {
            Candle.enabled = true;
            _setUp = true;
            SetupCollider.enabled = true;
            UnSetupCollider.enabled = false;
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetIsSetUpKey(), 1);
        }

        private void OnFire() {
            Fire.enabled = true;
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetIsFireKey(), 1);
        }

        public override void OnClick(int propID) {
            if (propID == (int)EPropID.Candle) {
                World.GetWorld<TouchPuzzeWorld>().ItemManager.RemoveItem((int)EPropID.Candle);
                OnSetUp();
            }
            if (_setUp && propID == (int)EPropID.Matches) {
                OnFire();
            }
        }
    }
}