using Gopet.Data.GopetClan;
using Gopet.Data.item;
using Gopet.Data.top;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MenuController
{
    public static void selectNpcOption(int option, Player player)
    {
        switch (option)
        {
            case OP_MAIN_TASK:
                {
                    if (player.controller.objectPerformed.ContainsKey(OBJKEY_NPC_ID_FOR_MAIN_TASK))
                    {
                        sendMenu(MenuController.MENU_SHOW_LIST_TASK, player);
                    }
                }
                break;
            case OP_MERGE_WING:
                {
                    player.controller.testMsg100();
                    break;
                }
            case OP_SHOP_PET:
                showShop(SHOP_PET, player);
                break;
            case OP_LIST_PET_FREE:
                sendMenu(MENU_LIST_PET_FREE, player);
                break;
            case OP_DELETE_TIEM_NANG:
                sendMenu(MENU_DELETE_TIEM_NANG, player); break;
            case OP_TOP_PET:
                showTop(TopPet.instance, player); break;
            case OP_TOP_GOLD:
                showTop(TopGold.instance, player); break;
            case OP_TOP_GEM:
                showTop(TopGem.instance, player); break;
            case OP_SHOW_TOP_ACCUMULATED_POINT:
                showTop(TopAccumulatedPoint.Instance, player); break;
            case OP_TOP_SPEND_GOLD:
                {
                    showTop(TopSpendGold.instance, player);
                }
                break;
            case OP_CHALLENGE:
                {
                    if (player.checkStar(GopetManager.STAR_JOIN_CHALLENGE))
                    {
                        player.MineStar(GopetManager.STAR_JOIN_CHALLENGE);
                        MapManager.maps.get(12).addRandom(player);
                    }
                    else
                    {
                        player.notEnoughStar();
                    }
                }
                break;
            case OP_SHOP_ARENA:
                {
                    sendMenu(SHOP_ARENA, player);
                }
                break;
            case OP_TYPE_GIFT_CODE:
                {
                    player.controller.showInputDialog(INPUT_TYPE_GIFT_CODE, "Nhập mã quà tặng", new String[] { "Giftcode:  " });
                }
                break;
            case OP_UPGRADE_PET:
                {
                    player.controller.setPetUpgradeInfo(new PetUpgradeInfo());
                    player.controller.showUpgradePet();
                }
                break;

            case OP_TRADE_GIFT_COIN:
                Trade(TradeGiftTemplate.TYPE_COIN, player);
                break;
            case OP_TRADE_GIFT_GOLD:
                Trade(TradeGiftTemplate.TYPE_GOLD, player);
                break;
            case OP_SHOP_ENERGY:
                showShop(SHOP_ENERGY, player);
                break;

            case OP_MERGE_PART_PET:
                sendMenu(MENU_MERGE_PART_PET, player); break;
            case OP_KIOSK_HAT:
                sendMenu(MENU_KIOSK_HAT, player); break;
            case OP_KIOSK_WEAPON:
                sendMenu(MENU_KIOSK_WEAPON, player); break;
            case OP_KIOSK_AMOUR:
                sendMenu(MENU_KIOSK_AMOUR, player); break;
            case OP_KIOSK_GEM:
                sendMenu(MENU_KIOSK_GEM, player); break;
            case OP_KIOSK_PET:
                sendMenu(MENU_KIOSK_PET, player); break;
            case OP_KIOSK_OHTER:
                sendMenu(MENU_KIOSK_OHTER, player); break;
            case OP_SHOP_THUONG_NHAN_AND_XOA_XAM:
                sendMenu(SHOP_THUONG_NHAN, player); break;
            case OP_OWNER_KIOSK_OHTER:
            case OP_OWNER_KIOSK_PET:
            case OP_OWNER_KIOSK_GEM:
            case OP_OWNER_KIOSK_AMOUR:
            case OP_OWNER_KIOSK_WEAPON:
            case OP_OWNER_KIOSK_HAT:
                {
                    sbyte typeKiosk = 0;
                    switch (option)
                    {
                        case OP_KIOSK_HAT:
                            typeKiosk = GopetManager.KIOSK_HAT; break;
                        case OP_OWNER_KIOSK_WEAPON:
                            typeKiosk = GopetManager.KIOSK_WEAPON; break;
                        case OP_OWNER_KIOSK_AMOUR:
                            typeKiosk = GopetManager.KIOSK_AMOUR; break;
                        case OP_OWNER_KIOSK_GEM:
                            typeKiosk = GopetManager.KIOSK_GEM; break;
                        case OP_OWNER_KIOSK_PET:
                            typeKiosk = GopetManager.KIOSK_PET; break;
                        case OP_OWNER_KIOSK_OHTER:
                            typeKiosk = GopetManager.KIOSK_OTHER; break;
                    }
                    player.controller.showKiosk(typeKiosk);
                }
                break;
            case OP_REVIVAL_PET_AFTER_PK:
                {
                    Pet pet = player.getPet();
                    if (pet != null)
                    {
                        if (pet.TimeDie > Utilities.CurrentTimeMillis)
                        {
                            if (player.checkGold(GopetManager.PRICE_REVIVAL_PET_FATER_PK))
                            {
                                player.mineGold(GopetManager.PRICE_REVIVAL_PET_FATER_PK);
                                pet.petDieByPK = false;
                                pet.TimeDie = 0;
                                player.okDialog(Utilities.Format("Hồi sinh %s thành công, nhờ trân trọng nó nhé", pet.getNameWithStar()));
                            }
                            else
                            {
                                player.controller.notEnoughGold();
                            }
                        }
                        else
                        {
                            player.redDialog("Nó vẫn bình thường mà");
                        }
                    }
                    else
                    {
                        player.petNotFollow();
                    }
                }
                break;
            case OP_UPGRADE_STAR_PET:
                sendMenu(MENU_SELECT_ITEM_PART_FOR_STAR_PET, player); break;
            case OP_PET_TATOO:
                player.controller.showPetTattoUI(); break;
            case OP_SHOW_GEM_INVENTORY:
                player.controller.showGemInvenstory(); break;
            case OP_MERGE_ITEM:
                {
                    sendMenu(MENU_MERGE_PART_ITEM, player);
                }
                break;
            case OP_CREATE_CLAN:
                {
                    player.controller.showInputDialog(INPUT_DIALOG_CREATE_CLAN, Utilities.Format("Tạo bang hội (Phí %s (ngoc) + %s (vang)", Utilities.FormatNumber(GopetManager.COIN_CREATE_CLAN), Utilities.FormatNumber(GopetManager.GOLD_CREATE_CLAN)), new String[] { "Tên bang hội: " });
                }
                break;
            case OP_NUM_OF_TASK:
                {
                    player.okDialog(Utilities.Format("Số nhiệm vụ đã hoàn thành là: ", player.playerData.wasTask.Count));
                }
                break;
            case OP_TOP_LVL_CLAN:
                {
                    showTop(TopLVLClan.instance, player);
                }
                break;
            case OP_ENTER_CLAN_PLACE:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        clanMember.getClan().getClanPlace().add(player);
                    }
                    else
                    {
                        player.redDialog("Bạn chưa vào bang");
                    }
                }
                break;
            case OP_EVENT_OF_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        player.okDialog("Bang hội hiện chưa có sự kiện gì...");
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case OP_FAST_INFO_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        player.okDialog(Utilities.Format("Bạn đã quyên góp quỹ được:%s\n Điểm cống hiến:%s", Utilities.FormatNumber(clanMember.fundDonate), Utilities.FormatNumber(clanMember.growthPointDonate)));
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case OP_APPROVAL_CLAN_MEMBER:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        if (clanMember.duty != Clan.TYPE_NORMAL)
                        {
                            sendMenu(MENU_APPROVAL_CLAN_MEMBER, player);
                        }
                        else
                        {
                            player.redDialog("Bạn chỉ là thành viên.");
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case OP_OUT_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        if (clanMember.duty == Clan.TYPE_LEADER)
                        {
                            player.redDialog("Bang chủ không thể rời bang.");
                        }
                        else
                        {
                            clanMember.getClan().outClan(clanMember);
                            player.playerData.clanId = -1;
                            player.session.Close();
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case OP_UPGRADE_MAIN_HOUSE:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        if (clanMember.duty == Clan.TYPE_LEADER)
                        {
                            showYNDialog(DIALOG_ASK_REQUEST_UPGRADE_MAIN_HOUSE, Utilities.Format("Bạn có muốn năng cấp nhà chính lên cấp %s không?", clanMember.getClan().getLvl() + 1), player);
                        }
                        else
                        {
                            player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case OP_SHOW_ALL_ITEM:
                {
                    sendMenu(MENU_SHOW_ALL_ITEM, player);
                    break;
                }

            case OP_CHANGE_SLOGAN_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        if (clanMember.duty == Clan.TYPE_LEADER)
                        {
                            player.controller.showInputDialog(INPUT_DIALOG_CHANGE_SLOGAN_CLAN, "Thay đổi khẩu hiệu bang hội", new String[] { "Khẩu hiệu: " });
                        }
                        else
                        {
                            player.redDialog("Bạn không có quyền này, chỉ có bang chủ mới thao tác được.");
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;
            case OP_CHANGE_GIFT:
                {
                    sendMenu(MENU_SELECT_TYPE_CHANGE_GIFT, player);
                }
                break;
            case OP_LIST_GIFT:
                {
                    sendMenu(MENU_ITEM_MONEY_INVENTORY, player);
                }
                break;
            case OP_PLUS_CLAN_BUFF:
                {
                    sendMenu(MENU_PLUS_SKILL_CLAN, player);
                }
                break;

            case OP_UPGRADE_MEMBER_DUTY:
                {
                    sendMenu(MENU_UPGRADE_MEMBER_DUTY, player);
                }
                break;

            case OP_SHOP_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if (clan.getLvl() >= 1)
                        {
                            //                        Calendar calendar = new GregorianCalendar();
                            //                        calendar.setTimeInMillis(Utilities.CurrentTimeMillis);
                            //                        if (calendar.get(Calendar.DAY_OF_WEEK) == Calendar.SATURDAY && calendar.get(Calendar.HOUR_OF_DAY) >= 20 && calendar.get(Calendar.HOUR_OF_DAY) < 24) {
                            showShop(SHOP_CLAN, player);
                            //                        } else {
                            //                            player.redDialog("Cửa hàng bang hội chỉ mở vào thứ 7 từ 20-24h");
                            //                        }
                        }
                        else
                        {
                            player.redDialog("Cửa hàng bang hội chưa đạt cấp 1 trở lên");
                        }
                    }
                    else
                    {
                        player.controller.notClan();
                    }
                }
                break;

            case OP_UPGRADE_SKILL_HOUSE:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if (clanMember.duty == Clan.TYPE_LEADER)
                        {
                            int marketHouse = clan.getbaseMarketLvl();
                            ClanHouseTemplate clanHouseTemplate = GopetManager.clanSkillHouseTemp.get(marketHouse + 1);
                            if (clanHouseTemplate != null)
                            {
                                showYNDialog(DIALOG_ASK_UPGRADE_SHOP_HOUSE, Utilities.Format("Bạn có chắc muốn nâng nhà kỹ năng bang hội không ? Cần %s quỹ và %s cống hiến", Utilities.FormatNumber(clanHouseTemplate.getFundNeed()), Utilities.FormatNumber(clanHouseTemplate.getGrowthPointNeed())), player);
                            }
                            else
                            {
                                player.redDialog("Nhà kỹ năng đã đạt cấp tối đa");
                            }
                        }
                        else
                        {
                            clan.notEngouhPermission(player);
                        }
                    }
                }
                break;

            case OP_UPGRADE_SHOP_CLAN:
                {
                    ClanMember clanMember = player.controller.getClan();
                    if (clanMember != null)
                    {
                        Clan clan = clanMember.getClan();
                        if (clanMember.duty == Clan.TYPE_LEADER)
                        {
                            int marketHouse = clan.getbaseMarketLvl();
                            ClanHouseTemplate clanHouseTemplate = GopetManager.clanMarketHouseTemp.get(marketHouse + 1);
                            if (clanHouseTemplate != null)
                            {
                                showYNDialog(DIALOG_ASK_UPGRADE_SHOP_HOUSE, Utilities.Format("Bạn có chắc muốn nâng shop bang hội không ? Cần %s quỹ và %s cống hiến", Utilities.FormatNumber(clanHouseTemplate.getFundNeed()), Utilities.FormatNumber(clanHouseTemplate.getGrowthPointNeed())), player);
                            }
                            else
                            {
                                player.redDialog("Shop đã đạt cấp tối đa");
                            }
                        }
                        else
                        {
                            clan.notEngouhPermission(player);
                        }
                    }
                }
                break;
            default:
                player.redDialog("Tính năng đang được xây dựng"); break;
        }
    }
}

