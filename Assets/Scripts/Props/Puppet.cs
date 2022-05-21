using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class OnSetUpPuppetPoseSuccessMessage : IPool, IMessage {
        public void Reset() {
        }
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class Puppet : Props {

        public ESaveKey SaveKey = ESaveKey.None;
        public int TargetPose = -1;

        private bool _isSetUp = false;
        private PuppetPart[] _parts = null;

        public override void Init(IField field) {
            base.Init(field);

            _parts = GetComponentsInChildren<PuppetPart>();
            for (int i = 0; i < _parts.Length; i++) {
                _parts[i].OnChangePose += OnChangePose;
            }

            if (World.GetWorld().GetManager<Blackboard>().HasValue((int)SaveKey)) {
                _isSetUp = true;
            }
        }

        private void OnChangePose() {
            if (null == _parts)
                return;

            bool isRight = true;
            for (int i = 0; i < _parts.Length; i++) {
                if (TargetPose != _parts[i].PoseIndex) {
                    isRight = false;
                    break;
                }
            }

            if (isRight) {
                World.GetWorld().GetManager<Blackboard>().SetValue((int)SaveKey, 1);
                _isSetUp = true;
            }

        }

#if UNITY_EDITOR
        public bool ResetPose = false;
        public bool CleanPose = false;
        public bool RecordPose = false;

        public void Update() {
            if (CleanPose) {
                PuppetPart[] parts = GetComponentsInChildren<PuppetPart>();
                for (int i = 0; i < parts.Length; i++) {
                    parts[i].CleanPose();
                }
                CleanPose = false;
            }
            if (RecordPose) {
                PuppetPart[] parts = GetComponentsInChildren<PuppetPart>();
                for (int i = 0; i < parts.Length; i++) {
                    parts[i].RecordCurrentPose();
                }
                RecordPose = false;
            }
            if (ResetPose) {
                PuppetPart[] parts = GetComponentsInChildren<PuppetPart>();
                for (int i = 0; i < parts.Length; i++) {
                    parts[i].SetPose(0);
                }
                ResetPose = false;
            }

        }
#endif

    }
}