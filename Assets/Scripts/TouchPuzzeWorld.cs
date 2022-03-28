using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class TouchPuzzeWorld : World, ITouchProcessor {
        public TouchRaycaster TouchRaycaster = null;
        public TransitionsPanel TransitionsPanel = null;
        public AtlasManager AtlasManager = new AtlasManager();
        public ItemManager ItemManager = new ItemManager();
        public EventActionBlackboard Blackboard = new EventActionBlackboard();
        public MessageManager MessageManager = new MessageManager();

        private int _usingItemID = 0;

        public void Awake() {
            ActiveWorld();

            InitWorld();
            TouchRaycaster.TouchProcessor = this;

            TransitionsPanel.gameObject.SetActive(true);
            TransitionsPanel.Execute(BeginLoad);

            ItemManager.BindChangeCallback(OnItemChange);
        }

        protected override void InitWorld(Assembly configAssembly = null, Assembly uiAssembly = null, Assembly gmAssemlby = null) {
            ConfigHelper = new ConfigHelper();
            base.InitWorld(typeof(Config.Props).Assembly, uiAssembly, gmAssemlby);

            AtlasManager.LoadAtlas("Res/ItemAtlas");
        }

        public void OnTouchDown(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props) {
                props.OnClickDown(_usingItemID);
            }
        }

        public void OnTouchUp(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props) {
                props.OnClickDown(_usingItemID);
            }
        }

        public void OnClick(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props) {
                props.OnClick(_usingItemID);
            }
        }

        public float GetMaxRayDistance() {
            return 1000.0f;
        }

        public int GetTouchLayer() {
            return 1 << 8;
        }

        private void BeginLoad() {
            LoadField("Res/Fields/Field_Middle", true);
            LoadField("Res/Fields/Field_Right");
            LoadField("Res/Fields/FieldDetailTraibuteTableLeftUp");
            LoadField("Res/Fields/FieldDetailTraibuteTableRightUp");
        }

        private void OnLoadFirstFieldComplete(Object field) {
            OnLoadFieldComplete(field);
            TransitionsPanel.SetComplete();
            Single<FieldManager>.GetInstance().GoTo(1);
        }

        private void OnLoadFieldComplete(Object fieldObject) {
            GameObject gameObject = fieldObject as GameObject;
            if (null == gameObject)
                return;

            Field field = gameObject.GetComponent<Field>();
            if (null == field) {
                return;
            }
            field.Init();
            Single<FieldManager>.GetInstance().Register(field);
        }

        private void LoadField(string path, bool isFirst = false) {
            if (isFirst) {
                Asset.LoadSync(path, OnLoadFirstFieldComplete, 1);
            }
            else {
                Asset.LoadSync(path, OnLoadFieldComplete, 1);
            }
        }

        public void SetUsingItem(int id) {
            _usingItemID = id;
        }

        public static int GetBlockboardKey(int blackBoardTypeKey, int MainKey, int ID, int value) {
            return (blackBoardTypeKey << 24) | (MainKey << 16) | (ID << 8) | value;
        }

        private int GetItemManagerKey(int index) {
            return GetBlockboardKey((int)EBlackBoardKey.System, (int)ESystemBlackboardKey.ITEM_MANAGER, 0, index);
        }

        private void OnItemChange(int id, int index, bool isAdd) {
            Blackboard.SetValue(GetItemManagerKey(index), isAdd ? id : 0);
        }

        public static int GetCandleCountKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.CandleCount, 0, 0);
        }
    }

}
