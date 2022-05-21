using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    [System.Serializable]
    public class PuppetPartTransform {
        public float Parent;
        public float Child;
    }

#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class PuppetPart : Props {
        public int PoseIndex = 0;
        
        public LineRenderer Line = null;
        public Transform LineEndPos = null;
        public Transform LineStartPos = null;

        public Transform Child = null;
        public List<PuppetPartTransform> PoseTransform = new List<PuppetPartTransform>(5);
        public event Action OnChangePose;

        public override void Init(IField field) {
            base.Init(field);

            SetPose(PoseIndex);
        }

        public override void OnClick(int propID) {
            base.OnClick(propID);

            PoseIndex++;
            PoseIndex %= PoseTransform.Count;

            SetPose(PoseIndex);
        }

        public void SetPose(int index) {
            if (index < 0 || index >= PoseTransform.Count)
                throw new System.Exception("Index out of Transform Count");

            PuppetPartTransform puppetPartTransform = PoseTransform[index];
            transform.rotation = Quaternion.AngleAxis(puppetPartTransform.Parent, Vector3.forward);
            Child.rotation = Quaternion.AngleAxis(puppetPartTransform.Child, Vector3.forward);

            if (null != Line) {
                Line.SetPosition(0, LineEndPos.position);
                Line.SetPosition(1, LineStartPos.position);
            }

            OnChangePose?.Invoke();
        }

#if UNITY_EDITOR
        public bool RecordPose = false;
        public bool TrySetPose = false;
        
        public void Update() {
            if (RecordPose) {
                PuppetPartTransform puppetPartTransform = new PuppetPartTransform {
                    Parent = transform.rotation.eulerAngles.z,
                    Child = Child.rotation.eulerAngles.z
                };
                PoseTransform.Add(puppetPartTransform);
                RecordPose = false;
            }
            if (TrySetPose) {
                SetPose(PoseIndex);
                TrySetPose = false;
            }
        }

        public void CleanPose() {
            PoseTransform.Clear();
        }
        public void RecordCurrentPose() {
            RecordPose = true;
        }
#endif

    }
}