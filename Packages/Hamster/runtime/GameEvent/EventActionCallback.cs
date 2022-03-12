[System.Serializable]
public class EventActionCallback : EventActionBase, IEventActionCallback {
    public virtual EEventActionResult Execute() {
        return EEventActionResult.Normal;
    }
}