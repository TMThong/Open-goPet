/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vn.me.network;

/**
 *
 * @author Admin
 */
public class Cmd {
    public static final byte SEND_ANIMATION_CHARACTER = 4;
    public static final byte COMMAND_GUIDER = 122;
    public static final byte ON_UPDATE_PLAYER_IN_MAP = 29;
    public static final byte ON_OTHER_USER_MOVE = 27;
    public static final byte INIT_PLAYER = 31;
    public static final byte ON_PLAYER_ENTER_MAP = 24;
    public static final byte ON_PLACE_CHAT = 9;
    public static final byte ON_PLAYER_WARPING = 25;
    public static final byte ON_PLAYER_EXIT_PLACE = 30;
    public static final byte ON_PLAYER_GET_CHANNEL_INFO = 7;
    public static final byte ON_PLAYER_CHANGE_CHANNEL = 24;
    public static final byte MGO_COMMAND = 42;
    public static final byte TELE_MENU = 12;
    public static final byte PET_SERVICE = 81;
    public static final byte CHAT_PUBLIC = 66;
    public static final byte PET_INVENTORY = 5;
    public static final byte CHANGE_NEW_PASSWORD = 93;
    public static final byte GAME_OBJECT = 34;
    public static final byte COMMAND_IMAGE = 96;
    public static final byte NPC_GUIDER = 2;
    public static final byte NPC_OPTION = 5;
    public static final byte CREATE_CHAR = 21;
    public static final byte REQUEST_PET_IMG = 9;
    public static final byte SEND_LIST_PET_ZONE = 8;
    public static final byte SEND_LIST_MOB_ZONE = 41;
    public static final byte MY_PET_INFO = 40;
    public static final byte REMOVE_MOB = 42;
    public static final byte STAR_INFO = 94;
    public static final byte MONEY_INFO = 25;
    public static final byte SELECT_OPTION = 5;
    public static final byte SELECT_MENU_ELEMENT = 3;
    public static final byte SHOW_MENU_ITEM = 8;
    public static final byte SERVER_MESSAGE = 45;
    public static final byte SEND_YES_NO = 4;
    public static final byte ATTACK_MOB = 36;
    public static final byte PET_BATTLE = 37;
    public static final byte PET_BATTLE_STATE = 16;
    public static final byte PET_RECOVERY_HP = 45;
    public static final byte PET_BATTLE_USE_ITEM = 3;
    public static final byte PetBattle_ATTACK = 1;
    public static final byte PET_BATTLE_USE_SKILL = 4;
//public static final byte PROFILE_IMAGE = 0;
    public static final byte UPDATE_PET_LVL = 18;
    public static final byte MAGIC = 11;
    public static final byte GYM = 21;
    public static final byte MAGIC_LEARN_SKILL = 31;
    public static final byte UP_TIEM_NANG = 19;

    /**
     * SUB CMD Thông tin trang bị của pet
     */
    public static final byte EQUIP_INFO = 28;

