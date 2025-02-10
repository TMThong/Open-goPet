
using Gopet.Battle;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySqlConnector;
using Gopet.Data.item;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Gopet.Data.top;
using Gopet.Data.Event;
using Gopet.Data.user;
using Gopet.Manager;
using Gopet.Data.Clan;
using Gopet.Data.Event.Year2024;
using Gopet.Data.pet;

public partial class MenuController
{
    public static void selectMenu(int menuId, int index, int paymentIndex, Player player)
    {
        switch (menuId)
        {
            case MENU_UNEQUIP_SKIN:
            case MENU_UNEQUIP_PET:
                {
                    if (player.getPlace() is ChallengePlace)
                    {
                        player.redDialog(player.Language.CannotManipulateInChallenge);
                        return;
                    }

                    if (player.controller.getPetBattle() != null)
                    {
                        player.redDialog(player.Language.CannotManipulateWhenFighting);
                    }

                    Pet p = player.getPet();
                    GopetPlace place_Lc = (GopetPlace)player.getPlace();
                    if (place_Lc == null)
                    {
                        return;
                    }
                    switch (menuId)
                    {
                        case MENU_UNEQUIP_PET:
                            {
                                if (p != null)
                                {
                                    player.playerData.petSelected = null;
                                    player.playerData.addPet(p, player);
                                    player.controller.unfollowPet(p);
                                    player.okDialog(player.Language.ManipulateOK);
                                    HistoryManager.addHistory(new History(player).setLog("Tháo pet").setObj(p));
                                }
                                else
                                {
                                    player.petNotFollow();
                                }
                            }
                            break;
                        case MENU_UNEQUIP_SKIN:
                            {
                                Item it = player.playerData.skin;
                                if (it != null)
                                {
                                    player.playerData.skin = null;
                                    player.addItemToInventory(it);
                                    place_Lc.sendMySkin(player);
                                    if (p != null)
                                    {
                                        p.applyInfo(player);
                                    }
                                    player.okDialog(player.Language.ManipulateOK);
                                    HistoryManager.addHistory(new History(player).setLog("Tháo cải trang " + it.getName(player)).setObj(it));
                                }
                                else
                                {
                                    player.redDialog(player.Language.CurrentYouNotHaveAnySkin);
                                }
                            }
                            break;


                    }
                }
                break;
            case MENU_SELECT_TYPE_PAYMENT_TO_ARENA_JOURNALISM:
                {

                    if (ArenaEvent.Instance.IdPlayerJoin.Contains(player.playerData.user_id))
                    {
                        player.okDialog(player.Language.YouAreHaveJournalism);
                        return;
                    }

                    if (index >= 0 && index < 2)
                    {
                        if (!checkMoney((sbyte)index, (index == 0 ? GopetManager.PRICE_GOLD_ARENA_JOURNALISM : GopetManager.PRICE_COIN_ARENA_JOURNALISM), player)) return;

                        if (ArenaEvent.Instance.CanJournalism)
                        {
                            addMoney((sbyte)index, -(index == 0 ? GopetManager.PRICE_GOLD_ARENA_JOURNALISM : GopetManager.PRICE_COIN_ARENA_JOURNALISM), player);
                            ArenaEvent.Instance.IdPlayerJoin.Add(player.playerData.user_id);
                            player.okDialog(player.Language.EventJournalismOK);
                        }
                        else
                        {
                            player.okDialog(player.Language.JournalismTimeOut);
                        }
                    }
                    break;
                }
            case MENU_OPTION_TO_SLECT_TYPE_MONEY_ENCHANT_TATTOO:
                {
                    if (index >= 0 && index < 2 && player.controller.objectPerformed.ContainsKey(OBJKEY_ID_TATTO_TO_ENCHANT))
                    {
                        PetTatto tatto = player.controller.objectPerformed[OBJKEY_ID_TATTO_TO_ENCHANT];
                        player.controller.objectPerformed[OBJKEY_TYPE_PRICE_TATTO_TO_ENCHANT] = index;
                        showYNDialog(DIALOG_ASK_ENCHANT_TATTO, string.Format(player.Language.AskSelectTattoEnchantLaw, tatto.Template.getName(player), tatto.lvl + 1, GopetManager.PERCENT_OF_ENCHANT_TATOO[tatto.lvl], getMoneyText((sbyte)index, index == 0 ? GopetManager.PRICE_GOLD_ENCHANT_TATTO : GopetManager.PRICE_COIN_ENCHANT_TATTO, player), GopetManager.NUM_LVL_DROP_ENCHANT_TATTO_FAILED[tatto.lvl]), player);
                    }
                    break;
                }
            case MENU_ATM:
                {
                    switch (index)
                    {
                        case 0:
                            sendMenu(MENU_EXCHANGE_GOLD, player);
                            break;
                        case 1:
                            player.controller.showInputDialog(INPUT_DIALOG_EXCHANGE_GOLD_TO_COIN, string.Format(player.Language.InputExchangeGoldToCoinTitle, GopetManager.PERCENT_EXCHANGE_GOLD_TO_COIN), new String[] { "Số gold :" });
                            break;
                        case 2:
                            player.controller.showInputDialog(INPUT_TYPE_EXCHANGE_LUA_TO_COIN, string.Format(player.Language.InputExchangeLuaToNgocTitle, 1, GopetManager.PERCENT_EXCHANGE_LUA_TO_COIN), new String[] { "Số (lua) :" });
                            break;
                    }
                }
                break;
            case MENU_CHOOSE_PET_FROM_PACKAGE_PET:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_ITEM_PACKAGE_PET_TO_USE))
                    {
                        Item item = player.controller.objectPerformed[OBJKEY_ITEM_PACKAGE_PET_TO_USE];
                        if (index >= 0 && index < item.Template.itemOptionValue.Length && item.count > 0)
                        {
                            Pet p = new Pet(item.Template.itemOptionValue[index]);
                            player.playerData.addPet(p, player);
                            player.controller.subCountItem(item, 1, GopetManager.NORMAL_INVENTORY);
                            player.okDialog(string.Format(player.Language.CongratulateGetNewPet, p.getNameWithStar(player)));
                        }
                        else
                        {
                            player.redDialog(player.Language.BugWarning);
                        }
                    }
                }
                break;
            case MENU_EXCHANGE_GOLD:
                {
                    if (index >= 0 && index < EXCHANGE_ITEM_INFOS.Count)
                    {
                        ExchangeItemInfo exchangeItemInfo = (ExchangeItemInfo)EXCHANGE_ITEM_INFOS.get(index);
                        int mycoin = player.user.getCoin();
                        if (mycoin >= exchangeItemInfo.getExchangeData().getAmount())
                        {
                            player.user.mineCoin(exchangeItemInfo.getExchangeData().getAmount(), mycoin);
                            if (player.user.getCoin() >= 0)
                            {
                                player.addGold(exchangeItemInfo.getExchangeData().getGold());
                                player.okDialog(string.Format(player.Language.ChangeGoldOK, Utilities.FormatNumber(exchangeItemInfo.getExchangeData().getGold())));
                                HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Đổi %s vàng trong game thành công", Utilities.FormatNumber(exchangeItemInfo.getExchangeData().getGold()))));
                            }
                            else
                            {
                                UserData.banBySQL(UserData.BAN_INFINITE, player.Language.BugGold, long.MaxValue, player.user.user_id);
                                player.session.Close();
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.NotEnoughMoney);
                        }
                    }
                }
                break;

            case MENU_SHOW_LIST_TASK:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_NPC_ID_FOR_MAIN_TASK))
                    {
                        int npcId = (int)player.controller.objectPerformed.get(OBJKEY_NPC_ID_FOR_MAIN_TASK);
                        JArrayList<TaskTemplate> taskTemplates = player.controller.getTaskCalculator().getTaskTemplate(npcId);
                        if (index >= 0 && index < taskTemplates.Count)
                        {
                            TaskTemplate taskTemplate = taskTemplates.get(index);
                            player.playerData.tasking.Add(taskTemplate.getTaskId());
                            player.playerData.task.Add(new TaskData(taskTemplate));
                            player.controller.getTaskCalculator().update();
                            player.okDialog(player.Language.CongratulateGetNewTask);
                        }
                        else
                        {
                            player.fastAction();
                        }
                    }
                }
                break;

            case MENU_SHOW_MY_LIST_TASK:
                {
                    player.controller.objectPerformed.put(OBJKEY_INDEX_TASK_IN_MY_LIST, index);

                    sendMenu(MENU_OPTION_TASK, player);
                }
                break;

            case MENU_OPTION_TASK:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_TASK_IN_MY_LIST))
                    {
                        int indexTask = (int)player.controller.objectPerformed.get(OBJKEY_INDEX_TASK_IN_MY_LIST);
                        player.controller.objectPerformed.Remove(OBJKEY_INDEX_TASK_IN_MY_LIST);

                        if (indexTask >= 0 && indexTask < player.playerData.task.Count)
                        {
                            TaskData taskData = player.playerData.task.get(indexTask);
                            switch (index)
                            {
                                case 0:
                                    player.controller.getTaskCalculator().onUpdateTask(taskData);
                                    player.okDialog(player.Language.UpdateOK);
                                    break;
                                case 1:
                                    if (player.controller.getTaskCalculator().taskSuccess(taskData))
                                    {
                                        player.controller.getTaskCalculator().onTaskSucces(taskData);
                                        player.controller.getTaskCalculator().update();
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.YouAreNotYetEligible);
                                    }
                                    break;
                                case 2:
                                    if (!taskData.CanCancelTask)
                                    {
                                        player.redDialog(player.Language.YouCannotCancelTask);
                                        return;
                                    }
                                    player.playerData.task.remove(taskData);
                                    player.playerData.tasking.remove(taskData.taskTemplateId);
                                    player.controller.getTaskCalculator().update();
                                    player.okDialog(player.Language.CancelOK);
                                    break;
                            }
                        }
                    }
                }
                break;

            case MENU_LIST_PET_FREE:
                {
                    var PetFreeList = getPetFreeLst(player);
                    if (index >= 0 && index < PetFreeList.Count)
                    {
                        PetMenuItemInfo petMenuItemInfo = (PetMenuItemInfo)PetFreeList.get(index);
                        if (!player.playerData.isFirstFree)
                        {
                            player.playerData.isFirstFree = true;
                            Pet p = new Pet(petMenuItemInfo.getPetTemplate().petId);
                            player.playerData.addPet(p, player);
                            player.okDialog(string.Format(player.Language.GetPetFreeOK, petMenuItemInfo.getPetTemplate().getName(player)));
                            HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Nhận pet %s miễn phí tại NPC trân trân", petMenuItemInfo.getPetTemplate().getName(player))).setObj(p));
                        }
                        else
                        {
                            player.redDialog(player.Language.YouHaveGotPetFreeBefore);
                        }
                    }
                }
                break;
            case MENU_INTIVE_CHALLENGE:
                {
                    if (index >= 0 && index < GopetManager.PRICE_BET_CHALLENGE.Length)
                    {
                        int priceChallenge = (int)GopetManager.PRICE_BET_CHALLENGE[index];
                        if (priceChallenge <= 0)
                        {
                            player.redDialog(player.Language.BugWarning);
                            return;
                        }
                        if (priceChallenge > 100000)
                        {
                            player.redDialog(string.Format(player.Language.GemLimitWarning, Utilities.FormatNumber(100000)));
                            return;
                        }
                        player.controller.sendChallenge((Player)player.controller.objectPerformed.get(OBJKEY_INVITE_CHALLENGE_PLAYER), priceChallenge);

                    }
                }
                break;
            case MENU_LEARN_NEW_SKILL:
                if (player.checkCoin(GopetManager.PriceLearnSkill))
                {
                    PetSkill[] petSkills = getPetSkills(player);
                    Pet pet = player.playerData.petSelected;
                    if (index >= 0 && index < petSkills.Length && pet != null)
                    {
                        if (petSkills[index].IsNeedCard)
                        {
                            player.redDialog(player.Language.ThisSkillNeedCard);
                            return;
                        }
                        foreach (int[] skillInfo in pet.skill)
                        {
                            if (skillInfo[0] == petSkills[index].skillID)
                            {
                                player.redDialog(player.Language.PetLearnDuplicateSkill);
                                return;
                            }
                        }

                        if (pet.skillPoint > 0 && player.skillId_learn == -1)
                        {
                            pet.skillPoint--;
                            pet.addSkill(petSkills[index].skillID, 1);
                            player.addCoin(-GopetManager.PriceLearnSkill);
                            player.controller.magic(GopetCMD.MAGIC_LEARN_SKILL, true);
                            player.okDialog(player.Language.LearnSkillPetOK);
                            player.controller.getTaskCalculator().onLearnSkillPet();
                            if (pet.skill.Length >= 2)
                            {
                                player.controller.getTaskCalculator().onLearnSkillPet2();
                            }
                        }
                        else if (player.skillId_learn != -1)
                        {
                            if (pet.skill.Length > 0)
                            {
                                bool flag = false;
                                int skillIndex = -1;
                                for (int i = 0; i < pet.skill.Length; i++)
                                {
                                    int[] skillInfo = pet.skill[i];
                                    if (skillInfo[0] == player.skillId_learn)
                                    {
                                        flag = true;
                                        skillIndex = i;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    pet.skill[skillIndex][0] = petSkills[index].skillID;
                                    pet.skill[skillIndex][1] = 1;
                                    player.addCoin(-GopetManager.PriceLearnSkill);
                                    player.controller.magic(GopetCMD.MAGIC_LEARN_SKILL, true);
                                    player.okDialog(player.Language.LearnSkillPetOK);
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Học kỹ năng thành công cho pet %s", pet.Template.name)).setObj(pet));
                                }
                                else
                                {
                                    player.redDialog(player.Language.SkillPetNotFound);
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.LearnSkillPetLaw);
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.LearnSkillPetLaw);
                        }
                    }
                }
                else
                {
                    player.controller.notEnoughCoin();
                }
                break;
            case MENU_DELETE_TIEM_NANG:
                if (player.getPet() != null)
                {
                    if (index >= 0 && index < player.controller.gym_options.Length)
                    {
                        Pet pet = player.getPet();
                        if (pet.tiemnang[index] > 0)
                        {
                            if (player.checkGold(PriceDeleteTiemNang))
                            {
                                player.mineGold(PriceDeleteTiemNang);
                                pet.tiemnang[index]--;
                                pet.tiemnang_point++;
                                pet.applyInfo(player);
                                player.okDialog(player.Language.DeleteGymOK);
                                HistoryManager.addHistory(new History(player).setLog("Tảy tìm năng cho pet" + pet.getNameWithoutStar(player)).setObj(pet));
                            }
                            else
                            {
                                player.controller.notEnoughGold();
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.ThisIndicatorHasBeenErased);
                        }
                    }
                }
                else
                {
                    player.petNotFollow();
                }
                break;
            case MENU_SELECT_PET_TO_DEF_LEAGUE:
            case MENU_PET_INVENTORY:
                if (index == -1 && menuId == MENU_PET_INVENTORY)
                {
                    sendMenu(MENU_UNEQUIP_PET, player);
                    return;
                }



                if (index >= 0 && index < player.playerData.pets.Count)
                {
                    Pet oldPet = menuId == MENU_PET_INVENTORY ? player.playerData.petSelected : player.playerData.PetDefLeague;
                    if (oldPet != null)
                    {
                        if (oldPet.TimeDieZ > Utilities.CurrentTimeMillis)
                        {
                            player.redDialog(player.Language.YourPetIsDie);
                            return;
                        }
                    }
                    Pet pet = player.playerData.pets.get(index);
                    player.playerData.pets.remove(pet);
                    if (oldPet != null)
                    {
                        player.playerData.addPet(oldPet, player);
                    }
                    if (menuId == MENU_PET_INVENTORY)
                    {
                        player.playerData.petSelected = pet;
                        pet.applyInfo(player);
                        player.controller.updatePetSelected(false);
                    }
                    else
                    {
                        player.playerData.PetDefLeague = pet;
                        pet.applyInfo(player);
                        player.okDialog(string.Format(player.Language.SelectPetDefOK, pet.getNameWithStar(player)));
                    }
                }
                break;
            case MENU_SKIN_INVENTORY:
                if (index == -1)
                {
                    sendMenu(MENU_UNEQUIP_SKIN, player);
                    return;
                }
                CopyOnWriteArrayList<Item> listSkinItems = player.playerData.getInventoryOrCreate(GopetManager.SKIN_INVENTORY);
                if (index >= 0 && index < listSkinItems.Count)
                {
                    Item skinItem = listSkinItems.get(index);
                    Item oldSkinItem = player.playerData.skin;
                    if (oldSkinItem != null)
                    {
                        listSkinItems.Add(oldSkinItem);
                    }
                    listSkinItems.remove(skinItem);
                    player.playerData.skin = skinItem;
                    Pet p = player.getPet();
                    if (p != null)
                    {
                        p.applyInfo(player);
                    }
                    player.controller.updateSkin();
                }
                break;
            case MENU_WING_INVENTORY:
                if (index == -1)
                {
                    player.redDialog(player.Language.YouAreUsingThisWing);
                    return;
                }
                CopyOnWriteArrayList<Item> listWingItems = player.playerData.getInventoryOrCreate(GopetManager.WING_INVENTORY);
                if (index >= 0 && index < listWingItems.Count)
                {
                    Item wingItem = listWingItems.get(index);
                    Item oldWingItem = player.playerData.wing;
                    if (oldWingItem != null)
                    {
                        listWingItems.Add(oldWingItem);
                    }
                    listWingItems.remove(wingItem);
                    player.playerData.wing = wingItem;
                    Pet p = player.getPet();
                    if (p != null)
                    {
                        p.applyInfo(player);
                    }
                    player.controller.updateWing();
                    player.okDialog(player.Language.EquipOK);
                }
                break;
            case MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN:
            case MENU_SELECT_SKILL_CLAN_TO_RENT:
                {
                    int indexSlot = (int)player.controller.objectPerformed.get(OBJKEY_INDEX_SLOT_SKILL_RENT);
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if ((index >= 0 && index < clan.SkillInfo.Count || menuId == MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN) && indexSlot < clan.slotSkill)
                        {
                            if (clanMember.IsLeader)
                            {
                                clanMember.clan.LOCKObject.WaitOne();
                                try
                                {
                                    ClanSkillTemplate clanSkillTemplate = menuId == MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN ? player.controller.objectPerformed[MenuController.OBJKEY_CLAN_SKILL_TEMPLATE_RENT] : GopetManager.ClanSkillViaId[clan.SkillInfo.ElementAt(index).Key];
                                    if (clanSkillTemplate != null)
                                    {
                                        if (clan.SkillRent.Any(p => p.SkillId == clanSkillTemplate.id))
                                        {
                                            player.redDialog(player.Language.SkillsAreAlreadyHired);
                                        }
                                        else
                                        {
                                            if (menuId == MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN)
                                            {
                                                if (index >= 0 && index < clanSkillTemplate.price.Length)
                                                {
                                                    if (checkMoney(clanSkillTemplate.moneyType[index], clanSkillTemplate.price[index], player))
                                                    {
                                                        addMoney(clanSkillTemplate.moneyType[index], -clanSkillTemplate.price[index], player);
                                                        ClanSkill clanSkill = new ClanSkill(clanSkillTemplate.id, DateTime.Now.AddMilliseconds(clanSkillTemplate.expire), clan.SkillInfo[clanSkillTemplate.id]);
                                                        if (clan.SkillRent.Count > indexSlot)
                                                        {
                                                            clan.SkillRent[indexSlot] = clanSkill;
                                                        }
                                                        else
                                                        {
                                                            clan.SkillRent.Add(clanSkill);
                                                        }
                                                        player.okDialog(player.Language.HiredOK);
                                                    }
                                                    else
                                                    {
                                                        NotEngouhMoney(clanSkillTemplate.moneyType[index], clanSkillTemplate.price[index], player);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.controller.objectPerformed[MenuController.OBJKEY_CLAN_SKILL_TEMPLATE_RENT] = clanSkillTemplate;
                                                MenuController.sendMenu(MenuController.MENU_SELECT_TYPE_MONEY_TO_RENT_SKILL_CLAN, player);
                                            }
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
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_PLUS_SKILL_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        if (clanMember.IsLeader)
                        {
                            clanMember.clan.LOCKObject.WaitOne();
                            try
                            {
                                if (clanMember.clan.potentialSkill > 0)
                                {
                                    var clanSKillFillter = GopetManager.clanSkillTemplateList.Where(p => clanMember.clan.lvl >= p.lvlClanRequire).AsList();
                                    if (index >= 0 && index < clanSKillFillter.Count)
                                    {
                                        ClanSkillTemplate clanSkillTemplate = clanSKillFillter[index];
                                        if (clanMember.clan.SkillInfo.ContainsKey(clanSkillTemplate.id))
                                        {
                                            if (clanMember.clan.SkillInfo[clanSkillTemplate.id] >= clanSkillTemplate.clanSkillLvlTemplates.Length)
                                            {
                                                player.redDialog(player.Language.ClanSkillIsMaxLevel);
                                            }
                                            else
                                            {
                                                clanMember.clan.SkillInfo[clanSkillTemplate.id]++;
                                                clanMember.clan.potentialSkill--;
                                                player.okDialog(player.Language.UpgradeOK);
                                            }
                                        }
                                        else
                                        {
                                            clanMember.clan.SkillInfo[clanSkillTemplate.id] = 1;
                                            clanMember.clan.potentialSkill--;
                                            player.okDialog(player.Language.UpgradeOK);
                                        }
                                    }
                                }
                                else
                                {
                                    player.redDialog(player.Language.NotEnoughClanSkillPoint);
                                }
                            }
                            finally
                            {
                                clanMember.clan.LOCKObject.ReleaseMutex();
                            }
                        }
                        else
                        {
                            Clan.notEngouhPermission(player);
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case SHOP_BIRTHDAY_EVENT:
            case SHOP_GIAN_THUONG:
            case SHOP_ENERGY:
            case SHOP_CLAN:
            case SHOP_WEAPON:
            case SHOP_HAT:
            case SHOP_SKIN:
            case SHOP_ARMOUR:
            case SHOP_THUONG_NHAN:
            case SHOP_PET:
            case SHOP_FOOD:
            case SHOP_ARENA:
                ShopTemplate shopTemplate = getShop((sbyte)menuId, player);
                if ((index >= 0 && index < shopTemplate.getShopTemplateItems().Count && menuId != SHOP_CLAN) || menuId == SHOP_CLAN)
                {
                    ShopTemplateItem shopTemplateItem = null;
                    if (menuId != SHOP_CLAN)
                    {
                        shopTemplateItem = shopTemplate.getShopTemplateItems().get(index);
                    }
                    else
                    {
                        ClanMember clanMember = player.controller.getClan();
                        if (clanMember != null)
                        {
                            shopTemplateItem = clanMember.getClan().getShopClan().getShopTemplateItem(index);
                            if (shopTemplateItem != null)
                            {
                                if (shopTemplateItem.TimeNeedReset.HasValue && shopTemplateItem.TimeNeedReset.Value > TimeSpan.Zero)
                                {
                                    if (clanMember.shopData.TryGetValue(shopTemplateItem.itemTemTempleId, out DateTime dateTime))
                                    {
                                        if (dateTime > DateTime.Now)
                                        {
                                            player.redDialog("Vui lòng chờ đến {0}", Utilities.ToDateString(dateTime));
                                            return;
                                        }
                                    }
                                }
                                if (shopTemplateItem.NeedFund > clanMember.fundDonate)
                                {
                                    player.redDialog("Bạn cần đóng góp {0} quỹ để mua vật phẩm này", Utilities.FormatNumber(shopTemplateItem.NeedFund));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            player.controller.notClan();
                            return;
                        }
                    }

                    if (shopTemplateItem == null)
                    {
                        player.redDialog(player.Language.ItemWasSell);
                        return;
                    }
                    sbyte[] typeMoney = shopTemplateItem.getMoneyType();
                    int[] price = shopTemplateItem.getPrice();
                    if (paymentIndex >= 0 && paymentIndex < typeMoney.Length)
                    {
                        if (checkMoney(typeMoney[paymentIndex], price[paymentIndex], player))
                        {
                            if (shopTemplateItem.isSellItem || player.controller.objectPerformed.ContainsKey(OBJKEY_NAME_PET_WANT))
                                addMoney(typeMoney[paymentIndex], -price[paymentIndex], player);
                            if (shopTemplateItem.isNeedRemove())
                            {
                                shopTemplate.getShopTemplateItems().remove(shopTemplateItem);
                            }
                            if (!shopTemplateItem.isSpceial)
                            {
                                if (shopTemplateItem.isSellItem)
                                {
                                    Item item = new Item(shopTemplateItem.getItemTempalteId()) { canTrade = !shopTemplateItem.isLock && (shopTemplateItem.itemTemTempleId != 240009 || shopTemplateItem.itemTemTempleId != 240010) };
                                    HistoryManager.addHistory(new History(player).setLog($"Mua vật phẩm {item.Template.name} với menuId = {menuId} và giá là {price[paymentIndex]}").setObj(new { Item = item, MenuId = menuId, Price = price[paymentIndex] }));
                                    item.SourcesItem.Add(ItemSource.MUA_ĐỒ_SHOP_NPC);
                                    item.count = shopTemplateItem.getCount();
                                    if (item.getTemp().expire > 0)
                                    {
                                        item.expire = Utilities.CurrentTimeMillis + item.Template.expire;
                                    }
                                    player.addItemToInventory(item);
                                    player.okDialog(string.Format(player.Language.YouBuyItemOK, item.getTemp().getName(player)));
                                    if (menuId == SHOP_CLAN)
                                    {
                                        ClanMember clanMember = player.controller.getClan();
                                        if (shopTemplateItem.TimeNeedReset.HasValue && shopTemplateItem.TimeNeedReset.Value > TimeSpan.Zero)
                                        {
                                            clanMember.shopData[shopTemplateItem.itemTemTempleId] = DateTime.Now + shopTemplateItem.TimeNeedReset.Value;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!player.controller.objectPerformed.ContainsKey(OBJKEY_NAME_PET_WANT))
                                    {
                                        player.controller.showInputDialog(INPUT_TYPE_NAME_PET_WHEN_BUY_PET, player.Language.InputNamePetTitle, new string[] { player.Language.NameDesctription });
                                        player.controller.objectPerformed[OBJKEY_ID_MENU_BUY_PET_TO_NAME] = menuId;
                                        player.controller.objectPerformed[OBJKEY_INDEX_MENU_BUY_PET_TO_NAME] = index;
                                        player.controller.objectPerformed[OBJKEY_PAYMENT_INDEX_WANT_TO_NAME_PET] = paymentIndex;
                                        return;
                                    }
                                    Pet p = new Pet(shopTemplateItem.getPetId());
                                    p.name = player.controller.objectPerformed[OBJKEY_NAME_PET_WANT];
                                    player.playerData.addPet(p, player);
                                    player.okDialog(string.Format(player.Language.YouBuyItemOK, p.getNameWithStar(player)));
                                    player.controller.objectPerformed.Remove(OBJKEY_ID_MENU_BUY_PET_TO_NAME);
                                    player.controller.objectPerformed.Remove(OBJKEY_INDEX_MENU_BUY_PET_TO_NAME);
                                    player.controller.objectPerformed.Remove(OBJKEY_PAYMENT_INDEX_WANT_TO_NAME_PET);
                                    player.controller.objectPerformed.Remove(OBJKEY_NAME_PET_WANT);

                                }
                                if (shopTemplateItem.isCloseScreenAfterClick())
                                {
                                    sendMenu(menuId, player);
                                }

                                if (menuId == SHOP_WEAPON)
                                {
                                    player.controller.getTaskCalculator().onBuyRandomWeapon();
                                }
                            }
                            else
                            {
                                shopTemplateItem.execute(player);
                            }
                        }
                        else
                        {
                            switch (typeMoney[paymentIndex])
                            {
                                case GopetManager.MONEY_TYPE_COIN:
                                    player.controller.notEnoughCoin();
                                    break;
                                case GopetManager.MONEY_TYPE_GOLD:
                                    player.controller.notEnoughGold();
                                    break;
                                case GopetManager.MONEY_TYPE_GOLD_BAR:
                                    player.controller.notEnoughGoldBar();
                                    break;
                                case GopetManager.MONEY_TYPE_SILVER_BAR:
                                    player.controller.notEnoughSilverBar();
                                    break;
                                case GopetManager.MONEY_TYPE_BLOOD_GEM:
                                    player.controller.notEnoughBloodGem();
                                    break;
                                case GopetManager.MONEY_TYPE_FUND_CLAN:
                                    player.controller.notEnoughFundClan();
                                    break;
                                case GopetManager.MONEY_TYPE_GROWTH_POINT_CLAN:
                                    player.controller.notEnoughGrowthPointClan();
                                    break;
                                case GopetManager.MONEY_TYPE_LUA:
                                    player.controller.notEnoughLua();
                                    break;
                            }
                        }
                    }
                }
                break;
            case MENU_SELECT_PET_UPGRADE_ACTIVE:
                {
                    if (index >= 0 && index < player.playerData.pets.Count)
                    {
                        Pet pet = player.playerData.pets.get(index);
                        player.controller.addPetUpgrade(pet, GopetCMD.PET_UPGRADE_ACTIVE, pet.petId);
                    }
                }
                break;
            case MENU_SELECT_PET_UPGRADE_PASSIVE:
                {
                    if (index >= 0 && index < player.playerData.pets.Count)
                    {
                        Pet pet = player.playerData.pets.get(index);
                        player.controller.addPetUpgrade(pet, GopetCMD.PET_UPGRADE_PASSIVE, pet.petId);
                    }
                }
                break;
            case MENU_ADMIN_BUFF_DUNG_HỢP:
            case MENU_FUSION_MENU_EQUIP:
            case MENU_UNLOCK_ITEM_PLAYER:
            case MENU_LOCK_ITEM_PLAYER:
            case MENU_SELECT_ALL_ITEM_MERGE:
            case MENU_SELECT_ITEM_TO_GET_BY_ADMIN:
            case MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN:
            case MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO:
            case MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO:
            case MENU_SELECT_MATERIAL_TO_ENCAHNT_WING:
            case MENU_SELECT_GEM_TO_INLAY:
            case MENU_SELECT_GEM_UP_TIER:
            case MENU_SELECT_ENCHANT_MATERIAL1:
            case MENU_SELECT_ENCHANT_MATERIAL2:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL1:
            case MENU_SELECT_GEM_ENCHANT_MATERIAL2:
            case MENU_MERGE_PART_PET:
            case MENU_SELECT_ITEM_UP_SKILL:
            case MENU_SELECT_ITEM_PK:
            case MENU_SELECT_ITEM_PART_FOR_STAR_PET:
            case MENU_SELECT_ITEM_GEN_TATTO:
            case MENU_SELECT_ITEM_REMOVE_TATTO:
            case MENU_SELECT_ITEM_SUPPORT_PET:
            case MENU_MERGE_PART_ITEM:
            case MENU_MERGE_WING:
                CopyOnWriteArrayList<Item> listItems = null;
                switch (menuId)
                {
                    case MENU_MERGE_PART_ITEM:
                        listItems = getItemByMenuId(menuId, player, (item) => GopetManager.itemTemplate[item.Template.itemOptionValue[0]].type != GopetManager.WING_ITEM);
                        break;
                    case MENU_MERGE_WING:
                        listItems = getItemByMenuId(menuId, player, (item) => GopetManager.itemTemplate[item.Template.itemOptionValue[0]].type == GopetManager.WING_ITEM);
                        break;
                    default:
                        listItems = getItemByMenuId(menuId, player);
                        break;
                }
                if (index >= 0 && listItems.Count > index)
                {
                    Item itemSelect = listItems.get(index);
                    switch (menuId)
                    {
                        case MENU_SELECT_ENCHANT_MATERIAL1:
                            player.controller.selectMaterialEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(player), 1);
                            break;
                        case MENU_SELECT_ENCHANT_MATERIAL2:
                            player.controller.selectMaterialEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(player), 2);
                            break;
                        case MENU_SELECT_GEM_ENCHANT_MATERIAL1:
                            {
                                player.controller.selectMaterialGemEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(player), 1);
                                player.controller.selectGemM1 = true;
                            }
                            break;
                        case MENU_SELECT_GEM_ENCHANT_MATERIAL2:
                            {
                                player.controller.selectMaterialGemEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(player), 12);
                                player.controller.selectGemM1 = false;
                            }
                            break;
                        case MENU_MERGE_PART_PET:
                            {
                                if (itemSelect.getTemp().itemOptionValue.Length > 0)
                                {
                                    int petTemplateId = itemSelect.getTemp().getOptionValue()[0];
                                    if (GopetManager.ListPetMustntUpTier.Contains(petTemplateId))
                                    {
                                        player.redDialog(player.Language.CannotMergePetWasUpTier);
                                        return;
                                    }
                                    if (itemSelect.count >= itemSelect.getTemp().getOptionValue()[1])
                                    {
                                        player.controller.subCountItem(itemSelect, itemSelect.getTemp().getOptionValue()[1], GopetManager.NORMAL_INVENTORY);

                                        Pet pet = new Pet(petTemplateId);
                                        player.playerData.addPet(pet, player);
                                        player.okDialog(string.Format(player.Language.MergePartPetOK, pet.getNameWithStar(player)));
                                        HistoryManager.addHistory(new History(player).setLog($"Gộp mảnh pet nhận được {pet.Template?.name}").setObj(pet));
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.NotEnough);
                                    }
                                }
                                else
                                {
                                    player.redDialog(player.Language.ErrorPartPet);
                                }
                            }
                            break;
                        case MENU_SELECT_ITEM_UP_SKILL:
                            {
                                int skillId = (int)player.controller.objectPerformed.get(OBJKEY_SKILL_UP_ID);
                                Pet pet = player.getPet();
                                int skillIndex = pet.getSkillIndex(skillId);
                                PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(skillId);
                                if (itemSelect.count > 0)
                                {
                                    if ((petSkill.skillID < 116 && pet.skill[skillIndex][1] < 10) || (petSkill.skillID >= 116 && pet.skill[skillIndex][1] < 37))
                                    {
                                        player.controller.objectPerformed.put(OBJKEY_ITEM_UP_SKILL, itemSelect);
                                        showYNDialog(DIALOG_UP_SKILL, string.Format(player.Language.AskDoYouWantUpgradeSkill, petSkill.getName(player), pet.skill[skillIndex][1] + 1, petSkill.skillID >= 116 ? GopetManager.PERCENT_UP_SKILL_SKY[pet.skill[skillIndex][1]] : GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]], itemSelect.getTemp().getOptionValue()[0], petSkill.skillID >= 116 ? GopetManager.PERCENT_UP_SKILL_SKY[pet.skill[skillIndex][1]] : GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]] + itemSelect.getTemp().getOptionValue()[0]).Replace("/", "%"), player);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.SkillIsMaxLevel);
                                    }
                                }
                            }
                            break;
                        case MENU_SELECT_ITEM_PK:
                            {
                                player.controller.objectPerformed.put(OBJKEY_ITEM_PK, itemSelect);
                                player.controller.confirmpk();
                            }
                            break;
                        case MENU_SELECT_ITEM_PART_FOR_STAR_PET:
                            {
                                player.controller.upStarPet(itemSelect);
                            }
                            break;
                        case MENU_SELECT_ITEM_GEN_TATTO:
                            {
                                player.controller.genTatto(itemSelect);
                            }
                            break;
                        case MENU_SELECT_ITEM_REMOVE_TATTO:
                            {
                                player.controller.removeTatto(itemSelect, (int)player.controller.objectPerformed.get(OBJKEY_TATTO_ID_REMOVE));
                            }
                            break;
                        case MENU_SELECT_ITEM_SUPPORT_PET:
                            {
                                PetBattle petBattle = player.controller.getPetBattle();
                                if (petBattle != null)
                                {
                                    petBattle.useItem(player, itemSelect);
                                }
                            }
                            break;

                        case MENU_SELECT_GEM_UP_TIER:
                            {
                                player.controller.selectGemUpTier(itemSelect.itemId, itemSelect.getTemp().getIconPath(), itemSelect.getEquipName(player), 1, itemSelect.lvl);
                            }
                            break;
                        case MENU_MERGE_WING:
                        case MENU_MERGE_PART_ITEM:
                            {
                                int[] optionValue = itemSelect.getTemp().getOptionValue();
                                if (player.controller.checkCount(itemSelect.itemTemplateId, optionValue[1], GopetManager.NORMAL_INVENTORY))
                                {
                                    player.controller.subCountItem(itemSelect, optionValue[1], GopetManager.NORMAL_INVENTORY);
                                    Item item = new Item(optionValue[0]);
                                    item.count = 1;
                                    item.SourcesItem.Add(ItemSource.GHÉP_MẢNH);
                                    player.okDialog(string.Format(player.Language.ChangeItemOK, item.getTemp().getName(player)));
                                    HistoryManager.addHistory(new History(player).setLog($"Gộp mảnh item nhận được {item.Template.name}").setObj(item));
                                    player.addItemToInventory(item);
                                }
                                else
                                {
                                    player.redDialog(string.Format(player.Language.MergePartItemFail, optionValue[1]));
                                }
                            }
                            break;
                        case MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO:
                            {
                                player.controller.sendItemSelectTattoMaterialToEnchant(itemSelect.itemId, itemSelect.Template.iconPath, itemSelect.Template.getName(player));
                                break;
                            }
                        case MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO:
                            {
                                player.controller.sendItemSelectTattoMaterialToEnchant(itemSelect.itemId, itemSelect.Template.iconPath, itemSelect.Template.getName(player));
                                break;
                            }
                        case MENU_SELECT_GEM_TO_INLAY:
                            {
                                player.controller.inlayGem(itemSelect, (int)player.controller.objectPerformed.get(OBJKEY_EQUIP_INLAY_GEM_ID));
                                player.controller.objectPerformed.Remove(OBJKEY_EQUIP_INLAY_GEM_ID);
                            }
                            break;
                        case MENU_SELECT_ITEM_TO_GET_BY_ADMIN:
                            {
                                if (player.checkIsAdmin())
                                {
                                    Player playerOnline = player.controller.objectPerformed[OBJKEY_PLAYER_GET_ITEM];
                                    if (PlayerManager.players.Contains(playerOnline))
                                    {
                                        player.controller.objectPerformed[OBJKEY_ITEM_ADMIN_GET] = itemSelect;
                                        if (itemSelect.Template.isStackable)
                                        {
                                            player.controller.showInputDialog(INPUT_TYPE_COUNT_ADMIN_GET, itemSelect.Template.name, "Số lượng: ");
                                        }
                                        else
                                        {
                                            sendMenu(MENU_OPTION_ADMIN_GET_ITEM, player);
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.PlayerOffline);
                                    }
                                }
                                break;
                            }
                        case MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN:
                            {
                                if (player.checkIsAdmin())
                                {
                                    Player playerOnline = player.controller.objectPerformed[OBJKEY_PLAYER_GIVE_ITEM];
                                    if (PlayerManager.players.Contains(playerOnline))
                                    {
                                        player.controller.objectPerformed[OBJKEY_ITEM_ADMIN_GIVE] = itemSelect;
                                        if (itemSelect.Template.isStackable)
                                        {
                                            player.controller.showInputDialog(INPUT_TYPE_COUNT_ADMIN_GIVE, itemSelect.Template.name, "Số lượng: ");
                                        }
                                        else
                                        {
                                            sendMenu(MENU_OPTION_ADMIN_GIVE_ITEM, player);
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.PlayerOffline);
                                    }
                                }
                                break;
                            }
                        case MENU_SELECT_MATERIAL_TO_ENCAHNT_WING:
                            {
                                if (!player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_WING_WANT_ENCHANT) || itemSelect == null) return;
                                Item wingItem = player.controller.findWingItemWantEnchant();
                                if (wingItem != null)
                                {
                                    if (wingItem.lvl >= 0 && wingItem.lvl < GopetManager.MAX_LVL_ENCHANT_WING)
                                    {
                                        EnchantWingData enchantWingData = GopetManager.EnchantWingData[wingItem.lvl + 1];
                                        int[] PAYMENT = new int[] { enchantWingData.Coin, enchantWingData.Gold };
                                        string[] PAYMENT_DISPLAY = new string[] { Utilities.FormatNumber(enchantWingData.Coin) + " (ngoc)", Utilities.FormatNumber(enchantWingData.Gold) + " (vang)" };
                                        int typePayment = player.controller.objectPerformed[OBJKEY_TYPE_PAY_FOR_ENCHANT_WING];
                                        if (typePayment >= 0 && typePayment < PAYMENT.Length)
                                        {
                                            if (PAYMENT[typePayment] > 0)
                                            {
                                                if (player.controller.checkCountItem(itemSelect, enchantWingData.NumItemMaterial))
                                                {
                                                    player.controller.objectPerformed[OBJKEY_ID_MATERIAL_ENCHANT_WING] = itemSelect.itemTemplateId;
                                                    showYNDialog(DIALOG_ASK_ENCHANT_WING, string.Format(player.Language.AskDoYouWantEnchantWing, wingItem.getEquipName(player), wingItem.lvl + 1, PAYMENT_DISPLAY[typePayment], enchantWingData.Percent, enchantWingData.NumDropLevelWing), player);
                                                }
                                                else
                                                {
                                                    player.controller.notEnoughItem(itemSelect, enchantWingData.NumItemMaterial);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            player.fastAction();
                                        }
                                    }
                                }
                            }
                            break;
                        case MENU_SELECT_ALL_ITEM_MERGE:
                            if (player.controller.mergeData.Items.Contains(itemSelect))
                            {
                                player.redDialog("Vật phẩm này trong hàng chờ rồi!");
                                return;
                            }
                            player.controller.mergeData.Items.Add(itemSelect);
                            player.okDialog("Thêm thành công");
                            break;
                        case MENU_UNLOCK_ITEM_PLAYER:
                            {
                                if (player.checkIsAdmin())
                                {
                                    itemSelect.canTrade = true;
                                    player.okDialog("Mở khoá thành công");
                                }
                            }
                            break;
                        case MENU_LOCK_ITEM_PLAYER:
                            {
                                if (player.checkIsAdmin())
                                {
                                    itemSelect.canTrade = false;
                                    player.okDialog("Khoá thành công");
                                }
                            }
                            break;
                        case MENU_FUSION_MENU_EQUIP:
                            {
                                player.controller.objectPerformed[OBJKEY_CURRENT_ITEM_ID_FUSION] = itemSelect.itemId;
                                player.controller.objectPerformed[OBJKEY_CURRENT_ITEM_TEMP_ID_FUSION] = itemSelect.Template.itemId;
                                sendMenu(MENU_FUSION_EQUIP_OPTION, player);
                            }
                            break;
                        case MENU_ADMIN_BUFF_DUNG_HỢP:
                            {
                                player.controller.objectPerformed[OBJKEY_ITEM_BUFF_DUNG_HỢP] = itemSelect.itemId;
                                player.controller.showInputDialog(INPUT_NUM_DUNG_HỢP, "BUFF DUNG HỢP", " Cấp: ");
                            }
                            break;
                    }
                }
                break;
            case MENU_SELECT_EQUIP_PET_TIER:
                CopyOnWriteArrayList<Item> listItemEquip = player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
                if (index >= 0 && listItemEquip.Count > index)
                {
                    Item itemSelect = listItemEquip.get(index);
                    player.controller.selectMaterialEnchant(itemSelect.itemId, itemSelect.getTemp().getIconPath(), itemSelect.getEquipName(player), int.MaxValue);
                }
                break;

            case MENU_SELECT_MONEY_TO_PAY_FOR_ENCHANT_WING:
                {
                    player.controller.objectPerformed[OBJKEY_TYPE_PAY_FOR_ENCHANT_WING] = index;
                    sendMenu(MENU_SELECT_MATERIAL_TO_ENCAHNT_WING, player);
                }
                break;

            case MENU_NORMAL_INVENTORY:
                /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                CopyOnWriteArrayList<Item> listItemNormal = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                if (index >= 0 && listItemNormal.Count > index)
                {
                    Item itemSelect = listItemNormal.get(index);
                    switch (itemSelect.getTemp().getType())
                    {
                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        case GopetManager.ITEM_BUFF_EXP:
                            {
                                BuffExp buffExp = player.playerData.buffExp;
                                if (buffExp.getItemTemplateIdBuff() != itemSelect.itemTemplateId)
                                {
                                    buffExp.setBuffExpTime(0);
                                    buffExp.set_buffPercent(0);
                                    buffExp.setItemTemplateIdBuff(itemSelect.itemTemplateId);
                                    buffExp.set_buffPercent(itemSelect.getTemp().getOptionValue()[0]);
                                }
                                player.playerData.buffExp.addTime(GopetManager.TIME_BUFF_EXP);
                                player.okDialog(string.Format(player.Language.UseBuffItem, buffExp.getPercent(), Utilities.round(buffExp.getBuffExpTime() / 1000 / 60)).Replace("/", "%"));
                                player.controller.showExp();
                                break;
                            }
                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        case GopetManager.ITEM_ADMIN:
                            {
                                if (player.checkIsAdmin())
                                {
                                    sendMenu(MENU_SELECT_ITEM_ADMIN, player);
                                    return;
                                }
                                else
                                {
                                    player.user.ban(UserData.BAN_INFINITE, "Dung VP ADMIN", long.MaxValue);
                                    player.session.Close();
                                }
                                return;
                            }
                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        case GopetManager.ITEM_ENERGY:
                            {
                                if (itemSelect.Template.itemOptionValue != null)
                                {
                                    if (itemSelect.Template.itemOptionValue.Length >= 2)
                                    {
                                        int numUse = 0;

                                        if (player.playerData.numUseEnergy.ContainsKey(itemSelect.Template.itemId)) numUse = player.playerData.numUseEnergy[itemSelect.Template.itemId];

                                        if (numUse >= itemSelect.Template.itemOptionValue[1])
                                        {
                                            player.redDialog(player.Language.UseEnergyFailByMax);
                                            return;
                                        }
                                        else
                                        {
                                            numUse++;
                                            player.playerData.star += itemSelect.Template.itemOptionValue[0];
                                            player.controller.updateUserInfo();
                                            player.playerData.numUseEnergy[itemSelect.itemTemplateId] = numUse;
                                            player.okDialog(player.Language.UseEnergyItemOK, Utilities.FormatNumber(player.playerData.star));
                                        }
                                    }
                                }
                                break;
                            }
                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        case GopetManager.ITEM_PET_PACKAGE:
                            {
                                player.controller.objectPerformed[OBJKEY_ITEM_PACKAGE_PET_TO_USE] = itemSelect;
                                sendMenu(MENU_CHOOSE_PET_FROM_PACKAGE_PET, player);
                                return;
                            }

                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        case GopetManager.ITEM_PART_PET:
                            {
                                if (itemSelect.Template.itemOptionValue.Length == 2)
                                {
                                    Pet pet = new Pet(itemSelect.Template.itemOptionValue[0]);
                                    player.okDialog(pet.getNameWithoutStar(player) + ": " + pet.getDesc(player));
                                }
                                else
                                {
                                    player.redDialog(player.Language.ErrorItem);
                                }
                                return;
                            }
                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        case GopetManager.ITEM_PART_ITEM:
                            {
                                if (itemSelect.Template.itemOptionValue.Length == 2)
                                {
                                    ItemTemplate itemTemplate = GopetManager.itemTemplate[itemSelect.Template.itemOptionValue[0]];
                                    player.okDialog($"{itemTemplate.getName(player)} {itemTemplate.getDescription(player)} {itemTemplate.getAtk()} {itemTemplate.getDef()} {itemTemplate.getHp()} {itemTemplate.getMp()}");
                                    HistoryManager.addHistory(new History(player).setLog($"Show mảnh item {itemTemplate.name}").setObj(itemTemplate));
                                }
                                else
                                {
                                    player.redDialog(player.Language.ErrorItem);
                                }
                                return;
                            }

                        case GopetManager.ITEM_EVENT:
                            EventManager.FindAndUseItemEvent(itemSelect.itemTemplateId, player);
                            return;
                        case GopetManager.ITEM_NEED_TO_TRAIN_COIN:
                            if (itemSelect.Template.itemOption[0] != ItemInfo.OptionType.OPTION_HOURS_UP_COIN)
                            {
                                throw new UnsupportedOperationException();
                            }
                            int hours = itemSelect.Template.itemOptionValue[0];
                            player.playerData.TimeDropCoin = DateTime.Now.AddHours(hours);
                            player.okDialog(player.Language.USE_ITEM_UP_COIN_OK, itemSelect.Template.getName(player), Utilities.ToDateString(player.playerData.TimeDropCoin));
                            break;

                        case GopetManager.ITEM_NATIVE_TITLE:
                            int[] optionValue = itemSelect.Template.itemOptionValue;
                            player.controller.onReiceiveGift(new int[][] { new int[] { GopetManager.GIFT_TITLE, optionValue[0], 0, optionValue[1], optionValue[2], optionValue[3], optionValue[4], optionValue[5] } });
                            player.okDialog(player.Language.UseOK);
                            break;
                        case GopetManager.ITEM_THẺ_KỸ_NĂNG:
                            {
                                Pet pet = player.playerData.petSelected;
                                if (pet != null)
                                {
                                    if (!pet.Template.IsSky)
                                    {
                                        player.redDialog(player.Language.IncorrectPetUseSkillCard);
                                        return;
                                    }
                                    if (pet.skillPoint > 0)
                                    {
                                        int[] skillCanLearn = itemSelect.Template.itemOptionValue.Where(x => !pet.skill.Select(m => m[0]).Contains(x)).ToArray();
                                        if (skillCanLearn.Length <= 0)
                                        {
                                            player.redDialog(player.Language.IncorrectPetUseSkillCard);
                                            return;
                                        }
                                        int skillid = Utilities.RandomArray(skillCanLearn);
                                        if (player.controller.TryUseCardSkill(skillid, -1, out var curPet))
                                        {
                                            player.okDialog(player.Language.UseSkillCardOK, GopetManager.PETSKILL_HASH_MAP[skillid].name);
                                            HistoryManager.addHistory(new History(player).
                                                    setLog($"Dùng thẻ skill {itemSelect.Template.name} học được kỹ năng {GopetManager.PETSKILL_HASH_MAP[skillid].name}").
                                                    setObj(new
                                                    {
                                                        ItemCard = itemSelect,
                                                        skillId = skillid,
                                                        Pet = pet
                                                    }));
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.LearnSkillPetLaw);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        player.controller.objectPerformed[OBJKEY_ITEM_SKILL_CARD_USE] = itemSelect.Template.itemId;
                                        sendMenu(MENU_SELECT_SLOT_USE_SKILL_CARD, player);
                                        return;
                                    }
                                }
                                else
                                {
                                    player.petNotFollow();
                                    return;
                                }
                            }
                            break;
                        /*VUI LÒNG CHÚ Ý HÀM TRỪ VP CUỐI HÀNG*/
                        default:
                            {
                                player.redDialog(player.Language.CannotUseThisItem);
                                return;
                            }
                    }
                    player.controller.subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                }
                break;
            case MENU_KIOSK_HAT_SELECT:
            case MENU_KIOSK_AMOUR_SELECT:
            case MENU_KIOSK_WEAPON_SELECT:
            case MENU_KIOSK_GEM_SELECT:
            case MENU_KIOSK_OHTER_SELECT:
                {
                    if (menuId == MENU_KIOSK_OHTER_SELECT)
                    {
                        CopyOnWriteArrayList<Item> listEquipItems = player.playerData.getInventoryOrCreate(GopetManager.NORMAL_INVENTORY);
                        if (listEquipItems.Count > index && index >= 0)
                        {
                            Item sItem = listEquipItems.get(index);
                            player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, sItem);
                            player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                            if (sItem.count == 1)
                            {
                                player.controller.showInputDialog(INPUT_DIALOG_KIOSK, player.Language.Pricing, new String[] { "  " }, new sbyte[] { 0 });
                                player.controller.objectPerformed.put(OBJKEY_COUNT_OF_ITEM_KIOSK, 1);
                            }
                            else if (sItem.count > 1)
                            {
                                player.controller.showInputDialog(INPUT_DIALOG_COUNT_OF_KISOK_ITEM, player.Language.Count, new String[] { "  " }, new sbyte[] { 0 });
                            }
                        }
                    }
                    else
                    {
                        CopyOnWriteArrayList<Item> listEquipItems = Item.search(typeSelectItemMaterial(menuId, player), player.playerData.getInventoryOrCreate(menuId != MENU_KIOSK_GEM_SELECT ? GopetManager.EQUIP_PET_INVENTORY : GopetManager.GEM_INVENTORY));
                        if (listEquipItems.Count > index && index >= 0)
                        {
                            Item sItem = listEquipItems.get(index);
                            if (sItem != null)
                            {
                                if (sItem.petEuipId <= 0)
                                {
                                    if (sItem.gemInfo == null)
                                    {
                                        player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, sItem);
                                        player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                                        player.controller.showInputDialog(INPUT_DIALOG_KIOSK, player.Language.Pricing, new String[] { "  " }, new sbyte[] { 0 });
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.PleaseUnequipGem);
                                    }
                                }
                                else
                                {
                                    player.redDialog(player.Language.PleaseDoUpToKioskItemHasPetEquip);
                                }
                            }
                        }
                    }
                }
                break;
            case MENU_PET_REINCARNATION:
            case MENU_PET_SACRIFICE:
            case MENU_FUSION_MENU_PET:
            case MENU_KIOSK_PET_SELECT:
                {
                    if (index >= 0 && index < player.playerData.pets.Count)
                    {
                        Pet pet = player.playerData.pets.get(index);
                        if (pet.Expire != null)
                        {
                            player.redDialog(player.Language.YouCannotSellPetTry);
                            return;
                        }
                        switch (menuId)
                        {
                            case MENU_KIOSK_PET_SELECT:
                                {
                                    player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, pet);
                                    player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                                    player.controller.showInputDialog(INPUT_DIALOG_KIOSK, player.Language.Pricing, new String[] { "  " }, new sbyte[] { 0 });
                                }
                                break;
                            case MENU_FUSION_MENU_PET:
                                {
                                    player.controller.objectPerformed[OBJKEY_CURRENT_SELECT_PET_ID_FUSION] = index;
                                    sendMenu(MENU_FUSION_PET_OPTION, player);
                                }
                                break;
                            case MENU_PET_SACRIFICE:
                                {
                                    if (player.controller.getTaskCalculator().TryCheckPetSacrifice(pet))
                                    {
                                        player.playerData.pets.remove(pet);
                                        player.controller.getTaskCalculator().OnSacrifice(pet.lvl);
                                        player.okDialog(player.Language.PetSacrificeOK);
                                        HistoryManager.addHistory(new History(player).setLog($"Hiến tặng thú cưng {pet.getNameWithStar(player)}").setObj(pet));
                                    }
                                    else player.redDialog(player.Language.PetSacrificeFail);
                                }
                                break;
                            case MENU_PET_REINCARNATION:
                                {
                                    player.controller.objectPerformed[OBJKEY_PET_REINCARNATION] = pet;
                                    sendMenu(MENU_OPTION_PET_REINCARNATION, player);
                                }
                                break;
                        }
                    }
                }
                break;
            case MENU_KIOSK_GEM:
            case MENU_KIOSK_HAT:
            case MENU_KIOSK_WEAPON:
            case MENU_KIOSK_AMOUR:
            case MENU_KIOSK_OHTER:
            case MENU_KIOSK_PET:
                MarketPlace marketPlace = (MarketPlace)player.getPlace();
                Kiosk kiosk = null;
                switch (menuId)
                {
                    case MENU_KIOSK_HAT:
                        kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_HAT);
                        break;

                    case MENU_KIOSK_GEM:
                        kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_GEM);
                        break;
                    case MENU_KIOSK_WEAPON:
                        kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_WEAPON);
                        break;
                    case MENU_KIOSK_AMOUR:
                        kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_AMOUR);
                        break;
                    case MENU_KIOSK_OHTER:
                        kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_OTHER);
                        break;
                    case MENU_KIOSK_PET:
                        kiosk = MarketPlace.getKiosk(GopetManager.KIOSK_PET);
                        break;
                }
                kiosk.buy(index, player);
                break;

            case MENU_APPROVAL_CLAN_MEMBER:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if (clanMember.duty != Clan.TYPE_NORMAL)
                        {
                            ClanRequestJoin requestJoin = clan.getJoinRequestByUserId(index);
                            if (requestJoin != null)
                            {
                                player.controller.objectPerformed.put(OBJKEY_JOIN_REQUEST_SELECT, requestJoin.user_id);
                                sendMenu(MENU_APPROVAL_CLAN_MEM_OPTION, player);
                            }
                            else
                            {
                                player.redDialog(player.Language.ApprovalRequestIsApplyOrRemove);
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.YouOnlyIsMemeber);
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_APPROVAL_CLAN_MEM_OPTION:
                {
                    int user_id = (int)player.controller.objectPerformed.get(OBJKEY_JOIN_REQUEST_SELECT);
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if (clanMember.duty != Clan.TYPE_NORMAL)
                        {
                            ClanRequestJoin requestJoin = clan.getJoinRequestByUserId(user_id);
                            if (requestJoin != null)
                            {
                                switch (index)
                                {
                                    case 0:
                                        if (clan.canAddNewMember())
                                        {
                                            MySqlConnection MySqlConnection = MYSQLManager.create();
                                            try
                                            {
                                                bool hasClan = false;
                                                Player onlinePlayer = PlayerManager.get(requestJoin.user_id);
                                                if (onlinePlayer == null)
                                                {
                                                    var data = MySqlConnection.QueryFirstOrDefault(Utilities.Format("SELECT * from `player` where user_id = %s AND clanId > 0", requestJoin.user_id));
                                                    hasClan = data != null;
                                                }
                                                else
                                                {
                                                    hasClan = onlinePlayer.playerData.clanId > 0;
                                                }
                                                if (!hasClan)
                                                {
                                                    clan.addMember(user_id, requestJoin.name);
                                                    clan.getRequestJoin().remove(requestJoin);
                                                    if (onlinePlayer == null)
                                                    {
                                                        MySqlConnection.Execute(Utilities.Format("UPDATE `player` set clanId =%s where user_id =%s;", requestJoin.user_id, clanMember.getClan().getClanId()));
                                                    }
                                                    else
                                                    {
                                                        onlinePlayer.playerData.clanId = clanMember.getClan().getClanId();
                                                        onlinePlayer.okDialog(player.Language.YourApplyToClanIsAccept);
                                                    }
                                                    player.okDialog(player.Language.ApplyOK);
                                                }
                                                else
                                                {
                                                    player.redDialog(player.Language.ThisPlayerHaveClan);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                e.printStackTrace();
                                            }
                                            finally
                                            {
                                                MySqlConnection.Close();
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.NumOfMemberClanIsMax);
                                        }
                                        break;
                                    case 1:
                                        clan.getRequestJoin().remove(requestJoin);
                                        player.okDialog(player.Language.RemoveOK);
                                        break;
                                    case 2:
                                        clan.getRequestJoin().remove(requestJoin);
                                        clan.getBannedJoinRequestId().addIfAbsent(user_id);
                                        break;
                                    case 3:
                                        clan.getRequestJoin().Clear();
                                        player.okDialog(player.Language.RemoveAllOK);
                                        break;
                                }
                            }
                            else
                            {
                                player.redDialog(player.Language.ApprovalRequestIsApplyOrRemove);
                            }
                        }
                        else
                        {
                            player.redDialog(player.Language.YouOnlyIsMemeber);
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_SELECT_ITEM_ADMIN:
                GopetPlace place = (GopetPlace)player.getPlace();
                if (player.checkIsAdmin())
                {
                    switch (index)
                    {
                        case ADMIN_INDEX_SET_PET_INFO:
                            player.controller.showInputDialog(INPUT_DIALOG_SET_PET_SELECTED_INFo, "Đặt chỉ số pet đang đi theo", new String[] { "LVL:  ", "STAR:  ", "GYM:  " });
                            break;
                        case ADMIN_INDEX_COUNT_PLAYER:
                            player.okDialog(string.Format("Online player: {0}", PlayerManager.players.Count));
                            break;
                        case ADMIN_INDEX_COUNT_OF_MAP:
                            int numPlayerMap = 0;
                            foreach (Place place1 in place.map.places)
                            {
                                numPlayerMap += place1.numPlayer;
                            }
                            player.okDialog(Utilities.Format("Online player %s: %s", place.map.mapTemplate.getName(player), numPlayerMap));
                            break;
                        case ADMIN_INDEX_TELE_TO_MAP:
                            sendMenu(MENU_ADMIN_MAP, player);
                            break;
                        case ADMIN_INDEX_SELECT_ITEM:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_GET_ITEM, "Lấy vật phẩm", new String[] { "IdTemplate  :", "Số lượng   :" });
                            break;
                        case ADMIN_INDEX_TELE_TO_PLAYER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_TELE_TO_PLAYER, "Dịch chuyển tới người chơi", new String[] { "Tên \n người chơi :" });
                            break;
                        case ADMIN_INDEX_BAN_PLAYER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_LOCK_USER, "Khóa tài khoản người chơi", new String[] { "Tên \n người chơi :", "1 - phút, 2 - vĩnh viễn) :", "Thời gian khóa (phút) :", "Lý do  :" });
                            break;
                        case ADMIN_INDEX_UNBAN_PLAYER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_UNLOCK_USER, "Gỡ khóa tài khoản người chơi", new String[] { "Tên người chơi :" });
                            break;
                        case ADMIN_INDEX_SHOW_BANNER:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_CHAT_GLOBAL, "Chát thế giới", new String[] { "Văn bản :" });
                            break;
                        case ADMIN_INDEX_SHOW_HISTORY:
                            player.controller.showInputDialog(INPUT_DIALOG_ADMIN_GET_HISTORY, "Lấy lịch sử", new String[] { "Tên nhân vật :", "Ngày/tháng/năm (dd/mm/YYYY) : " });
                            break;
                        case ADMIN_INDEX_FIND_ITEM_LVL_10:
                            sendMenu(MENU_SHOW_ALL_PLAYER_HAVE_ITEM_LVL_10, player);
                            break;
                        case ADMIN_INDEX_BUFF_ENCHANT:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_TO_BUFF_ENCHANT, "Buff đập đồ", new String[] { "Tên nhân vật :" });
                            break;
                        case ADMIN_INDEX_GET_ITEM_FROM_PLAYER:
                            player.controller.adminSelectItemDatas.Clear();
                            player.controller.showInputDialog(INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM, "Lấy item", new String[] { "Tên nv lấy:" });
                            break;
                        case ADMIN_INDEX_GIVE_ITEM_TO_PLAYER:
                            player.controller.adminSelectItemDatas.Clear();
                            player.controller.showInputDialog(INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM, "Đưa item", new String[] { "Tên nv đưa :" });
                            break;
                        case ADMIN_INDEX_COIN:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_TO_BUFF_COIN, "Cộng từ tiền", new String[] { "Tiền :", "Tài khoản :" });
                            break;
                        case ADMIN_INDEX_GET_ZONE_ID:
                            player.okDialog($"Bạn đang ở khu {player.getPlace().zoneID} của map {player.getPlace().map.mapTemplate.getName(player)} mapId = {player.getPlace().map.mapID}");
                            break;
                        case ADMIN_INDEX_DELETE_ALL_EQUIP_PET_ITEM:
                            {
                                player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY).Clear();
                                player.okDialog("Dọn thành công");
                                break;
                            }
                        case ADMIN_INDEX_TELEPORT_ALL_PLAYER_TO_ADMIN:
                            {
                                foreach (var playerOnline in PlayerManager.players)
                                {
                                    if (playerOnline != player)
                                    {
                                        place.add(playerOnline);
                                        playerOnline.okDialog($"{player.playerData.name} đã dịch chuyển bạn đến đấy!");
                                    }
                                }
                                break;
                            }
                        case ADMIN_INDEX_ADD_ACHIEVEMENT:
                            sendMenu(MENU_ADMIN_SHOW_ALL_ACHIEVEMENT, player);
                            break;
                        case ADMIN_INDEX_SHOW_LIST_SERVER:
                            player.showListServer(GopetManager.ServerInfos.ToArray());
                            break;
                        case ADMIN_INDEX_DELETE_ALL_WING:
                            player.playerData.getInventoryOrCreate(GopetManager.WING_INVENTORY).Clear();
                            player.okDialog("Dọn thành công");
                            break;
                        case ADMIN_INDEX_BUFF_ENCHANT_TATTOO:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_BUFF_ENCHANT_TATTOO, "Buff cường hóa xăm", new String[] { "Tên nhân vật :" });
                            break;
                        case ADMIN_INDEX_PLAYER_LOCATION:
                            player.okDialog($"{player.playerData.x}|{player.playerData.y}   zone {place.zoneID}  map {place.map.mapID}");
                            break;
                        case ADMIN_INDEX_SET_MERGE_SERVER:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_PLAYER_TO_ENBALE_MERGE_SERVER, "Bật gộp cho nhân vật", "Tên nhân vật :");
                            break;
                        case ADMIN_INDEX_LOCK_ITEM_PLAYER:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_LOCK_ITEM_PLAYER, "Khoá item", new String[] { "Tên nv:" });
                            break;
                        case ADMIN_INDEX_UNLOCK_ITEM_PLAYER:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_UNLOCK_ITEM_PLAYER, "Mở khoá item", new String[] { "Tên nv:" });
                            break;
                        case ADMIN_INDEX_FAST_UP_ITEM:
                            player.controller.showInputRevertDialog(INPUT_TYPE_FAST_UP_ITEM, "Đập đồ nhanh", "Id vật phẩm", "Cấp từng món", "Lần tiến hoá", "Số lượng VP", "Max dòng chỉ số", "Cấp cuối", "Dung hợp");
                            break;
                        case ADMIN_INDEX_VIEW_CUR_PET_HIDDEN_STAT:
                            {
                                if (player.playerData.petSelected != null)
                                {
                                    player.playerData.petSelected.applyInfo(player);
                                    player.okDialog($"Những kích ẩn bao gồm: {string.Join(",", player.playerData.petSelected.TakeAllHiddenStat().Select(x => x.Comment))}");
                                }
                                else
                                {
                                    player.petNotFollow();
                                }
                            }
                            return;
                        case ADMIN_INDEX_BUFF_DUNG_HỢP:
                            {
                                sendMenu(MENU_ADMIN_BUFF_DUNG_HỢP, player);
                            }
                            return;
                    }
                }
                break;
            case MENU_ADMIN_SHOW_ALL_ACHIEVEMENT:
                {
                    if (player.checkIsAdmin())
                    {
                        if (index >= 0 && index < GopetManager.achievements.Count())
                        {
                            var ach = GopetManager.achievements.ElementAt(index);
                            if (player.playerData.achievements.Where(p => p.IdTemplate == ach.IdTemplate).Any())
                            {
                                player.redDialog("Bạn có danh hiệu này rồi");
                            }
                            else
                            {
                                Achievement achievement = new Achievement(ach.IdTemplate);
                                player.playerData.addAchivement(achievement, player);
                                player.okDialog($"Chúc mừng bạn nhận được danh hiệu {ach.Name}");
                            }
                        }
                    }
                    break;
                }
            case MENU_ADMIN_MAP:
                if (player.checkIsAdmin())
                {
                    MapManager.mapArr.get(index).addRandom(player);
                }
                break;
            case MENU_ITEM_MONEY_INVENTORY:
                {
                    player.controller.objectPerformed[OBJKEY_INDEX_ITEM_MONEY] = index;
                    sendMenu(MENU_MONEY_DISPLAY_SETTING, player);
                    break;
                }
            case MENU_MONEY_DISPLAY_SETTING:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_ITEM_MONEY))
                    {
                        int itemIndex = player.controller.objectPerformed[OBJKEY_INDEX_ITEM_MONEY];
                        CopyOnWriteArrayList<Item> items = player.playerData.getInventoryOrCreate(GopetManager.MONEY_INVENTORY);
                        if (itemIndex >= 0 && items.Count > itemIndex)
                        {
                            Item item = items[itemIndex];
                            switch (index)
                            {
                                case 0:
                                    player.playerData.MoneyDisplays.addIfAbsent(item.Template.itemId);
                                    player.okDialog(player.Language.PinOK);
                                    break;
                                case 1:
                                    player.playerData.MoneyDisplays.remove(item.Template.itemId);
                                    player.okDialog(player.Language.UnpinOK);
                                    break;
                            }
                            player.controller.updateUserInfo();
                        }
                    }
                }
                break;
            case MENU_SELECT_TYPE_CHANGE_GIFT:
                {
                    int count = 1;
                    switch (index)
                    {
                        case 0:
                            count = 1;
                            break;
                        case 1:
                            count = 5;
                            break;
                    }
                }
                break;

            case MENU_SELECT_TYPE_UPGRADE_DUTY:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        ClanMember memberSelect = clan.getMemberByUserId((int)player.controller.objectPerformed.get(OBJKEY_MEM_ID_UPGRADE_DUTY));
                        if (memberSelect == null)
                        {
                            player.redDialog(player.Language.ThisPlayerIsNotInThisClan);
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
                                        showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, string.Format(player.Language.YouGiveUpGuildPosition1, memberSelect.name), player);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.YouIsNotLeader);
                                    }
                                    break;
                                case 1:
                                    if (clanMember.duty == Clan.TYPE_LEADER)
                                    {
                                        showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, string.Format(player.Language.YouGiveUpGuildPosition2, memberSelect.name), player);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.YouIsNotLeader);
                                    }
                                    break;

                                case 2:
                                    if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER)
                                    {
                                        showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, string.Format(player.Language.YouGiveUpGuildPosition3, memberSelect.name), player);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.YouEnoughPermission);
                                    }
                                    break;

                                case 3:
                                    if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER)
                                    {
                                        showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, string.Format(player.Language.YouGiveUpGuildPosition4, memberSelect.name), player);
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.YouEnoughPermission);
                                    }
                                    break;

                                case 4:
                                    if (clanMember.duty == Clan.TYPE_LEADER || clanMember.duty == Clan.TYPE_DEPUTY_LEADER || clanMember.duty == Clan.TYPE_SENIOR)
                                    {
                                        showYNDialog(DIALOG_CONFIRM_ASK_UPGRADE_MEM_CLAN, string.Format(player.Language.DoYouWantKickMember, memberSelect.name), player);
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

            case MENU_UPGRADE_MEMBER_DUTY:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        ClanMember memberSelect = clan.getMemberByUserId(index);
                        if (memberSelect == null)
                        {
                            player.redDialog(player.Language.ThisPlayerIsNotInThisClan);
                        }
                        else if (clanMember == memberSelect)
                        {
                            player.redDialog(player.Language.YouCannotManipulateYourself);
                        }
                        else
                        {
                            player.controller.objectPerformed.put(OBJKEY_MEM_ID_UPGRADE_DUTY, index);
                            JArrayList<Option> list = new();
                            if (clanMember.duty == Clan.TYPE_LEADER)
                            {
                                list.add(new Option(0, player.Language.GiveUpClanPositionLeaderOption, 1));
                                list.add(new Option(1, player.Language.GiveUpClanPositionDeputyLeaderOption, 1));
                                list.add(new Option(2, player.Language.GiveUpClanPositionSeniorOption, 1));
                                list.add(new Option(3, player.Language.GiveUpClanPositionMemberOption, 1));
                            }
                            else if (clanMember.duty == Clan.TYPE_DEPUTY_LEADER)
                            {
                                list.add(new Option(2, player.Language.GiveUpClanPositionSeniorOption, 1));
                                list.add(new Option(3, player.Language.GiveUpClanPositionMemberOption, 1));
                            }

                            if (clanMember.duty != Clan.TYPE_NORMAL)
                            {
                                list.add(new Option(4, player.Language.Kick, 1));
                            }

                            player.controller.sendListOption(MENU_SELECT_TYPE_UPGRADE_DUTY, player.Language.GiveUpClanPositionTitle, CMD_CENTER_OK, list);
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case MENU_USE_ACHIEVEMNT:
                {
                    if (index >= 0 && index < player.playerData.achievements.Count)
                    {
                        player.controller.objectPerformed[OBJKEY_INDEX_ACHIEVEMNT_USE] = index;
                        sendMenu(MENU_USE_ACHIEVEMNT_OPTION, player);
                    }
                }
                break;
            case MENU_USE_ACHIEVEMNT_OPTION:
                {
                    if (!player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_ACHIEVEMNT_USE))
                    {
                        return;
                    }
                    switch (index)
                    {
                        case 0:
                            int index_Achievement = player.controller.objectPerformed[OBJKEY_INDEX_ACHIEVEMNT_USE];
                            if (index_Achievement >= 0 && index_Achievement < player.playerData.achievements.Count)
                            {
                                Achievement achievement = player.playerData.achievements[index_Achievement];
                                player.playerData.CurrentAchievementId = achievement.Id;
                                player.okDialog(player.Language.UseOK);
                                player.getPlace()?.updatePlayerAnimation(player);
                            }
                            break;
                        case 1:
                            if (player.playerData.CurrentAchievementId > 0)
                            {
                                player.playerData.CurrentAchievementId = -1;
                                player.okDialog(player.Language.UnequipOK);
                                player.getPlace()?.updatePlayerAnimation(player);
                            }
                            else
                            {
                                player.redDialog(player.Language.YouNotEquipArchievement);
                            }
                            break;
                    }
                }
                break;
            case MENU_LIST_REQUEST_ADD_FRIEND:
            case MENU_LIST_BLOCK_FRIEND:
            case MENU_LIST_FRIEND:
                {
                    player.controller.objectPerformed[OBJKEY_INDEX_FRIEND] = index;
                    switch (menuId)
                    {
                        case MENU_LIST_FRIEND:
                            sendMenu(MENU_LIST_FRIEND_OPTION, player);
                            break;
                        case MENU_LIST_BLOCK_FRIEND:
                            sendMenu(MENU_LIST_BLOCK_FRIEND_OPTION, player);
                            break;
                        case MENU_LIST_REQUEST_ADD_FRIEND:
                            sendMenu(MENU_LIST_REQUEST_ADD_FRIEND_OPTION, player);
                            break;
                    }
                }
                break;
            case MENU_LIST_FRIEND_OPTION:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_FRIEND))
                    {
                        int friendIndex = player.controller.objectPerformed[OBJKEY_INDEX_FRIEND];
                        if (friendIndex >= 0 && player.playerData.ListFriends.Count > friendIndex)
                        {
                            int friendId = player.playerData.ListFriends[friendIndex];
                            Player friend = PlayerManager.get(friendId);
                            switch (index)
                            {
                                case 0:
                                    {
                                        if (friend != null)
                                        {
                                            GopetPlace gopetPlace = friend.getPlace();
                                            if (gopetPlace != null)
                                            {
                                                player.okDialog(player.Language.GetFriendLocationInfo, friend.playerData.name, gopetPlace.map.mapTemplate.getName(player), gopetPlace.zoneID);
                                            }
                                            else
                                            {
                                                player.redDialog(player.Language.PlayerIsChangeMap, friend.playerData.name);
                                            }
                                        }
                                        else
                                        {
                                            player.redDialog(player.Language.PlayerOffline);
                                        }
                                    }
                                    break;
                                case 1:
                                    {
                                        goto DELETE_FRIEND;
                                    }
                                    break;
                                case 2:
                                    {
                                        player.playerData.BlockFriendLists.addIfAbsent(friendId);
                                        goto DELETE_FRIEND;
                                    }
                                    break;

                            }
                            break;
                        DELETE_FRIEND:
                            {
                                if (friend != null)
                                {
                                    friend.playerData.ListFriends.remove(player.user.user_id);
                                    player.playerData.ListFriends.remove(friendId);
                                    player.okDialog(player.Language.RemoveFriendOK);
                                }
                                else
                                {
                                    using (var conn = MYSQLManager.create())
                                    {
                                        FriendRequest friendRequest = conn.QueryFirstOrDefault<FriendRequest>("SELECT * FROM `request_remove_friend` WHERE `userId` = @userId AND `targetId` = @targetId;", new { userId = player.user.user_id, targetId = friendId });
                                        if (friendRequest == null)
                                        {
                                            conn.Execute("INSERT INTO `request_remove_friend`(`userId`, `targetId`, `time`) VALUES (@userId,@targetId,@time)", new FriendRequest(player.user.user_id, friendId, DateTime.Now));
                                        }
                                    }
                                    player.playerData.ListFriends.remove(friendId);
                                    player.okDialog(player.Language.RemoveFriendOK);
                                }
                            }
                        }
                    }
                }
                break;
            case MENU_LIST_REQUEST_ADD_FRIEND_OPTION:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_FRIEND))
                    {
                        int friendIndex = player.controller.objectPerformed[OBJKEY_INDEX_FRIEND];
                        if (friendIndex >= 0 && player.playerData.RequestAddFriends.Count > friendIndex)
                        {
                            int friendId = player.playerData.RequestAddFriends[friendIndex];
                            Player playerRequest = PlayerManager.get(friendId);
                            switch (index)
                            {
                                case 0:
                                    {
                                        if (playerRequest == null)
                                        {
                                            using (var conn = MYSQLManager.create())
                                            {
                                                FriendRequest friendRequest = new FriendRequest(player.user.user_id, friendId, DateTime.Now);
                                                if (!conn.Query("SELECT `userId`, `targetId`, `time` FROM `request_accept_friend` WHERE userId = @userId AND targetId = @targetId", friendRequest).Any())
                                                {
                                                    conn.Execute("INSERT INTO `request_accept_friend`(`userId`, `targetId`, `time`) VALUES (@userId,@targetId,@time)", friendRequest);
                                                }
                                            }
                                            player.playerData.ListFriends.addIfAbsent(friendId);
                                            player.playerData.RequestAddFriends.remove(friendId);
                                            player.okDialog(player.Language.AddFriendOK);
                                        }
                                        else
                                        {
                                            playerRequest.playerData.ListFriends.addIfAbsent(player.user.user_id);
                                            player.playerData.ListFriends.addIfAbsent(friendId);
                                            player.playerData.RequestAddFriends.remove(friendId);
                                            player.okDialog(player.Language.AddFriendOK);
                                        }
                                    }
                                    break;
                                case 1:
                                    {
                                        goto DeleteRequestAddFriends;
                                    }
                                    break;
                                case 2:
                                    {
                                        player.playerData.BlockFriendLists.addIfAbsent(friendId);
                                        goto DeleteRequestAddFriends;
                                    }
                                    break;
                                case 3:
                                    {
                                        player.playerData.ListFriends.Clear();
                                        player.okDialog(player.Language.RemoveFriendOK);
                                    }
                                    break;

                            }
                        DeleteRequestAddFriends:
                            {
                                player.playerData.RequestAddFriends.remove(friendId);
                                player.okDialog(player.Language.RemoveOK);
                            }
                        }
                    }
                }
                break;

            case MENU_LIST_BLOCK_FRIEND_OPTION:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_INDEX_FRIEND))
                    {
                        int friendIndex = player.controller.objectPerformed[OBJKEY_INDEX_FRIEND];
                        if (friendIndex >= 0 && player.playerData.BlockFriendLists.Count > friendIndex)
                        {
                            int friendId = player.playerData.BlockFriendLists[friendIndex];
                            switch (index)
                            {
                                case 0:
                                    {
                                        player.playerData.BlockFriendLists.remove(friendId);
                                        player.okDialog(player.Language.UnblockOK);
                                    }
                                    break;
                                case 1:
                                    {
                                        player.playerData.BlockFriendLists.Clear();
                                        player.okDialog(player.Language.UnblockAllOK);
                                    }
                                    break;
                            }
                        }
                    }
                }
                break;
            case MENU_SELL_TRASH_ITEM:
                {
                    List<Item> items = new List<Item>();
                    foreach (var itemKeypair in player.playerData.items)
                    {
                        items.AddRange(itemKeypair.Value);
                    }
                    if (index >= 0 && index < items.Count)
                    {
                        Item item = items[index];
                        player.controller.objectPerformed[OBJKEY_ITEM_TRASH_WANT_TO_SELL] = item;
                        if (item.count <= 1)
                        {
                            player.controller.sellItem(1, item);
                            return;
                        }
                        player.controller.showInputDialog(INPUT_COUNT_OF_ITEM_TRASH_WANT_SELL, player.Language.CountOfItemTrashWantSell, player.Language.Count);
                    }
                }
                break;
            case MENU_SELECT_ITEM_MERGE:
                {
                    if (player.playerData.IsMergeServer)
                    {
                        return;
                    }

                    switch (index)
                    {
                        case 0:
                            sendMenu(MENU_SELECT_ALL_ITEM_MERGE, player);
                            return;
                        case 1:
                            sendMenu(MENU_SELECT_ALL_PET_MERGE, player);
                            return;
                        case 2:
                            player.controller.mergeData.pets.Clear();
                            player.controller.mergeData.Items.Clear();
                            player.okDialog("Xoá vật phẩm đã chọn thành công");
                            return;
                        case 3:
                            {
                                if (!player.playerData.IsMergeServer)
                                {
                                    /* if (player.controller.mergeData.pets.Where(p => !GopetManager.ID_PET_MERGE_SERVER.Contains(p.petIdTemplate)).Count() > GopetManager.MAX_PET_MERGE_SERVER || player.controller.mergeData.Items.Where(x => !x.Template.isStackable).Sum(x => x.count) > GopetManager.MAX_ITEM_MERGE_SERVER)
                                     {
                                         player.redDialog($"Tối đa {GopetManager.MAX_PET_MERGE_SERVER} thú cưng");
                                         return;
                                     }

                                     if (player.controller.mergeData.Items.Any(x => !GopetManager.ID_ITEM_EQUIP_SILVER_MERGE_SERVER.Contains(x.itemTemplateId) && !GopetManager.ID_ITEM_MERGE_SERVER.Contains(x.itemTemplateId)))
                                     {
                                         player.redDialog($"Vật phẩm bạn đã chọn không nằm trong danh sạch cho phép gộp");
                                         return;
                                     }

                                     if (player.controller.mergeData.Items.Count(x => GopetManager.ID_ITEM_EQUIP_SILVER_MERGE_SERVER.Contains(x.itemTemplateId)) > 3)
                                     {
                                         player.redDialog("Đồ bạc không quá 3 món");
                                         return;
                                     }
                                     if (player.controller.mergeData.Items.Count(x => GopetManager.ID_ITEM_EQUIP_HAI_TAC_MERGE_SERVER.Contains(x.itemTemplateId)) > 8)
                                     {
                                         player.redDialog("Đồ hải tặc không quá 8 món");
                                         return;
                                     }

                                     if (player.controller.mergeData.Items.Count(x => GopetManager.ID_ITEM_EQUIP_TINH_VAN_MERGE_SERVER.Contains(x.itemTemplateId)) > 8)
                                     {
                                         player.redDialog("Đồ tinh vân không quá 8 món");
                                         return;
                                     }*/

                                    player.playerData.pets.AddRange(player.controller.mergeData.pets);
                                    foreach (var item in player.controller.mergeData.Items)
                                    {
                                        player.addItemToInventory(item);
                                    }
                                    player.playerData.IsMergeServer = true;
                                    player.playerData.save();
                                    player.okDialog("Gộp thành công vật phẩm và thú cưng đã về tay");
                                }
                                else
                                {
                                    player.redDialog("Lấy đồ về rồi");
                                }
                            }
                            return;
                    }
                }
                break;
            case MENU_SELECT_ALL_PET_MERGE:
                {
                    if (!player.playerData.IsMergeServer && player.controller.MergePlayerData != null)
                    {
                        CopyOnWriteArrayList<Pet> listPet = (CopyOnWriteArrayList<Pet>)player.controller.MergePlayerData.pets.clone();
                        Pet p = player.controller.MergePlayerData.petSelected;
                        if (p != null)
                        {
                            listPet.add(0, p);
                        }

                        if (index >= 0 && index < listPet.Count)
                        {
                            if (player.controller.mergeData.pets.Contains(listPet[index]))
                            {
                                player.redDialog("Thú cưng đã trong hàng chờ");
                                return;
                            }
                            player.controller.mergeData.pets.Add(listPet[index]);
                            player.okDialog("Thêm thành công");
                        }
                    }
                }
                return;
            case MENU_OPTION_ADMIN_GET_ITEM:
                {
                    if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_ITEM_ADMIN_GET, OBJKEY_PLAYER_GET_ITEM))
                    {
                        Player ThatPlayer = player.controller.objectPerformed[OBJKEY_PLAYER_GET_ITEM];
                        Item ItemSelect = player.controller.objectPerformed[OBJKEY_ITEM_ADMIN_GET];
                        int count = 1;
                        if (player.controller.objectPerformed.ContainsKey(OBJKEY_COUNT_ITEM_TO_GET_BY_ADMIN))
                        {
                            count = player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GET_BY_ADMIN];
                        }
                        switch (index)
                        {
                            case 0:
                                {
                                    var queryList = player.controller.adminSelectItemDatas.Where(x => x.Item == ItemSelect);
                                    if (queryList.Any())
                                    {
                                        player.redDialog("Vật phẩm đã tồn tại");
                                        return;
                                    }
                                    if (count > ItemSelect.count && ItemSelect.Template.isStackable)
                                    {
                                        player.redDialog(player.Language.WrongNumOfItem);
                                        return;
                                    }
                                    player.controller.adminSelectItemDatas.Add(new AdminSelectItemData(count, ItemSelect));
                                    player.okDialog(player.Language.OK);
                                }
                                break;
                            case 1:
                                {
                                    var queryList = player.controller.adminSelectItemDatas.Where(x => x.Item == ItemSelect);
                                    if (!queryList.Any())
                                    {
                                        player.redDialog("Vật phẩm chưa thêm vào");
                                        return;
                                    }
                                    player.controller.adminSelectItemDatas.Remove(queryList.FirstOrDefault());
                                    player.okDialog(player.Language.OK);
                                }
                                break;
                            case 2:
                                {
                                    if (player.controller.adminSelectItemDatas.IsEmpty)
                                    {
                                        player.redDialog("Không có món nào");
                                        return;
                                    }
                                    if (Move(ThatPlayer, player, player.controller.adminSelectItemDatas))
                                    {
                                        player.controller.adminSelectItemDatas.Clear();
                                        player.okDialog("Lấy vật phẩm thành công");
                                        ThatPlayer.okDialog($"Người chơi {player.playerData.name} lấy vật phẩm thành công");
                                    }
                                }
                                break;
                        }
                    }
                }
                break;
            case MENU_OPTION_ADMIN_GIVE_ITEM:
                {
                    if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_ITEM_ADMIN_GIVE, OBJKEY_PLAYER_GIVE_ITEM))
                    {
                        Player ThatPlayer = player.controller.objectPerformed[OBJKEY_PLAYER_GIVE_ITEM];
                        Item ItemSelect = player.controller.objectPerformed[OBJKEY_ITEM_ADMIN_GIVE];
                        int count = 1;
                        if (player.controller.objectPerformed.ContainsKey(OBJKEY_COUNT_ITEM_TO_GIVE_BY_ADMIN))
                        {
                            count = player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GIVE_BY_ADMIN];
                        }
                        switch (index)
                        {
                            case 0:
                                {
                                    var queryList = player.controller.adminSelectItemDatas.Where(x => x.Item == ItemSelect);
                                    if (queryList.Any())
                                    {
                                        player.redDialog("Vật phẩm đã tồn tại");
                                        return;
                                    }
                                    if (count > ItemSelect.count && ItemSelect.Template.isStackable)
                                    {
                                        player.redDialog(player.Language.WrongNumOfItem);
                                        return;
                                    }
                                    player.controller.adminSelectItemDatas.Add(new AdminSelectItemData(count, ItemSelect));
                                    player.okDialog(player.Language.OK);
                                }
                                break;
                            case 1:
                                {
                                    var queryList = player.controller.adminSelectItemDatas.Where(x => x.Item == ItemSelect);
                                    if (!queryList.Any())
                                    {
                                        player.redDialog("Vật phẩm chưa thêm vào");
                                        return;
                                    }
                                    player.controller.adminSelectItemDatas.Remove(queryList.FirstOrDefault());
                                    player.okDialog(player.Language.OK);
                                }
                                break;
                            case 2:
                                {
                                    if (player.controller.adminSelectItemDatas.IsEmpty)
                                    {
                                        player.redDialog("Không có món nào");
                                        return;
                                    }
                                    if (Move(player, ThatPlayer, player.controller.adminSelectItemDatas))
                                    {
                                        player.controller.adminSelectItemDatas.Clear();
                                        player.okDialog("Chuyển vật phẩm thành công");
                                        ThatPlayer.okDialog($"Người chơi {player.playerData.name} chuyển vật phẩm thành công");
                                    }
                                }
                                break;
                        }
                    }
                }
                break;
            case MENU_SELECT_SLOT_USE_SKILL_CARD:
                {
                    Pet p = player.getPet();
                    if (p == null || !player.playerData.petSelected.Template.IsSky)
                    {
                        player.redDialog(player.Language.IncorrectPetUseSkillCard);
                        return;
                    }
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_ITEM_SKILL_CARD_USE))
                    {
                        switch (index)
                        {
                            case 3: return;
                            default:
                                {
                                    if (index >= 0 && index < 3)
                                    {
                                        Item item = player.controller.selectItemsbytemp(player.controller.objectPerformed[OBJKEY_ITEM_SKILL_CARD_USE], GopetManager.NORMAL_INVENTORY);
                                        if (item != null && GameController.checkCount(item, 1))
                                        {
                                            int[] skillCanLearn = item.Template.itemOptionValue.Where(x => !p.skill.Select(m => m[0]).Contains(x)).ToArray();
                                            if (skillCanLearn.Length <= 0)
                                            {
                                                player.redDialog(player.Language.IncorrectPetUseSkillCard);
                                                return;
                                            }
                                            int skillId = Utilities.RandomArray(skillCanLearn);
                                            if (player.controller.TryUseCardSkill(skillId, index, out var pet))
                                            {
                                                player.controller.subCountItem(item, 1, GopetManager.NORMAL_INVENTORY);
                                                player.okDialog(player.Language.UseSkillCardOK, GopetManager.PETSKILL_HASH_MAP[skillId].name);
                                                HistoryManager.addHistory(new History(player).
                                                    setLog($"Dùng thẻ skill {item.Template.name} học được kỹ năng {GopetManager.PETSKILL_HASH_MAP[skillId].name}").
                                                    setObj(new
                                                    {
                                                        ItemCard = item,
                                                        skillId = skillId,
                                                        Pet = pet
                                                    }));
                                            }
                                            else
                                            {
                                                player.redDialog(player.Language.LearnSkillPetLaw);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.redDialog(player.Language.BugWarning);
                                    }
                                }
                                break;
                        }
                    }
                    else player.fastAction();
                }
                break;
            case MENU_OPTION_USE_FLOWER:
                {
                    int num = 1;
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_COUNT_USE_BÓ_HOA))
                    {
                        num = Math.Max(1, player.controller.objectPerformed[OBJKEY_COUNT_USE_BÓ_HOA]);
                    }
                    if (TeacherDay2024.Instance.Condition)
                    {
                        if (index >= 0 && index < 2)
                        {
                            Item itemFlower = player.controller.selectItemsbytemp(TeacherDay2024.BOÁ_HOA, GopetManager.NORMAL_INVENTORY);
                            if (checkMoney((sbyte)index, TeacherDay2024.Data[index].Item1 * num, player) && player.controller.checkCountItem(itemFlower, TeacherDay2024.Data[index].Item2 * num))
                            {
                                player.controller.subCountItem(itemFlower, TeacherDay2024.Data[index].Item2 * num, GopetManager.NORMAL_INVENTORY);
                                addMoney((sbyte)index, -(TeacherDay2024.Data[index].Item1 * num), player);
                                switch (index)
                                {
                                    case 0:
                                        player.playerData.NumGiveFlowerGold += 10 * num;
                                        player.playerData.FlowerGold += 10 * num;
                                        break;
                                    case 1:
                                        player.playerData.NumGiveFlowerGem += 10 * num;
                                        player.playerData.FlowerCoin += 10 * num;
                                        break;
                                }
                                player.okDialog(player.Language.UseFlowerOK, 10 * num);
                            }
                            else
                            {
                                player.redDialog(player.Language.NotEnoughMaterial);
                            }
                        }
                        else if (index == 3)
                        {
                            player.controller.showInputDialog(INPUT_TYPE_COUNT_USE_BÓ_HOA, player.Language.CountUseFlower, player.Language.Count);
                        }
                    }
                }
                break;
            case MENU_OPTION_SHOW_FUSION_MENU:
                {
                    switch (index)
                    {
                        case 0:
                            player.controller.objectPerformed.Clear();
                            sendMenu(MENU_FUSION_MENU_EQUIP, player);
                            break;
                        case 1:
                            sendMenu(MENU_FUSION_MENU_PET, player);
                            break;
                    }
                }
                break;
            case MENU_FUSION_EQUIP_OPTION:
                {
                    switch (index)
                    {
                        case 0:
                            {
                                if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_CURRENT_ITEM_ID_FUSION, OBJKEY_CURRENT_ITEM_TEMP_ID_FUSION))
                                {
                                    Item item = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_CURRENT_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                                    if (item != null)
                                    {
                                        if (item.lvl >= 10)
                                        {
                                            player.controller.objectPerformed[OBJKEY_MAIN_ITEM_ID_FUSION] = item.itemId;
                                            sendMenu(MENU_FUSION_MENU_EQUIP, player);
                                            player.okDialog(player.Language.SelectMainFusionItemOK);
                                        }
                                        else player.redDialog(player.Language.SelectMainFusionItemFail);
                                    }
                                    else player.redDialog(player.Language.ItemNotFound);
                                }
                            }
                            break;
                        case 1:
                            {
                                if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_CURRENT_ITEM_ID_FUSION, OBJKEY_CURRENT_ITEM_TEMP_ID_FUSION))
                                {
                                    Item item = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_CURRENT_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                                    if (item != null)
                                    {
                                        player.controller.objectPerformed[OBJKEY_DEPUTY_ITEM_ID_FUSION] = item.itemId;
                                        sendMenu(MENU_FUSION_MENU_EQUIP, player);
                                        player.okDialog(player.Language.SelectDeputyFusionItemOK);
                                    }
                                    else player.redDialog(player.Language.ItemNotFound);
                                }
                            }
                            break;
                        case 2:
                            {
                                if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_MAIN_ITEM_ID_FUSION, OBJKEY_DEPUTY_ITEM_ID_FUSION))
                                {
                                    Item itemMain = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_MAIN_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                                    Item itemDeputy = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_DEPUTY_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                                    if (itemDeputy == null || itemMain == null)
                                    {
                                        player.redDialog(player.Language.ItemNotFound);
                                        return;
                                    }
                                    if (itemMain.NumFusion < 16)
                                    {
                                        sendMenu(MENU_FUSION_EQUIP_OPTION_COMFIRM, player);
                                    }
                                }
                                else player.redDialog(player.Language.FusionFailByNotEnoughtItem);
                            }
                            break;
                    }
                }
                break;
            case MENU_FUSION_EQUIP_OPTION_COMFIRM:
                {
                    if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_MAIN_ITEM_ID_FUSION, OBJKEY_DEPUTY_ITEM_ID_FUSION))
                    {
                        Item itemMain = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_MAIN_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                        Item itemDeputy = player.controller.selectItemByItemId(player.controller.objectPerformed[OBJKEY_DEPUTY_ITEM_ID_FUSION], GopetManager.EQUIP_PET_INVENTORY);
                        if (itemDeputy == null || itemMain == null)
                        {
                            player.redDialog(player.Language.ItemNotFound);
                            return;
                        }
                        if (itemDeputy == itemMain)
                        {
                            player.redDialog(player.Language.DuplicateItem);
                            return;
                        }

                        if (itemMain.NumFusion < GopetManager.FusionGOLD.Length)
                        {
                            if (index >= 0 && index < 2)
                            {
                                Tuple<int, float> Fusion = GopetManager.FusionData[index][itemMain.NumFusion];
                                if (checkMoney((sbyte)index, Fusion.Item1, player))
                                {
                                    addMoney((sbyte)index, -Fusion.Item1, player);
                                    player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY).remove(itemDeputy);
                                    bool IsSuccess = Utilities.NextFloatPer() < Fusion.Item2 || player.controller.isBuffEnchent;
                                    if (IsSuccess)
                                    {
                                        itemMain.NumFusion++;
                                        player.okDialog(player.Language.FusionOK);
                                    }
                                    else player.redDialog(player.Language.FusionFail);
                                }
                                else player.redDialog(player.Language.NotEnoughMaterial);
                            }
                        }
                    }
                }
                break;
            case MENU_FUSION_PET_OPTION:
                {
                    if (!player.controller.objectPerformed.ContainsKey(OBJKEY_CURRENT_SELECT_PET_ID_FUSION))
                    {
                        return;
                    }
                    int indexPet = player.controller.objectPerformed[OBJKEY_CURRENT_SELECT_PET_ID_FUSION];
                    if (indexPet < 0 || indexPet > player.playerData.pets.Count)
                    {
                        return;
                    }
                    Pet pet = player.playerData.pets[indexPet];
                    if (pet == null) return;
                    switch (index)
                    {
                        case 0:
                            {
                                player.controller.objectPerformed[OBJKEY_MAIN_PET_ID_FUSION] = pet.petId;
                                sendMenu(MENU_FUSION_MENU_PET, player);
                                player.okDialog(player.Language.SelectMainFusionPetOK);
                            }
                            break;
                        case 1:
                            {
                                player.controller.objectPerformed[OBJKEY_DEPUTY_PET_ID_FUSION] = pet.petId;
                                sendMenu(MENU_FUSION_MENU_PET, player);
                                player.okDialog(player.Language.SelectDeputyFusionPetOK);
                            }
                            break;
                        case 2:
                            {
                                sendMenu(MENU_FUSION_PET_OPTION_COMFIRM, player);
                            }
                            break;
                    }
                }
                break;
            case MENU_FUSION_PET_OPTION_COMFIRM:
                {
                    if (index >= 0 && index < 2)
                    {
                        if (player.controller.objectPerformed.ContainsKeyZ(OBJKEY_MAIN_PET_ID_FUSION, OBJKEY_DEPUTY_PET_ID_FUSION))
                        {
                            Pet petMain = player.playerData.pets.BinarySearch((int)player.controller.objectPerformed[OBJKEY_MAIN_PET_ID_FUSION]);
                            Pet petDeputy = player.playerData.pets.BinarySearch((int)player.controller.objectPerformed[OBJKEY_DEPUTY_PET_ID_FUSION]);
                            if (petMain == null || petDeputy == null)
                            {
                                player.redDialog(player.Language.CouldNotFoundPet);
                                return;
                            }
                            if (petMain == petDeputy)
                            {
                                player.redDialog(player.Language.DuplicatePet);
                                return;
                            }
                            if (checkMoney((sbyte)index, GopetManager.FusionPetPrice[index], player) && player.playerData.pets.Contains(petDeputy))
                            {
                                addMoney((sbyte)index, -GopetManager.FusionPetPrice[index], player);
                                player.playerData.pets.Remove(petDeputy);
                                petMain.tiemnang_point += petDeputy.Template.FusionScore;
                                player.okDialog(player.Language.FusionPetOK, petDeputy.Template.FusionScore);
                            }
                            else player.redDialog(player.Language.NotEnoughMaterial);
                        }
                        else player.redDialog(player.Language.NotEnoughMaterial);
                    }
                }
                break;
            case MENU_OPTION_PET_REINCARNATION:
                {
                    if (!player.checkIsAdmin())
                    {
                        player.redDialog("Tính năng này cho admin kiểm thử");
                        return;
                    }
                    if (index >= 0 && index < 2)
                    {
                        if (!player.controller.objectPerformed.ContainsKey(OBJKEY_PET_REINCARNATION))
                        {
                            player.redDialog("Chưa chọn thú cưng trùng sinh");
                            return;
                        }

                        Pet pet = player.controller.objectPerformed[OBJKEY_PET_REINCARNATION];
                        if (pet.lvl < 41)
                        {
                            player.redDialog("Thú cưng phải cấp 41 trở lên mới có thể trùng sinh");
                            return;
                        }
                        if (!GopetManager.Reincarnations.ContainsKey(pet.petIdTemplate))
                        {
                            player.redDialog("Thú cưng này không thể trùng sinh");
                            return;
                        }
                        PetReincarnation petReincarnation = GopetManager.Reincarnations[pet.petIdTemplate];
                        Item cardReincarnation = player.controller.selectItemsbytemp(GopetManager.ID_ITEM_CARD_REINCARNATION, GopetManager.NORMAL_INVENTORY);
                        if (cardReincarnation == null)
                        {
                            player.redDialog("Không có thẻ trùng sinh");
                            return;
                        }
                        if (!GameController.checkCount(cardReincarnation, petReincarnation.NumCard))
                        {
                            player.redDialog("Không đủ thẻ trùng sinh, cần {0} thẻ", petReincarnation.NumCard);
                            return;
                        }
                        if (checkMoney((sbyte)index, GopetManager.ReincarnationPetPrice[index], player))
                        {
                            addMoney((sbyte)index, -GopetManager.ReincarnationPetPrice[index], player);
                            player.controller.subCountItem(cardReincarnation, petReincarnation.NumCard, GopetManager.NORMAL_INVENTORY);
                            pet.petIdTemplate = petReincarnation.PetIdReincarnation;
                            pet.skill = new int[0][];
                            pet.lvl = 1;
                            pet.exp = 0;
                            pet.pointTiemNangLvl = pet.Template.gymUpLevel;
                            pet.tiemnang_point = 0;
                            pet.str = pet.Template.str;
                            pet.agi = pet.Template.agi;
                            pet._int = pet.Template._int;
                            Array.Fill(pet.tiemnang, 0);
                            int maxTatto = Utilities.nextInt(0, 3);
                            while (maxTatto <= pet.tatto.Count && pet.tatto.Count != 0)
                            {
                                pet.tatto.removeAt(Utilities.nextInt(0, pet.tatto.Count));
                            }
                            player.okDialog("Trùng sinh thành công");
                        }
                        else
                        {
                            NotEngouhMoney((sbyte)index, GopetManager.ReincarnationPetPrice[index], player);
                        }
                    }
                }
                break;
            case MENU_OPTION_KIOSK:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_PRICE_KIOSK_ITEM))
                    {
                        int price = player.controller.objectPerformed[OBJKEY_PRICE_KIOSK_ITEM];
                        switch (index)
                        {
                            case 0:
                                {
                                    player.controller.objectPerformed.Remove(OBJKEY_PRICE_KIOSK_ITEM);
                                    SellKioskItem(player, price);
                                }
                                break;
                            case 1:
                                {
                                    player.controller.showInputDialog(INPUT_ASSIGNED_NAME_KIOSK, "Chỉ định", "Tên nhân vật: ");
                                }
                                break;
                        }
                    }
                    break;
                }
            case MENU_OPTION_KIOSK_CANCEL_ITEM:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_ITEM_KIOSK_CANCEL))
                    {
                        switch (index)
                        {
                            case 0:
                                player.controller.removeSellItem(player.controller.objectPerformed[OBJKEY_ITEM_KIOSK_CANCEL]);
                                return;
                            case 1:
                                player.controller.showInputDialog(INPUT_ASSIGNED_CHANGE_NAME_KIOSK, "Chỉ định", "Tên nhân vật: ");
                                return;
                            case 2:
                                {
                                    int itemId = player.controller.objectPerformed[OBJKEY_ITEM_KIOSK_CANCEL];
                                    sbyte typeKiosk = (sbyte)player.controller.objectPerformed.get(MenuController.OBJKEY_TYPE_SHOW_KIOSK);
                                    Kiosk kiosk_ = MarketPlace.getKiosk(typeKiosk);
                                    SellItem sellItem = kiosk_.searchItem(itemId);
                                    if (sellItem != null)
                                    {
                                        if (sellItem.pet != null)
                                        {
                                            player.redDialog("Thú cưng bán theo kí à! @@");
                                            return;
                                        }
                                        if (!sellItem.ItemSell.Template.isStackable)
                                        {
                                            player.redDialog("Mấy món này bịp nha, có phải đồ gộp đâu!");
                                            return;
                                        }
                                        if (sellItem.TotalCount * 10 > sellItem.price)
                                        {
                                            player.redDialog("ít nhất giá tổng phải lớn hơn {0} (ngoc)", Utilities.FormatNumber(sellItem.TotalCount * 10));
                                            return;
                                        }
                                        if (sellItem.sumVal > 0)
                                        {
                                            player.redDialog("Không thể hủy vì đã có người mua lẻ vài món");
                                        }
                                        else
                                        {
                                            sellItem.IsRetail = !sellItem.IsRetail;
                                            player.okDialog("Thay đổi thành công. Hiện tại {0}", sellItem.IsRetail ? "cho phép bán lẻ" : "không cho phép bán lẻ");
                                        }
                                    }
                                }
                                return;
                        }
                    }
                }
                break;
            case MENU_OPTION_BUY_KIOSK_ITEM:
                {
                    if (!player.controller.objectPerformed.ContainsKey(OBJKEY_KIOSK_ITEM))
                    {
                        return;
                    }
                    var obj = player.controller.objectPerformed.get(OBJKEY_KIOSK_ITEM);
                    var objENtry = (KeyValuePair<Kiosk, SellItem>)obj;
                    switch (index)
                    {
                        case 0:
                            player.controller.objectPerformed.Remove(OBJKEY_KIOSK_ITEM);
                            objENtry.Key.confirmBuy(player, objENtry.Value);
                            return;
                        case 1:
                            player.controller.showInputDialog(INPUT_NUM_BUY_RETAIL_ITEM_KIOSK, "Nhập số lượng", "Số lượng: ");
                            return;
                    }
                }
                break;
            default:
                {
                    player.redDialog(player.Language.CannotFindMenu, menuId);
                    Thread.Sleep(1000);
                }
                break;
        }
    }
}

