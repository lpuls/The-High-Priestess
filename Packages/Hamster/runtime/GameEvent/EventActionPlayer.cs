using UnityEngine;
using Hamster;


public class EventActionPlayer : MonoBehaviour {
    [SerializeField]
    private EventActionInst _instance = null;

    public bool PlayOnAwake = false;
    public bool PlayOnStar = false;

    public void Awake() {
        _instance.Owner = gameObject;
        _instance.Initialize();
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
        _instance.Owner = gameObject;
    }

    public EventActionInst GetInstance() {
        return _instance;
    }

    public void Execute() {
        _instance.Execute();
    }
}
