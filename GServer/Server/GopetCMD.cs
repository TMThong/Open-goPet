
public class GopetCMD
{
    public const sbyte COMMAND_GUIDER = 122;
    public const sbyte ON_UPDATE_PLAYER_IN_MAP = 29;
    public const sbyte ON_OTHER_USER_MOVE = 27;
    public const sbyte INIT_PLAYER = 31;
    public const sbyte ON_PLAYER_ENTER_MAP = 24;
    public const sbyte ON_PLACE_CHAT = 9;
    public const sbyte ON_PLAYER_WARPING = 25;
    public const sbyte ON_PLAYER_EXIT_PLACE = 30;
    public const sbyte ON_PLAYER_GET_CHANNEL_INFO = 7;
    public const sbyte ON_PLAYER_CHANGE_CHANNEL = 24;
    public const sbyte MGO_COMMAND = 42;
    public const sbyte TELE_MENU = 12;
    public const sbyte PET_SERVICE = 81;
    public const sbyte CHAT_PUBLIC = 66;
    public const sbyte PET_INVENTORY = 5;
    public const sbyte CHANGE_NEW_PASSWORD = 93;
    public const sbyte GAME_OBJECT = 34;
    public const sbyte COMMAND_IMAGE = 96;
    public const sbyte NPC_GUIDER = 2;
    public const sbyte NPC_OPTION = 5;
    public const sbyte CREATE_CHAR = 21;
    public const sbyte REQUEST_PET_IMG = 9;
    public const sbyte SEND_LIST_PET_ZONE = 8;
    public const sbyte SEND_LIST_MOB_ZONE = 22;
    public const sbyte MY_PET_INFO = 40;
    public const sbyte REMOVE_MOB = 42;
    public const sbyte STAR_INFO = 94;
    public const sbyte MONEY_INFO = 25;
    public const sbyte SELECT_OPTION = 5;
    public const sbyte SELECT_MENU_ELEMENT = 3;
    public const sbyte SHOW_MENU_ITEM = 8;
    public const sbyte SERVER_MESSAGE = 45;
    public const sbyte SEND_YES_NO = 4;
    public const sbyte ATTACK_MOB = 36;
    public const sbyte PET_BATTLE = 37;
    public const sbyte PET_BATTLE_STATE = 16;
    public const sbyte PET_RECOVERY_HP = 45;
    public const sbyte PET_BATTLE_USE_ITEM = 3;
    public const sbyte PetBattle_ATTACK = 1;
    public const sbyte PET_BATTLE_USE_SKILL = 4;
    public const sbyte UPDATE_PET_LVL = 18;
    public const sbyte MAGIC = 11;
    public const sbyte GYM = 21;
    public const sbyte MAGIC_LEARN_SKILL = 31;
    public const sbyte UP_TIEM_NANG = 19;

    /**
     * SUB CMD Thông tin trang bị của pet
     */
    public const sbyte EQUIP_INFO = 28;

