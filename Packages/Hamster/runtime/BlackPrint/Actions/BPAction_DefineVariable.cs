#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hamster.GameEvent {

    public class BPAction_DefineVariable : BlackPrintAction {
        public enum EDefineVariableType {
            None,
            Integer,
            Float,
            Bool,
            String
        }


        private static string[] TypeNames = new string[] { "int", "float", "bool", "string" };

        private EDefineVariableType _index = EDefineVariableType.None;
        private string _name = string.Empty;
        private string _value = string.Empty;

        public override EBPActionResult Execute() {
            switch (_index) {
                case EDefineVariableType.None:
                    UnityEngine.Debug.LogError("Invalid Type");
                    break;
                case EDefineVariableType.Integer:
                    break;
                case EDefineVariableType.Float:
                    break;
                case EDefineVariableType.Bool:
                    break;
                case EDefineVariableType.String:
                    break;
                default:
                    UnityEngine.Debug.LogError("Invalid Type");
                    break;
            }

            return EBPActionResult.Normal;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            EditorGUILayout.BeginVertical();
            _index = (EDefineVariableType)EditorGUILayout.Popup("类型", (int)_index, TypeNames);
            _name = EditorGUILayout.TextField("变量名", _name);
            _value = EditorGUILayout.TextField("值", _value);
            EditorGUILayout.EndVertical();
        }

#endif
    }
}
