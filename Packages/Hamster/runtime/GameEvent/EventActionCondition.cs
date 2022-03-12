[System.Serializable]
public class EventActionCondition : EventActionBase, IEventActionCondition {
    public virtual bool CheckCondition() {
        return false;
    }
}
