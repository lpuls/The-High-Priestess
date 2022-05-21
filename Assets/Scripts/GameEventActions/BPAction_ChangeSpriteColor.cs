using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hamster.TouchPuzzle {
    [Hamster.BP.BlackPrint("修改Sprite颜色", "Common")]
    public class BPAction_ChangeSpriteColor :  Hamster.BP.BlackPrintConditionAction {
        public Color TargetColor = Color.white;

        public override Hamster.BP.EBPActionResult Execute() {
            GameObject owenrObject = GetOwnerObject();
            if (null == owenrObject) {
                throw new System.Exception("无效的GameObject");
            }

            SpriteRenderer spriteRenderer = owenrObject.GetComponent<SpriteRenderer>();
            if (null == spriteRenderer) {
                throw new System.Exception("无效的Sprite");
            }

            spriteRenderer.color = TargetColor;

            return BP.EBPActionResult.Normal;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            base.DrawInspector();
            TargetColor = UnityEditor.EditorGUILayout.ColorField("目标颜色:", TargetColor);
            Descript = "Spritte Color: " + TargetColor.ToString();
        }
#endif
    }
}