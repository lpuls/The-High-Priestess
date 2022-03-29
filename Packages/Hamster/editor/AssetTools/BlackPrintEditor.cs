using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;
using Hamster.BP;


public class BlackPrintEditor : EditorWindow, IActionListDrawer {

    public static void ShowEventActionEditor(List<Assembly> assemblies) {
        EditorWindow editorWindow = EditorWindow.GetWindow(typeof(BlackPrintEditor));
        for (int i = 0; i < assemblies.Count; i++) {
            BlackPrintAttribute.Spawner(assemblies[i]);
        }
    }

    private float _splitterWidth = 5;
    private float _descPanelSize = 0.3f;
    private float _listPanelSize = 0.6f;
    private float _descTextSize = 0.2f;

    private Rect _listRect = Rect.zero;
    private GUIStyle _styleView = null;

    private Vector2 _listPosition = Vector2.zero;

    private string _path = string.Empty;
    private int _currentActionPageIndex = 0;
    private BlackPrintInst _currentActionInst = null;


    public BlackPrintEditor() {
        this.titleContent = new GUIContent("Event Action Editor");
        BlackPrintAttribute.Clean();
        BlackPrintAttribute.Spawner(typeof(Hamster.BP.BPAction_DebugInfo).Assembly);
    }

    public void OnGUI() {
        if (null == _styleView)
            _styleView = new GUIStyle(GUI.skin.box);

        _currentActionInst = EditorGUILayout.ObjectField("当前事件实例：", _currentActionInst, typeof(BlackPrintInst), true) as BlackPrintInst;
        if (null == _currentActionInst) {
            OnAciontInstanceIsNull();
        }
        else {
            OnActionInstanceIsNotNull();
        }

        UpdateEvent();
    }

    private void OnAciontInstanceIsNull() {
        if (GUILayout.Button("编辑事件实例")) {
            _path = EditorUtility.OpenFilePanel("打开实例", Application.dataPath + "/Res/Configs/EventAction/", "asset");
            _path = _path.Replace(Application.dataPath, "Assets");
            _currentActionInst = AssetDatabase.LoadAssetAtPath<BlackPrintInst>(_path);

            if (_currentActionInst.Pages.Count <= 0) {
                BlackPrintPage page = ScriptableObject.CreateInstance<BlackPrintPage>();
                page.Name = "Page 0";
                _currentActionInst.Pages.Add(page);
                AssetDatabase.AddObjectToAsset(page, _path);

                page.Save();
            }
        }
        if (GUILayout.Button("创建事件实例")) {
            _currentActionInst = ScriptableObject.CreateInstance<BlackPrintInst>();
            _path = EditorUtility.SaveFilePanel("保存实例", Application.dataPath + "/Res/Configs/EventAction/", "NewEventAction.asset", "asset");
            _path = _path.Replace(Application.dataPath, "Assets");
            AssetDatabase.CreateAsset(_currentActionInst, _path);

            if (_currentActionInst.Pages.Count <= 0) {
                BlackPrintPage page = ScriptableObject.CreateInstance<BlackPrintPage>();
                page.Name = "Page 0";
                _currentActionInst.Pages.Add(page);
                AssetDatabase.AddObjectToAsset(page, _path);
            }

            EditorUtility.SetDirty(_currentActionInst);
            AssetDatabase.SaveAssets();
        }
    }

