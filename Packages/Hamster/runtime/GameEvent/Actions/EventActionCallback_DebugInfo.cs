using UnityEngine;

namespace Hamster.GameEvent {
    [EventActionInfo("打印", "Common")]
    public class EventActionCallback_DebugInfo : EventActionCallback {
        public string Log = string.Empty;

        public override EEventActionResult Execute() {
            Debug.Log(Log);

            return EEventActionResult.Normal;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            Log = UnityEditor.EditorGUILayout.TextField("日志信息:", Log);
            Descript = Log;
        }

#endif
    }
}