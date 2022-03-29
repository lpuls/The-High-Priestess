using UnityEngine;

namespace Hamster.BP {
    [BlackPrint("打印", "Common")]
    public class BPAction_DebugInfo : BlackPrintAction {
        public string Log = string.Empty;

        public override EBPActionResult Execute() {
            Debug.Log(Log);

            return EBPActionResult.Normal;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            Log = UnityEditor.EditorGUILayout.TextField("日志信息:", Log);
            Descript = Log;
        }

#endif
    }
}