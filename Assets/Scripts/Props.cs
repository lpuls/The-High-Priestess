using System;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class Props : MonoBehaviour, IProps {

        protected IField _filed = null;

        public virtual void Init(IField field) {
            _filed = field;
        }

        public virtual void Finish() {
            _filed = null;
        }

        public virtual void OnClickDown(int propID) {
        }

        public virtual void OnClickUp(int propID) {
        }

        public virtual void OnClick(int propID) {
        }

        public virtual void OnEnterField() {
        }

        public virtual void OnLeaveField() {
        }

        public virtual void Destroy() {
            _filed.DestoryProp(this);
            GameObject.Destroy(gameObject);
        }

    }
}
