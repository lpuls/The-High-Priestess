using System.Collections.Generic;
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
        public FieldManager FieldManager = new FieldManagerForSave();

        public List<string> LoadFieldName = new List<string>(32);

        public void Awake() {
            ActiveWorld();

            RegisterManager<Blackboard>(Blackboard);

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
                ItemManager as ItemManagerForSave,
                FieldManager as FieldManagerForSave);

            // 加载图集
            AtlasManager.LoadAtlas("Res/ItemAtlas");
        }

        public void OnTouchDown(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props && props.enabled) {
                props.OnClickDown(ItemManager.UsingItemKey);
            }
        }

        public void OnTouchUp(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props && props.enabled) {
                props.OnClickDown(ItemManager.UsingItemKey);
            }
        }

        public void OnClick(GameObject gameObject) {
            Props props = gameObject.GetComponent<Props>();
            if (null != props && props.enabled) {
                props.OnClick(ItemManager.UsingItemKey);
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
            FieldManager.LoadFieldByArray(LoadFieldName, () => {
                TransitionsPanel.SetComplete();
            });
        }

        public void SetUsingItem(int id, int index) {
            ItemManager.SetUsingItem(id, index);
        }

        public void RemoveCurrentUsingItem() {
            ItemManager.RemoveUsingItem();
        }

        public static int GetCandleCountKey() {
            return (int)ESaveKey.CANDLE_COUNT;
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
