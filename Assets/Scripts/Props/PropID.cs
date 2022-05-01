namespace Hamster.TouchPuzzle {
    public enum EPropID {
        None,
        Candle,
        CandleStick,                // 烛台
        Matches,                    // 小孩纸人给的火柴
        Cake,                       // 给小孩纸人的蛋糕
        Door,                       // 
        
        PhotoPart1 = 6,             // 照片碎片
        PhotoPart2 = 7,             // 照片碎片
        PhotoPart3 = 8,             // 照片碎片
        PhotoPart4 = 9,             // 照片碎片
        PhotoFrame = 101,           // 全家福
        
        TributeTableLitterDrawer,   // 小贡桌抽屉

        Necklace = 10,              // 项链
        Ash= 11,                    // 金纸灰烬
           
        Sandalwood = 15,            // 香
        Knife = 16,                 // 刀
        Deed = 17,                  // 房产证

        Bowl = 20,                  // 碗
        Blood = 21,                 // 血
    }

    public enum EFieldID {
        None,
        Middle,                         // 房间中央
        Right,                          // 房间右侧
        Left,                           // 房间左侧
        ExitDoor,                       // 房间后侧，出口
        DetailTraibuteTableLeftUp,      // 贡桌左上抽屉
        DetailTraibuteTableRightUp,     // 贡桌右上抽屉
        DetailPhotoFrame,               // 全家福
        UnderTable,                     // 项桌下方
        PaperChild,                     // 小孩纸人
        PaperWoman,                     // 女人纸人
        PaperMan,                       // 男人纸人
        PaperWomanDead,                 // 女人纸人死亡
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

        ChildFood,          // 小孩纸人得到食物
        ChildMatches,       // 小孩纸人拿到火柴
        WomanNecklace,      // 女人纸人得到项链
        WomanSandalwood,    // 女人纸人拿出香
        ManKnife,           // 男人纸人拿到刀
        ManMoney,           // 男人纸人拿到房产证
        ManKill,            // 男人纸人杀人
        ManSandalwood,      // 男人纸人拿出香
        PlayerLeaveRight,   // 玩家离开纸人的房间
        TakeBlood,          // 从死亡的女人纸人身上得到血

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

        PAPER_CHILD_MATCHES = 20,  // 小孩纸人 火柴

        EMPTY_BOWL = 21,        // 空碗
        KNIFE_COBINET = 22,     // 柜子里的刀

    }

    public static class CommonString {
        public const string LOCK_DOOR = "上锁了";
    }

}