    /**
     * SUB CMD Dùng trang bị của pet
     */
    public static final byte USE_EQUIP_ITEM = 29;
    public static final byte GET_PLAYER_INFO = 55;
    public static final byte GET_PET_PLAYER_INFO = 0;
    public static final byte GET_PET_EQUIP_PLAYER_INFO = 1;
    public static final byte TATTOO = 90;
    public static final byte TATTOO_INIT_SCREEN = 1;
    public static final byte TATTOO_ENCHANT_SELECT_MATERIAL = 4;
    public static final byte TATTOO_ENCHANT = 5;
    public static final byte TATTOO_ENCHANT_SELECT_MATERIAL1 = 1;
    public static final byte TATTOO_ENCHANT_SELECT_MATERIAL2 = 2;
    public static final byte CLAN = 91;
    public static final byte CLAN_INFO_MEMBER = 3;
    public static final byte CLAN_INFO = 14;
    public static final byte DONATE_CLAN = 9;
    public static final byte REQUEST_SHOP = 2;
    public static final byte GUIDER_TYPE_PAY = 9;
    public static final byte CHARGE_MONEY_INFO = 44;
    public static final byte VERSION = 48;
    public static final byte UNEQUIP_ITEM = 39;
    public static final byte SHOW_BIG_TEXT_EFF = 63;
    public static final byte TIME_PLACE = 64;
    public static final byte SHOW_UPGRADE_PET = 67;
    public static final byte WING = 92;
    public static final byte SEND_SKIN = 61;
    public static final byte SKIN_INVENTORY = 62;
    public static final byte PRICE_UPGRADE_PET = 72;
    public static final byte SELECT_PET_UPGRADE = 68;
    public static final byte REMOVE_ITEM_EQUIP = 56;
    public static final byte GUIDER_ANS = 1;
    public static final byte ANS_YES_OR_NO = 2;
    public static final byte PET_UPGRADE_ACTIVE = 1;
    public static final byte PET_UPGRADE_PASSIVE = 2;
    public static final byte PET_UPGRADE_PET_INFO = 69;
    public static final byte LOGIN = 1;
    public static final byte REGISTER = 35;
    public static final byte CHANGE_PASSWORD = 100;
    public static final byte CLIENT_INFO = -36;
    public static final byte LOGIN_SUCCES = 3;
    public static final byte LOGIN_FAILED = 4;
    public static final byte BANNER_MESSAGE = 1;
    public static final byte POPUP_MESSAGE = 5;
    public static final byte KIOSK = 86;
    public static final byte REQUEST_SHOP_SKIN = 60;
    public static final byte SELECT_METERIAL_ENCHANT = 46;
    public static final byte SELECT_METERIAL_ENCHANT_PET_INFO = 47;
    public static final byte ENCHANT_ITEM = 48;
    public static final byte UP_TIER_ITEM = 49;
    public static final byte PET_UNEQUIP_GEM_ITEM_INFO = 75;
    public static final byte NORMAL_INVENTORY = 30;
    public static final byte INFO_UP_TIER_PET = 70;
    public static final byte PET_UP_TIER = 71;
    public static final byte SELECT_KIOSK_ITEM = 85;
    public static final byte TYPE_DIALOG_INPUT = 7;
    public static final byte REMOVE_SELL_ITEM = 87;
    public static final byte PLAYER_CHALLENGE = 12;
    public static final byte PLAYER_PK = 96;
    public static final byte PLAYER_BATTLE = 59;
    public static final byte SELECT_ITEM_GEM_TATTO = 2;
    public static final byte SELECT_ITEM_REMOVE_TATOO = 3;
    public static final byte GUIDER_IMGDIALOG = 11;
    public static final byte GEM_INVENTORY = 73;
    public static final byte SHOW_GEM_INVENTORY = 74;
    public static final byte GL_MAIL = 20;
    public static final byte SELECT_GEM_ENCHANT = 80;
    public static final byte REMOVE_GEM_ITEM = 83;
    public static final byte ENCHANT_GEM_ITEM = 76;
    public static final byte SEND_GEM_INFo = 84;
    public static final byte SELECT_GEM_UP_TIER = 81;
    public static final byte UP_TIER_GEM_ITEM = 79;
    public static final byte ON_UNQUIP_GEM = 82;
    public static final byte FAST_UNQUIP_GEM = 78;
    public static final byte GUILD_LIST = 1;
    public static final byte GUILD_JOIN = 2;
    public static final byte GUILD_LIST_MEMBER = 3;
    public static final byte GUILD_KICK_MEMBER = 6;
    public static final byte SEARCH_GUILD = 13;
    public static final byte PLAYER_DONATE_CLAN = 10;
    public static final byte GUILD_TOP_FUND = 16;
    public static final byte GUILD_TOP_GROWTH_POINT = 15;
    public static final byte GUILD_NAME_IN_PLACE = 23;
    public static final byte GUILD_REQUEST_JOIN = 2;
    public static final byte GUIDER_LIST_OPTION = 3;
    public static final byte GUILD_CHAT = 20;
    public static final byte GUILD_PLAYER_CHAT = 21;
    public static final byte GUILD_ON_PLAYER_CHAT = 22;
    public static final byte GUILD_CLAN_SKILL = 24;
    public static final byte GUILD_CLAN_UNLOCK_SKILL = 25;
    public static final byte GUILD_CLAN_RENT_SKILL = 26;
    public static final byte GUILD_SHOW_OHTER_PLAYER_CLAN_SKILL = 27;

    public static final byte SKILL_CLAN_LOCK = -1;
    public static final byte SKILL_CLAN_RENT = 0;
    public static final byte SKILL_CLAN_CHANGE = 1;

    public static final byte SHOW_LIST_TASK = 54;

    public static final byte INVITE_MATCH = 12;

    public static final byte PET_UNFOLLOW = 3;
    public static final byte WING_TYPE_USE = 4;
    public static final byte WING_TYPE_INVENTORY = 2;
    public static final byte WING_TYPE_UNEQUIP = 5;
    public static final byte WING_TYPE_ENCHANT = 6;
    public static final byte SHOW_TATTO_PET_IN_KIOSK = 99;
    public static final byte ON_PET_INTERACT = 17;
    public static final byte ON_PET_INTERACT_KISS = 0;
    public static final byte ON_PET_INTERACT_PLAY = 1;
    public static final byte ON_PET_INTERACT_POKE = 2;
    public static final byte SHOW_EXP = 95;
    public static final byte ANIMATION_MENU = 100;
    public static final byte SERVER_LIST = 64;
    public static final byte SEND_LIST_ANIMATION_CHARACTER = 5;
    public static final byte LETTER_COMMAND = 121;
    public static final byte LETTER_BOX = 13;
    public static final byte LETTER_COMMAND_REQUEST_ADD_FRIEND = 3;
    public static final byte LETTER_COMMAND_REQUEST_ADD_FRIEND_WITH_NAME = 4;
    public static final byte LETTER_COMMAND_SEND_LETTER = 15;
    public static final byte LETTER_COMMAND_SET_MARK = 16;
    public static final byte LETTER_COMMAND_REMOVE_LETTER = 17;
    public static final byte FAST_REMOVE_MOB = 99;
}
