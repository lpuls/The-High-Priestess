using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
#endif

namespace Hamster.BP {
    public interface IBlackPrintActionCallback {
        EBPActionResult Execute();
    }

    public interface IBlackPrintCondition {
        bool CheckCondition();
    }

#if UNITY_EDITOR
    public interface IBlackPrintActionDrawInspector {
        void DrawInspector();
    }
#endif

    public class BPPropertyAttribute : System.Attribute {
#if UNITY_EDITOR
        public string Label = string.Empty;
        public FieldInfo PropertyInfo = null;

        public BPPropertyAttribute(string label) {
            Label = label;
        }

        public void DrawPropertys<T>(T inst) where T: BlackPrintActionBase {
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++) {
                PropertyInfo propertyInfo = propertyInfos[i];
                propertyInfo.GetCustomAttribute<BPPropertyAttribute>();
            }
        }
#else 
        public BPPropertyAttribute(string label) { }
#endif
    }


#if UNITY_EDITOR
    public class BlackPrintActionBase : ScriptableObject, IBlackPrintActionDrawInspector {
#else
    public class BlackPrintActionBase : ScriptableObject {
#endif

        protected BlackPrintPage OwnerPage {
            get;
            private set;
        }

        public virtual void SetOwnerPage(BlackPrintPage page) {
            OwnerPage = page;
        }

        public virtual void Reset() {
#if UNITY_EDITOR
            Descript = string.Empty;
#endif
        }

#if UNITY_EDITOR
        public string Descript = string.Empty;

        protected bool _isInit = false;
        protected List<BPPropertyAttribute> _drawProperty = new List<BPPropertyAttribute>();

        protected void InitializeAttribute(Type type) {
            //if (_isInit)
            //    return;

            _drawProperty.Clear();
            FieldInfo[] propertyInfos = type.GetFields();
            for (int i = 0; i < propertyInfos.Length; i++) {
                FieldInfo propertyInfo = propertyInfos[i];
                BPPropertyAttribute attribute = propertyInfo.GetCustomAttribute<BPPropertyAttribute>();
                if (null != attribute) {
                    attribute.PropertyInfo = propertyInfo;
                    _drawProperty.Add(attribute);
                }
            }
            // _isInit = true;
        }

        public virtual void DrawInspector() {
            for (int i = 0; i < _drawProperty.Count; i++) {
                BPPropertyAttribute attribute = _drawProperty[i];
                Type propertyType = attribute.PropertyInfo.GetType();
                if (propertyType == typeof(byte)) {
                    byte value = (byte)UnityEditor.EditorGUILayout.IntField(attribute.Label, (int)attribute.PropertyInfo.GetValue(this));
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType == typeof(short)) {
                    short value = (short)UnityEditor.EditorGUILayout.IntField(attribute.Label, (int)attribute.PropertyInfo.GetValue(this));
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType == typeof(ushort)) {
                    ushort value = (ushort)UnityEditor.EditorGUILayout.IntField(attribute.Label, (int)attribute.PropertyInfo.GetValue(this));
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType == typeof(int)) {
                    int value = (int)UnityEditor.EditorGUILayout.IntField(attribute.Label, (int)attribute.PropertyInfo.GetValue(this));
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType == typeof(uint)) {
                    string value = UnityEditor.EditorGUILayout.TextField(attribute.Label, attribute.PropertyInfo.GetValue(this).ToString());
                    if (uint.TryParse(value, out uint v))
                        attribute.PropertyInfo.SetValue(this, v);
                }
                else if (propertyType == typeof(long)) {
                    string value = UnityEditor.EditorGUILayout.TextField(attribute.Label, attribute.PropertyInfo.GetValue(this).ToString());
                    if (long.TryParse(value, out long v))
                        attribute.PropertyInfo.SetValue(this, v);
                }
                else if (propertyType == typeof(ulong)) {
                    string value = UnityEditor.EditorGUILayout.TextField(attribute.Label, attribute.PropertyInfo.GetValue(this).ToString());
                    if (ulong.TryParse(value, out ulong v))
                        attribute.PropertyInfo.SetValue(this, v);
                }
                else if (propertyType == typeof(string)) {
                    string value = UnityEditor.EditorGUILayout.TextField(attribute.Label, attribute.PropertyInfo.GetValue(this).ToString());
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType == typeof(float)) {
                    float value = UnityEditor.EditorGUILayout.FloatField(attribute.Label, (float)attribute.PropertyInfo.GetValue(this));
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType == typeof(double)) {
                    string value = UnityEditor.EditorGUILayout.TextField(attribute.Label, attribute.PropertyInfo.GetValue(this).ToString());
                    if (double.TryParse(value, out double v))
                        attribute.PropertyInfo.SetValue(this, v);
                }
                else if (propertyType == typeof(bool)) {
                    bool value = UnityEditor.EditorGUILayout.Toggle(attribute.Label, (bool)attribute.PropertyInfo.GetValue(this));
                    attribute.PropertyInfo.SetValue(this, value);
                }
                else if (propertyType.IsSubclassOf(typeof(UnityEngine.Object))) {
                    // UnityEngine.Object value = UnityEditor.EditorGUILayout.ObjectField(attribute.Label, propertyType, (UnityEngine.Object)attribute.PropertyInfo.GetValue(this));
                }
            }
        }

#endif
    }
}