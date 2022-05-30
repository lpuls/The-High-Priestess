using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class TouchPuzzeWorld : World, ITouchProcessor {
        public float MaxSaveTime = 0.5f;

        public TransitionsPanel TransitionsPanel = null;
        public MainUIPanel MainUIPanel = null;
        public BeginGamePanel BeginGamePanel = null;
        public TextPanel TextPanel = null;

        public TouchRaycaster TouchRaycaster = null;
        public AtlasManager AtlasManager = new AtlasManager();
        public ItemManager ItemManager = new ItemManagerForSave();
        public Blackboard Blackboard = new BlackboardForSave();
        public MessageManager MessageManager = new MessageManager();
        public SaveHelper SaveHelper = null;
        public FieldManager FieldManager = new FieldManagerForSave();

        public List<string> LoadFieldName = new List<string>(32);
        public List<AudioClip> SoundEffectClips = new List<AudioClip>(8);

        private float _saveTime = 0.0f;
        private AudioSource CommonPlayer = null;

        public void Awake() {
            ActiveWorld();

            TouchRaycaster.TouchProcessor = this;
            InitWorld();

            // 显示开始游戏
            TextPanel.gameObject.SetActive(true);
            BeginGamePanel.gameObject.SetActive(true);
        }

        protected override void InitWorld(Assembly configAssembly = null, Assembly uiAssembly = null, Assembly gmAssemlby = null) {
            // 初始化系统
            Screen.SetResolution(1920, 1080, true);

            // 初始化配置
            ConfigHelper = new ConfigHelper();
            base.InitWorld(typeof(Config.Props).Assembly, uiAssembly, GetType().Assembly);

            // 注册管理器
            RegisterManager<Blackboard>(Blackboard);
            RegisterManager<ItemManager>(ItemManager);
            RegisterManager<SaveHelper>(SaveHelper);
            RegisterManager<FieldManager>(FieldManager);

            // 初始化UI
            MainUIPanel.InitMainUI();

            // 加载图集
            AtlasManager.Init();
            AtlasManager.LoadAtlas("Res/SpriteAtlas/ItemAtlas");

            // 加载通用音效播放器
            CommonPlayer = GetComponent<AudioSource>();

            // 初始化存档管理器
            Debug.Log("存档位置在: " + Application.persistentDataPath);
            SaveHelper = new SaveHelper(Application.persistentDataPath + "/Save.save", // @"H:\Dev\Demo\HighPriestess\Dava.sav",
                0, 
                Blackboard as BlackboardForSave, 
                ItemManager as ItemManagerForSave,
                FieldManager as FieldManagerForSave);
            SaveHelper.Load();
        }

        protected override void Update() {
            base.Update();

            _saveTime += Time.deltaTime;
            if (_saveTime > MaxSaveTime) {
                _saveTime -= MaxSaveTime;
                SaveHelper.Save();
            }
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

        private void BeginLoadField() {
            // 加载场景
            FieldManager.LoadFieldByArray(LoadFieldName, () => {
                MainUIPanel.gameObject.SetActive(true);

                if (!Blackboard.HasValue((int)ESaveKey.NEW_BEGIN)) {
                    ShowBeginText();
                    TransitionsPanel.gameObject.SetActive(true);
                    TransitionsPanel.Execute(null);

                    Blackboard.SetValue((int)ESaveKey.NEW_BEGIN, 1);
                }
                else {
                    OnShowTextComplete();
                }
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

        public void EnterGame() {
            TransitionsPanel.gameObject.SetActive(true);
            TransitionsPanel.Execute(BeginLoadField);

            BeginGamePanel.gameObject.SetActive(false);
        }

        public bool PlaySoundEffect(int id) {
            if (id >= 0 && id < SoundEffectClips.Count) {
                CommonPlayer.clip = SoundEffectClips[id];
                CommonPlayer.Play();
                return true;
            }
            return false;
        }

        public void ShowMessage(string message) {
            ShowMessageBoxMessage messageInst = ObjectPool<ShowMessageBoxMessage>.Malloc();
            messageInst.Message = message;
            MessageManager.Trigger(messageInst);
            ObjectPool<ShowMessageBoxMessage>.Free(messageInst);
        }

        public void OnShowTextComplete() {
            TextPanel.HideAll();
            TransitionsPanel.SetComplete();
        }

        public void ShowBeginText() {
            TextPanel.ShowBeginText();
        }

        public void ShowEndText() {
            TextPanel.ShowEndText();
        }

        public void ShowProductionPersonnelListText() {
            TextPanel.ShowProductionPersonnelListText();
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
