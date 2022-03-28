using System.Collections.Generic;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;
#endif

[System.Serializable]
public class EventActionCallback : EventActionBase, IEventActionCallback {
    public virtual EEventActionResult Execute() {
        return EEventActionResult.Normal;
    }
}

[System.Serializable]
public class EventActionConditionCallback : EventActionCallback, IDrawContext {

    protected int CodeIndex = 0;
    protected List<EventActionCallback> Current = null;

    public override EEventActionResult Execute() {
        while (CodeIndex < Current.Count) {
            EventActionCallback callback = Current[CodeIndex];
            if (EEventActionResult.Block == callback.Execute()) {
                return EEventActionResult.Block;
            }
            CodeIndex++;
        }
        return EEventActionResult.Normal;
    }

    public override void Reset() {
        base.Reset();
        CodeIndex = 0;
        Current = null;
    }

#if UNITY_EDITOR

    protected virtual void DrawAction(List<EventActionCallback> actions, IActionDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
        for (int i = 0; i < actions.Count; i++) {
            EventActionCallback eventActionArgs = actions[i];
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
                drawer.DrawMoveUp(baseWidth, maxWidth, minWidth, i >= 1, () => {
                    actions.RemoveAt(i);
                    actions.Insert(i - 1, eventActionArgs);
                });
                drawer.DrawMoveDown(baseWidth, maxWidth, minWidth, i < actions.Count - 1, () => {
                    actions.RemoveAt(i);
                    actions.Insert(i + 1, eventActionArgs);
                });
                drawer.DrawDel(baseWidth, maxWidth, minWidth, () => {
                    actions.RemoveAt(i);
                });
                EditorGUILayout.EndHorizontal();
            }
        }

        if (GUILayout.Button("新增事件",
        GUILayout.Width(0.65f * baseWidth),
        GUILayout.MaxWidth(0.65f * maxWidth),
        GUILayout.MinWidth(0.65f * minWidth))) {
            drawer.AddNewAction(null, (bool success, object inst) => {
                AssetDatabase.AddObjectToAsset(inst as EventActionCallback, drawer.GetPath());
                actions.Add(inst as EventActionCallback);
            });
        }
    }

    public virtual void Draw(IActionDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
    }
#endif
}