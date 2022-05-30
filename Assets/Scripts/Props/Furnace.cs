using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class OnSandalwoodFullMessage : IPool, IMessage {
        public void Reset() {
        }
    }

    public class Furnace : Props {

        public int MaxSandalWood = 3;
        public List<Renderer> Sandalwoods = new List<Renderer>();

        private int _count = 1;

        private void Awake() {
            // 读取记录值
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetFurnaceKey(), out int value)) {
                _count = value;
            }
            UpdateSandalwood();
        }

        public override void OnClick(int propID) {
            if (EPropID.Sandalwood == (EPropID)propID) {
                _count++;
                UpdateSandalwood();

                World.GetWorld<TouchPuzzeWorld>().RemoveCurrentUsingItem();
                World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetFurnaceKey(), _count);
            }
        }

        private void UpdateSandalwood() {
            for (int i = 0; i < Sandalwoods.Count; i++) {
                Sandalwoods[i].enabled = i < _count;
            }

            if (_count >= MaxSandalWood) {
                TouchPuzzeWorld world =  World.GetWorld<TouchPuzzeWorld>();

                OnSandalwoodFullMessage message = ObjectPool<OnSandalwoodFullMessage>.Malloc();
                world.MessageManager.Trigger<OnSandalwoodFullMessage>(message);
                ObjectPool<OnSandalwoodFullMessage>.Free(message);

                // 播放怪笑
                if (!world.Blackboard.HasValue(GetWitchLaught())) {
                    world.PlaySoundEffect((int)ESoundEffectID.WitchLaught);
                    world.Blackboard.SetValue(GetWitchLaught(), 1);
                }
            }
        }

        private int GetFurnaceKey() {
            return (int)ESaveKey.FURNACE_SANDALWOOD_COUNT;
        }

        private int GetWitchLaught() {
            return (int)ESaveKey.WITCH_LAUGHT;
        }

    }
}
