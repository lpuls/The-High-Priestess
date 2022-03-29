using System.Collections.Generic;
using UnityEngine;

namespace Hamster.BP {
    public class BlackPrintInst : ScriptableObject {
        public string Name = string.Empty;
        public string Desc = string.Empty;

        public GameObject Owner {
            get; protected set;
        }
        public EventActionBlackboard Blackboard = new EventActionBlackboard();
        public List<BlackPrintPage> Pages = new List<BlackPrintPage>();

        private int ValidPageIndex = -1;

        public void Initialize(GameObject owner) {
            Owner = owner;
            for (int i = 0; i < Pages.Count; i++) {
                Pages[i].Initialize(this);
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
                    BlackPrintPage page = Pages[i];
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
}