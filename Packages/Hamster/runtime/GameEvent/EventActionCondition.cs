[System.Serializable]
public class EventActionCondition : EventActionBase, IEventActionCondition {

    protected EventActionPage _ownerPage = null;

    public virtual void SetOwnerPage(EventActionPage page) {
        _ownerPage = page;
    } 

    public virtual bool CheckCondition() {
        return false;
    }
}

public class ManualCondition : EventActionCondition {
    public override void SetOwnerPage(EventActionPage page) {
        base.SetOwnerPage(page);
    }
}
