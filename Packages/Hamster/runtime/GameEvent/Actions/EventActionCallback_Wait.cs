using UnityEngine;

namespace Hamster.GameEvent {
    [EventActionInfo("等待", "Common")]
    public class EventActionCallback_Wait : EventActionCallback {
        public float Wait = 0.0f;
        public float CurrentTime = 0;


        public override EEventActionResult Execute() {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime > 0)
                return EEventActionResult.Block;
            return EEventActionResult.Normal;
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