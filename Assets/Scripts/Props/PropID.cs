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
        
        Necklace = 10,
        Ash= 11,

        Sandalwood = 15,
    }

    public enum EFieldID {
        None,
        Middle,
        Right,
        Left,
        ExitDoor,
        DetailTraibuteTableLeftUp,
        DetailTraibuteTableRightUp,
        DetailPhotoFrame,
        UnderTable
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
        CandleCount,

        ChildFood,
        WomanNecklace,
        ManKnife,
        ManMoney

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

        NECKLACE_IN_PHOTO_FRAME = 4,  // 照片上的项链

        PHOTO_PARTY_1 = 10,  // 照片碎片1
        PHOTO_PARTY_2 = 11,  // 照片碎片1

        SANDAL_WOOD = 15,  // 香

    }

    public static class CommonString {
        public const string LOCK_DOOR = "上锁了";
    }

}
