using UnityEngine;

namespace Hamster.BP {
    [BlackPrint("等待", "Common")]
    public class BPAction_Wait : BlackPrintAction {
        public float Wait = 0.0f;
        public float CurrentTime = 0;


        public override EBPActionResult Execute() {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime > 0)
                return EBPActionResult.Block;
            return EBPActionResult.Normal;
        }

        public override void Reset() {
            base.Reset();
            CurrentTime = Wait;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            Wait = UnityEditor.EditorGUILayout.FloatField("等待：", Wait);
            CurrentTime = Wait;
            Descript = "Wait: " + Wait;
        }

#endif
    }
}