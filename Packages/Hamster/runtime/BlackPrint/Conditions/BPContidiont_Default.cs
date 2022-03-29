
namespace Hamster.BP {
    [BlackPrint("默认成功", "Common")]
    public class BPContidiont_Default : BlackPrintCondition {

        public override bool CheckCondition() {
            return true;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
        }

#endif
    }
}