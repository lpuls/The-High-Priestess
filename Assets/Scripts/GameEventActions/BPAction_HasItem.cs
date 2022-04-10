using System.Collections.Generic;
using Hamster.BP;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hamster.TouchPuzzle {
    [Hamster.BP.BlackPrint("如果拥有", "Item")]
    public class BPAction_HasItem : Hamster.BP.BlackPrintConditionAction {
        public int TargetItem = 0;

        private List<BlackPrintAction> OnHas = new List<BlackPrintAction>();
        private List<BlackPrintAction> OnNot = new List<BlackPrintAction>();


        public override EBPActionResult Execute() {
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

        public override void Draw(IActionListDrawer drawer, float baseWidth, float maxWidth, float minWidth) {
            EditorGUILayout.LabelField("如果拥有" + TargetItem);
            DrawAction(OnHas, drawer, baseWidth, maxWidth, minWidth);
            EditorGUILayout.LabelField("当未拥有" + TargetItem);
            DrawAction(OnNot, drawer, baseWidth, maxWidth, minWidth);
        }
#endif
    }
}
