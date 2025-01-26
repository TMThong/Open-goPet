

using Gopet.Battle;
using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.Map;
using Gopet.Data.Mob;
using Gopet.IO;
using Gopet.Util;
using Gopet.Server.IO;
using Gopet.Data.User;
using Gopet.Language;

public class GopetPlace : Place
{
    public long placeTime = Utilities.CurrentTimeMillis;
    public CopyOnWriteArrayList<Mob> mobs = new();
    public CopyOnWriteArrayList<PetBattle> petBattles = new();
    public ConcurrentHashMap<MobLocation, long> newMob = new();
    public const long TIME_NEW_MOB = 25000;
    public int[] numMobDie;
    public int[] numMobDieNeed
    {
        get
        {
            return base.map.mapTemplate.numPetDie;
        }
    }

    public GopetPlace(GopetMap m, int ID) : base(m, ID)
    {
        if (m == null) throw new ArgumentNullException();

        numMobDie = new int[numMobDieNeed.Length];
        Array.Fill(numMobDie, 0);

        if (GopetManager.mobLocation.ContainsKey(m.mapID) && GopetManager.MOBLVL_MAP.ContainsKey(m.mapID))
        {
            createNewMob(GopetManager.mobLocation.get(map.mapID));
        }
    }


    public override void add(Player player)
    {
        HistoryManager.addHistory(new History(player).setLog(Utilities.Format("Bạn đã vào khu %s map %s", zoneID, map.mapTemplate.name)));
        PetBattle petBattle = player.controller.getPetBattle();
        if (petBattle != null)
        {
            petBattle.Close(player);
        }
        Place place = player.getPlace();
        if (place != null)
        {
            place.remove(player);
        }
        player.controller.setLastTimeKillMob(0L);
        sendNewPlayer(player);
        players.addIfAbsent(player);
        player.setPlace(this);
        loadInfo(player);
        sendGameObj(player);
        sendMob(player);
        sendListPet(player);
        sendPetBattleList(player);
        sendWing(player);
        sendSkin(player);
        sendClan(player, true);
        updatePlayerAnimation(player);
        sendListAnimationOfAllPlayers(player);
    }


    public override void remove(Player player)
    {
        PetBattle petBattle = player.controller.getPetBattle();
        if (petBattle != null)
        {
            petBattle.Close(player);
        }
        base.remove(player);
    }

    public void addNewMob(Mob gopetMob)
    {

        while (true)
        {
            int mobId = -Utilities.nextInt(2, int.MaxValue - 12);
            bool hasId = false;
            foreach (Mob mob in mobs)
            {
                if (mob.getMobId() == mobId && mob != gopetMob)
                {
                    hasId = true;
                    break;
                }
            }
            if (!hasId)
            {
                gopetMob.setMobId(mobId);
                mobs.Add(gopetMob);
                return;
            }
        }
    }

    public void mobDie(Mob gopetMob)
    {
        mobs.remove(gopetMob);
        long timeGen = Utilities.CurrentTimeMillis + TIME_NEW_MOB;
        newMob.TryAdd(gopetMob.getMobLocation(), timeGen);
    }

    public Mob getMob(int mobId)
    {
        foreach (Mob mob in mobs)
        {
            if (mob.getMobId() == mobId)
            {
                return mob;
            }
        }
        return null;
    }

    public Player getPlayer(int user_id)
    {
        foreach (Player player in players)
        {
            if (player.user.user_id == user_id)
            {
                return player;
            }
        }
        return null;
    }

    private void sendMob(Player player)
    {
        CopyOnWriteArrayList<Mob> gopetMobs = (CopyOnWriteArrayList<Mob>)mobs.clone();
        Message message = new Message(GopetCMD.PET_SERVICE);
        message.putsbyte(GopetCMD.SEND_LIST_MOB_ZONE);
        message.putInt(gopetMobs.Count);
        foreach (Mob mob in gopetMobs)
        {
            message.putInt(mob.getMobId());
            message.putUTF(mob.getPetTemplate().frameImg);
            message.putUTF(mob.getPetTemplate().name);
            message.putInt(mob.getMobLvInfo().lvl);
            message.putInt(mob.getMobLocation().getX());
            message.putInt(mob.getMobLocation().getY());
            message.putsbyte(0);
            message.putsbyte(mob.Template.frameNum);
            message.putShort(mob.Template.vY);
            message.putbool(mob is Boss);
        }
        message.cleanup();
        player.session.sendMessage(message);
    }

