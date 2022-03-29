using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Collections.Generic;

public class BP_NewActionOrConditionWindow : EditorWindow {

    public static BP_NewActionOrConditionWindow NewWindow(IBlackPrintActionDrawInspector inst, Action<bool> onComplete) {
        BP_NewActionOrConditionWindow instance = EditorWindow.GetWindow(typeof(BP_NewActionOrConditionWindow), false) as BP_NewActionOrConditionWindow;
        instance.Inst = inst;
        instance.OnComplete = onComplete;
        return instance;
    }


    public Action<bool> OnComplete = null;
    public IBlackPrintActionDrawInspector Inst = null;

    public void OnGUI() {
        GUILayout.BeginVertical();
        {
            Inst.DrawInspector();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("OK")) {
                OnComplete?.Invoke(true);
            }
            if (GUILayout.Button("Cancel")) {
                OnComplete?.Invoke(false);

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }
}

public class BP_AddActionOrConditionSelectList : EditorWindow {

    private EBlackPrintSpawnType _type = EBlackPrintSpawnType.Action;

    private object _inst = null;
    private string _selectCategory = string.Empty;
    private Vector2 _listPosition = Vector2.zero;
    private Action<bool, object> _onSelectComplete = null;
    private BP_NewActionOrConditionWindow _currentWindow = null;

    public static void NewWindow(EBlackPrintSpawnType type, Action<bool, object> onSelectComplete) {
        BP_AddActionOrConditionSelectList instance = EditorWindow.GetWindow(typeof(BP_AddActionOrConditionSelectList), false) as BP_AddActionOrConditionSelectList;
        instance.maxSize = new Vector2(400, 600);
        instance.minSize = new Vector2(400, 600);

        instance._inst = null;
        instance._type = type;
        instance._selectCategory = string.Empty;
        instance._listPosition = Vector2.zero;
        instance._onSelectComplete = onSelectComplete;
        instance._currentWindow = null;
    }

    public void OnGUI() {
        _listPosition = EditorGUILayout.BeginScrollView(_listPosition);
        if (string.IsNullOrEmpty(_selectCategory)) {
            var it = BlackPrintAttribute.CallbackAndCallbacks.GetEnumerator();
            while (it.MoveNext()) {
                string Category = it.Current.Key;
                if (GUILayout.Button(Category)) {
                    _selectCategory = Category;
                }
            }
        }
        else {
            Dictionary<string, Type> result = BlackPrintAttribute.GetTypesByCategory(_selectCategory);
            if (null == result)
                return;

            var it = result.GetEnumerator();
            while (it.MoveNext()) {
                Type value = it.Current.Value;
                string Name = it.Current.Key;

                BlackPrintAttribute attribute = value.GetCustomAttribute<BlackPrintAttribute>();
                if (null == attribute)
                    continue;


                if ((EBlackPrintSpawnType.Action == _type && value.IsSubclassOf(typeof(BlackPrintAction))) 
                    || EBlackPrintSpawnType.Condition == _type && value.IsSubclassOf(typeof(BlackPrintCondition))) {
                    if (GUILayout.Button(attribute.DisplayName)) {
                        _inst = ScriptableObject.CreateInstance(value);
                        // _inst = Activator.CreateInstance(value);
                    }
                }
            }

            if (null != _inst) {
                _currentWindow = BP_NewActionOrConditionWindow.NewWindow(_inst as IBlackPrintActionDrawInspector, OnCreateNewActionOrConditionComplete);
            }

        }
        EditorGUILayout.EndScrollView();
    }

    private void OnCreateNewActionOrConditionComplete(bool value) {
        if (!value) {
            _inst = null; 
        }
        _onSelectComplete.Invoke(value, _inst);

        _currentWindow.Close();
        Close();
    }
}