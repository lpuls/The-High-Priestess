using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hamster.TouchPuzzle {

    public class ShowMessageBoxMessage : IPool, IMessage {
        public string Message = string.Empty;

        public void Reset() {
            Message = string.Empty;
        }
    }

    public class MainUIPanel : MonoBehaviour {

        private Button _leftButton = null;
        private Button _rightButton = null;
        private Button _backButton = null;
        private Field _currentField = null;

        private List<ItemBoxUI> _itemUIs = new List<ItemBoxUI>(8);

        public Text _messageBoxText = null;
        public Animator _messageBoxAnimator = null;

        private void Start() {
            _leftButton = transform.GetComponentInChild<Button>("Left");
            _rightButton = transform.GetComponentInChild<Button>("Right");
            _backButton = transform.GetComponentInChild<Button>("Back");

            _leftButton.onClick.AddListener(OnClickLeft);
            _rightButton.onClick.AddListener(OnClickRight);
            _backButton.onClick.AddListener(OnClickBack);

            for (int index = 0; index < 7; index++) {
                ItemBoxUI boxUI = transform.GetComponentInChild<ItemBoxUI>("Items/Item" + index);
                if (null == boxUI)
                    break;
                _itemUIs.Add(boxUI);
            }

            Single<FieldManager>.GetInstance().BindOnGotoField(OnGotoField);
            World.GetWorld<TouchPuzzeWorld>().ItemManager.BindChangeCallback(OnItemChange);
            World.GetWorld<TouchPuzzeWorld>().MessageManager.Bind<ShowMessageBoxMessage>(OnReceiveShowMessage);
        }

        private void OnDestroy() {
            Single<FieldManager>.GetInstance().UnbindOnGotoField(OnGotoField);
        }

        private void OnGotoField(IField field) {
            Field fieldInst = field as Field;
            _currentField = fieldInst;
            if (null == _currentField) {
                Debug.LogError("Go to Filed is null");
                return;
            }

            _leftButton.gameObject.SetActive(fieldInst.IsValidLeftID());
            _rightButton.gameObject.SetActive(fieldInst.IsValidRightID());
            _backButton.gameObject.SetActive(fieldInst.IsValidBackID());
        }

        private void OnClickLeft() {
            if (null == _currentField) {
                Debug.LogError("Filed is null");
                return;
            }
            GotoField(_currentField.GetLeftID());
        }

        private void OnClickRight() {
            if (null == _currentField) {
                Debug.LogError("Filed is null");
                return;
            }
            GotoField(_currentField.GetRightID());
        }

        private void OnClickBack() {
            if (null == _currentField) {
                Debug.LogError("Filed is null");
                return;
            }
            GotoField(_currentField.GetBackID());
        }

        private void GotoField(int id) {
            World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.Execute(()=> {
                Single<FieldManager>.GetInstance().GoTo(id);
                World.GetWorld<TouchPuzzeWorld>().TransitionsPanel.SetComplete();
            });
        }

        private void OnItemChange(int id, int index, bool isAdd) {
            if (isAdd) {
                if (!World.GetWorld<TouchPuzzeWorld>().ConfigHelper.TryGetConfig<Config.Props>(id, out Config.Props props))
                    return;
                Sprite sprite = World.GetWorld<TouchPuzzeWorld>().AtlasManager.GetSprite("Res/ItemAtlas", props.Icon);
                if (null == sprite) {
                    Debug.LogError("Can't Find Sprite By Res/ItemAtlas, Candle");
                    return;
                }
                SetItemIcon(index, id, props.Name, sprite);
            }
            else {
                CleanItem(index);
            }
        }

        public void SetItemIcon(int index, int id, string itemName, Sprite sprite) {
            if (index >= 0 && index < _itemUIs.Count) {
                _itemUIs[index].SetIcon(id, itemName, sprite);
            }
        }

        public void CleanItem(int index) {
            if (index >= 0 && index < _itemUIs.Count) {
                _itemUIs[index].Clean();
            }
        }

        public void OnReceiveShowMessage(ShowMessageBoxMessage message) {
            if (null != _messageBoxText)
                _messageBoxText.text = message.Message;
            _messageBoxAnimator.Play("Show", 0, 0);
            //_messageBoxAnimator.SetTrigger("Show");
        }
    }
}
