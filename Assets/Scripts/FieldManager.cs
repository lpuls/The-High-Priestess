using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class FieldManager {

        private int _currentID = 0;
        private IField _current = null;
        private Dictionary<int, IField> _fields = new Dictionary<int, IField>(new Int32Comparer());

        private Action _onLoadFirstComplete = null;
        private event Action<IField> _onGotoFiled;

        public void BindOnGotoField(Action<IField> callback) {
            _onGotoFiled += callback;
        }

        public void UnbindOnGotoField(Action<IField> callback) {
            _onGotoFiled -= callback;
        }

        public void Register(IField field) {
            if (!_fields.ContainsKey(field.GetID()))
                _fields.Add(field.GetID(), field);
        }

        public void Unregister(IField field) {
            if (_fields.ContainsKey(field.GetID()))
                _fields.Remove(field.GetID());
        }


        public int GetCurrentID() {
            return _currentID;
        }

        protected void SetCurrentID(int id) {
            _currentID = id;
        }

        public void GoTo(int fieldID) {
            if (null != _current)
                _current.OnLeave();

            if (_fields.TryGetValue(fieldID, out IField nextField)) {
                nextField.OnEnter();
                _onGotoFiled?.Invoke(nextField);
                _current = nextField;
                _currentID = _current.GetID();
            }
            else {
                _currentID = 0;
                _current = null;
                Debug.LogError("Can't Find Field By " + fieldID);
            }
        }

        public void LoadFieldByArray(List<string> path, Action OnLoadFirstComplete) {
            _onLoadFirstComplete = OnLoadFirstComplete;

            // 没有存档的情况下，默认第一张图为起始图 
            if (0 == GetCurrentID())
                SetCurrentID(1);

            for (int i = 0; i < path.Count; i++) {
                Asset.LoadSync(path[i], OnLoadFieldComplete, 1);
            }
        }

        private void OnLoadFieldComplete(UnityEngine.Object fieldObject) {
            GameObject gameObject = fieldObject as GameObject;
            if (null == gameObject)
                return;

            Field field = gameObject.GetComponent<Field>();
            if (null == field) {
                return;
            }
            field.Init();
            Register(field);

            if (GetCurrentID() == field.GetID()) {
                GoTo(field.GetID());
                _onLoadFirstComplete.Invoke();
            }
        }

    }
}