    /**
     * SUB CMD Dùng trang bị của pet
     */
    public const sbyte USE_EQUIP_ITEM = 29;
    public const int MAGIC_INFO = 1;
    public const int MAGIC_SKILL = 3;
    public const sbyte GET_PLAYER_INFO = 55;
    public const sbyte GET_PET_PLAYER_INFO = 0;
    public const sbyte GET_PET_EQUIP_PLAYER_INFO = 1;
    public const sbyte TATTOO = 90;
    public const sbyte TATTOO_INIT_SCREEN = 1;
    public const sbyte TATTOO_ENCHANT_SELECT_MATERIAL = 4;
    public const sbyte TATTOO_ENCHANT = 5;
    public const sbyte TATTOO_ENCHANT_SELECT_MATERIAL1 = 1;
    public const sbyte TATTOO_ENCHANT_SELECT_MATERIAL2 = 2;
    public const sbyte CLAN = 91;
    public const sbyte CLAN_INFO_MEMBER = 3;
    public const sbyte CLAN_INFO = 14;
    public const sbyte DONATE_CLAN = 9;
    public const sbyte REQUEST_SHOP = 2;
    public const sbyte GUIDER_TYPE_PAY = 9;
    public const sbyte CHARGE_MONEY_INFO = 44;
    public const sbyte VERSION = 48;
    public const sbyte UNEQUIP_ITEM = 39;
    public const sbyte SHOW_BIG_TEXT_EFF = 63;
    public const sbyte TIME_PLACE = 64;
    public const sbyte SHOW_UPGRADE_PET = 67;
    public const sbyte WING = 92;
    public const sbyte SEND_SKIN = 61;
    public const sbyte SKIN_INVENTORY = 62;
    public const sbyte PRICE_UPGRADE_PET = 72;
    public const sbyte SELECT_PET_UPGRADE = 68;
    public const sbyte REMOVE_ITEM_EQUIP = 56;
    public const sbyte GUIDER_ANS = 1;
    public const sbyte ANS_YES_OR_NO = 2;
    public const sbyte PET_UPGRADE_ACTIVE = 1;
    public const sbyte PET_UPGRADE_PASSIVE = 2;
    public const sbyte PET_UPGRADE_PET_INFO = 69;
    public const sbyte LOGIN = 1;
    public const sbyte REGISTER = 35;
    public const sbyte CHANGE_PASSWORD = 100;
    public const sbyte CLIENT_INFO = -36;
    public const sbyte LOGIN_SUCCES = 3;
    public const sbyte LOGIN_FAILED = 4;
    public const sbyte BANNER_MESSAGE = 1;
    public const sbyte POPUP_MESSAGE = 5;
    public const sbyte KIOSK = 86;
    public const sbyte REQUEST_SHOP_SKIN = 60;
    public const sbyte SELECT_METERIAL_ENCHANT = 46;
    public const sbyte SELECT_METERIAL_ENCHANT_PET_INFO = 47;
    public const sbyte ENCHANT_ITEM = 48;
    public const sbyte UP_TIER_ITEM = 49;
    public const sbyte PET_UNEQUIP_GEM_ITEM_INFO = 75;
    public const sbyte NORMAL_INVENTORY = 30;
    public const sbyte INFO_UP_TIER_PET = 70;
    public const sbyte PET_UP_TIER = 71;
    public const sbyte SELECT_KIOSK_ITEM = 85;
    public const sbyte TYPE_DIALOG_INPUT = 7;
    public const sbyte REMOVE_SELL_ITEM = 87;
    public const sbyte PLAYER_CHALLENGE = 12;
    public const sbyte PLAYER_PK = 96;
    public const sbyte PLAYER_BATTLE = 59;
    public const sbyte SELECT_ITEM_GEM_TATTO = 2;
    public const sbyte SELECT_ITEM_REMOVE_TATOO = 3;
    public const sbyte GUIDER_IMGDIALOG = 11;
    public const sbyte GEM_INVENTORY = 73;
    public const sbyte SHOW_GEM_INVENTORY = 74;
    public const sbyte GL_MAIL = 20;
    public const sbyte SELECT_GEM_ENCHANT = 80;
    public const sbyte REMOVE_GEM_ITEM = 83;
    public const sbyte ENCHANT_GEM_ITEM = 76;
    public const sbyte SEND_GEM_INFo = 84;
    public const sbyte SELECT_GEM_UP_TIER = 81;
    public const sbyte UP_TIER_GEM_ITEM = 79;
    public const sbyte ON_UNQUIP_GEM = 82;
    public const sbyte FAST_UNQUIP_GEM = 78;
    public const sbyte GUILD_LIST = 1;
    public const sbyte GUILD_JOIN = 2;
    public const sbyte GUILD_LIST_MEMBER = 3;
    public const sbyte GUILD_KICK_MEMBER = 6;
    public const sbyte SEARCH_GUILD = 13;
    public const sbyte PLAYER_DONATE_CLAN = 10;
    public const sbyte GUILD_TOP_FUND = 16;
    public const sbyte GUILD_TOP_GROWTH_POINT = 15;
    public const sbyte GUILD_NAME_IN_PLACE = 23;
    public const sbyte GUILD_REQUEST_JOIN = 2;
    public const sbyte GUIDER_LIST_OPTION = 3;
    public const sbyte GUILD_CHAT = 20;
    public const sbyte GUILD_PLAYER_CHAT = 21;
    public const sbyte GUILD_ON_PLAYER_CHAT = 22;
    public const sbyte GUILD_CLAN_SKILL = 24;
    public const sbyte GUILD_CLAN_UNLOCK_SKILL = 25;
    public const sbyte GUILD_CLAN_RENT_SKILL = 26;
    public const sbyte GUILD_SHOW_OHTER_PLAYER_CLAN_SKILL = 27;

    public const sbyte SKILL_CLAN_LOCK = -1;
    public const sbyte SKILL_CLAN_RENT = 0;
    public const sbyte SKILL_CLAN_CHANGE = 1;

    public const sbyte SHOW_LIST_TASK = 54;

    public const sbyte INVITE_MATCH = 12;

    public const sbyte PET_UNFOLLOW = 3;
    public const sbyte WING_TYPE_USE = 4;
    public const sbyte WING_TYPE_INVENTORY = 2;
    public const sbyte WING_TYPE_UNEQUIP = 5;
    public const sbyte WING_TYPE_ENCHANT = 6;
    public const sbyte SHOW_TATTO_PET_IN_KIOSK = 99;
    public const sbyte ON_PET_INTERACT = 17;
    public const sbyte ON_PET_INTERACT_KISS = 0;
    public const sbyte ON_PET_INTERACT_PLAY = 1;
    public const sbyte ON_PET_INTERACT_POKE = 2;
    public const sbyte SHOW_EXP = 95;
    public const sbyte ANIMATION_MENU = 100;
    public const sbyte SERVER_LIST = 64;
    public const sbyte SEND_ANIMATION_CHARACTER = 4;
    public const sbyte SEND_LIST_ANIMATION_CHARACTER = 5;
    public const sbyte LETTER_COMMAND = 121;
    public const sbyte LETTER_COMMAND_LIST_FRIEND = 1;
    public const sbyte LETTER_COMMAND_LIST_REQUEST_ADD_FRIEND = 2;
    public const sbyte LETTER_COMMAND_REQUEST_ADD_FRIEND = 3;
    public const sbyte LETTER_COMMAND_REQUEST_ADD_FRIEND_WITH_NAME = 4;
    public const sbyte LETTER_COMMAND_LIST_BLOCK_FRIEND = 10;
    public const sbyte LETTER_COMMAND_SEND_LETTER = 15;
    public const sbyte LETTER_COMMAND_SET_MARK = 16;
    public const sbyte LETTER_COMMAND_REMOVE_LETTER = 17;
    public const sbyte LETTER_COMMAND_HAS_LETTER = 18;
    public const sbyte LETTER_BOX = 13;
    public const sbyte FAST_REMOVE_MOB = 99;
    public const sbyte ENERGY_INFO = 102;
    public const sbyte CHECK_SPEED = 103;
    public const sbyte REMOVE_BATTLE_BY_MOB_ID = 96;
    public const sbyte UPDATE_HP_BOSS = 89;

    public const sbyte ADMIN_GET_ITEM = 6;
    public const sbyte ADMIN_GIVE_ITEM = 7;
    public const sbyte CHAT_GLOBAL = 10;


    public const sbyte AUTO_ATTACK_SUPPORT = 22;
}
