using UnityEngine;

namespace Hamster.TouchPuzzle {

    public interface ITouchProcessor {

        void OnTouchDown(GameObject gameObject);
        void OnTouchUp(GameObject gameObject);
        void OnClick(GameObject gameObject);
        float GetMaxRayDistance();
        int GetTouchLayer();
    }

    public class TouchRaycaster : MonoBehaviour {
        public Camera MainCamera = null;
        public float ClickDelta = 0.1f;
        public ITouchProcessor TouchProcessor = null;

        private float _lastDownTime = 0;

        public void Update() {
            if (null == MainCamera || null == TouchProcessor) {
                UnityEngine.Debug.LogError("Invalid MainCamera or TouchProcessor");  
                return;
            }

            Ray screenRay = MainCamera.ScreenPointToRay(Input.mousePosition);
            Vector2 touchBegin = new Vector2(screenRay.origin.x, screenRay.origin.y);
            RaycastHit2D hitResult = Physics2D.Raycast(touchBegin, Vector2.zero, TouchProcessor.GetMaxRayDistance(), TouchProcessor.GetTouchLayer());

            if (Input.GetMouseButtonDown(0)) {
                if (null != hitResult.collider) {
                    TouchProcessor.OnTouchDown(hitResult.collider.gameObject);
                }

                _lastDownTime = Time.realtimeSinceStartup;
            }
            if (Input.GetMouseButtonUp(0)) {
                if (null != hitResult.collider) {
                    TouchProcessor.OnTouchDown(hitResult.collider.gameObject);

                    float time = Time.realtimeSinceStartup;
                    if (time - _lastDownTime <= ClickDelta) {
                        TouchProcessor.OnClick(hitResult.collider.gameObject);
                        _lastDownTime = 0;
                    }
                }

            }
        }
    }
}
