using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class VaseFlower : Props {

        public Color FlowerColor = Color.white;
        public SpriteRenderer FlowerSprite = null;

        public override void OnEnterField() {
            base.OnEnterField();

            if (World.GetWorld().GetManager<Blackboard>().TryGetValue((int)ESaveKey.VASE_HAS_BLOOD, out int _)) {
                FlowerSprite.color = FlowerColor;
            }
        }
    }
}