using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class OnFireCandleStickMessage : IPool, IMessage {
        public void Reset() {
        }
    }

    public class CandleStick : Props {
        public int ID = 0;

        public SpriteRenderer Candle = null;
        public SpriteRenderer Fire = null;

        public BoxCollider2D SetupCollider = null;
        public BoxCollider2D UnSetupCollider = null;

        public ESaveKey SetUpKey = ESaveKey.SETUP_CANDLE_LEFT;
        public ESaveKey FireKey = ESaveKey.FIRE_CANDLE_LEFT;

        private bool _setUp = false;
        private bool _isFire = false;

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
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetIsSetUpKey(), out int _))
                OnSetUp();
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetIsFireKey(), out int _))
                OnFire();
        }

        private int GetIsSetUpKey() {
            return (int)SetUpKey;
        }

        private int GetIsFireKey() {
            return (int)FireKey;
        }

        private void OnSetUp() {
            Candle.enabled = true;
            _setUp = true;
            SetupCollider.enabled = true;
            UnSetupCollider.enabled = false;
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetIsSetUpKey(), 1);
        }

        private void OnFire() {
            _isFire = true;
            Fire.enabled = true;

            // 写入黑板数据
            Blackboard blackboard = World.GetWorld<TouchPuzzeWorld>().Blackboard;
            blackboard.SetValue(GetIsFireKey(), 1);
            int candleCountKey = TouchPuzzeWorld.GetCandleCountKey();
            blackboard.TryGetValue(candleCountKey, out int candleCount);
            blackboard.SetValue(candleCountKey, candleCount + 1);

            // 发送消息
            OnFireCandleStickMessage message = ObjectPool<OnFireCandleStickMessage>.Malloc();
            World.GetWorld<TouchPuzzeWorld>().MessageManager.Trigger<OnFireCandleStickMessage>(message);
            ObjectPool<OnFireCandleStickMessage>.Free(message);
        }

        public override void OnClick(int propID) {
            if (propID == (int)EPropID.Candle && !_setUp) {
                World.GetWorld<TouchPuzzeWorld>().ItemManager.RemoveItem((int)EPropID.Candle);
                OnSetUp();
            }
            if (_setUp && !_isFire && propID == (int)EPropID.Matches) {
                OnFire();
            }
        }
    }
}