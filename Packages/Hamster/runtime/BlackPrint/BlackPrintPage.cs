using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

namespace Hamster.BP {
#if UNITY_EDITOR
    public interface IActionListDrawer {
        void DrawMoveUp(float baseWidth, float maxWidth, float minWidth, bool valid, Action OnMoveUp);
        void DrawMoveDown(float baseWidth, float maxWidth, float minWidth, bool valid, Action OnMoveDown);
        void DrawDel(float baseWidth, float maxWidth, float minWidth, Action Del);
        void OnClickAction(BlackPrintAction eventAction, Action<bool> callback);

        void AddNewAction(object userData, Action<bool, object> callback);

        string GetPath();
    }

    public interface IDrawActionContext {
        void Draw(IActionListDrawer drawer, float baseWidth, float maxWidth, float minWidth);
    }

#endif

    [System.Serializable]
#if UNITY_EDITOR
    public class BlackPrintPage : ScriptableObject, IDrawActionContext {
#else
        public class BlackPrintPage : ScriptableObject {
#endif
            public delegate bool CheckActionComplete();

        public string Name = string.Empty;

        public BlackPrintInst Owner {
            get; protected set;
        }
        public BlackPrintCondition Condition = null;
        public List<BlackPrintAction> ActionCalls = new List<BlackPrintAction>();

        protected Dictionary<string, int> _propertys = new Dictionary<string, int>();

        public int CodeIndex {
            get; set;
        }

        public void Initialize(BlackPrintInst ownerInst) {
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
                BlackPrintAction callback = ActionCalls[CodeIndex];
                if (EBPActionResult.Block == callback.Execute()) {
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

        public void AddProperty(string key, int value) {
            _propertys.Add(key, value);
        }

        public bool TryGetProperty(string key, out int value) {
            return _propertys.TryGetValue(key, out value);
        }

        public bool TryModifyProperty(string key, int newValue) {
            if (_propertys.ContainsKey(key)) {
                _propertys[key] = newValue;
                return true;
            }
            return false;
        }

#if UNITY_EDITOR
        public void Save() {
            UnityEditor.EditorUtility.SetDirty(Condition);
            for (int i = 0; i < ActionCalls.Count; i++) {
                UnityEditor.EditorUtility.SetDirty(ActionCalls[i]);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        }

        public void Draw(IActionListDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
            for (int i = 0; i < ActionCalls.Count; i++) {
                BlackPrintAction eventActionArgs = ActionCalls[i];
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
}