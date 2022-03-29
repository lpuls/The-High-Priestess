using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster.TouchPuzzle {
    [BlackPrint("点击道具", "TouchPuzzle")]
    public class BPCondition_PropsOnClick : BlackPrintCondition {

        public int TargetPropsID = 0;
        public bool BindClickEvent = false;
        public bool BindClickDownEvent = false;
        public bool BindClickUpEvent = false;

        public override void SetOwnerPage(BlackPrintPage page) {
            base.SetOwnerPage(page);

            GameEventProps gameEventProps = page.Owner.Owner.GetComponent<GameEventProps>();
            if (null != gameEventProps) {
                if (BindClickEvent)
                    gameEventProps.OnClickEvent += OnClick;
                else if (BindClickDownEvent)
                    gameEventProps.OnClickDownEvent += OnClick;
                else if (BindClickUpEvent)
                    gameEventProps.OnClickUpEvent += OnClick;
            }
        }

        private void OnClick(int propID) {
            if (TargetPropsID == propID) {
                OwnerPage.Reset();
                OwnerPage.Execute();
            }
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            base.DrawInspector();
            TargetPropsID = UnityEditor.EditorGUILayout.IntField("目标道具:", TargetPropsID);
            Descript = "当使用道具：" + TargetPropsID;
            BindClickEvent = UnityEditor.EditorGUILayout.Toggle("点击事件:", BindClickEvent);
            BindClickDownEvent = UnityEditor.EditorGUILayout.Toggle("按下事件:", BindClickDownEvent);
            BindClickUpEvent = UnityEditor.EditorGUILayout.Toggle("抬起事件:", BindClickUpEvent);
        }
#endif

    }
}