    public void sendMob()
    {
        CopyOnWriteArrayList<Mob> gopetMobs = (CopyOnWriteArrayList<Mob>)mobs.clone();
        Message message = new Message(GopetCMD.PET_SERVICE);
        message.putsbyte(GopetCMD.SEND_LIST_MOB_ZONE);
        message.putInt(gopetMobs.Count);
        foreach (Mob mob in gopetMobs)
        {
            message.putInt(mob.getMobId());
            message.putUTF(mob.getPetTemplate().frameImg);
            message.putUTF(mob.getPetTemplate().name);
            message.putInt(mob.getMobLvInfo().lvl);
            message.putInt(mob.getMobLocation().getX());
            message.putInt(mob.getMobLocation().getY());
            message.putsbyte(0);
            message.putsbyte(mob.Template.frameNum);
            message.putShort(mob.Template.vY);
            message.putbool(mob is Boss);
        }
        message.cleanup();
        sendMessage(message);
    }

    public void sendMob(JArrayList<Mob> newMobs)
    {
        Message message = new Message(GopetCMD.PET_SERVICE);
        message.putsbyte(GopetCMD.SEND_LIST_MOB_ZONE);
        message.putInt(newMobs.Count);
        foreach (Mob mob in newMobs)
        {
            message.putInt(mob.getMobId());
            message.putUTF(mob.getPetTemplate().frameImg);
            message.putUTF(mob.getPetTemplate().name);
            message.putInt(mob.getMobLvInfo().lvl);
            message.putInt(mob.getMobLocation().getX());
            message.putInt(mob.getMobLocation().getY());
            message.putsbyte(0);
            message.putsbyte(mob.Template.frameNum);
            message.putShort(mob.Template.vY);
            message.putbool(mob is Boss);
        }
        message.cleanup();
        sendMessage(message);
    }

    public void sendListPet(Player player)
    {
        HashMap<Player, Pet> hashMap = new();
        foreach (Player player1 in players)
        {
            if (player1.playerData.petSelected != null)
            {
                hashMap.put(player1, player1.playerData.petSelected);
            }
        }
        if (hashMap.Count != 0)
        {
            Dictionary<Message, Func<Version, bool>> messagesDict = new Dictionary<Message, Func<Version, bool>>();
            ListWriterMessage message = new ListWriterMessage(2, GopetCMD.PET_SERVICE);
            message.putsbyte(GopetCMD.SEND_LIST_PET_ZONE);
            message.putsbyte(hashMap.Count);
            foreach (var entry in hashMap)
            {
                Player player1 = entry.Key;
                Pet petSelected = entry.Value;
                message.putInt(player1.user.user_id);
                message.putInt(petSelected.petIdTemplate);
                message.putUTF(petSelected.getPetTemplate().frameImg);
                message.putUTF(petSelected.getNameWithStar(player));
                message.putInt(petSelected.lvl);
                message[1].putsbyte(petSelected.getPetTemplate().frameNum);
                message[1].putShort(petSelected.getPetTemplate().vY);
                GameController.WritePetEffect(message[1], petSelected.EffectTemplates);
            }
            message.cleanup();
            messagesDict[message[0]] = GopetManager.LessThanAndEquals(GopetManager.VERSION_133);
            messagesDict[message[1]] = GopetManager.GreaterThan(GopetManager.VERSION_133);
            sendMessageWithCheckVersion(messagesDict);
            if (hashMap.ContainsKey(player))
            {
                player.controller.sendMyPetInfo();
            }
        }
    }

