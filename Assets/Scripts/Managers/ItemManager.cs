using System;
using System.Collections.Generic;

namespace Hamster.TouchPuzzle {
    public class ItemManager {
        public const int MAX_ITEM_BOX = 8;

        private event Action<int, int, bool> _onItemChange;
        protected List<int> _items = new List<int>(MAX_ITEM_BOX);

        public int UsingItemKey { get; private set; }
        public int UsingItemIndex { get; private set; }

        public ItemManager() {
            for (int i = 0; i < MAX_ITEM_BOX; i++) {
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

        public void RemoveItemByIndex(int index) {
            if (index >= 0 && index < _items.Count)
                RemoveItem(_items[index]);
        }

        public void SetUsingItem(int id, int index) {
            UsingItemKey = id;
            UsingItemIndex = index;
        }

        public bool HasItem(int id) {
            return _items.Contains(id); 
        }

        public void RemoveUsingItem() {
            RemoveItemByIndex(UsingItemIndex);
            SetUsingItem(0, -1);
        }

        public void BindChangeCallback(Action<int, int, bool> callback) {
            _onItemChange += callback;
        }

        public void UnbindChangeCallback(Action<int, int, bool> callback) {
            _onItemChange -= callback;
        }

        public void Reset() {
            for (int i = 0; i < MAX_ITEM_BOX; i++) {
                _items[i] = 0;
                _onItemChange?.Invoke(0, i, false);
            }
            UsingItemKey = 0;
            UsingItemIndex = -1;
        }
    }
}
