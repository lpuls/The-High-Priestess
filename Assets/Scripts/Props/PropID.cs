namespace Hamster.TouchPuzzle {
    public enum EPropID {
        None,
        Candle,
        CandleStick,
        Matches,
        Cake,
        Door,
        PhotoPart1,
        PhotoPart2,
        PhotoPart3,
    }

    public enum EFieldID {
        None,
        Middle,
        Right,
        Left,
        ExitDoor,
        DetailTraibuteTableLeftUp,
        DetailTraibuteTableRightUp,
    }

    public enum EBlackBoardKey {
        None,
        System = 1,
        Prop = 2,
        Event = 3
    }

    public enum EEventKey {
        None,
        CandleCount
    }

    public enum ESystemBlackboardKey {
        None,
        ITEM_MANAGER = 1,
    }

    public static class CommonString {
        public const string LOCK_DOOR = "上锁了";
    }

}
