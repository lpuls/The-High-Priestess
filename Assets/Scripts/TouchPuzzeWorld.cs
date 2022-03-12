using System.Collections;
using System.Reflection;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class TouchPuzzeWorld : World, ITouchProcessor {

        public TouchRaycaster TouchRaycaster = null;
        public TransitionsPanel TransitionsPanel = null;
        public AtlasManager AtlasManager = new AtlasManager();
        public ItemManager ItemManager = new ItemManager();

        private int _usingItemID = 0;

        public void Awake() {
            ActiveWorld();

            InitWorld();
            TouchRaycaster.TouchProcessor = this;

            TransitionsPanel.gameObject.SetActive(true);
            TransitionsPanel.Execute(BeginLoad);
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

        private void LoadField(string path, bool isFirst=false) {
            if (isFirst) {
                // TransitionsPanel.Execute();
                Asset.LoadSync(path, OnLoadFirstFieldComplete, 1);
            }
            else {
                Asset.LoadSync(path, OnLoadFieldComplete, 1);
            }
        }

        public void SetUsingItem(int id) {
            _usingItemID = id; 
        }
    }


}
