using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class FieldManager {
        private IField _current = null;
        private Dictionary<int, IField> _fields = new Dictionary<int, IField>(new Int32Comparer());

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

        public void GoTo(int fieldID) {
            if (null != _current)
                _current.OnLeave();

            if (_fields.TryGetValue(fieldID, out IField nextField)) {
                nextField.OnEnter();
                _onGotoFiled?.Invoke(nextField);
                _current = nextField;
            }
            else {
                Debug.LogError("Can't Find Field By " + fieldID);
            }
        }
    }
}
