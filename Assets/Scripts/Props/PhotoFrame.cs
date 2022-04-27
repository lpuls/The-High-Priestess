using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class OnPhotosAllFindMessage : IPool, IMessage {
        public void Reset() {
        }
    }

    public class PhotoFrame : Props {
        public List<int> PhotoIDs = new List<int>();
        public Renderer ResultRenderer = null;
        public GameObject ShowOnAllFind = null;

        public List<SpriteRenderer> Photos = new List<SpriteRenderer>();
        private List<bool> PhotoReady = new List<bool>();

        private void Awake() {
            bool result = true;
            Blackboard Blackboard = World.GetWorld<TouchPuzzeWorld>().Blackboard;
            for (int i = 0; i < PhotoIDs.Count; i++) {
                if (Blackboard.TryGetValue(GetPhotoReady(i), out int value))
                    PhotoReady.Add(true);
                else
                    PhotoReady.Add(false);

                Photos[i].enabled = PhotoReady[i];
                result &= PhotoReady[i];

                if (null != ShowOnAllFind)
                    ShowOnAllFind.SetActive(false);
                ResultRenderer.enabled = false;
            }

            if (result)
                SendAllFindMessage();
        }

        public override void OnEnterField() {
            base.OnEnterField();

            Blackboard Blackboard = World.GetWorld<TouchPuzzeWorld>().Blackboard;
            for (int i = 0; i < PhotoIDs.Count; i++) {
                if (Blackboard.TryGetValue(GetPhotoReady(i), out int value))
                    Photos[i].enabled = true;
                else
                    Photos[i].enabled = false;
            }
        }

        private int GetPhotoReady(int id) {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.PhotoFrame, id, 0);
        }

        public override void OnClick(int propID) {
            for (int i = 0; i < PhotoIDs.Count; i++) {
                if (!PhotoReady[i] && PhotoIDs[i] == propID) {
                    PhotoReady[i] = true;
                    Photos[i].enabled = true;

                    World.GetWorld<TouchPuzzeWorld>().ItemManager.RemoveItem(PhotoIDs[i]);

                    // 写入数据
                    World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetPhotoReady(i), 1);
                }
            }

            bool result = true;
            for (int i = 0; i < PhotoReady.Count; i++) {
                result &= PhotoReady[i];
            }
            if (result) {
                // 显示目标结果，隐藏碎片
                if (null != ShowOnAllFind)
                    ShowOnAllFind.SetActive(true);
                ResultRenderer.enabled = true;
                for (int i = 0; i < Photos.Count; i++) {
                    Photos[i].enabled = false;
                }

                // 发送消息
                SendAllFindMessage();
            }
        }

        private void SendAllFindMessage() {
            OnPhotosAllFindMessage message = ObjectPool<OnPhotosAllFindMessage>.Malloc();
            World.GetWorld<TouchPuzzeWorld>().MessageManager.Trigger<OnPhotosAllFindMessage>(message);
            ObjectPool<OnPhotosAllFindMessage>.Free(message);
        }
    }
}
