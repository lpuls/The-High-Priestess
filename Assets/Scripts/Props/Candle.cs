using UnityEngine;

namespace Hamster.TouchPuzzle {
    public class Candle : Props {
        public bool IsSetup {
            get;
            set;
        }

        public override void OnClick(int propID) {
            if (IsSetup) {
                if (propID != (int)EPropID.Matches)
                    return;

                // todo 点燃
            }
            else {
                World.GetWorld<TouchPuzzeWorld>().ItemManager.AddItem((int)EPropID.Candle);
                Destroy();
            }
        }
    }
}
