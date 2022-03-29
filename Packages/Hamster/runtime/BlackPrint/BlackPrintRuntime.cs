
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Hamster.BP {
    public enum EBPActionResult {
        Normal,
        Block,
        Async
    }

    public class BlackPrintRuntime {
        public delegate bool CheckCondition(BlackPrintPage owner, BlackPrintCondition Codition);
        public delegate EBPActionResult ExecuteAction(BlackPrintPage owner, BlackPrintAction Args);

        public Dictionary<Type, ExecuteAction> ActionDict = new Dictionary<Type, ExecuteAction>();
        public Dictionary<Type, CheckCondition> ConditionDict = new Dictionary<Type, CheckCondition>();

        public void RegisterAction<T>(ExecuteAction action) where T : BlackPrintAction {
            ActionDict.Add(typeof(T), action);
        }

        public EBPActionResult CallAction(BlackPrintPage owner, BlackPrintAction args) {
            if (ActionDict.TryGetValue(args.GetType(), out ExecuteAction callback)) {
                return callback.Invoke(owner, args);
            }
            else {
                throw new Exception("Can't Find Action By " + args.GetType());
            }
        }

        public void RegisterCondition<T>(CheckCondition condition) where T : BlackPrintCondition {
            ConditionDict.Add(typeof(T), condition);
        }

        public bool CallCondition(BlackPrintPage owner, BlackPrintCondition args) {
            if (ConditionDict.TryGetValue(args.GetType(), out CheckCondition callback)) {
                return callback.Invoke(owner, args);
            }
            else {
                throw new Exception("Can't Find Condition By " + args.GetType());
            }
        }

        private void RegisterActionAndCondition(Assembly assembly) {
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; i++) {
                Type classType = types[i];
                if (classType.IsSubclassOf(typeof(BlackPrintAction))) {

                }
                else if (classType.IsSubclassOf(typeof(BlackPrintCondition))) {

                }
            }
        }
    }
}