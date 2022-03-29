using System;
using System.Collections.Generic;
using UnityEngine;

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

    public virtual void DrawInspector() {
    }

#endif
}
