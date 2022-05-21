using Hamster;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.GameEventConditions {
    [Hamster.BP.BlackPrint("黑板是否有值", "Blackboard")]
    public class BPCondition_CheckBBHasValue : Hamster.BP.BlackPrintCondition {
        public int BBKey = 0;

        public override bool CheckCondition() {

            Blackboard blackboard = World.GetWorld().GetManager<Blackboard>();
            if (null == blackboard)
                return false;

            return blackboard.TryGetValue(BBKey, out int _);
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            BBKey = EditorGUILayout.IntField("黑板Key", BBKey);
        }
#endif
    }
}