
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum EEventActionResult {
    Normal,
    Block,
    Async
}

public class EventActionRuntime {
    public delegate bool CheckCondition(EventActionPage owner, EventActionCondition Codition);
    public delegate EEventActionResult ExecuteAction(EventActionPage owner, EventActionCallback Args);

    public Dictionary<Type, ExecuteAction> ActionDict = new Dictionary<Type, ExecuteAction>();
    public Dictionary<Type, CheckCondition> ConditionDict = new Dictionary<Type, CheckCondition>();

    public void RegisterAction<T>(ExecuteAction action) where T : EventActionCallback {
        ActionDict.Add(typeof(T), action);
    }

    public EEventActionResult CallAction(EventActionPage owner, EventActionCallback args) {
        if (ActionDict.TryGetValue(args.GetType(), out ExecuteAction callback)) {
            return callback.Invoke(owner, args);
        }
        else {
            throw new Exception("Can't Find Action By " + args.GetType());
        }
    }

    public void RegisterCondition<T>(CheckCondition condition) where T : EventActionCondition {
        ConditionDict.Add(typeof(T), condition);
    }

    public bool CallCondition(EventActionPage owner, EventActionCondition args) {
        if (ConditionDict.TryGetValue(args.GetType(), out CheckCondition callback)) {
            return callback.Invoke(owner, args);
        }
        else {
            throw new Exception("Can't Find Condition By " + args.GetType());
        }
    }

    private void RegisterActionAndCondition(Assembly assembly) {
        Type[] types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++) {
            Type classType = types[i];
            if (classType.IsSubclassOf(typeof(EventActionCallback))) {

            }
            else if (classType.IsSubclassOf(typeof(EventActionCondition))) {
                 
            }
        }
    }
}
