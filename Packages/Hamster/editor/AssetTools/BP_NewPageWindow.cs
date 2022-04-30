﻿using UnityEngine;
using UnityEditor;
using System;
using Hamster.BP;

public class BP_NewPageWindow : EditorWindow {

    private string _pageName = string.Empty;
    private Action<string> _onCreateComplete = null;

    public static void ShowWindow(Action<string> onCreateComplete) {
        BP_NewPageWindow editorWindow = EditorWindow.GetWindow(typeof(BP_NewPageWindow), false) as BP_NewPageWindow;
        editorWindow.maxSize = new Vector2(400, 200);
        editorWindow.minSize = new Vector2(400, 200);
        editorWindow._onCreateComplete = onCreateComplete;
    }

    public void OnGUI() {
        EditorGUILayout.BeginHorizontal();
        {
            _pageName = EditorGUILayout.TextField("名称:", _pageName);
            if (GUILayout.Button("OK")) {
                _onCreateComplete?.Invoke(_pageName);
                Close();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}