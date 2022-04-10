using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hamster.TouchPuzzle {
    public class ItemBoxUI : MonoBehaviour {
        public Action<ItemBoxUI> OnSelectItem;

        private Image _icon = null;
        private Text _itemName = null;
        private Button _button = null;

        private int _id = 0;
        private bool _select = false;

        public void Awake() {
            _icon = transform.GetComponentInChild<Image>("ItemIcon");
            _itemName = transform.GetComponentInChild<Text>("ItemName");
            _button = transform.GetComponent<Button>();

            _button.onClick.AddListener(OnClickItem);
            Clean();
        }

        private void OnClickItem() {
            // _select = !_select;
            SetSelect(!_select);
            // World.GetWorld<TouchPuzzeWorld>().SetUsingItem(_select ? _id : 0);
            // _icon.color = _select ? Color.black : Color.white;
            OnSelectItem?.Invoke(this);
        }

        public void SetSelect(bool value) {
            _select = value;
            World.GetWorld<TouchPuzzeWorld>().SetUsingItem(_select ? _id : 0);
            _icon.color = _select ? Color.black : Color.white;
        }

        public void Clean() {
            _id = 0;
            _select = false;
            _icon.color = _select ? Color.black : Color.white;
            _icon.enabled = false;
            _itemName.enabled = false;
        }

        public void SetIcon(int id, string itemName, Sprite sprite) {
            _icon.enabled = true;
            _itemName.enabled = true;

            _id = id;
            _itemName.text = itemName;
            _icon.sprite = sprite;
            _icon.SetNativeSize();
        }
    }
}
