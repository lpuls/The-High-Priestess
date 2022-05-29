using System;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class ItemPicker : Props {

        public ESaveKey SaveKey = ESaveKey.None;
        public EPropID ItemID = EPropID.None;

        public event Action<ItemPicker> OnPick; 

        public void Awake() {
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.HasValue(GetItemKey())) {
                Destroy(gameObject);
            }
        }

        public override void OnClick(int propID) {
            World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetItemKey(), 1);
            World.GetWorld<TouchPuzzeWorld>().ItemManager.AddItem((int)ItemID);
            OnPick?.Invoke(this);
            Destroy(gameObject);

            // 播放音效
            World.GetWorld<TouchPuzzeWorld>().PlaySoundEffect((int)ESoundEffectID.PickItem);
        }

        private int GetItemKey() {
            return (int)SaveKey;
        }
    }
}
