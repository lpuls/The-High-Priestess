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
        KeyInVase = 22,             // 花瓶中的钥匙


        Max
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
        RemainPhoto,                    // 遗像
        FlowerInVase,                   // 花瓶
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

        FurnaceSandalwood,  // 香炉上香的数量
        PhotoCandle,        // 从遗像上拿到蜡烛

    }


    public enum ESaverID {
        None,
        BLOACK_BOARD_ID = 1,
        ITEM_MANAGER_ID = 2,
        FIELD_MANAGER_ID = 3
    }

    public enum ESaveKey {
        None,
        FIELD_DETIAL_TRAIBUTE_TABLE_LEFT_UP_CANDLE = 1,         // 大贡桌左侧抽屉的蜡烛
        FIELD_DETIAL_TRAIBUTE_TABLE_RIGHT_UP_PHOTO_PARTY = 2,   // 大贡桌左侧抽屉的照片碎片
        FIELD_DETIAL_TRAIBUTE_TABLE_MIDDLE_CAKE = 2,            // 中间贡桌的糕点
        FIELD_DETAIL_RIGHT_PAPER_CHILD_MATCHES = 3,             // 右侧纸人小孩火柴

        NECKLACE_IN_PHOTO_FRAME = 4,                            // 照片上的项链

        PHOTO_PARTY_1 = 10,                                     // 照片碎片1
        PHOTO_PARTY_2 = 11,                                     // 照片碎片2
        PHOTO_FRAME_READY_1 = 12,                               // 拼好照片碎片1
        PHOTO_FRAME_READY_2 = 13,                               // 拼好照片碎片2


        SANDAL_WOOD = 15,                                       // 香

        PAPER_CHILD_MATCHES = 20,                               // 小孩纸人 火柴

        EMPTY_BOWL = 21,                                        // 空碗
        KNIFE_COBINET = 22,                                     // 柜子里的刀
        KEY_IN_VASE = 23,                                       // 花瓶中的钥匙

        VASE_HAS_BLOOD = 30,                                    // 花瓶中是否放血了
        VASE_KEY_BE_TAKE = 31,                                  // 花瓶中是否被拿了
        OPEN_DRAWER_BY_VASE_KEY = 32,                           // 贡桌中间的抽屉是否用钥匙打开了
        MIDDLE_DRAWER_DEED = 33,                                // 贡桌中间的抽屉中的房产证

        // 修改了存档key之后补入
        MOVE_ASH = 100,                                         // 移动烧金炉的灰
        SETUP_CANDLE_LEFT = 101,                                // 左侧烛台是否设置蜡烛
        SETUP_CANDLE_RIGHT = 102,                               // 右侧烛台是否设置蜡烛
        FIRE_CANDLE_LEFT = 103,                                 // 左侧烛台是否设置蜡烛
        FIRE_CANDLE_RIGHT = 104,                                // 右侧烛台是否设置蜡烛
        FURNACE_SANDALWOOD_COUNT = 105,                         // 香炉上的香的数量
        CHILD_FOOD = 106,                                       // 小孩纸人得到食物
        CHILD_MATCHES = 107,                                    // 小孩纸人拿到火柴
        WOMAN_NECKLACE = 108,                                   // 女人纸人得到项链
        WOMAN_SANDALWOOD = 109,                                 // 女人纸人拿出香
        MAN_KNIFE = 110,                                        // 男人纸人拿到刀
        MAN_MONEY = 111,                                        // 男人纸人拿到房产证
        MAN_KILL = 112,                                         // 男人纸人杀人
        MAN_SANDALWOOD = 113,                                   // 男人纸人拿出香
        PLAYER_LEAVE_RIGHT = 114,                               // 玩家离开纸人的房间
        TAKE_BLOOD = 115,                                       // 从死亡的女人纸人身上得到血
        REMAIN_PHOTO_CANDLE = 116,                              // 从遗像上拿到蜡烛
        FAMILY_PHOTO_READLY = 117,                              // 全家福照片集刘
        LOW_COBINET_LEFT_DOOR = 118,                            // 左侧矮柜左边柜门
        LOW_COBINET_Right_DOOR = 119,                           // 左侧矮柜右边柜门
        CANDLE_COUNT = 120,                                     // 点燃蜡烛数量

        LEFT_PUPPET_POSE = 121,                                 // 左侧人偶pose
        RIGHT_PUPPET_POSE = 122,                                // 右侧人偶pose
        EXIT_DOOR_OPEN_LEFT = 123,                              // 出口左大门
        EXIT_DOOR_OPEN_RIGHT = 124,                             // 出口右大门

        WITCH_LAUGHT = 125,                                     // 是否发出女巫笑

        NEW_BEGIN,                                              // 新开始
        EXIT_SAVE,                                              // 有过存档                              
        PASS_GAME,                                              // 是否通关
        GAME_WIN,                                               // 游戏结束
    }

    public static class CommonString {
        public const string LOCK_DOOR = "上锁了";
        public const string LOCK_EXIT_DOOR = "太重了，无法推动";
        public const string WOMAN_WHISPER = "那个祖母绿……";
        public const string CHILD_WHISPER = "好饿……";
        public const string MAN_WHISPER = "再搞不到钱的话……";
        public const string MAN_WHISPER_KILL = "那个宝石!";
        public const string MAN_WHISPER_AFTER_KILL = "还是不够!";
        public const string NEWSPAPER_TITLE = "市年一老人被入室抢动杀害";
        public const string NEWSPAPER_CONTENT = "老人被杀，凶手不明";
        public const string MAIL_CONTENT_1 = "很抱歉地通知您";
        public const string MAIL_CONTENT_2 = "该祖母绿为假宝石";
        public const string VASE_ROOT = "长根了，难以拿出来";
        public const string NEWSPAPER = "我市一妇人被入室抢劫杀害<size=20>我市于今夜凌晨市效外一独幢发生一起入室抢劫，凶手不明，只知道是左撇子</size>";
        public const string LETTER = "经机构鉴定，祖母绿宝石为假";
    }

    public enum ESoundEffectID {
        PickItem,           // 拾取道具
        OpenLock,           // 打开锁
        OpenExitDoor,       // 打开大门
        WomanBeKill,        // 女人被杀
        WaterBottle,        // 水声
        WitchLaught         // 女巫笑
    }

}
