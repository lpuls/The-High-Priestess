using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace Hamster.TouchPuzzle {
    [EventActionInfo("如果拥有", "Item")]
    public class EventActionCallback_HasItem : EventActionConditionCallback {
        public int TargetItem = 0;

        private List<EventActionCallback> OnHas = new List<EventActionCallback>();
        private List<EventActionCallback> OnNot = new List<EventActionCallback>();


        public override EEventActionResult Execute() {
            TouchPuzzeWorld world = World.GetWorld<TouchPuzzeWorld>();
            Debug.Assert(null != world, "Can't Find World");
            Debug.Assert(null != world.ItemManager, "Can't Find World.ItemManager");
            if (world.ItemManager.HasItem(TargetItem))
                Current = OnHas;
            else
                Current = OnNot;
            return base.Execute();
        }

        public override void Reset() {
            CodeIndex = 0;
        }

#if UNITY_EDITOR
        public override void DrawInspector() {
            base.DrawInspector();
            TargetItem = UnityEditor.EditorGUILayout.IntField("日志信息:", TargetItem);
            Descript = "如果拥有" + TargetItem;
        }

        public override void Draw(IActionDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
            EditorGUILayout.LabelField("如果拥有" + TargetItem);
            DrawAction(OnHas, drawer, baseWidth, maxWidth, minWidth);
            EditorGUILayout.LabelField("当未拥有" + TargetItem);
            DrawAction(OnNot, drawer, baseWidth, maxWidth, minWidth);
        }
#endif
    }
}
