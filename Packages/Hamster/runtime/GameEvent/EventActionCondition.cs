[System.Serializable]
public class EventActionCondition : EventActionBase, IEventActionCondition {

    public virtual bool CheckCondition() {
        return false;
    }
}

public class ManualCondition : EventActionCondition {
    public override void SetOwnerPage(EventActionPage page) {
        base.SetOwnerPage(page);
    }
}
