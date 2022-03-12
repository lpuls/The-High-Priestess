namespace Hamster.TouchPuzzle {

    public interface IField {

        int GetID();

        void OnEnter();

        void OnLeave();

        void DestoryProp(IProps props);
    }
}
