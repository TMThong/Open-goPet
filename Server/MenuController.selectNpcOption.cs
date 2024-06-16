using Gopet.Data.Event.Year2024;
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
                    sendMenu(MENU_MERGE_WING, player);
                    break;
                }
            case OP_SHOP_PET:
                showShop(SHOP_PET, player);
                break;
            case OP_LIST_PET_FREE:
                sendMenu(MENU_LIST_PET_FREE, player);
                break;
            case OP_EVENT_TASK:
                {
                    var listTaskEvent = player.playerData.task.Where(p => p.taskTemplateId >= 38 && p.taskTemplateId <= 49).ToList();
                    if (listTaskEvent.Count > 0)
                    {
                        player.redDialog("Ngươi đã nhận nhiệm vụ thì đi làm đi. \n Tới đây hỏi gì nữa hả?!");
                    }
                    else
                    {
                        if (player.checkStar(1))
                        {
                            player.MineStar(1);
                            TaskTemplate[] taskTemplates = GopetManager.taskTemplateList.Where(p => p.taskId >= 38 && p.taskId <= 49).ToArray();
                            TaskTemplate taskTemplate = Utilities.RandomArray(taskTemplates);
                            TaskData taskData = new TaskData(taskTemplate);
                            taskData.CanCancelTask = false;
                            player.playerData.task.add(taskData);
                            player.playerData.tasking.add(taskData.taskTemplateId);
                            player.okDialog("Nhận nhiệm vụ thành công");
                        }
                        else
                        {
                            player.notEnoughStar();
                        }
                    }
                    break;
                }
            case OP_TOP_EVENT_TASK:
                {
                    showTop(TopEvent.Instance, player);
                    break;
                }
            case OP_DELETE_TIEM_NANG:
                sendMenu(MENU_DELETE_TIEM_NANG, player); break;
            case OP_TOP_PET:
                showTop(TopPet.instance, player); break;
            case OP_TOP_GOLD:
                showTop(TopGold.instance, player); break;
            case OP_TOP_GEM:
                showTop(TopGem.instance, player); break;
            case OP_SHOW_TOP_ACCUMULATED_POINT:
                showTop(TopAccumulatedPoint.Instance, player);
                break;

            case OP_ARENA_JOURNALISM:
                sendMenu(MENU_SELECT_TYPE_PAYMENT_TO_ARENA_JOURNALISM, player);
                break;
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
                        if (pet.TimeDieZ > Utilities.CurrentTimeMillis)
                        {
                            if (player.checkGold(GopetManager.PRICE_REVIVAL_PET_FATER_PK))
                            {
                                player.mineGold(GopetManager.PRICE_REVIVAL_PET_FATER_PK);
                                pet.petDieByPK = false;
                                pet.TimeDieZ = 0;
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
                        player.okDialog(Utilities.Format("Bạn đã quyên góp quỹ được:%s", Utilities.FormatNumber(clanMember.fundDonate)));
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
                        if (clanMember.IsLeader)
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
                        if (clan.getLvl() >= 15)
                        {
                              showShop(SHOP_CLAN, player);                     
                        }
                        else
                        {
                            player.redDialog("Bang hội chưa đạt cấp 15 trở lên");
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
                    player.redDialog(GopetManager.OldFeatures);
                }
                break;

            case OP_UPGRADE_SHOP_CLAN:
                {
                    player.redDialog(GopetManager.OldFeatures);
                }
                break;
            case OP_SELECT_PET_DEF_LEAGUE:
                sendMenu(MENU_SELECT_PET_TO_DEF_LEAGUE, player);
                break;
            case OP_SHOW_ALL_TATTO:
                sendMenu(MENU_SHOW_ALL_TATTO, player);
                break;
            case OP_SHOW_ME_ACHIEVEMENT:
                sendMenu(MENU_ME_SHOW_ACHIEVEMENT, player);
                break;
            case OP_EVENT_SUMMER_2024_GUIDE:
                player.okDialog($"Trong thời gian diễn ra sự kiện khi đánh quái sẽ rớt ra nguyên liệu sự kiện như cuộn vải, giấy, tre, cuộn dây. Màu để làm diều có bán tại Thương nhân nhé các đội viên của ta. Công thức để làm diều giấy là: {Summer2024Event.Instance.RecipeText(Summer2024Event.RECIPE_KITE_NORMAL)}. Công thức để làm diều vải là: {Summer2024Event.Instance.RecipeText(Summer2024Event.RECIPE_KITE_VIP)}. Khi thả diều sẽ nhận được các phần quà nhưng các phần quà này sớ lượng có hạn.");
                break;
            case OP_EVENT_SUMMER_2024_MAKE_KITE_NORMAL:
                Summer2024Event.Instance.MakeKite(Summer2024Event.RECIPE_KITE_NORMAL, player);
                break;
            case OP_EVENT_SUMMER_2024_MAKE_KITE_VIP:
                Summer2024Event.Instance.MakeKite(Summer2024Event.RECIPE_KITE_VIP, player);
                break;
            case OP_EVENT_SUMMER_2024_TOP_KITE_NORMAL:
                showTop(Summer2024Event.TopKiteNormal.Instance, player);
                break;
            case OP_EVENT_SUMMER_2024_TOP_KITE_VIP:
                showTop(Summer2024Event.TopKiteVip.Instance, player);
                break;
            default:
                player.redDialog("Tính năng đang được xây dựng"); break;
        }
    }
}