    private void sendGameObj(Player player)
    {
        Message ms = new Message(GopetCMD.GAME_OBJECT);
        foreach (int npcId in map.mapTemplate.npc)
        {
            NpcTemplate npcTemplate = GopetManager.npcTemplate.get(npcId);
            if (npcTemplate != null)
            {
                ms.putsbyte(0);
                ms.putInt(npcTemplate.getBounds()[0]);
                ms.putInt(npcTemplate.getBounds()[1]);
                ms.putInt(npcTemplate.getBounds()[2]);
                ms.putInt(npcTemplate.getBounds()[3]);
                ms.putInt(npcTemplate.getNpcId());
                ms.putUTF(npcTemplate.getImgPath());
                ms.putInt(2);
                ms.putInt(npcTemplate.getX());
                ms.putInt(npcTemplate.getY());
                ms.putInt(6);
                String[] chat = npcTemplate.getChat();
                ms.putInt(chat.Length);
                foreach (String CHAT_String in chat)
                {
                    ms.putUTF(CHAT_String);
                }
                ms.putUTF(npcTemplate.getName(player));
                ms.putsbyte(npcTemplate.getType());
            }
        }
        ms.writer().flush();
        ms.cleanup();
        player.session.sendMessage(ms);
    }

    private void initPlayer(Player player)
    {
        Message ms = new Message(GopetCMD.INIT_PLAYER);
        ms.putInt(player.user.user_id);
        ms.putString(player.playerData.name);
        ms.putInt(player.playerData.gender);
        ms.putsbyte(0);
        ms.putInt(0);
        ms.writer().flush();
        ms.cleanup();
        player.session.sendMessage(ms);
    }


    public void sendNewPlayer(Player player)
    {
        initPlayer(player);
        Message message = new Message(24);
        message.putInt(player.playerData.user_id);
        message.putUTF(player.playerData.name);
        message.putsbyte(player.playerData.gender);
        // relation
        message.putsbyte(0);
        message.putsbyte(player.playerData.speed);
        message.putsbyte(player.playerData.faceDir);
        message.putsbyte(player.playerData.waypointIndex);
        message.putInt(player.playerData.x);
        message.putInt(player.playerData.y);
        message.cleanup();
        sendMessage(message);
    }


    public void sendMove(int channelID, int userID, sbyte lastDir, short[][] points)
    {

    }


    public void chat(Player player, String text)
    {
        Message message = new Message(GopetCMD.ON_PLACE_CHAT);
        message.putInt(player.playerData.user_id);
        message.putUTF(text);
        message.cleanup();
        sendMessage(message);
    }

    public void sendMove(int userID, sbyte lastDir, int[] points)
    {
        Message message = new Message(GopetCMD.ON_OTHER_USER_MOVE);
        message.putInt(userID);
        message.putsbyte(lastDir);
        message.putInt(map.mapID);
        message.putInt(points.Length);
        for (int i = 0; i < points.Length; i++)
        {
            message.putInt(points[i]);
        }
        message.cleanup();
        sendMessage(message);
    }


    public override void sendRemove(Player player)
    {
        Message message = new Message(GopetCMD.ON_PLAYER_EXIT_PLACE);
        message.putInt(player.playerData.user_id);
        message.putsbyte(player.playerData.faceDir);
        message.putInt(0);
        message.cleanup();
        sendMessage(message);
    }


    public void chat(int user_id, String name, String text)
    {
        Message message = new Message(GopetCMD.PET_SERVICE);
        message.putsbyte(GopetCMD.CHAT_PUBLIC);
        message.putsbyte(1);
        message.putUTF(name);
        message.putUTF(text);
        message.cleanup();
        sendMessage(message);
    }


