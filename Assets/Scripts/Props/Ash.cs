using System.Collections;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Ash : Props {
        public int ID = 0;
        public Vector3 TargetLocation = Vector3.zero;
        public float MoveTime = 1.0f;

        private float _time = 0;
        private Vector3 _origin = Vector3.zero;
        private bool _isMoving = false;
        private bool _isMoveComplete = false;

        public void Awake() {
            _origin = transform.position;

            // 读取记录值
            if (World.GetWorld<TouchPuzzeWorld>().Blackboard.TryGetValue(GetAshKey(), out int result)) {
                _isMoving = false;
                _isMoveComplete = true;
                transform.position = TargetLocation;
            }
        }

        public void Update() {
            if (_isMoving) {
                _time += Time.deltaTime;
                transform.position = Vector3.Lerp(_origin, TargetLocation, _time / MoveTime);
                if (_time >= MoveTime) {
                    _isMoving = false;
                    _isMoveComplete = true;

                    World.GetWorld<TouchPuzzeWorld>().Blackboard.SetValue(GetAshKey(), 1);
                }
            }
        }

        public override void OnClick(int propID) {
            if (!_isMoveComplete) {
                _isMoving = true;
            }
        }

        private int GetAshKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Prop, (int)EPropID.Ash, ID, 0);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            Gizmos.DrawSphere(TargetLocation, .3f);

        }
#endif
    }
}