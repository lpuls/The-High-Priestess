using UnityEngine;
using Hamster;


public class EventActionPlayer : MonoBehaviour {
    [SerializeField]
    private EventActionInst _instance = null;

    public void Awake() {
        _instance.Owner = gameObject;
        Execute();
    }

    public void SetInstance(EventActionInst inst) {
        _instance = inst;
        _instance.Owner = gameObject;
    }

    public EventActionInst GetInstance() {
        return _instance;
    }

    public void Execute() {
        _instance.Execute();
    }
}
