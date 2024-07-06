
using Gopet.Battle;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.Dialog;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.User;
using Gopet.IO;
using Gopet.Util;
using MySql.Data.MySqlClient;
using Gopet.Data.item;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Gopet.Data.top;
using Gopet.Data.Event;
using Gopet.Data.user;
using Gopet.Manager;
using Gopet.Data.Clan;

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
                                    player.playerData.pets.Add(p);
                                    player.controller.unfollowPet(p);
                                    player.okDialog(player.Language.ManipulateOK);
                                    HistoryManager.addHistory(new History(player).setLog("Tháo pet"));
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
                                    HistoryManager.addHistory(new History(player).setLog("Tháo cải trang " + it.getName()).setObj(it));
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
                        if (!checkMoney((sbyte)index, -(index == 0 ? GopetManager.PRICE_GOLD_ARENA_JOURNALISM : GopetManager.PRICE_COIN_ARENA_JOURNALISM), player)) return;

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
                        showYNDialog(DIALOG_ASK_ENCHANT_TATTO, string.Format(player.Language.AskSelectTattoEnchantLaw, tatto.Template.name, tatto.lvl + 1, GopetManager.PERCENT_OF_ENCHANT_TATOO[tatto.lvl], getMoneyText((sbyte)index, index == 0 ? GopetManager.PRICE_GOLD_ENCHANT_TATTO : GopetManager.PRICE_COIN_ENCHANT_TATTO, player), GopetManager.NUM_LVL_DROP_ENCHANT_TATTO_FAILED[tatto.lvl]), player);
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
                            player.okDialog(string.Format(player.Language.CongratulateGetNewPet, p.getNameWithStar()));
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
                if (index >= 0 && index < PetFreeList.Count)
                {
                    PetMenuItemInfo petMenuItemInfo = (PetMenuItemInfo)PetFreeList.get(index);
                    if (!player.playerData.isFirstFree)
                    {
                        player.playerData.isFirstFree = true;
                        Pet p = new Pet(petMenuItemInfo.getPetTemplate().petId);
                        player.playerData.addPet(p, player);
                        player.okDialog(string.Format(player.Language.GetPetFreeOK, petMenuItemInfo.getPetTemplate().name));
                        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Nhận pet %s miễn phí tại NPC trân trân", petMenuItemInfo.getPetTemplate().name)).setObj(p));
                    }
                    else
                    {
                        player.redDialog(player.Language.YouHaveGotPetFreeBefore);
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
                                    HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Học kỹ năng thành công cho pet %s", pet.getNameWithoutStar())).setObj(pet));
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
                    if (index >= 0 && index < gym_options.Length)
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
                                HistoryManager.addHistory(new History(player).setLog("Tảy tìm năng cho pet" + pet.getNameWithoutStar()).setObj(pet));
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
                        player.okDialog(string.Format(player.Language.SelectPetDefOK, pet.getNameWithStar()));
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
                                    Item item = new Item(shopTemplateItem.getItemTempalteId());
                                    item.count = shopTemplateItem.getCount();
                                    if (item.getTemp().expire > 0)
                                    {
                                        item.expire = Utilities.CurrentTimeMillis + item.getTemp().getExpire();
                                    }
                                    player.addItemToInventory(item);
                                    player.okDialog(string.Format(player.Language.YouBuyItemOK, item.getTemp().getName()));
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
                                    player.okDialog(string.Format(player.Language.YouBuyItemOK, p.getNameWithStar()));
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
                            player.controller.selectMaterialEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 1);
                            break;
                        case MENU_SELECT_ENCHANT_MATERIAL2:
                            player.controller.selectMaterialEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 2);
                            break;
                        case MENU_SELECT_GEM_ENCHANT_MATERIAL1:
                            {
                                player.controller.selectMaterialGemEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 1);
                                player.controller.selectGemM1 = true;
                            }
                            break;
                        case MENU_SELECT_GEM_ENCHANT_MATERIAL2:
                            {
                                player.controller.selectMaterialGemEnchant(itemSelect.getTemp().getItemId(), itemSelect.getTemp().getIconPath(), itemSelect.getTemp().getName(), 12);
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
                                        player.okDialog(string.Format(player.Language.MergePartPetOK, pet.getNameWithStar()));
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
                                    if (pet.skill[skillIndex][1] < 10)
                                    {
                                        player.controller.objectPerformed.put(OBJKEY_ITEM_UP_SKILL, itemSelect);
                                        showYNDialog(DIALOG_UP_SKILL, string.Format(player.Language.AskDoYouWantUpgradeSkill, petSkill.name, pet.skill[skillIndex][1] + 1, GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]], itemSelect.getTemp().getOptionValue()[0], GopetManager.PERCENT_UP_SKILL[pet.skill[skillIndex][1]] + itemSelect.getTemp().getOptionValue()[0]).Replace("/", "%"), player);
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
                                player.controller.selectGemUpTier(itemSelect.itemId, itemSelect.getTemp().getIconPath(), itemSelect.getEquipName(), 1, itemSelect.lvl);
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
                                    player.addItemToInventory(item);
                                    player.okDialog(string.Format(player.Language.ChangeItemOK, item.getTemp().getName()));
                                }
                                else
                                {
                                    player.redDialog(string.Format(player.Language.MergePartItemFail, optionValue[1]));
                                }
                            }
                            break;
                        case MENU_SELECT_MATERIAL2_TO_ENCHANT_TATOO:
                            {
                                player.controller.sendItemSelectTattoMaterialToEnchant(itemSelect.itemId, itemSelect.Template.iconPath, itemSelect.Template.name);
                                break;
                            }
                        case MENU_SELECT_MATERIAL1_TO_ENCHANT_TATOO:
                            {
                                player.controller.sendItemSelectTattoMaterialToEnchant(itemSelect.itemId, itemSelect.Template.iconPath, itemSelect.Template.name);
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
                                Player playerOnline = player.controller.objectPerformed[OBJKEY_PLAYER_GET_ITEM];
                                if (PlayerManager.players.Contains(playerOnline))
                                {
                                    sbyte inventory = playerOnline.playerData.items.Where(p => p.Value.Any(ic => ic == itemSelect)).First().Key;
                                    Item item = itemSelect;
                                    if (itemSelect.Template.isStackable)
                                    {
                                        int count = player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GET_BY_ADMIN];
                                        if (count > item.count)
                                        {
                                            player.redDialog(player.Language.WrongNumOfItem);
                                            return;
                                        }
                                        else
                                        {
                                            playerOnline.controller.subCountItem(itemSelect, count, inventory);
                                            item = new Item(itemSelect.Template.itemId, count);
                                        }
                                    }
                                    else
                                    {
                                        playerOnline.playerData.removeItem(inventory, itemSelect);
                                    }
                                    playerOnline.playerData.save();

                                    player.addItemToInventory(item, inventory);
                                    player.okDialog($"Bạn đã lấy thành công {item.Template.name}");
                                    playerOnline.redDialog($"Bạn đã bị lấy mất {item.Template.name}");
                                }
                                else
                                {
                                    player.redDialog(player.Language.PlayerOffline);
                                }
                                break;
                            }
                        case MENU_SELECT_ITEM_TO_GIVE_BY_ADMIN:
                            {
                                Player playerOnline = player.controller.objectPerformed[OBJKEY_PLAYER_GIVE_ITEM];
                                if (PlayerManager.players.Contains(playerOnline))
                                {
                                    sbyte inventory = player.playerData.items.Where(p => p.Value.Any(ic => ic == itemSelect)).First().Key;
                                    Item item = itemSelect;
                                    if (itemSelect.Template.isStackable)
                                    {
                                        int count = player.controller.objectPerformed[OBJKEY_COUNT_ITEM_TO_GIVE_BY_ADMIN];
                                        if (count > item.count)
                                        {
                                            player.redDialog(player.Language.WrongNumOfItem);
                                            return;
                                        }
                                        else
                                        {
                                            player.controller.subCountItem(itemSelect, count, inventory);
                                            item = new Item(itemSelect.Template.itemId, count);
                                        }
                                    }
                                    else
                                    {
                                        player.playerData.removeItem(inventory, itemSelect);
                                    }

                                    playerOnline.addItemToInventory(item, inventory);
                                    player.okDialog($"Bạn đã đưa thành công {item.Template.name}");
                                    playerOnline.okDialog($"Bạn đã nhận được {item.Template.name}");
                                    playerOnline.playerData.save();
                                }
                                else
                                {
                                    player.redDialog(player.Language.PlayerOffline);
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
                                                    showYNDialog(DIALOG_ASK_ENCHANT_WING, string.Format(player.Language.AskDoYouWantEnchantWing, wingItem.getEquipName(), wingItem.lvl + 1, PAYMENT_DISPLAY[typePayment], enchantWingData.Percent, enchantWingData.NumDropLevelWing), player);
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
                    }
                }
                break;
            case MENU_SELECT_EQUIP_PET_TIER:
                CopyOnWriteArrayList<Item> listItemEquip = player.playerData.getInventoryOrCreate(GopetManager.EQUIP_PET_INVENTORY);
                if (index >= 0 && listItemEquip.Count > index)
                {
                    Item itemSelect = listItemEquip.get(index);
                    player.controller.selectMaterialEnchant(itemSelect.itemId, itemSelect.getTemp().getIconPath(), itemSelect.getEquipName(), int.MaxValue);
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
                                    player.okDialog(pet.getNameWithoutStar() + ": " + pet.getDesc());
                                    pet = null;
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
                                    player.okDialog($"{itemTemplate.name} {itemTemplate.description} {itemTemplate.getAtk()} {itemTemplate.getDef()} {itemTemplate.getHp()} {itemTemplate.getMp()}");
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
                        player.controller.objectPerformed.put(OBJKEY_SELECT_SELL_ITEM, pet);
                        player.controller.objectPerformed.put(OBJKEY_MENU_OF_KIOSK, menuId);
                        player.controller.showInputDialog(INPUT_DIALOG_KIOSK, player.Language.Pricing, new String[] { "  " }, new sbyte[] { 0 });
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
                                            using (var conn = MYSQLManager.create())
                                            {

                                            }
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
                            player.okDialog(Utilities.Format("Online player %s: %s", place.map.mapTemplate.name, numPlayerMap));
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
                            player.controller.showInputDialog(INPUT_TYPE_NAME_PLAYER_TO_GET_ITEM, "Lấy item", new String[] { "Tên nv lấy:", "Số lượng  :" });
                            break;
                        case ADMIN_INDEX_GIVE_ITEM_TO_PLAYER:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_PLAYER_TO_GIVE_ITEM, "Đưa item", new String[] { "Tên nv đưa :", "Số lượng  :" });
                            break;
                        case ADMIN_INDEX_COIN:
                            player.controller.showInputDialog(INPUT_TYPE_NAME_TO_BUFF_COIN, "Cộng từ tiền", new String[] { "Tiền :", "Tài khoản :" });
                            break;
                        case ADMIN_INDEX_GET_ZONE_ID:
                            player.okDialog($"Bạn đang ở khu {player.getPlace().zoneID} của map {player.getPlace().map.mapTemplate.name} mapId = {player.getPlace().map.mapID}");
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
                            player.okDialog($"{player.playerData.x}|{player.playerData.y}");
                            break;
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
                                                player.okDialog($"Người chơi {friend.playerData.name} đang ở map {gopetPlace.map.mapTemplate.name} khu {gopetPlace.zoneID}");
                                            }
                                            else
                                            {
                                                player.redDialog($"Người chơi {friend.playerData.name} đang chuyển map");
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
                                    player.okDialog("Xóa bạn thành công");
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
                                                if (!conn.Query("SELECT `userId`, `targetId`, `time` FROM `request_accept_friend` WHERE userId = @userId, targetId = @targetId", friendRequest).Any())
                                                {
                                                    conn.Execute("INSERT INTO `request_accept_friend`(`userId`, `targetId`, `time`) VALUES (@userId,@targetId,@time)", friendRequest);
                                                }
                                            }
                                            player.playerData.ListFriends.addIfAbsent(friendId);
                                            player.playerData.RequestAddFriends.remove(friendId);
                                            player.okDialog("Kết bạn thành công");
                                        }
                                        else
                                        {
                                            playerRequest.playerData.ListFriends.addIfAbsent(player.user.user_id);
                                            player.playerData.ListFriends.addIfAbsent(friendId);
                                            player.playerData.RequestAddFriends.remove(friendId);
                                            player.okDialog("Kết bạn thành công");
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
                                        player.okDialog("Xóa bạn thành công");
                                    }
                                    break;

                            }
                        DeleteRequestAddFriends:
                            {
                                player.playerData.RequestAddFriends.remove(friendId);
                                player.okDialog("Xóa thành công");
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
                                        player.okDialog("Bỏ chặn thành công");
                                    }
                                    break;
                                case 1:
                                    {
                                        player.playerData.BlockFriendLists.Clear();
                                        player.okDialog("Bỏ chặn tất cả thành công");
                                    }
                                    break;
                            }
                        }
                    }
                }
                break;
            default:
                {
                    player.redDialog("KHONG TON TAI MENU NAY");
                    Thread.Sleep(1000);
                }
                break;
        }
    }
}

