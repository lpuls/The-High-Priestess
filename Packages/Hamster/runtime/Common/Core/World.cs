using System.Reflection;
using UnityEngine;

namespace Hamster {
    public class World : MonoBehaviour {

        private static World _instance = null;

        public static T GetWorld<T>() where T : World {
            return _instance as T; 
        }

        protected void ActiveWorld() {
            _instance = this;
        }


        public ConfigHelper ConfigHelper {
            get;
            protected set;
        }

        public UIManager UIManager {
            get;
            protected set;
        }

        protected virtual void InitWorld(Assembly configAssembly = null, Assembly uiAssembly = null, Assembly gmAssemlby = null) {
            // 初始化GM组件
            if (null != gmAssemlby)
                GMAttributeProcessor.Processor(gmAssemlby);

            // 初始化资源
            Asset.UseAssetBundle = false;
            Asset.AssetBundleBasePath = Application.dataPath + "/../AssetBundle/Win";
            Asset.Initialize("AssetBundleConfig", new string[] { "Win" });

            // 初始化配置文件
            if (null != configAssembly) {
                TextAsset textAsset = Asset.Load<TextAsset>("Res/Config");
                ConfigHelper.Initialize(textAsset.bytes, configAssembly);
            }

            // 初始化UI组件
            if (null != uiAssembly) {
                UIManager.Initialize(uiAssembly);
            }
        }
    }
}