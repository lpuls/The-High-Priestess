using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Hamster.TouchPuzzle.Editor {
    public class WindowTools : EditorWindow {
        [MenuItem("Tools/Editor/EventActionEditor")]
        static void OpenEventActionEditor() {
            List<Assembly> assemblies = new List<Assembly>{ typeof(EventActionCallback_HasItem).Assembly};
            EventActionEditor.ShowEventActionEditor(assemblies);
        }
    }
}
