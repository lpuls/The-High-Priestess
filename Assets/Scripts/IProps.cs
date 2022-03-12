using System;
using UnityEngine;

namespace Hamster.TouchPuzzle {
    public interface IProps {
        void Init(IField field);

        void Finish();

        void OnEnterField();

        void OnLeaveField();

        void OnClickDown(int propID);

        void OnClickUp(int propID);

        void OnClick(int propID);

        void Destroy();
    }

    
}