    public void loadInfo(Player player)
    {
        Message message = new Message(GopetCMD.ON_UPDATE_PLAYER_IN_MAP);
        // Map ID
        message.putInt(map.mapID);

        message.putInt(zoneID);
        //waypoint Index
        message.putsbyte(player.playerData.waypointIndex);
        message.putInt(player.playerData.x);
        message.putInt(player.playerData.y);

        foreach (Player player1 in players)
        {
            if (player1 != player)
            {
                message.putInt(player1.user.user_id);
                message.putUTF(player1.playerData.name);
                message.putsbyte(player1.playerData.gender);
                // relation
                message.putsbyte(0);
                message.putsbyte(player1.playerData.speed);
                message.putsbyte(player1.playerData.faceDir);
                message.putInt(player1.playerData.x);
                message.putInt(player1.playerData.y);
            }
        }

        message.cleanup();
        player.session.sendMessage(message);
    }

    public void startFightMob(int mobId, Player player)
    {
        if (this.map.mapID == 12)
        {
            if (Utilities.CurrentTimeMillis - player.controller.getLastTimeKillMob() < 4500)
            {
                player.user.ban(UserData.BAN_TIME, player.Language.BanUserByAutoAttackMob, Utilities.CurrentTimeMillis + 60000L * 5);
                player.session.Close();
                return;
            }
        }
        Mob mob = getMob(mobId);
        if (mob != null)
        {
            if (player.playerData.petSelected != null)
            {
                if (!(mob is Boss) && mob.hp <= 0 && mob.getPetBattle(player) == null)
                {
                    mobDie(mob);
                    return;
                }
                if (player.playerData.petSelected.hp > 0)
                {
                    if (mob.getPetBattle(player) == null && player.controller.getPetBattle() == null)
                    {
                        if (mob is Boss boss && !(this is ChallengePlace))
                        {
                            if (boss.OwnerClan != null && boss.OwnerClan != player.controller.getClan()?.getClan())
                            {
                                player.Popup($"Boss này không thuộc bang hội của bạn. Boss này thuộc bang {boss.OwnerClan.name}");
                                return;
                            }
                            if (player.playerData.star - 1 >= 0)
                            {
                                player.playerData.star--;
                                player.controller.getTaskCalculator().onAttackBoss((Boss)mob);
                            }
                            else
                            {
                                player.Popup(player.Language.NotEnoughtEnergy);
                                return;
                            }
                        }
                        PetBattle petBattle = new PetBattle(mob, this, player);
                        player.controller.setPetBattle(petBattle);
                        addPetBattle(petBattle);
                        mob.setPetBattle(petBattle, player);
                        petBattle.sendStartFightMob(mob, player);
                    }
                }
                else
                {
                    player.Popup(player.Language.YourPetEnoughHP);
                }
            }
            else
            {
                player.Popup(player.Language.PetNotFollow);
            }
        }
    }

    public void startFightPlayer(int user_id, Player player, bool isPkMode, int coinBet)
    {
        Player passivePlayer = getPlayer(user_id);
        if (passivePlayer != null)
        {
            if (passivePlayer != player)
            {

                Pet activePet = player.getPet();
                Pet passovePet = player.getPet();
                if (activePet == null || passovePet == null)
                {
                    player.petNotFollow();
                }
                else
                {
                    if (player.controller.getPetBattle() != null || player.controller.getPetBattle() != null)
                    {
                        player.redDialog(player.Language.YouOrOtherPlayerIsFighting);
                    }
                    else
                    {
                        PetBattle petBattle = new PetBattle(this, passivePlayer, player);
                        petBattle.setIsPK(isPkMode);
                        petBattle.setUserInvitePK(player.user.user_id);
                        if (!isPkMode)
                        {
                            petBattle.setPrice(coinBet);
                        }
                        player.controller.setPetBattle(petBattle);
                        passivePlayer.controller.setPetBattle(petBattle);
                        addPetBattle(petBattle);
                        petBattle.sendStartFightPlayer();
                    }
                }
            }
        }
        else
        {
            player.redDialog(player.Language.PlayerQuit);
        }
    }


