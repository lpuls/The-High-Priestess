namespace Hamster.TouchPuzzle {
    public enum EPropID {
        None,
        Candle,
        CandleStick,
        Matches,
        Cake,
        Door,
        PhotoPart1 = 6,
        PhotoPart2,
        PhotoPart3,
        PhotoPart4,
        PhotoFrame,
        TributeTableLitterDrawer,
    }

    public enum EFieldID {
        None,
        Middle,
        Right,
        Left,
        ExitDoor,
        DetailTraibuteTableLeftUp,
        DetailTraibuteTableRightUp,
        DetailPhotoFrame
    }

    public enum EBlackBoardKey {
        None,
        System = 1,
        Prop = 2,
        Event = 3,
        Save = 4
    }

    public enum EEventKey {
        None,
        CandleCount
    }

    public enum ESystemBlackboardKey {
        None,
        ITEM_MANAGER = 1,
    }

    public enum ESaverID {
        None,
        BLOACK_BOARD_ID = 1,
        ITEM_MANAGER_ID = 2
    }

    public enum ESaveKye {
        None,
        FIELD_DETIAL_TRAIBUTE_TABLE_LEFT_UP_CANDLE = 1,  // 大贡桌左侧抽屉的蜡烛
        FIELD_DETIAL_TRAIBUTE_TABLE_RIGHT_UP_PHOTO_PARTY = 2, // 大贡桌左侧抽屉的照片碎片
        FIELD_DETIAL_TRAIBUTE_TABLE_MIDDLE_CAKE = 2, // 中间贡桌的糕点
        FIELD_DETAIL_RIGHT_PAPER_CHILD_MATCHES = 3,  // 右侧纸人小孩火柴
    }

    public static class CommonString {
        public const string LOCK_DOOR = "上锁了";
    }

}
