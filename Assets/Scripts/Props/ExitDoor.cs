using System;
using System.Collections;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class ExitDoor : Props {

        public int ID = 0;
        public ESaveKey ExitDoorKey = ESaveKey.None;
        public GameObject Bolt = null;

        public override void Init(IField field) {
            base.Init(field);

            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            if (world.Blackboard.TryGetValue(TouchPuzzeWorld.GetCandleCountKey(), out int value) && value > ID) {
                if (null != Bolt)
                    Bolt.SetActive(false);
            }

           world.MessageManager.Bind<OnFireCandleStickMessage>(OnReceiveFireCandleStickMessage);
        }

        private void OnReceiveFireCandleStickMessage(OnFireCandleStickMessage obj) {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            if (world.Blackboard.TryGetValue(TouchPuzzeWorld.GetCandleCountKey(), out int value)) {
                if (value > ID) {
                    World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue((int)ExitDoorKey, 1);
                    if (null != Bolt)
                        Bolt.SetActive(false);
                    world.PlaySoundEffect((int)ESoundEffectID.OpenExitDoor);
                }
            }
        }
    }
}