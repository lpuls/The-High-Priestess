using System.Reflection;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class TouchPuzzeWorld : World, ITouchProcessor {
        public TouchRaycaster TouchRaycaster = null;
        public TransitionsPanel TransitionsPanel = null;
        public AtlasManager AtlasManager = new AtlasManager();
        public ItemManager ItemManager = new ItemManagerForSave();
        public Blackboard Blackboard = new BlackboardForSave();
        public MessageManager MessageManager = new MessageManager();
        public SaveHelper SaveHelper = null;

        private int _usingItemID = 0;
        private int _usingItemIndex = 0;

        public void Awake() {
            ActiveWorld();

            InitWorld();
            TouchRaycaster.TouchProcessor = this;

            TransitionsPanel.gameObject.SetActive(true);
            TransitionsPanel.Execute(BeginLoad);
        }

        protected override void InitWorld(Assembly configAssembly = null, Assembly uiAssembly = null, Assembly gmAssemlby = null) {
            // 初始化配置
            ConfigHelper = new ConfigHelper();
            base.InitWorld(typeof(Config.Props).Assembly, uiAssembly, GetType().Assembly);

            // 初始化存档管理器
            Debug.Log("存档位置在: " + Application.persistentDataPath);
            SaveHelper = new SaveHelper(Application.persistentDataPath + "/Save.save", // @"H:\Dev\Demo\HighPriestess\Dava.sav",
                0, 
                Blackboard as BlackboardForSave, 
                ItemManager as ItemManagerForSave);

            // 加载图集
            AtlasManager.LoadAtlas("Res/ItemAtlas");
        }

        public void OnTouchDown(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props && props.enabled) {
                props.OnClickDown(_usingItemID);
            }
        }

        public void OnTouchUp(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props && props.enabled) {
                props.OnClickDown(_usingItemID);
            }
        }

        public void OnClick(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props && props.enabled) {
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
            // 加载存档
            SaveHelper.Load();

            // 加载场景
            LoadField("Res/Fields/Field_Middle", true);
            LoadField("Res/Fields/Field_Right");
            LoadField("Res/Fields/Field_Left");
            LoadField("Res/Fields/FieldDetailTraibuteTableLeftUp");
            LoadField("Res/Fields/FieldDetailTraibuteTableRightUp");
            LoadField("Res/Fields/Field_PhoteFrame");
            LoadField("Res/Fields/Field_UnderTable");
            LoadField("Res/Fields/Field_PaperChild");
            LoadField("Res/Fields/Field_PaperWoman");
            LoadField("Res/Fields/Field_PaperMan");
            LoadField("Res/Fields/Field_PaperWomanDead");
            LoadField("Res/Fields/Field_OldManRemain");
            LoadField("Res/Fields/Filed_Flower");
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

        public void SetUsingItem(int id, int index) {
            _usingItemID = id;
            _usingItemIndex = index;
        }

        public void RemoveCurrentUsingItem() {
            ItemManager.RemoveItemByIndex(_usingItemIndex);
        }

        public static int GetBlockboardKey(int blackBoardTypeKey, int MainKey, int ID, int value) {
            return (blackBoardTypeKey << 24) | (MainKey << 16) | (ID << 8) | value;
        }

        public static int GetCandleCountKey() {
            return TouchPuzzeWorld.GetBlockboardKey((int)EBlackBoardKey.Event, (int)EEventKey.CandleCount, 0, 0);
        }

        #region GM
        [GM]
        public static void GM_AddItem(string[] args) {
            for (int i = 1; i < args.Length; i++) {
                if (int.TryParse(args[i], out int value))
                    World.GetWorld<TouchPuzzeWorld>().ItemManager.AddItem(value);
            }
            
        }

        [GM]
        public static void GM_Save(string[] args) {
            World.GetWorld<TouchPuzzeWorld>().SaveHelper.Save();
        }

        [GM]
        public static void GM_Del(string[] args) {
            World.GetWorld<TouchPuzzeWorld>().SaveHelper.Delete();
        }
        #endregion

    }

}