    public override void update()
    {
        base.update();
        foreach (Mob mob in mobs)
        {
            goto NEXT;
        REMOVE_LABEL:
            {
                this.mobDie(mob);
                if (mob is Boss b)
                {
                    foreach (var item in b.Battle)
                    {
                        item.Value?.sendFastRemove();
                        item.Value?.clean();
                    }
                }
                else
                {
                    mob.getPetBattle(null)?.sendFastRemove();
                    mob.getPetBattle(null)?.clean();
                }
                continue;
            }
        NEXT:
            if (mob is Boss b2)
            {
                if (b2.TimeOut < DateTime.Now)
                {
                    goto REMOVE_LABEL;
                }
            }
            if (mob.hp <= 0)
            {
                if (mob.TimeEndUpdate.HasValue && mob.TimeEndUpdate < DateTime.Now)
                {
                    goto REMOVE_LABEL;
                }
                else
                {
                    mob.TimeEndUpdate = DateTime.Now.AddSeconds(5);
                }
            }
        }

        foreach (PetBattle petBattle in petBattles)
        {
            if (petBattle != null)
            {
                petBattle.update();
                if (petBattle.hasWinner())
                {
                    petBattle.clean();
                    petBattles.remove(petBattle);
                }
            }
            else
            {
                petBattles.remove(petBattle);
            }
        }

        JArrayList<MobLocation> mobLocations_newMob = new();

        foreach (var entry in newMob)
        {
            MobLocation location = entry.Key;
            long timeNewMob = entry.Value;
            if (timeNewMob < Utilities.CurrentTimeMillis)
            {
                mobLocations_newMob.add(location);
                newMob.remove(location);
            }
        }
        createNewMob(mobLocations_newMob.ToArray());
    }

    public void addPetBattle(PetBattle petBattle)
    {
        petBattles.Add(petBattle);
    }

