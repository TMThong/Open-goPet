/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package server;

/**
 *
 * @author MINH THONG
 */
public class GopetCMD {
    public const byte COMMAND_GUIDER = 122;
    public const byte ON_UPDATE_PLAYER_IN_MAP = 29;
    public const byte ON_OTHER_USER_MOVE = 27;
    public const byte INIT_PLAYER = 31;
    public const byte ON_PLAYER_ENTER_MAP = 24;
    public const byte ON_PLACE_CHAT = 9;
    public const byte ON_PLAYER_WARPING = 25;
    public const byte ON_PLAYER_EXIT_PLACE = 30;
    public const byte ON_PLAYER_GET_CHANNEL_INFO = 7;
    public const byte ON_PLAYER_CHANGE_CHANNEL = 24;
    public const byte MGO_COMMAND = 42;
    public const byte TELE_MENU = 12;
    public const byte PET_SERVICE = 81;
    public const byte CHAT_PUBLIC = 66;
    public const byte PET_INVENTORY = 5;
    public const byte CHANGE_NEW_PASSWORD = 93;
    public const byte GAME_OBJECT = 34;
    public const byte COMMAND_IMAGE = 96;
    public const byte NPC_GUIDER = 2;
    public const byte NPC_OPTION = 5;
    public const byte CREATE_CHAR = 21;
    public const byte REQUEST_PET_IMG = 9;
    public const byte SEND_LIST_PET_ZONE = 8;
    public const byte SEND_LIST_MOB_ZONE = 41;
    public const byte MY_PET_INFO = 40;
    public const byte REMOVE_MOB = 42;
    public const byte STAR_INFO = 94;
    public const byte MONEY_INFO = 25;
    public const byte SELECT_OPTION = 5;
    public const byte SELECT_MENU_ELEMENT = 3;
    public const byte SHOW_MENU_ITEM = 8;
    public const byte SERVER_MESSAGE = 45;
    public const byte SEND_YES_NO = 4;
    public const byte ATTACK_MOB = 36;
    public const byte PET_BATTLE = 37;
    public const byte PET_BATTLE_STATE = 16;
    public const byte PET_RECOVERY_HP = 45;
    public const byte PET_BATTLE_USE_ITEM = 3;
    public const byte PetBattle_ATTACK = 1;
    public const byte PET_BATTLE_USE_SKILL = 4;
    //public const byte PROFILE_IMAGE = 0;
    public const byte UPDATE_PET_LVL = 18;
    public const byte MAGIC = 11;
    public const byte GYM = 21;
    public const byte MAGIC_LEARN_SKILL = 31;
    public const byte UP_TIEM_NANG = 19;

    /**
     * SUB CMD Thông tin trang bị của pet
     */
    public const byte EQUIP_INFO = 28;

