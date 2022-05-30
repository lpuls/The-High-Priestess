using System;
using System.Collections;
using UnityEngine;

namespace Hamster.TouchPuzzle {

    public class TextPanel : MonoBehaviour {
        public GameObject BeginText = null;
        public GameObject EndText = null;
        public GameObject ProductionPersonnelList = null;
        public Animator TextAnimator = null;

        public void HideAll() {
            gameObject.SetActive(false);
            BeginText.SetActive(false);
            EndText.SetActive(false);
            ProductionPersonnelList.SetActive(false);
        }

        public void ShowBeginText() {
            HideAll();
            gameObject.SetActive(true);
            BeginText.SetActive(true);
            TextAnimator.Play("Begin");
        }

        public void ShowEndText() {
            HideAll();
            gameObject.SetActive(true);
            EndText.SetActive(true);
            TextAnimator.Play("End");
        }

        public void ShowProductionPersonnelListText() {
            HideAll();
            gameObject.SetActive(true);
            ProductionPersonnelList.SetActive(true);
        }

        public void OnAnimationPlayComplete() {
            HideAll();
            World.GetWorld<TouchPuzzeWorld>().OnShowTextComplete();
        }
    }
}