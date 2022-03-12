using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class CandleStick : Props {

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
        }

        public override void OnClick(int propID) {
            if (propID == (int)EPropID.Candle) {
                Candle.enabled = true;
                World.GetWorld<TouchPuzzeWorld>().ItemManager.RemoveItem((int)EPropID.Candle);

                _setUp = true;
                SetupCollider.enabled = true;
                UnSetupCollider.enabled = false;
            }
            if (_setUp && propID == (int)EPropID.Matches) {
                 // todo 点燃蜡烛
            }
        }
    }
}