    /**
     * SUB CMD Dùng trang bị của pet
     */
    public const byte USE_EQUIP_ITEM = 29;
    public const int MAGIC_INFO = 1;
    public const int MAGIC_SKILL = 3;
    public const byte GET_PLAYER_INFO = 55;
    public const byte GET_PET_PLAYER_INFO = 0;
    public const byte GET_PET_EQUIP_PLAYER_INFO = 1;
    public const byte TATTOO = 90;
    public const byte TATTOO_INIT_SCREEN = 1;
    public const byte CLAN = 91;
    public const byte CLAN_INFO_MEMBER = 3;
    public const byte CLAN_INFO = 14;
    public const byte DONATE_CLAN = 9;
    public const byte REQUEST_SHOP = 2;
    public const byte GUIDER_TYPE_PAY = 9;
    public const byte CHARGE_MONEY_INFO = 44;
    public const byte VERSION = 48;
    public const byte UNEQUIP_ITEM = 39;
    public const byte SHOW_BIG_TEXT_EFF = 63;
    public const byte TIME_PLACE = 64;
    public const byte SHOW_UPGRADE_PET = 67;
    public const byte WING = 92;
    public const byte SEND_SKIN = 61;
    public const byte SKIN_INVENTORY = 62;
    public const byte PRICE_UPGRADE_PET = 72;
    public const byte SELECT_PET_UPGRADE = 68;
    public const byte REMOVE_ITEM_EQUIP = 56;
    public const byte GUIDER_ANS = 1;
    public const byte ANS_YES_OR_NO = 2;
    public const byte PET_UPGRADE_ACTIVE = 1;
    public const byte PET_UPGRADE_PASSIVE = 2;
    public const byte PET_UPGRADE_PET_INFO = 69;
    public const byte LOGIN = 1;
    public const byte REGISTER = 35;
    public const byte CHANGE_PASSWORD = 100;
    public const byte CLIENT_INFO = -36;
    public const byte LOGIN_SUCCES = 3;
    public const byte LOGIN_FAILED = 4;
    public const byte BANNER_MESSAGE = 1;
    public const byte POPUP_MESSAGE = 5;
    public const byte KIOSK = 86;
    public const byte REQUEST_SHOP_SKIN = 60;
    public const byte SELECT_METERIAL_ENCHANT = 46;
    public const byte SELECT_METERIAL_ENCHANT_PET_INFO = 47;
    public const byte ENCHANT_ITEM = 48;
    public const byte UP_TIER_ITEM = 49;
    public const byte PET_UNEQUIP_GEM_ITEM_INFO = 75;
    public const byte NORMAL_INVENTORY = 30;
    public const byte PRICE_UP_TIER_PET = 70;
    public const byte PET_UP_TIER = 71;
    public const byte SELECT_KIOSK_ITEM = 85;
    public const byte TYPE_DIALOG_INPUT = 7;
    public const byte REMOVE_SELL_ITEM = 87;
    public const byte PLAYER_CHALLENGE = 12;
    public const byte PLAYER_PK = 96;
    public const byte PLAYER_BATTLE = 59;
    public const byte SELECT_ITEM_GEM_TATTO = 2;
    public const byte SELECT_ITEM_REMOVE_TATOO = 3;
    public const byte GUIDER_IMGDIALOG = 11;
    public const byte GEM_INVENTORY = 73;
    public const byte SHOW_GEM_INVENTORY = 74;
    public const byte GL_MAIL = 20;
    public const byte SELECT_GEM_ENCHANT = 80;
    public const byte REMOVE_GEM_ITEM = 83;
    public const byte ENCHANT_GEM_ITEM = 76;
    public const byte SEND_GEM_INFo = 84;
    public const byte SELECT_GEM_UP_TIER = 81;
    public const byte UP_TIER_GEM_ITEM = 79;
    public const byte ON_UNQUIP_GEM = 82;
    public const byte FAST_UNQUIP_GEM = 78;
    public const byte GUILD_LIST = 1;
    public const byte GUILD_JOIN = 2;
    public const byte GUILD_LIST_MEMBER = 3;
    public const byte GUILD_KICK_MEMBER = 6;
    public const byte SEARCH_GUILD = 13;
    public const byte PLAYER_DONATE_CLAN = 10;
    public const byte GUILD_TOP_FUND = 16;
    public const byte GUILD_TOP_GROWTH_POINT = 15;
    public const byte GUILD_NAME_IN_PLACE = 23;
    public const byte GUILD_REQUEST_JOIN = 2;
    public const byte GUIDER_LIST_OPTION = 3;
    public const byte GUILD_CHAT = 20;
    public const byte GUILD_PLAYER_CHAT = 21;
    public const byte GUILD_ON_PLAYER_CHAT = 22;
    public const byte GUILD_CLAN_SKILL = 24;
    public const byte GUILD_CLAN_UNLOCK_SKILL = 25;
    public const byte GUILD_CLAN_RENT_SKILL = 26;
    public const byte GUILD_SHOW_OHTER_PLAYER_CLAN_SKILL = 27;

    public const byte SKILL_CLAN_LOCK = -1;
    public const byte SKILL_CLAN_RENT = 0;
    public const byte SKILL_CLAN_CHANGE = 1;
    
    public const byte SHOW_LIST_TASK = 54;
    
    public const byte INVITE_MATCH = 12;
    
    public const byte PET_UNFOLLOW = 3;
}
