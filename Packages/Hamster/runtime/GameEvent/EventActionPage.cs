using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

public interface IActionDrawer {
    void DrawMoveUp(float baseWidth, float maxWidth, float minWidth, bool valid, Action OnMoveUp);
    void DrawMoveDown(float baseWidth, float maxWidth, float minWidth, bool valid, Action OnMoveDown);
    void DrawDel(float baseWidth, float maxWidth, float minWidth, Action Del);
    void OnClickAction(EventActionCallback eventAction, Action<bool> callback);

    void AddNewAction(object userData, Action<bool, object> callback);

    string GetPath();
}

public interface IDrawContext {
    void Draw(IActionDrawer drawer, float baseWidth, float maxWidth, float minWidth);
}

#endif

[System.Serializable]
public class EventActionPage : ScriptableObject, IDrawContext {
    public delegate bool CheckActionComplete();

    public string Name = string.Empty;

    public EventActionInst Owner { get; protected set; }
    public EventActionCondition Condition = null;
    public List<EventActionCallback> ActionCalls = new List<EventActionCallback>();

    protected Dictionary<string, IBackboardVar> _variables = new Dictionary<string, IBackboardVar>();

    public int CodeIndex {
        get; set;
    }

    public void Initialize(EventActionInst ownerInst) {
        Owner = ownerInst;
        Condition.SetOwnerPage(this);
        for (int i = 0; i < ActionCalls.Count; i++) {
            ActionCalls[i].SetOwnerPage(this);
        }
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
        if (null != Condition)
            Condition.Reset();
    }


#if UNITY_EDITOR
    public void Save() {
        UnityEditor.EditorUtility.SetDirty(Condition);
        for (int i = 0; i < ActionCalls.Count; i++) {
            UnityEditor.EditorUtility.SetDirty(ActionCalls[i]);
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }

    public void Draw(IActionDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
        for (int i = 0; i < ActionCalls.Count; i++) {
            EventActionCallback eventActionArgs = ActionCalls[i];
            if (eventActionArgs is IDrawContext) {
                (eventActionArgs as IDrawContext).Draw(drawer, baseWidth, maxWidth, minWidth);
            }
            else {
                EditorGUILayout.BeginHorizontal();
                EventActionInfoAttribute attribute = eventActionArgs.GetType().GetCustomAttribute<EventActionInfoAttribute>();
                string context = attribute.DisplayName + " -> " + eventActionArgs.Descript;
                if (context.Length >= 75) {
                    context = context.Substring(0, 75);
                }
                else {
                    for (int k = context.Length; k < 75; k++) {
                        context += " ";
                    }
                }
                GUILayout.Label(i.ToString(), GUILayout.Width(0.05f * baseWidth),
                    GUILayout.MaxWidth(0.05f * maxWidth),
                    GUILayout.MinWidth(0.05f * minWidth));
                if (GUILayout.Button(context,
                    GUILayout.Width(0.65f * baseWidth),
                    GUILayout.MaxWidth(0.65f * maxWidth),
                    GUILayout.MinWidth(0.65f * minWidth))) {
                    drawer.OnClickAction(eventActionArgs, (bool success) => { });
                }
                drawer.DrawMoveUp(baseWidth, maxWidth, minWidth, i >= 1, ()=> {
                    ActionCalls.RemoveAt(i);
                    ActionCalls.Insert(i - 1, eventActionArgs);
                });
                drawer.DrawMoveDown(baseWidth, maxWidth, minWidth, i < ActionCalls.Count - 1, () => {
                    ActionCalls.RemoveAt(i);
                    ActionCalls.Insert(i + 1, eventActionArgs);
                });
                drawer.DrawDel(baseWidth, maxWidth, minWidth, () => {
                    ActionCalls.RemoveAt(i);
                });
                EditorGUILayout.EndHorizontal();
            }
        }
    }
#endif
}
