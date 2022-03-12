
namespace Hamster.GameEvent {
    [EventActionInfo("默认成功", "Common")]
    public class EventActionContidiont_Default : EventActionCondition {

        public override bool CheckCondition() {
            return true;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
        }

#endif
    }
}