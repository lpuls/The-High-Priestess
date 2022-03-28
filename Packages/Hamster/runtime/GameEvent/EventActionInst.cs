using System.Collections.Generic;
using UnityEngine;

public class EventActionInst : ScriptableObject {
    public string Name = string.Empty;
    public string Desc = string.Empty;

    public GameObject Owner = null;
    public EventActionBlackboard Blackboard = new EventActionBlackboard();
    public List<EventActionPage> Pages = new List<EventActionPage>();

    private int ValidPageIndex = -1;

    public void Initialize() {
        for (int i = 0; i < Pages.Count; i++) {
            Pages[i].Owner = this;
            Pages[i].Initialize();
        }
    }

    public void Reset() {
        ValidPageIndex = -1;
        for (int i = 0; i < Pages.Count; i++) {
            Pages[i].Reset();
        }
    }

    public void Execute() {
        if (ValidPageIndex < 0) {
            for (int i = 0; i < Pages.Count; i++) {
                EventActionPage page = Pages[i];
                if (!page.CheckCondition()) {
                    break;
                }
                ValidPageIndex = i;
            }
        }

        if (ValidPageIndex >= 0 && ValidPageIndex < Pages.Count) {
            Pages[ValidPageIndex].Execute();
        }
    }
}
