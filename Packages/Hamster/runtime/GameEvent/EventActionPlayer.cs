using UnityEngine;
using Hamster;


public class EventActionPlayer : MonoBehaviour {
    [SerializeField]
    private EventActionInst _instance = null;

    public bool PlayOnAwake = false;
    public bool PlayOnStar = false;

    public void Awake() {
        if (null != _instance)
            _instance.Initialize(gameObject);
        if (PlayOnAwake) {
            Execute();
        }
    }

    public void Start() {
        if (PlayOnStar) {
            Execute();
        }
    }

    public void SetInstance(EventActionInst inst) {
        _instance = inst;
        _instance.Initialize(gameObject);
    }

    public EventActionInst GetInstance() {
        return _instance;
    }

    public void Execute() {
        if (null != _instance)
            _instance.Execute();
    }
}
