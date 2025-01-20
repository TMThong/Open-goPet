using Gopet.Data.GopetClan;
using Gopet.Data.GopetItem;
using Gopet.Data.item;
using Gopet.Data.Map;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class MenuController
{
    public static void answerYesNo(int dialogId, bool ok, Player player)
    {
        if (ok)
        {
            Object obj;
            switch (dialogId)
            {
                case DIALOG_CONFIRM_REMOVE_ITEM_EQUIP:
                    {
                        obj = player.controller.objectPerformed.get(OBJKEY_REMOVE_ITEM_EQUIP);
                        if (obj != null)
                        {
                            player.controller.removeItemEquip((int)obj);
                            player.controller.objectPerformed.Remove(OBJKEY_REMOVE_ITEM_EQUIP);
                        }
                    }
                    break;
                case DIALOG_CONFIRM_BUY_KIOSK_ITEM:
                    {
                        obj = player.controller.objectPerformed.get(OBJKEY_KIOSK_ITEM);
                        if (obj != null)
                        {
                            var objENtry = (KeyValuePair<Kiosk, SellItem>)obj;
                            player.controller.objectPerformed.Remove(OBJKEY_KIOSK_ITEM);
                            objENtry.Key.confirmBuy(player, objENtry.Value);
                        }
                    }
                    break;
                case DIALOG_ASK_ENCHANT_WING:
                    {
                        if (!player.controller.objectPerformed.ContainsKey(OBJKEY_ID_MATERIAL_ENCHANT_WING)) return;
                        Item itemSelect = player.controller.selectItemsbytemp(player.controller.objectPerformed[OBJKEY_ID_MATERIAL_ENCHANT_WING], GopetManager.NORMAL_INVENTORY);
                        if (!player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_WING_WANT_ENCHANT) || itemSelect == null) return;

                        Item wingItem = player.controller.findWingItemWantEnchant();
                        if (itemSelect.Template.type == GopetManager.ITEM_MATERIAL_ENCHANT_WING)
                        {
                            if (wingItem != null)
                            {
                                if (wingItem.lvl >= 10)
                                {
                                    player.redDialog(player.Language.WingIsMaxLevel);
                                    player.controller.objectPerformed.Remove(OBJKEY_ID_MATERIAL_ENCHANT_WING);
                                    player.controller.objectPerformed.Remove(OBJKEY_INDEX_WING_WANT_ENCHANT);
                                    player.controller.objectPerformed.Remove(OBJKEY_TYPE_PAY_FOR_ENCHANT_WING);
                                    return;
                                }
                                if (wingItem.lvl >= 0 && wingItem.lvl < GopetManager.MAX_LVL_ENCHANT_WING)
                                {
                                    EnchantWingData enchantWingData = GopetManager.EnchantWingData[wingItem.lvl + 1];
                                    int[] PAYMENT = new int[] { enchantWingData.Coin, enchantWingData.Gold };
                                    string[] PAYMENT_DISPLAY = new string[] { Utilities.FormatNumber(enchantWingData.Coin) + " (ngoc)", Utilities.FormatNumber(enchantWingData.Gold) + " (gold)" };
                                    int typePayment = player.controller.objectPerformed[OBJKEY_TYPE_PAY_FOR_ENCHANT_WING];
                                    if (typePayment >= 0 && typePayment < PAYMENT.Length)
                                    {
                                        if (PAYMENT[typePayment] > 0)
                                        {
                                            if (player.controller.checkCountItem(itemSelect, enchantWingData.NumItemMaterial))
                                            {
                                                switch (typePayment)
                                                {
                                                    case 0:
                                                        if (player.checkCoin(PAYMENT[typePayment]))
                                                        {
                                                            player.mineCoin(PAYMENT[typePayment]);
                                                        }
                                                        else
                                                        {
                                                            player.controller.notEnoughCoin();
                                                            return;
                                                        }
                                                        break;
                                                    case 1:
                                                        if (player.checkGold(PAYMENT[typePayment]))
                                                        {
                                                            player.mineGold(PAYMENT[typePayment]);
                                                        }
                                                        else
                                                        {
                                                            player.controller.notEnoughCoin();
                                                            return;
                                                        }
                                                        break;
                                                }
                                                player.controller.subCountItem(itemSelect, enchantWingData.NumItemMaterial, GopetManager.NORMAL_INVENTORY);
                                                bool IsSucces = Utilities.NextFloatPer() < enchantWingData.Percent || player.controller.isBuffEnchent;
                                                if (IsSucces)
                                                {
                                                    wingItem.lvl++;
                                                    player.getPet()?.applyInfo(player);
                                                    player.okDialog(string.Format(player.Language.EnchantWingOK, wingItem.getName(player), wingItem.lvl));
                                                }
                                                else
                                                {
                                                    wingItem.lvl -= enchantWingData.NumDropLevelWing;
                                                    player.getPet()?.applyInfo(player);
                                                    player.redDialog(string.Format(player.Language.EnchantWingFail, enchantWingData.NumDropLevelWing));
                                                }

                                                player.controller.objectPerformed.Remove(OBJKEY_ID_MATERIAL_ENCHANT_WING);
                                                player.controller.objectPerformed.Remove(OBJKEY_INDEX_WING_WANT_ENCHANT);
                                                player.controller.objectPerformed.Remove(OBJKEY_TYPE_PAY_FOR_ENCHANT_WING);
                                            }
                                            else
                                            {
                                                player.controller.notEnoughItem(itemSelect, enchantWingData.NumItemMaterial);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.BugWarning);
                                    }
                                }
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.NotEnoughMaterial);
                        }
                    }
                    break;
                case DIALOG_ENCHANT:
                    player.controller.enchantItem(); break;
                case DIALOG_UP_TIER_ITEM:
                    player.controller.upTierItem(); break;
                case DIALOG_UP_SKILL:
                    player.controller.upSkill(); break;

                case DIALOG_INVITE_CHALLENGE:
                    {
                        player.controller.startChallenge(); break;
                    }

                case DIALOG_CONFIRM_REMOVE_GEM:
                    {
                        player.controller.removeGem((int)player.controller.objectPerformed.get(OBJKEY_ID_GEM_REMOVE));
                    }
                    break;

                case DIALOG_ASK_KEEP_GEM:
                    {
                        if (player.checkGold(GopetManager.PRICE_KEEP_GEM))
                        {
                            player.controller.objectPerformed.put(OBJKEY_IS_KEEP_GOLD, true);
                            MenuController.showYNDialog(MenuController.DIALOG_UP_TIER_ITEM, (String)player.controller.objectPerformed.get(OBJKEY_ASK_UP_TIER_GEM_STR), player);
                        }
                        else
                        {
                            player.controller.notEnoughGold();
                            return;
                        }
                    }
                    break;

                case DIALOG_ASK_REMOVE_GEM:
                    {
                        player.controller.confirmUnequipGem((int)player.controller.objectPerformed.get(OBJKEY_ID_ITEM_REMOVE_GEM));
                        player.controller.objectPerformed.Remove(OBJKEY_ID_ITEM_REMOVE_GEM);
                    }
                    break;

                case DIALOG_ASK_FAST_REMOVE_GEM:
                    {
                        player.controller.confirmUnequipFastGem((int)player.controller.objectPerformed.get(OBJKEY_ID_ITEM_FAST_REMOVE_GEM));
                        player.controller.objectPerformed.Remove(OBJKEY_ID_ITEM_FAST_REMOVE_GEM);
                    }
                    break;
                case DIALOG_ASK_REQUEST_JOIN_CLAN:
                    {
                        player.controller.requestJoinClan((String)player.controller.objectPerformed.get(OBJKEY_CLAN_NAME_REQUEST));
                        player.controller.objectPerformed.Remove(OBJKEY_CLAN_NAME_REQUEST);
                    }
                    break;
                case DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN:
                    {
                        ClanMember clanMember = player.controller.getClan();
                        if (clanMember != null)
                        {
                            Clan clan = clanMember.getClan();
                            ClanMember memberSelect = clan.getMemberByUserId((int)player.controller.objectPerformed.get(OBJKEY_MEM_ID_UPGRADE_DUTY));
                            int index = (int)player.controller.objectPerformed.get(OBJKEY_INDEX_MENU_UPGRADE_DUTY);
                            if (memberSelect == null)
                            {
                                player.redDialog(player.Language.OtherPlayerNotHaveAClan);
                            }
                            else if (clanMember == memberSelect)
                            {
                                player.redDialog(player.Language.YouCannotManipulateYourself);
                            }
                            else
                            {
                                player.controller.objectPerformed.put(OBJKEY_INDEX_MENU_UPGRADE_DUTY, index);
                                switch (index)
                                {
                                    case 0:
                                        if (clanMember.duty == Clan.TYPE_LEADER)
                                        {
                                            memberSelect.duty = clanMember.duty;
                                            memberSelect.clan.leaderId = memberSelect.user_id;
                                            clanMember.duty = Clan.TYPE_NORMAL;
                                            player.okDialog(player.Language.GiveUpGuildPositionOK);
                                            Player onPlayer = PlayerManager.get(memberSelect.name);
                                            if (onPlayer != null)
                                            {
                                                onPlayer.okDialog(player.Language.YouWasUpToLeader);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.YouIsNotLeader);
                                        }
                                        break;
                                    case 1:
                                        if (clanMember.duty == Clan.TYPE_LEADER)
                                        {
                                            if (clan.checkDuty(Clan.TYPE_DEPUTY_LEADER))
                                            {
                                                memberSelect.duty = Clan.TYPE_DEPUTY_LEADER;
                                                player.okDialog(player.Language.SuccessfulOrdination);
                                                Player onPlayer = PlayerManager.get(memberSelect.name);
                                                if (onPlayer != null)
                                                {
                                                    onPlayer.okDialog(player.Language.YouWasUpToDeputyLeader);
                                                }
                                            }
                                            else
                                            {
                                                clan.showFullDuty(player);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.YouIsNotLeader);
                                        }
                                        break;

                                    case 2:
                                        if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER)
                                        {
                                            if (clan.checkDuty(Clan.TYPE_SENIOR))
                                            {
                                                memberSelect.duty = Clan.TYPE_SENIOR;
                                                player.okDialog(player.Language.SuccessfulOrdination);
                                                Player onPlayer = PlayerManager.get(memberSelect.name);
                                                if (onPlayer != null)
                                                {
                                                    onPlayer.okDialog(player.Language.YouWasUpToSenior);
                                                }
                                            }
                                            else
                                            {
                                                clan.showFullDuty(player);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.YouEnoughPermission);
                                        }
                                        break;
                                    case 3:
                                        if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER)
                                        {
                                            memberSelect.duty = Clan.TYPE_NORMAL;
                                            player.okDialog(player.Language.SuccessfulOrdination);
                                            Player onPlayer = PlayerManager.get(memberSelect.name);
                                            if (onPlayer != null)
                                            {
                                                onPlayer.okDialog(player.Language.YouWasGaveDownPosition);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.YouEnoughPermission);
                                        }
                                        break;

                                    case 4:
                                        if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER || clanMember.duty == Clan.TYPE_SENIOR)
                                        {
                                            if (clanMember.duty != Clan.TYPE_NORMAL)
                                            {
                                                if (clanMember.duty < memberSelect.duty)
                                                {
                                                    clan.kick(memberSelect.user_id);
                                                    player.okDialog(player.Language.ClanKickOK);
                                                }
                                                else
                                                {
                                                    player.redDialog(player.Language.YouCannotKickMoreThanPosition);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.YouEnoughPermission);
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            player.controller.notClan();
                        }
                    }
                    break;
                case DIALOG_ASK_REQUEST_UPGRADE_MAIN_HOUSE:
                    {
                        if (player.HaveClan)
                        {
                            ClanMember member = player.controller.getClan();
                            if (member == null)
                            {
                                player.controller.notClan();
                                return;
                            }
                            else
                            {
                                member.clan.LOCKObject.WaitOne();
                                try
                                {
                                    if (member.IsLeader)
                                    {
                                        if (GopetManager.clanTemp.ContainsKey(member.clan.lvl + 1))
                                        {
                                            var temp = GopetManager.clanTemp[member.clan.lvl + 1];
                                            if (member.clan.checkFund(temp.fundNeed))
                                            {
                                                member.clan.mineFund(temp.fundNeed);
                                                member.clan.lvl++;
                                                member.clan.potentialSkill += temp.tiemnangPoint;
                                                player.okDialog(player.Language.UpgradeClanOK);
                                            }
                                            else
                                            {
                                                Clan.notEnoughFund(player, temp.fundNeed);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.ClanIsMaxLevel);
                                        }
                                    }
                                    else
                                    {
                                        Clan.notEngouhPermission(player);
                                    }
                                }
                                finally
                                {
                                    member.clan.LOCKObject.ReleaseMutex();
                                }
                            }
                        }
                        else
                        {
                            player.controller.notClan();
                        }
                    }
                    break;
                case DIALOG_ASK_UPGRADE_SHOP_HOUSE:
                    {
                        player.redDialog(GopetManager.OldFeatures);
                    }
                    break;
                case DIALOG_ASK_ENCHANT_TATTO:
                    {
                        player.controller.enchantTatto();
                        break;
                    }
                case DIALOG_ASK_UPGRADE_SKILL_HOUSE:
                    {
                        player.redDialog(GopetManager.OldFeatures);
                    }
                    break;
                case DIALOG_ASK_UNLOCK_SLOT_SKILL_CLAN:
                    {
                        ClanMember clanMember = player.controller.getClan();
                        if (clanMember != null)
                        {
                            if (clanMember.IsLeader)
                            {
                                clanMember.clan.LOCKObject.WaitOne();
                                try
                                {
                                    Clan clan = clanMember.getClan();
                                    if (clan.slotSkill >= GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN.Length)
                                    {
                                        player.redDialog(player.Language.PlayerWasUnlockedAllSlotSkillClan);
                                    }
                                    else
                                    {
                                        if (clan.lvl >= GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[clan.slotSkill] && clan.fund >= GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill])
                                        {
                                            clan.mineFund(GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill]);
                                            clan.slotSkill++;
                                            player.controller.showSkillClan(player.user.user_id);
                                            player.okDialog(player.Language.UnlockSlotSkillNextOK);
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.UnlockSkillSlotLaw, GopetManager.LVL_CLAN_NEED_TO_ADD_SLOT_SKILL[clan.slotSkill], Utilities.FormatNumber(GopetManager.PRICE_UNLOCK_SLOT_SKILL_CLAN[clan.slotSkill]));
                                        }
                                    }
                                }
                                finally
                                {
                                    clanMember.clan.LOCKObject.ReleaseMutex();
                                }
                            }
                            else Clan.notEngouhPermission(player);
                        }
                        else
                        {
                            player.controller.notClan();
                        }
                    }
                    break;
                default:
                    {
                        player.redDialog(player.Language.CannotFindDialog, dialogId);
                        Thread.Sleep(1000);
                    }
                    break;
            }
        }
        else
        {
            switch (dialogId)
            {
                case DIALOG_ASK_KEEP_GEM:
                    player.controller.objectPerformed.put(OBJKEY_IS_KEEP_GOLD, false);
                    MenuController.showYNDialog(MenuController.DIALOG_UP_TIER_ITEM, (String)player.controller.objectPerformed.get(OBJKEY_ASK_UP_TIER_GEM_STR), player);
                    break;
                case DIALOG_ASK_ENCHANT_TATTO:
                    {
                        player.controller.objectPerformed.Remove(OBJKEY_ID_TATTO_TO_ENCHANT);
                        player.controller.objectPerformed.Remove(OBJKEY_ID_MATERIAL1_TATTO_TO_ENCHANT);
                        player.controller.objectPerformed.Remove(OBJKEY_ID_MATERIAL2_TATTO_TO_ENCHANT);
                    }
                    break;
            }
        }
    }

}