    private void createNewMob(MobLocation[] locations)
    {
        MobLocation[] mobLocations = locations;
        MobLvlMap[] mobLvlMaps = GopetManager.MOBLVL_MAP.get(map.mapID);
        if (mobLvlMaps != null)
        {
            if (mobLocations.Length > 0 && mobLvlMaps.Length > 0)
            {
                JArrayList<Mob> nGopetMobs = new();
                int index = -1;
                foreach (MobLocation mobLocation in mobLocations)
                {
                    index++;
                    if (this.map.mapTemplate.boss.Length > 0)
                    {
                        for (global::System.Int32 i = 0; i < map.mapTemplate.boss.Length; i++)
                        {
                            if (numMobDie[i] >= numMobDieNeed[i])
                            {
                                if (GopetManager.ID_BOSS_TASK.Contains(map.mapTemplate.boss[i]))
                                {
                                    bool flag = false;
                                    foreach (var p in players)
                                    {
                                        if (flag) break;
                                        foreach (var task in p.playerData.task)
                                        {
                                            if (flag) break;
                                            foreach (var item in task.taskInfo)
                                            {
                                                if (item[0] == TaskCalculator.REQUEST_KILL_SPECIAL_BOSS)
                                                {
                                                    if (item[2] == map.mapTemplate.boss[i])
                                                    {
                                                        flag = true;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (!flag)
                                    {
                                        numMobDie[i] = 0;
                                        continue;
                                    }
                                }
                                Boss boss = new Boss(map.mapTemplate.boss[i], mobLocation);
                                boss.isTimeOut = true;
                                boss.TimeOut = DateTime.Now.AddMilliseconds(GopetManager.TIME_BOSS_DISPOINTED);
                                addNewMob(boss);
                                nGopetMobs.add(boss);
                                PlayerManager.showBanner((l) => string.Format(l.BannerLanguage[LanguageData.BANNER_SHOW_BOSS_SUMMON], boss.Template.name, this.map.mapTemplate.name, this.zoneID));
                                numMobDie[i] = 0;
                                goto END_INIT_MOB;
                            }
                        }
                    }
                    long deltaTime = Utilities.CurrentTimeMillis + 3000;
                    while (deltaTime > Utilities.CurrentTimeMillis)
                    {
                        MobLvlMap mobLvlMap = Utilities.RandomArray(mobLvlMaps);
                        if (GopetManager.PETTEMPLATE_HASH_MAP.ContainsKey(mobLvlMap.getPetId()))
                        {
                            PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(mobLvlMap.getPetId());
                            Mob m = new Mob(petTemplate, this, mobLvlMap, mobLocation);

                            addNewMob(m);
                            nGopetMobs.add(m);
                            break;
                        }
                    }
                    for (global::System.Int32 i = 0; i < map.mapTemplate.boss.Length; i++)
                    {
                        numMobDie[i]++;
                    }
                END_INIT_MOB:;
                }
                sendMob(nGopetMobs);
            }
        }
    }

    private void sendPetBattleList(Player player)
    {
        foreach (PetBattle petBattle in petBattles)
        {
            petBattle.sendBattleInfo(player);
        }
    }

    public void showBigTextEff(String text)
    {
        Message message = messagePetService(GopetCMD.SHOW_BIG_TEXT_EFF);
        message.putUTF(text);
        message.cleanup();
        sendMessage(message);
    }

    private void sendWing(Player player)
    {
        CopyOnWriteArrayList<Player> currentPlayers = (CopyOnWriteArrayList<Player>)players.clone();
        HashMap<int, Item> wingPlayer = new();
        foreach (Player currentPlayer in currentPlayers)
        {
            Item wingItem = currentPlayer.playerData.wing;
            if (wingItem != null)
            {
                wingPlayer.put(currentPlayer.user.user_id, wingItem);
            }
        }
        Message m = messagePetService(GopetCMD.WING);
        m.putsbyte(3);
        m.putInt(wingPlayer.Count);
        foreach (var entry in wingPlayer)
        {
            int key = entry.Key;
            Item val = entry.Value;
            m.putInt(key);
            m.putUTF(val.Template.frameImgPath);
            m.putsbyte(val.getTemp().getOptionValue()[0]);
        }
        m.cleanup();
        player.session.sendMessage(m);

    }

    public void sendMyWing(Player player)
    {
        Item wingItem = player.playerData.wing;
        if (wingItem != null)
        {
            Message m = messagePetService(GopetCMD.WING);
            m.putsbyte(3);
            m.putInt(1);
            m.putInt(player.user.user_id);
            m.putUTF(wingItem.getTemp().getFrameImgPath());
            m.putsbyte(wingItem.getTemp().getOptionValue()[0]);
            m.cleanup();
            sendMessage(m);
        }
    }

    public void sendUnEquipWing(Player player)
    {
        Message m = messagePetService(GopetCMD.WING);
        m.putsbyte(3);
        m.putInt(1);
        m.putInt(player.user.user_id);
        m.putUTF("");
        m.putsbyte(0);
        m.cleanup();
        sendMessage(m);
    }

    private void sendSkin(Player player)
    {
        CopyOnWriteArrayList<Player> currentPlayers = (CopyOnWriteArrayList<Player>)players.clone();
        HashMap<int, Item> skinPlayer = new();
        foreach (Player currentPlayer in currentPlayers)
        {
            Item itemSkin = currentPlayer.playerData.skin;
            if (itemSkin != null)
            {
                skinPlayer.put(currentPlayer.user.user_id, itemSkin);
            }
        }
        Message m = messagePetService(GopetCMD.SEND_SKIN);
        m.putInt(skinPlayer.Count);
        foreach (var entry in skinPlayer)
        {
            int key = entry.Key;
            Item val = entry.Value;
            m.putInt(key);
            m.putUTF(val.getTemp().getFrameImgPath());
        }
        m.cleanup();
        player.session.sendMessage(m);
        sendMySkin(player);
    }

    public void sendMySkin(Player player)
    {
        Message m = messagePetService(GopetCMD.SEND_SKIN);
        m.putInt(1);
        m.putInt(player.user.user_id);
        Item itemSkin = player.playerData.skin;
        if (itemSkin != null)
        {
            m.putUTF(itemSkin.getTemp().getFrameImgPath());
        }
        else
        {
            m.putUTF("");
        }

        m.cleanup();
        sendMessage(m);
    }

    public void sendMessage(Message message, JArrayList<Player> listNoneSend)
    {
        foreach (Player player in players)
        {
            if (!listNoneSend.Contains(player))
            {
                player.session.sendMessage(message);
            }
        }
    }

    public void petInteract(sbyte type, int user_id)
    {
        Message m = messagePetService(GopetCMD.ON_PET_INTERACT);
        m.putInt(user_id);
        m.putsbyte(type);
        sendMessage(m);
    }

    public void sendClan(Player player, bool isAddToplace)
    {
        ClanMember clanMember = player.controller.getClan();
        if (clanMember != null)
        {
            Message m = GameController.clanMessage(GopetCMD.GUILD_NAME_IN_PLACE);
            m.putInt(1);
            m.putInt(clanMember.user_id);
            m.putUTF(clanMember.getClan().getName().ToUpper());
            m.cleanup();
            sendMessage(m);
        }

        if (isAddToplace)
        {
            JArrayList<ClanMember> clanMembers = new();
            foreach (Player player1 in players)
            {
                ClanMember clanMember1 = player1.controller.getClan();
                if (clanMember1 != null)
                {
                    clanMembers.add(clanMember1);
                }
            }

            Message m = GameController.clanMessage(GopetCMD.GUILD_NAME_IN_PLACE);
            m.putInt(clanMembers.Count);
            foreach (ClanMember clanMember1 in clanMembers)
            {
                m.putInt(clanMember1.user_id);
                m.putUTF(clanMember1.getClan().getName().ToUpper());
            }
            m.cleanup();
            player.session.sendMessage(m);
        }
    }
    public void sendTimePlace()
    {
        Message message = GopetPlace.messagePetService(GopetCMD.TIME_PLACE);
        message.putInt(Utilities.round(placeTime - Utilities.CurrentTimeMillis) / 1000);
        message.cleanup();
        sendMessage(message);
    }

    public void updatePlayerAnimation(Player player)
    {
        Message message = messagePetService(GopetCMD.SEND_ANIMATION_CHARACTER);
        message.putInt(player.user.user_id);
        Animation[] animations = player.controller.Animations;
        message.putInt(animations.Length);
        for (int i = 0; i < animations.Length; i++)
        {
            Animation anim = animations[i];
            message.putsbyte(anim.numFrame);
            message.putUTF(anim.frameImgPath);
            message.putShort(anim.vX);
            message.putShort(anim.vY);
            message.putbool(anim.isDrawEnd);
            message.putbool(anim.mirrorWithChar);
            message.putsbyte(anim.type);
        }
        message.cleanup();
        sendMessage(message);
    }

    private void sendListAnimationOfAllPlayers(Player player)
    {
        Message message = messagePetService(GopetCMD.SEND_LIST_ANIMATION_CHARACTER);
        foreach (var p in players)
        {
            message.putInt(p.playerData.user_id);
            Animation[] animations = p.controller.Animations;
            message.putInt(animations.Length);
            for (int i = 0; i < animations.Length; i++)
            {
                Animation anim = animations[i];
                message.putsbyte(anim.numFrame);
                message.putUTF(anim.frameImgPath);
                message.putShort(anim.vX);
                message.putShort(anim.vY);
                message.putbool(anim.isDrawEnd);
                message.putbool(anim.mirrorWithChar);
                message.putsbyte(anim.type);
            }
        }
        player.session.sendMessage(message);
    }

    public void RemoveBattleByMobId(int mobId)
    {
        Message message = GameController.messagePetService(GopetCMD.REMOVE_BATTLE_BY_MOB_ID);
        message.putInt(mobId);
        message.cleanup();
        sendMessage(message);
    }
    public void UpdateHpMob(int mobId, int hp)
    {
        Message message = GameController.messagePetService(GopetCMD.UPDATE_HP_BOSS);
        message.putInt(mobId);
        message.putInt(hp);
        message.cleanup();
        sendMessage(message);
    }
}
