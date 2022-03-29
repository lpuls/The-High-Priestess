using System.Collections.Generic;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;
#endif

[System.Serializable]
public class BlackPrintAction : BlackPrintActionBase, IBlackPrintActionCallback {
    public virtual EBPActionResult Execute() {
        return EBPActionResult.Normal;
    }
}

[System.Serializable]
public class BlackPrintConditionAction : BlackPrintAction, IDrawActionContext {

    protected int CodeIndex = 0;
    protected List<BlackPrintAction> Current = null;

    public override EBPActionResult Execute() {
        while (CodeIndex < Current.Count) {
            BlackPrintAction callback = Current[CodeIndex];
            if (EBPActionResult.Block == callback.Execute()) {
                return EBPActionResult.Block;
            }
            CodeIndex++;
        }
        return EBPActionResult.Normal;
    }

    public override void Reset() {
        base.Reset();
        CodeIndex = 0;
        Current = null;
    }

#if UNITY_EDITOR

    protected virtual void DrawAction(List<BlackPrintAction> actions, IActionListDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
        for (int i = 0; i < actions.Count; i++) {
            BlackPrintAction eventActionArgs = actions[i];
            if (eventActionArgs is IDrawActionContext) {
                (eventActionArgs as IDrawActionContext).Draw(drawer, baseWidth, maxWidth, minWidth);
            }
            else {
                EditorGUILayout.BeginHorizontal();
                BlackPrintAttribute attribute = eventActionArgs.GetType().GetCustomAttribute<BlackPrintAttribute>();
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
                AssetDatabase.AddObjectToAsset(inst as BlackPrintAction, drawer.GetPath());
                actions.Add(inst as BlackPrintAction);
            });
        }
    }

    public virtual void Draw(IActionListDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
    }
#endif
}