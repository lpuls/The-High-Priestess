using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamster.TouchPuzzle {
    public class GameEventProps : Props {

        public Action<int> OnClickDownEvent;
        public Action<int> OnClickUpEvent;
        public Action<int> OnClickEvent;

        public override void OnClickDown(int propID) {
            OnClickDownEvent?.Invoke(propID);
        }

        public override void OnClickUp(int propID) {
            OnClickUpEvent?.Invoke(propID);
        }

        public override void OnClick(int propID) {
            OnClickEvent?.Invoke(propID);
        }

    }
}
