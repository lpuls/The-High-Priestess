using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster.TouchPuzzle {
    [EventActionInfo("点击道具", "TouchPuzzle")]
    public class GameActionCondition_PropsOnClick : ManualCondition {

        public int TargetPropsID = 0;

        public override void SetOwnerPage(EventActionPage page) {
            base.SetOwnerPage(page);

            GameEventProps gameEventProps = page.Owner.Owner.GetComponent<GameEventProps>();
            if (null != gameEventProps) {
                gameEventProps.OnClickEvent += OnClick;
            }
        }

        private void OnClick(int propID) {
            if (TargetPropsID == propID) {
                _ownerPage.Reset();
                _ownerPage.Execute();
            }
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            base.DrawInspector();
            TargetPropsID = UnityEditor.EditorGUILayout.IntField("目标道具:", TargetPropsID);
            Descript = "当使用道具：" + TargetPropsID;
        }

#endif

    }
}
