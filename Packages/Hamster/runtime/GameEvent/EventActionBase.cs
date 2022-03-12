using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEventActionCallback {
    EEventActionResult Execute();
}

public interface IEventActionCondition {
    bool CheckCondition();
}

#if UNITY_EDITOR
public interface IEventActionDrawInspector {
    void DrawInspector();
}
#endif

#if UNITY_EDITOR
public class EventActionBase : ScriptableObject, IEventActionDrawInspector {
#else
public class EventActionBase : ScriptableObject {
#endif

    public virtual void Reset() {
#if UNITY_EDITOR
        Descript = string.Empty;
#endif
    }

#if UNITY_EDITOR
    public string Descript = string.Empty;

    public virtual void DrawInspector() {
    }

#endif
}
