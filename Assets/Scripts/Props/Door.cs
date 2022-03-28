using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class Door : Props {
        public int ID = 0;

        public AudioSource AudioPlayer = null;
        public SpriteRenderer DoorForwardground = null;
        public SpriteRenderer DoorBackground = null;
        public bool IsLock = false;

        private bool IsOpenDoor = false;


        public void Awake() {
            if (null != DoorForwardground)
                DoorForwardground.enabled = true;
            if (null != DoorBackground)
                DoorBackground.enabled = false;
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetDoorKey(), out int value)) {
                IsLock = 1 == value;
            }

            World.GetWorld<TouchPuzzeWorld>().MessageManager.Bind<OnFireCandleStickMessage>(OnReceiveFireCandleStickMessage);
        }

        public override void OnClick(int propID) {
            if (!IsLock) {
                IsOpenDoor = !IsOpenDoor;
                DoorForwardground.enabled = !IsOpenDoor;
                DoorBackground.enabled = IsOpenDoor;
            }
            else {
                ShowMessageBoxMessage message = ObjectPool<ShowMessageBoxMessage>.Malloc();
                message.Message = CommonString.LOCK_DOOR;
                World.GetWorld<TouchPuzzeWorld>().MessageManager.Trigger(message);
                ObjectPool<ShowMessageBoxMessage>.Free(message);
            }
        }

        public int GetDoorKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Door, ID, 0);
        }

        private void OnReceiveFireCandleStickMessage(OnFireCandleStickMessage message) {
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(TouchPuzzeWorld.GetCandleCountKey(), out int value)) {
                if (value > ID) {
                    IsLock = false;
                    if (null != AudioPlayer)
                        AudioPlayer.Play();
                } 
            }
        }
    }
}
