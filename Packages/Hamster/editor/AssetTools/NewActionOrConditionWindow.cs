using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Collections.Generic;

public class NewActionOrConditionWindow : EditorWindow {

    public static NewActionOrConditionWindow NewWindow(IEventActionDrawInspector inst, Action<bool> onComplete) {
        NewActionOrConditionWindow instance = EditorWindow.GetWindow(typeof(NewActionOrConditionWindow), false) as NewActionOrConditionWindow;
        instance.Inst = inst;
        instance.OnComplete = onComplete;
        return instance;
    }


    public Action<bool> OnComplete = null;
    public IEventActionDrawInspector Inst = null;

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

public class AddActionOrConditionSelectList : EditorWindow {

    private EEventActionSpawnType _type = EEventActionSpawnType.Action;

    private object _inst = null;
    private string _selectCategory = string.Empty;
    private Vector2 _listPosition = Vector2.zero;
    private Action<bool, object> _onSelectComplete = null;
    private NewActionOrConditionWindow _currentWindow = null;

    public static void NewWindow(EEventActionSpawnType type, Action<bool, object> onSelectComplete) {
        AddActionOrConditionSelectList instance = EditorWindow.GetWindow(typeof(AddActionOrConditionSelectList), false) as AddActionOrConditionSelectList;
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
            var it = EventActionInfoAttribute.CallbackAndCallbacks.GetEnumerator();
            while (it.MoveNext()) {
                string Category = it.Current.Key;
                if (GUILayout.Button(Category)) {
                    _selectCategory = Category;
                }
            }
        }
        else {
            Dictionary<string, Type> result = EventActionInfoAttribute.GetTypesByCategory(_selectCategory);
            if (null == result)
                return;

            var it = result.GetEnumerator();
            while (it.MoveNext()) {
                Type value = it.Current.Value;
                string Name = it.Current.Key;

                EventActionInfoAttribute attribute = value.GetCustomAttribute<EventActionInfoAttribute>();
                if (null == attribute)
                    continue;

                if ((EEventActionSpawnType.Action == _type && value.IsSubclassOf(typeof(EventActionCallback))) 
                    || EEventActionSpawnType.Condition == _type && value.IsSubclassOf(typeof(EventActionCondition))) {
                    if (GUILayout.Button(attribute.DisplayName)) {
                        _inst = ScriptableObject.CreateInstance(value);
                        // _inst = Activator.CreateInstance(value);
                    }
                }
            }

            if (null != _inst) {
                _currentWindow = NewActionOrConditionWindow.NewWindow(_inst as IEventActionDrawInspector, OnCreateNewActionOrConditionComplete);
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