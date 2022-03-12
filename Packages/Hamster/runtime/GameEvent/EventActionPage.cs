using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventActionPage : ScriptableObject {
    public delegate bool CheckActionComplete();

    public string Name = string.Empty;

    public EventActionInst Owner = null;
    public EventActionCondition Condition = null;
    public List<EventActionCallback> ActionCalls = new List<EventActionCallback>();

    protected Dictionary<string, IBackboardVar> _variables = new Dictionary<string, IBackboardVar>();

    public int CodeIndex {
        get; set;
    }

    public bool CheckCondition() {
        return Condition.CheckCondition();
    }

    public void Execute() {
        while (CodeIndex < ActionCalls.Count) {
            EventActionCallback callback = ActionCalls[CodeIndex];
            if (EEventActionResult.Block == callback.Execute()) {
                break;
            }
            CodeIndex++;
        }
    }

    public void Reset() {
        CodeIndex = 0;
        for (int i = 0; i < ActionCalls.Count; i++) {
            ActionCalls[i].Reset();
        }
        Condition.Reset();
    }

    public void AddVariable<T>(string name, T value = default) {
        if (_variables.ContainsKey(name)) {
            Debug.LogError("Variable Name Redefine");
            return; 
        }

        BackboardVar<T> inst = BackboardVar<T>.Malloc();
        inst.SetName(name);
        inst.SetValue(value);
        _variables.Add(name, inst);
    }

    public void DelVariable<T>(string name) {
        if (!_variables.TryGetValue(name, out IBackboardVar inst)) {
            Debug.LogError("Variable Name not define");
            return;
        }

        _variables.Remove(name);
        BackboardVar<T>.Free(inst);
    }

    public void ModifyVaraible<T>(string name, T value) {
        if (!_variables.TryGetValue(name, out IBackboardVar inst)) {
            Debug.LogError("Variable Name not define");
            return;
        }

        (inst as BackboardVar<T>).SetValue(value);
    }

#if UNITY_EDITOR
    public void Save() {
        UnityEditor.EditorUtility.SetDirty(Condition);
        for (int i = 0; i < ActionCalls.Count; i++) {
            UnityEditor.EditorUtility.SetDirty(ActionCalls[i]);
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
