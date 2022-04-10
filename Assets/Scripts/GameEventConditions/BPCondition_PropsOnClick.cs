using Hamster.BP;

namespace Hamster.TouchPuzzle {
    [BlackPrint("点击道具", "TouchPuzzle")]
    public class BPCondition_PropsOnClick : BlackPrintCondition {

        [BPProperty("目标道具")] public int TargetPropsID = 0;
        [BPProperty("点击事件")] public bool BindClickEvent = false;
        [BPProperty("按下事件")] public bool BindClickDownEvent = false;
        [BPProperty("抬起事件")] public bool BindClickUpEvent = false;

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
            InitializeAttribute(GetType());
            base.DrawInspector();
        //    //TargetPropsID = UnityEditor.EditorGUILayout.IntField("目标道具:", TargetPropsID);
        //    //Descript = "当使用道具：" + TargetPropsID;
        //    //BindClickEvent = UnityEditor.EditorGUILayout.Toggle("点击事件:", BindClickEvent);
        //    //BindClickDownEvent = UnityEditor.EditorGUILayout.Toggle("按下事件:", BindClickDownEvent);
        //    //BindClickUpEvent = UnityEditor.EditorGUILayout.Toggle("抬起事件:", BindClickUpEvent);
        }
#endif

    }
}
