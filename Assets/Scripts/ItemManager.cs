using System;
using System.Collections.Generic;

namespace Hamster.TouchPuzzle {
    public class ItemManager {
        private event Action<int, int, bool> _onItemChange;
        private List<int> _items = new List<int>(8);

        public ItemManager() {
            for (int i = 0; i < 7; i++) {
                _items.Add(0);
            } 
        }

        public void AddItem(int id) {
            for (int i = 0; i < _items.Count; i++) {
                if (0 == _items[i]) {
                    _items[i] = id;
                    _onItemChange?.Invoke(id, i, true);
                    break;
                }
            }
        }

        public void RemoveItem(int id) {
            int index = _items.IndexOf(id);
            _items[index] = 0;
            _onItemChange?.Invoke(id, index, false);
        }

        public bool HasItem(int id) {
            return _items.Contains(id); 
        }

        public void BindChangeCallback(Action<int, int, bool> callback) {
            _onItemChange += callback;
        }

        public void UnbindChangeCallback(Action<int, int, bool> callback) {
            _onItemChange -= callback;
        }
    }
}
