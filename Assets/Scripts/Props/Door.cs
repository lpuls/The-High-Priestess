using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Door : Props {
        public int ID = 0;

        public SpriteRenderer DoorForwardground = null;
        public SpriteRenderer DoorBackground = null;
        public bool IsLock = false;

        public void Awake() {
            if (null != DoorForwardground)
                DoorForwardground.enabled = true;
            if (null != DoorBackground)
                DoorBackground.enabled = true;
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetDoorKey(), out int value)) {
                IsLock = 1 == value;
            }
        }

        public int GetDoorKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Door, ID, 0);
        }
    }
}