    private void OnActionInstanceIsNotNull() {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                OnAciontInstanceIsNull();
                if (GUILayout.Button("保存")) {
                    for (int i = 0; i < _currentActionInst.Pages.Count; i++) {
                        _currentActionInst.Pages[i].Save();
                    }
                    EditorUtility.SetDirty(_currentActionInst);
                    AssetDatabase.SaveAssets();
                }
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            {
                // 绘制详情界面
                DrawDescPanel();

                // 中间分隔线
                GUILayout.Box("",
                      GUILayout.Width(_splitterWidth),
                      GUILayout.MaxWidth(_splitterWidth),
                      GUILayout.MinWidth(_splitterWidth),
                      GUILayout.ExpandHeight(true));

                // 绘制事件列表
                DrawActionList();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawDescPanel() {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * _descPanelSize),
                      GUILayout.MaxWidth(maxSize.x * _descPanelSize),
                      GUILayout.MinWidth(minSize.x * _descPanelSize),
                      GUILayout.ExpandHeight(true));
        {
            // 名称和描述
            _currentActionInst.Name = EditorGUILayout.TextField(_currentActionInst.Name);
            _currentActionInst.Desc = EditorGUILayout.TextArea(_currentActionInst.Desc,
                GUILayout.Width(position.width * _descPanelSize),
                GUILayout.MaxWidth(maxSize.x * _descPanelSize),
                GUILayout.MinWidth(minSize.x * _descPanelSize),
                GUILayout.Height(position.height * _descTextSize),
                GUILayout.MaxWidth(maxSize.y * _descTextSize),
                GUILayout.MinWidth(minSize.y * _descTextSize));
            if (GUILayout.Button("新建页面")) {
                BP_NewPageWindow.ShowWindow(OnCreateNewPageComplete);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            // page信息
            BlackPrintPage page = _currentActionInst.Pages[_currentActionPageIndex];
            page.Name = EditorGUILayout.TextField(page.Name);
            EditorGUILayout.BeginHorizontal();
            {
                string conditionDisplay = string.Empty;
                if (null != page.Condition) {
                    BlackPrintAttribute attribute = page.Condition.GetType().GetCustomAttribute<BlackPrintAttribute>();
                    if (null != attribute)
                        conditionDisplay = attribute.DisplayName;
                }
                EditorGUILayout.LabelField("解发条件: " + conditionDisplay);
                if (GUILayout.Button("New Condition")) {
                    BP_AddActionOrConditionSelectList.NewWindow(EBlackPrintSpawnType.Condition,
                        (bool success, object inst) => {
                            if (success) {
                                page.Condition = inst as BlackPrintCondition;
                                AssetDatabase.AddObjectToAsset(page.Condition, _path);
                            }
                        });
                }
                if (null != page.Condition) {
                    if (GUILayout.Button(/*"编辑条件"*/page.Condition.Descript)) {
                        BP_NewActionOrConditionWindow.NewWindow(page.Condition, (bool value)=> { });
                        //page.Condition.DrawInspector();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                if (_currentActionPageIndex >= 1) {
                    if (GUILayout.Button("前移")) {
                        _currentActionInst.Pages.RemoveAt(_currentActionPageIndex);
                        _currentActionInst.Pages.Insert(_currentActionPageIndex - 1, page);
                        _currentActionPageIndex -= 1;
                    }
                }
                if (_currentActionPageIndex < _currentActionInst.Pages.Count - 1) {
                    if (GUILayout.Button("后移")) {
                        _currentActionInst.Pages.RemoveAt(_currentActionPageIndex);
                        _currentActionInst.Pages.Insert(_currentActionPageIndex + 1, page);
                        _currentActionPageIndex++;
                    }
                }
                if (GUILayout.Button("删除")) {
                    _currentActionInst.Pages.RemoveAt(_currentActionPageIndex);
                }
            }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndVertical();
    }

    public void DrawMoveUp(float baseWidth, float maxWidth, float minWidth, bool valid, Action OnMoveUp) {
        if (valid) {
            if (GUILayout.Button("MoveUp",
            GUILayout.Width(0.1f * baseWidth),
            GUILayout.MaxWidth(0.1f * maxWidth),
            GUILayout.MinWidth(0.1f * minWidth))) {
                OnMoveUp?.Invoke();
            }
        }
        else {
            GUILayout.Box("MoveUp",
                GUILayout.Width(0.1f * baseWidth),
                GUILayout.MaxWidth(0.1f * maxWidth),
                GUILayout.MinWidth(0.1f * minWidth));
        }
    }
    public void DrawMoveDown(float baseWidth, float maxWidth, float minWidth, bool valid, Action OnMoveDown) {
        if (valid) {
            if (GUILayout.Button("MoveDown",
            GUILayout.Width(0.1f * baseWidth),
            GUILayout.MaxWidth(0.1f * maxWidth),
            GUILayout.MinWidth(0.1f * minWidth))) {
                OnMoveDown?.Invoke();
            }
        }
        else {
            GUILayout.Box("MoveDown",
                GUILayout.Width(0.1f * baseWidth),
                GUILayout.MaxWidth(0.1f * maxWidth),
                GUILayout.MinWidth(0.1f * minWidth));
        }
    }

    public void DrawDel(float baseWidth, float maxWidth, float minWidth, Action Del) {
        if (GUILayout.Button("Del",
            GUILayout.Width(0.1f * baseWidth),
            GUILayout.MaxWidth(0.1f * maxWidth),
            GUILayout.MinWidth(0.1f * minWidth))) {
            Del?.Invoke();
        }
    }

    public void OnClickAction(BlackPrintAction eventAction, Action<bool> callback) {
        BP_NewActionOrConditionWindow.NewWindow(eventAction, callback);
    }

    private void DrawActionList() {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width * _listPanelSize),
                     GUILayout.MaxWidth(maxSize.x * _listPanelSize),
                     GUILayout.MinWidth(minSize.x * _listPanelSize),
                     GUILayout.ExpandHeight(true));
        {
            DrawPages();
            _listRect = EditorGUILayout.BeginVertical();
            _listPosition = EditorGUILayout.BeginScrollView(_listPosition, _styleView);
            {
                float baseWidth = position.width * _listPanelSize;
                float maxBaseWidth = maxSize.x * _listPanelSize;
                float minBaseWidth = minSize.x * _listPanelSize;
                BlackPrintPage page = _currentActionInst.Pages[_currentActionPageIndex];
                page.Draw(this, baseWidth, maxBaseWidth, minBaseWidth);
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    private void DrawPages() {
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < _currentActionInst.Pages.Count; i++) {
            BlackPrintPage page = _currentActionInst.Pages[i];
            string PageName = string.IsNullOrEmpty(page.Name) ? ("Page " + i) : page.Name;
            if (i == _currentActionPageIndex)
                PageName = "->" + PageName;
            if (GUILayout.Button(PageName, GUILayout.MaxWidth(100), GUILayout.MinWidth(10))) {
                _currentActionPageIndex = i;
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void OnCreateNewPageComplete(string pageName) {
        BlackPrintPage page = ScriptableObject.CreateInstance<BlackPrintPage>();
        page.Name = pageName;
        _currentActionInst.Pages.Add(page);
        AssetDatabase.AddObjectToAsset(page, _path);
    }

    private void UpdateEvent() {
        Event guiEvent = Event.current;
        switch (guiEvent.type) {
            case EventType.ContextClick:
                if (_listRect.Contains(guiEvent.mousePosition)) {
                    GenericMenu genericMenu = new GenericMenu();
                    genericMenu.AddItem(new GUIContent("新增事件"), false, AddNewAction, null);
                    genericMenu.ShowAsContext();
                    guiEvent.Use();
                }
                break;
            default:
                break;
        }
    }

    public void AddNewAction(object userData, Action<bool, object> callback) {
        BP_AddActionOrConditionSelectList.NewWindow(EBlackPrintSpawnType.Action, callback);
    }

    private void AddNewAction(object userData) {
        BP_AddActionOrConditionSelectList.NewWindow(EBlackPrintSpawnType.Action,
                    (bool success, object inst) => {
                        if (success) {
                            BlackPrintPage page = _currentActionInst.Pages[_currentActionPageIndex];
                            page.Condition = ScriptableObject.CreateInstance<Hamster.BP.BPContidiont_Default>();
                            AssetDatabase.AddObjectToAsset(page.Condition, _path);

                            page.ActionCalls.Add(inst as BlackPrintAction);
                            AssetDatabase.AddObjectToAsset(inst as BlackPrintAction, _path);
                        }
                    });
    }

    public string GetPath() {
        return _path;
    }

}