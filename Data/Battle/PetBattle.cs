using Gopet.Data.GopetClan;
using Gopet.Data.Collections;
using Gopet.Data.GopetItem;
using Gopet.Data.Mob;
using Gopet.IO;
using Gopet.Util;
using Gopet.Manager;

namespace Gopet.Battle
{
    public class PetBattle
    {

        private bool petAttackMob = true;
        private Pet activePet, passivePet;
        private Mob mob;
        private GopetPlace place;
        private Player passivePlayer, activePlayer;
        private bool isActiveTurn = false;
        private PetBattleInfo activeBattleInfo = new PetBattleInfo(), passiveBattleInfo = new PetBattleInfo();
        private bool isPK = false;
        private int price = 0;
        private bool isClose = false;
        private Player ClosePlayer = null;
        private int userInvitePK = -1;
        private long timeCheckplayer = 0;

        public PetBattle(GopetPlace place, Player passivePlayer, Player activePlayer)
        {
            this.place = place;
            this.passivePlayer = passivePlayer;
            this.activePlayer = activePlayer;
            activePet = activePlayer.playerData.petSelected;
            passivePet = passivePlayer.playerData.petSelected;
            setIsActiveTurn(activePet.getAgi() >= passivePet.getAgi());
            setPetAttackMob(false);
            setDelaTimeTurn(Utilities.CurrentTimeMillis + GopetManager.TimeNextTurn);
            setActiveBattleInfo(new PetBattleInfo(activePlayer));
            PassiveBattleInfo = new PetBattleInfo(passivePlayer);

            getActiveBattleInfo().addBuff(new Debuff(new ItemInfo[] { new ItemInfo(10, Utilities.round(GopetManager.MitigatePetData[activePet.Template.element][passivePet.Template.element] * 100)) }, int.MaxValue));
            PassiveBattleInfo.addBuff(new Debuff(new ItemInfo[] { new ItemInfo(10, Utilities.round(GopetManager.MitigatePetData[passivePet.Template.element][activePet.Template.element] * 100)) }, int.MaxValue));
            if (place == null) throw new ArgumentNullException(nameof(place));
        }

        public PetBattle(Mob mob, GopetPlace place, Player activePlayer)
        {
            this.mob = mob;
            this.place = place;
            this.activePlayer = activePlayer;
            this.activePlayer.isPetRecovery = false;
            setActivePet(activePlayer.playerData.petSelected);
            setDelaTimeTurn(Utilities.CurrentTimeMillis + GopetManager.TimeNextTurn);
            setIsActiveTurn(false);
            setActiveBattleInfo(new PetBattleInfo(activePlayer));
            getActiveBattleInfo().addBuff(new Debuff(new ItemInfo[] { new ItemInfo(10, Utilities.round(GopetManager.MitigatePetData[activePet.Template.element][mob.Template.element] * 100)) }, int.MaxValue));
            PassiveBattleInfo.addBuff(new Debuff(new ItemInfo[] { new ItemInfo(10, Utilities.round(GopetManager.MitigatePetData[mob.Template.element][activePet.Template.element] * 100)) }, int.MaxValue));
            timeCheckplayer = Utilities.CurrentTimeMillis + 7000L;
            if (place == null) throw new ArgumentNullException(nameof(place));
        }

        public void setUserInvitePK(int userInvitePK)
        {
            this.userInvitePK = userInvitePK;
        }

        public bool isIsPK()
        {
            return isPK;
        }

        public void setIsPK(bool isPK)
        {
            this.isPK = isPK;
        }

        public int getPrice()
        {
            return price;
        }

        public void setPrice(int price)
        {
            this.price = price;
        }

        public PetBattleInfo getActiveBattleInfo()
        {
            return activeBattleInfo;
        }

        public void setActiveBattleInfo(PetBattleInfo activeBattleInfo)
        {
            this.activeBattleInfo = activeBattleInfo;
        }

        public PetBattleInfo PassiveBattleInfo
        {
            get
            {
                return passiveBattleInfo;
            }
            set
            {
                passiveBattleInfo = value;
            }
        }



        public bool isIsActiveTurn()
        {
            return isActiveTurn;
        }

        public void setIsActiveTurn(bool isActiveTurn)
        {
            this.isActiveTurn = isActiveTurn;
        }

        public bool isPetAttackMob()
        {
            return petAttackMob;
        }

        public void setPetAttackMob(bool petAttackMob)
        {
            this.petAttackMob = petAttackMob;
        }

        public Pet getActivePet()
        {
            return activePet;
        }

        public void setActivePet(Pet activePet)
        {
            this.activePet = activePet;
        }

        public Pet getPassivePet()
        {
            return passivePet;
        }

        public void setPassivePet(Pet passivePet)
        {
            this.passivePet = passivePet;
        }

        public Mob getMob()
        {
            return mob;
        }

        public void setMob(Mob mob)
        {
            this.mob = mob;
        }

        public GopetPlace getPlace()
        {
            return place;
        }

        public void setPlace(GopetPlace place)
        {
            this.place = place;
        }

        public Player getPassivePlayer()
        {
            return passivePlayer;
        }

        public void setPassivePlayer(Player passivePlayer)
        {
            this.passivePlayer = passivePlayer;
        }

        public Player getActivePlayer()
        {
            return activePlayer;
        }

        public void setActivePlayer(Player activePlayer)
        {
            this.activePlayer = activePlayer;
        }

        public void onMessage(Message message, Player player)
        {
            sbyte subId = message.readsbyte();
            switch (subId)
            {
                case GopetCMD.PetBattle_ATTACK:
                    petAttack(player);
                    break;
                case GopetCMD.PET_BATTLE_USE_SKILL:
                    useSkill(player, message.reader().readInt());
                    break;
                case GopetCMD.PET_BATTLE_USE_ITEM:
                    MenuController.sendMenu(MenuController.MENU_SELECT_ITEM_SUPPORT_PET, player);
                    break;
            }
        }


        private void petAttack(Player player)
        {
            bool isStun = ItemInfo.getValueById(getUserPetBattleInfo().getBuff(), ItemInfo.Type.STUN) > 0 || Utilities.NextFloatPer() < ItemInfo.getValueById(getUserPetBattleInfo().getBuff(), ItemInfo.Type.PER_STUN_1_TURN) / 100f;
            if (isStun)
            {
                nextTurn();
                return;
            }
            if (checkWhoseTurn(player))
            {
                bool isMiss = randMiss(getNonUserPetBattleInfo());
                JArrayList<TurnEffect> turnEffects = new();
                if (!isMiss)
                {
                    PetDamgeInfo damge = makeDamage(getUserPetBattleInfo(), getNonUserPetBattleInfo(), null);
                    if (getPet().IsCrit)
                    {
                        damge.setDamge(damge.getDamge() * 2);
                        turnEffects.add(new TurnEffect(TurnEffect.SKILL_CRIT, getFocus(), TurnEffect.SKILL_CRIT, -damge.getDamge(), 0));
                    }
                    else
                    {
                        turnEffects.add(new TurnEffect(TurnEffect.SKILL_NORMAL, getFocus(), TurnEffect.SKILL_NORMAL, -damge.getDamge(), 0));
                    }
                    if (petAttackMob)
                    {
                        mob.addHp(damge.getDamge());
                    }
                    else
                    {
                        getNonPet().subHp(damge.getDamge());
                    }
                }
                else
                {
                    turnEffects.add(new TurnEffect(TurnEffect.SKILL_MISS, getFocus(), TurnEffect.SKILL_MISS, 0, 0));
                }
                sendPetAttack(turnEffects, TurnEffect.createNormalAttack(activePet.mp, 0, getUserTurnId()));
                nextTurn();
                if (hasWinner())
                {
                    win();
                }
                /*
                if (petAttackMob)
                {
                    Thread.Sleep(GopetManager.DELAY_TURN_PET_BATTLE);
                }*/
            }
            else
            {
                player.redDialog("Chưa tới lượt của bạn");
            }
        }

        private void sendPetAttack(JArrayList<TurnEffect> turnDatas, TurnEffect mainTurnData)
        {
            Message message = new Message(GopetCMD.PET_SERVICE);
            message.putsbyte(GopetCMD.PET_BATTLE);
            message.putInt(activePlayer.user.user_id);
            message.putInt(mainTurnData.petId);
            message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
            message.putInt((int)GopetManager.TimeNextTurn);
            message.putsbyte(mainTurnData.type);
            if (mainTurnData.type == TurnEffect.TYPE_EFFECT_WAIT)
            {
                // old version            
                message.putInt(0);
                message.putUTF("");
                // old version

                message.putInt(mainTurnData.mp);
            }
            message.putInt(turnDatas.Count);
            foreach (TurnEffect turnData in turnDatas)
            {

                message.putInt(turnData.petId);
                message.putInt(turnData.skillId);
                // old version
                message.putUTF("");
                message.putInt(0);
                message.putInt(0);
                message.putInt(0);
                // old version

                message.putInt(turnData.hp);
                message.putInt(turnData.mp);

                // old version            
                message.putInt(0);
                message.putInt(0);
                // old version
            }
            message.cleanup();
            place.sendMessage(message);
            if (!petAttackMob)
            {
                message = new Message(GopetCMD.PET_SERVICE);
                message.putsbyte(GopetCMD.PET_BATTLE);
                message.putInt(passivePlayer.user.user_id);
                message.putInt(mainTurnData.petId);
                message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
                message.putInt((int)GopetManager.TimeNextTurn);
                message.putsbyte(mainTurnData.type);
                if (mainTurnData.type == TurnEffect.TYPE_EFFECT_WAIT)
                {
                    // old version            
                    message.putInt(0);
                    message.putUTF("");
                    // old version

                    message.putInt(mainTurnData.mp);
                }
                message.putInt(turnDatas.Count);
                foreach (TurnEffect turnData in turnDatas)
                {

                    message.putInt(turnData.petId);
                    message.putInt(turnData.skillId);
                    // old version
                    message.putUTF("");
                    message.putInt(0);
                    message.putInt(0);
                    message.putInt(0);
                    // old version

                    message.putInt(turnData.hp);
                    message.putInt(turnData.mp);

                    // old version            
                    message.putInt(0);
                    message.putInt(0);
                    // old version
                }
                message.cleanup();
                passivePlayer.session.sendMessage(message);
            }
        }

        public GameObject ActiveObject
        {
            get
            {
                return petAttackMob ? isActiveTurn ? activePet : mob : isActiveTurn ? activePet : passivePet;
            }
        }
        public GameObject PassiveObject
        {
            get
            {
                return petAttackMob ? !isActiveTurn ? activePet : mob : !isActiveTurn ? activePet : passivePet;
            }
        }

        private int getUserTurnId()
        {
            return petAttackMob ? isActiveTurn ? activePlayer.user.user_id : mob.getMobId() : isActiveTurn ? activePlayer.user.user_id : passivePlayer.user.user_id;
        }

        private PetBattleInfo getUserPetBattleInfo()
        {
            return petAttackMob ? isActiveTurn ? activeBattleInfo : passiveBattleInfo : isActiveTurn ? activeBattleInfo : passiveBattleInfo;
        }

        private PetBattleInfo getNonUserPetBattleInfo()
        {
            return petAttackMob ? !isActiveTurn ? activeBattleInfo : passiveBattleInfo : !isActiveTurn ? activeBattleInfo : passiveBattleInfo;
        }

        private bool checkWhoseTurn(Player player)
        {
            return (isActiveTurn ? player == activePlayer : false) || (!isActiveTurn ? player == passivePlayer : false);
        }

        private Pet getPet()
        {
            return petAttackMob ? isActiveTurn ? activePet : null : isActiveTurn ? activePet : passivePet;
        }

        private Pet getNonPet()
        {
            return petAttackMob ? !isActiveTurn ? activePet : null : !isActiveTurn ? activePet : passivePet;
        }


        public void sendBattleInfo(Player playerInZone)
        {
            if (petAttackMob)
            {
                Player player = activePlayer;
                Message message = new Message(GopetCMD.PET_SERVICE);
                message.putsbyte(GopetCMD.ATTACK_MOB);
                //Turn time
                message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
                //Max turn Time
                message.putInt((int)GopetManager.TimeNextTurn);
                message.putInt(player.user.user_id);
                writeMyPetInfo(player.playerData.petSelected, message);
                message.putInt(mob.getMobId());
                writeMobInfo(mob, message);
                message.cleanup();
                playerInZone.session.sendMessage(message);
            }
            else
            {
                Message message = GameController.messagePetSerive(GopetCMD.PLAYER_BATTLE);
                message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
                message.putInt((int)GopetManager.TimeNextTurn);
                message.putInt(activePlayer.user.user_id);
                message.putsbyte(0);
                writeMyPetInfo(activePlayer.getPet(), message);
                message.putInt(passivePlayer.user.user_id);
                writePetPassiveInfo(passivePlayer.getPet(), message);
                message.cleanup();
                playerInZone.session.sendMessage(message);
            }
        }

        public void sendStartFightPlayer()
        {
            Message message = GameController.messagePetSerive(GopetCMD.PLAYER_BATTLE);
            message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
            message.putInt((int)GopetManager.TimeNextTurn);
            message.putInt(activePlayer.user.user_id);
            message.putsbyte(1);
            writeMyPetInfo(activePlayer.getPet(), message);
            message.putInt(passivePlayer.user.user_id);
            writePetPassiveInfo(passivePlayer.getPet(), message);
            message.putbool(false);
            message.cleanup();
            activePlayer.session.sendMessage(message);

            message = GameController.messagePetSerive(GopetCMD.PLAYER_BATTLE);
            message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
            message.putInt((int)GopetManager.TimeNextTurn);
            message.putInt(passivePlayer.user.user_id);
            message.putsbyte(0);
            writeMyPetInfo(passivePlayer.getPet(), message);
            message.putInt(activePlayer.user.user_id);
            writePetPassiveInfo(activePlayer.getPet(), message);
            message.putbool(false);
            message.cleanup();
            passivePlayer.session.sendMessage(message);
            JArrayList<Player> listNoneSend = new();
            listNoneSend.add(activePlayer);
            listNoneSend.add(passivePlayer);
            place.sendMessage(message, listNoneSend);
        }

        public void sendStartFightMob(Mob mob, Player player)
        {
            Message message = new Message(GopetCMD.PET_SERVICE);
            message.putsbyte(GopetCMD.ATTACK_MOB);
            //Turn time
            message.putInt(Utilities.round(delaTimeTurn - Utilities.CurrentTimeMillis));
            //Max turn Time
            message.putInt((int)GopetManager.TimeNextTurn);
            message.putInt(player.user.user_id);
            writeMyPetInfo(player.playerData.petSelected, message);
            message.putInt(mob.getMobId());
            writeMobInfo(mob, message);
            message.cleanup();
            place.sendMessage(message);
        }

        public static void writeMyPetInfo(Pet pet, Message message)
        {
            message.putInt(pet.getPetIdTemplate());
            message.putUTF(pet.getPetTemplate().frameImg);
            message.putUTF(pet.getNameWithStar());
            message.putInt(1);
            message.putInt(pet.getPetTemplate().str);
            message.putInt(pet.getPetTemplate().agi);
            message.putInt(pet.getPetTemplate()._int);

            //
            message.putInt(1);
            message.putInt(1);
            message.putInt(pet.hp);
            message.putInt(pet.mp);
            message.putInt(pet.maxHp);
            message.putInt(pet.maxMp);
            message.putsbyte(pet.skill.Length);
            for (int i = 0; i < pet.skill.Length; i++)
            {
                int skillId = pet.skill[i][0];
                int skilllvl = pet.skill[i][1];
                PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(skillId);
                PetSkillLv petSkillLv = petSkill.skillLv.get(skilllvl - 1);
                message.putInt(skillId);
                message.putUTF(petSkill.name + " " + skilllvl);
                message.putUTF(petSkill.getDescription(petSkillLv));
                message.putInt(petSkillLv.mpLost);
            }
        }

        public static void writeMobInfo(Mob mob, Message message)
        {
            message.putInt(mob.getMobId());
            message.putUTF(mob.getPetTemplate().frameImg);
            message.putUTF(mob.getPetTemplate().name);

            //
            message.putInt(1);
            message.putInt(mob.hp);
            message.putInt(mob.mp);
            message.putInt(mob.maxHp);
            message.putInt(mob.maxMp);
            message.putsbyte(0);
        }

        public static void writePetPassiveInfo(Pet petPassive, Message message)
        {
            message.putInt(petPassive.getPetIdTemplate());
            message.putUTF(petPassive.getPetTemplate().frameImg);
            message.putUTF(petPassive.getPetTemplate().name);

            //
            message.putInt(petPassive.lvl);
            message.putInt(petPassive.hp);
            message.putInt(petPassive.mp);
            message.putInt(petPassive.maxHp);
            message.putInt(petPassive.maxMp);
            message.putsbyte(petPassive.skill.Length);
            for (int i = 0; i < petPassive.skill.Length; i++)
            {
                int skillId = petPassive.skill[i][0];
                int skilllvl = petPassive.skill[i][1];
                PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(skillId);
                PetSkillLv petSkillLv = petSkill.skillLv.get(skilllvl - 1);
                message.putInt(skillId);
                message.putUTF(petSkill.name + " " + skilllvl);
            }
        }

        private long delaTimeTurn;

        public long getDelaTimeTurn()
        {
            return delaTimeTurn;
        }

        public void setDelaTimeTurn(long delaTimeTurn)
        {
            this.delaTimeTurn = delaTimeTurn;
        }

        public void update()
        {
            if (hasWinner())
            {
                return;
            }
            else
            {
                if (petAttackMob)
                {
                    if (timeCheckplayer < Utilities.CurrentTimeMillis)
                    {
                        if (!mob.bound.Contains(activePlayer.playerData.x, activePlayer.playerData.y))
                        {
                            activePlayer.user.ban(UserData.BAN_TIME, "Hệ thống thấy bạn dùng auto", Utilities.CurrentTimeMillis + 1000l * 60 * 15);
                            activePlayer.session.Close();
                        }
                    }
                }
            }
            if (Utilities.CurrentTimeMillis > delaTimeTurn)
            {
                if (isPetAttackMob())
                {
                    if (getUserTurnId() != mob.getMobId())
                    {
                        petAttack(activePlayer);
                    }
                }
                nextTurn();
            }
            if (isPetAttackMob())
            {
                if (getUserTurnId() == mob.getMobId())
                {
                    mobAttack();
                }
            }
            if (hasWinner())
            {
                win();
            }
        }

        public Player getWinner()
        {
            return getWinId() == activePlayer.user.user_id ? activePlayer : passivePlayer;
        }

        public Player getNonWinner()
        {
            return getWinId() == activePlayer.user.user_id ? passivePlayer : activePlayer;
        }

        public bool hasWinner()
        {
            return (mob != null ? mob.getHp() <= 0 : false) || (activePet != null ? activePet.hp <= 0 || isClose : false) || (!petAttackMob ? activePet.hp <= 0 || passivePet.hp <= 0 || isClose : false);
        }

        private void nextTurn()
        {
            setIsActiveTurn(!isActiveTurn);
            setDelaTimeTurn(Utilities.CurrentTimeMillis + GopetManager.TimeNextTurn);
            updateDamageToxic();
            updateDamagePhanDoan();
            if (isActiveTurn)
            {
                getActiveBattleInfo().nextTurn();
            }
            else
            {
                PassiveBattleInfo.nextTurn();
            }

            if (petAttackMob)
            {
                activePlayer.controller.sendMyPetInfo();
            }
            else
            {
                activePlayer.controller.sendMyPetInfo();
                passivePlayer.controller.sendMyPetInfo();
            }
        }

        private int getFocus()
        {
            return petAttackMob ? !isActiveTurn ? activePlayer.user.user_id : mob.getMobId() : !isActiveTurn ? activePlayer.user.user_id : passivePlayer.user.user_id;
        }

        private bool hadFinished = false;

        private void win()
        {
            if (hadFinished)
            {
                return;
            }
            else
            {
                hadFinished = true;
            }
            JArrayList<Popup> petBattleTexts = new();
            if (petAttackMob)
            {
                int exp = 0;
                int coin = 0;
                if (getWinId() == activePlayer.user.user_id)
                {
                    if (!(mob is Boss))
                    {
                         
                        ClanMember clanMember = activePlayer.controller.getClan();
                        float perExpPlus = 0f;
                        float coinPlus = 0f;
                        if (clanMember != null)
                        {
                            perExpPlus += clanMember.getClan().getBuffByIdBuff(ClanBuff.BUFF_EXP).getPercent();
                            coinPlus += clanMember.getClan().getBuffByIdBuff(ClanBuff.BUFF_GEM).getPercent();
                        }
                        exp = genExpWhenMobDie(activePlayer, activePet, mob, mob.getMobLvInfo().exp);
                        exp = Math.Max(0, exp + Utilities.round(Utilities.GetValueFromPercent(exp, activePlayer.playerData.buffExp.getPercent() + perExpPlus)));
                        int constCoin = genGemWhenMobDie(activePlayer, activePet, mob);
                        coin = (int)(constCoin + Utilities.GetValueFromPercent(constCoin, coinPlus));
                        activePlayer.addCoin(coin);
                        activePet.addExp(exp);
                        activePlayer.controller.updatePetLvl();
                        place.mobDie(mob);
                        JArrayList<DropItem> listItemDrop = new(GopetManager.dropItem.get(place.map.mapID).ToList());
                        if (GopetManager.mapHasDropItemLvlRange.Contains(place.map.mapID))
                        {
                            foreach (DropItem next in listItemDrop.ToArray())
                            {
                                if (next.getLvlRange() != null)
                                {
                                    if (!(next.getLvlRange()[0] <= mob.getMobLvInfo().lvl && next.getLvlRange()[1] >= mob.getMobLvInfo().lvl))
                                    {
                                        listItemDrop.remove(next);
                                    }
                                }
                            }
                        }
                        if (listItemDrop != null)
                        {
                            if (!listItemDrop.isEmpty())
                            {
                                DropItem next = listItemDrop.get(Utilities.nextInt(listItemDrop.Count));
                                float nextPer = Utilities.NextFloatPer();
                                if (next.getPercent() > nextPer)
                                {
                                    Item item = new Item(next.getItemTemplateId());
                                    item.count = next.getCount();
                                    activePlayer.addItemToInventory(item);
                                    PetBattleText petBattleText = new PetBattleText(item.getTemp().getName() + " x" + item.count);
                                    petBattleTexts.add(petBattleText);
                                }
                            }
                        }
                        activePlayer.controller.getTaskCalculator().onKillMob(mob.getPetTemplate().petId);
                    }
                    else
                    {
                        Boss boss = (Boss)mob;
                        petBattleTexts.AddRange(activePlayer.controller.onReiceiveGift(boss.Template.gift));
                        place.mobDie(mob);
                        JArrayList<string> txtInfo = new();
                        foreach (Popup petBattleText in petBattleTexts)
                        {
                            txtInfo.add(petBattleText.getText());
                        }
                        activePlayer.okDialog(Utilities.Format("Chức mừng bạn kích sát %s nhận được :\n%s", boss.Template.name, string.Join(",", txtInfo)));
                        activePlayer.controller.getTaskCalculator().onKillBoss(boss);
                    }
                    HistoryManager.addHistory(new History(activePlayer).setLog(Utilities.Format("Tiếu diệt quái %s", mob.getName())).setObj(mob).setSpceialType(History.KILL_MOB));
                    activePlayer.controller.randomCaptcha();
                }
                else
                {
                    activePlayer.controller.delayTimeHealPet = Utilities.CurrentTimeMillis + GopetManager.TIME_DELAY_HEAL_WHEN_MOB_KILL_PET;
                }
                win(petBattleTexts.ToArray(), coin, exp);
                activePlayer.controller.setLastTimeKillMob(Utilities.CurrentTimeMillis);
            }
            else
            {
                Player winner = getWinner();
                Player nonWinner = getNonWinner();
                ClanMember nonPlayerClanMember = nonWinner.controller.getClan();
                ClanMember winPlayerClanMember = winner.controller.getClan();
                if (isPK)
                {
                    long exp_sub = 0;
                    long exp_sub_winner = 0;
                    Pet nonPet = nonWinner.getPet();
                    Pet winnerPet = winner.getPet();
                    long coinPK = Utilities.round(Utilities.GetValueFromPercent(nonWinner.playerData.coin, 1f));
                    long expCurrentLvl = GopetManager.PetExp.get(nonPet.lvl);
                    if (nonWinner.playerData.pkPoint > 0)
                    {
                        exp_sub = Utilities.round(Utilities.GetValueFromPercent(expCurrentLvl, 10f));
                        nonWinner.playerData.pkPoint--;
                    }
                    else
                    {
                        exp_sub = Utilities.round(Utilities.GetValueFromPercent(expCurrentLvl, 5f));
                    }
                    expCurrentLvl = GopetManager.PetExp.get(winnerPet.lvl);
                    if (getWinId() == userInvitePK)
                    {
                        exp_sub_winner = Utilities.round(Utilities.GetValueFromPercent(expCurrentLvl, 3f));
                    }
                    if (nonPlayerClanMember != null)
                    {
                        ClanBuff minBuff = nonPlayerClanMember.getClan().getBuffByIdBuff(ClanBuff.BUFF_DESCREASE_EXP_WHEN_PK);
                        if (!(minBuff.getPercent() >= 100f))
                        {
                            exp_sub -= (int)Utilities.GetValueFromPercent(exp_sub, minBuff.getPercent());
                        }
                        else
                        {
                            exp_sub = 0;
                        }
                        ClanBuff minBuffSkipDie = nonPlayerClanMember.getClan().getBuffByIdBuff(ClanBuff.BUFF_SKIP_PET_DIE_WHEN_PK);
                        if (!(Utilities.NextFloatPer() < minBuffSkipDie.getPercent()))
                        {
                            nonPet.petDieByPK = true;
                        }
                    }
                    else
                    {
                        nonPet.petDieByPK = true;
                    }
                    nonPet.subExpPK(exp_sub);
                    if (winPlayerClanMember != null)
                    {
                        ClanBuff minBuff = nonPlayerClanMember.getClan().getBuffByIdBuff(ClanBuff.BUFF_DESCREASE_EXP_WHEN_PK);
                        if (!(minBuff.getPercent() >= 100f))
                        {
                            exp_sub_winner -= (int)Utilities.GetValueFromPercent(exp_sub_winner, minBuff.getPercent());
                        }
                        else
                        {
                            exp_sub_winner = 0;
                        }
                    }
                    winnerPet.addExp((int)exp_sub_winner);
                    win(petBattleTexts.ToArray(), price, 0);
                    winner.addCoin(Utilities.round(Utilities.GetValueFromPercent(coinPK, 50f)));
                    nonWinner.mineCoin(coinPK);
                    if (exp_sub_winner > 0)
                    {
                        winner.okDialog(Utilities.Format("Pet của bạn đã bị trừ %s exp", Utilities.FormatNumber(exp_sub_winner)));
                    }
                    nonWinner.okDialog(Utilities.Format("Pet của bạn đã bị trừ %s exp", Utilities.FormatNumber(exp_sub)));
                    nonPet.TimeDie = Utilities.CurrentTimeMillis + (1000l * 60 * 15);
                    MapManager.maps.get(MapManager.ID_LINH_THU_CITY).addRandom(nonWinner);
                }
                else
                {
                    if (price > 0)
                    {
                        int totalPrice = Utilities.round(price * 2 - Utilities.GetValueFromPercent(price * 2, GopetManager.BET_PRICE_PLAYER_CHALLENGE));
                        winner.addCoin(totalPrice);
                        winner.controller.getTaskCalculator().onWinBetBattle();
                        win(petBattleTexts.ToArray(), price, 0);
                    }
                }
            }

        }

        private void win(Popup[] petBattleTexts, int coin, int exp)
        {
            place.sendMessage(win(petBattleTexts, coin, exp, activePlayer.user.user_id));
            activePlayer.controller.sendMyPetInfo();
            if (!petAttackMob)
            {
                passivePlayer.session.sendMessage(win(petBattleTexts, coin, exp, passivePlayer.user.user_id));
                passivePlayer.controller.sendMyPetInfo();
            }
        }

        private Message win(Popup[] petBattleTexts, int coin, int exp, int battleId)
        {
            Message m = new Message(GopetCMD.PET_SERVICE);
            m.putsbyte(GopetCMD.PET_BATTLE_STATE);
            m.putInt(battleId);
            m.putInt(getWinId());
            m.putsbyte(0);
            m.putInt(coin);
            m.putInt(exp);
            m.putsbyte(petBattleTexts.Length);
            for (int i = 0; i < petBattleTexts.Length; i++)
            {
                Popup petBattleText = petBattleTexts[i];
                m.putUTF(petBattleText.getText());
                m.putUTF("2");
            }
            m.cleanup();
            return m;
        }

        private int getWinId()
        {
            return isClose ? petAttackMob ? mob.getMobId() : getPlayerHere().user.user_id : petAttackMob ? mob.hp <= 0 ? activePlayer.user.user_id : mob.getMobId() : activePet.hp <= 0 ? passivePlayer.user.user_id : activePlayer.user.user_id;
        }

        public void Close(Player player)
        {
            isClose = true;
            ClosePlayer = player;
            clean();
        }

        public Player getPlayerNotHere()
        {
            return ClosePlayer == activePlayer ? activePlayer : passivePlayer;
        }

        public Player getPlayerHere()
        {
            return ClosePlayer == activePlayer ? passivePlayer : activePlayer;
        }

        public void clean()
        {
            if (petAttackMob)
            {
                activePlayer.controller.setPetBattle(null);
                mob.setPetBattle(null);
            }
            else
            {
                activePlayer.controller.setPetBattle(null);
                passivePlayer.controller.setPetBattle(null);
            }
            place.petBattles.remove(this);
        }

        private void useSkill(Player player, int skillId)
        {
            bool isStun = ItemInfo.getValueById(getUserPetBattleInfo().getBuff(), ItemInfo.Type.STUN) > 0 || Utilities.NextFloatPer() < ItemInfo.getValueById(getUserPetBattleInfo().getBuff(), ItemInfo.Type.PER_STUN_1_TURN) / 100f;

            if (isStun)
            {
                nextTurn();
                return;
            }

            PetBattleInfo petBattleInfo = getUserPetBattleInfo();
            if (petBattleInfo.getPlayer() == player)
            {
                if (!petBattleInfo.isCoolDown(skillId))
                {
                    Pet pet = player.getPet();
                    Pet nonPet = getNonPet();
                    bool flag = false;
                    int skillindex = -1;
                    for (int i = 0; i < pet.skill.Length; i++)
                    {
                        int[] skillInfo = pet.skill[i];
                        if (skillInfo[0] == skillId)
                        {
                            flag = true;
                            skillindex = i;
                            break;
                        }
                    }
                    if (flag)
                    {
                        PetBattleInfo nonPetBattleInfo = getNonUserPetBattleInfo();
                        PetSkill petSkill = GopetManager.PETSKILL_HASH_MAP.get(pet.skill[skillindex][0]);
                        PetSkillLv petSkillLv = petSkill.skillLv.get(pet.skill[skillindex][1]);
                        if (pet.mp - petSkillLv.mpLost >= 0)
                        {
                            int mpdelta = 0;
                            pet.mp -= petSkillLv.mpLost;
                            petBattleInfo.addSkillCoolDown(skillId, GopetManager.MAX_SKILL_COOLDOWN);
                            JArrayList<TurnEffect> turnEffects = new();
                            bool isMiss = randMiss(nonPetBattleInfo);
                            applySkill(petSkillLv, petBattleInfo, nonPetBattleInfo);
                            PetDamgeInfo damageInfo = makeDamage(petBattleInfo, nonPetBattleInfo, petSkillLv);
                            if (dotmana(petSkillLv))
                            {
                                if (isPetAttackMob())
                                {
                                    mpdelta = Utilities.round(Utilities.GetValueFromPercent(mob.mp, ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA) / 100f) + Utilities.GetValueFromPercent(damageInfo.getDamge(), ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA_BY_ATK) / 100f));
                                    if (mob.mp - mpdelta > 0)
                                    {
                                        mob.mp -= mpdelta;
                                    }
                                    else
                                    {
                                        mpdelta = mob.mp;
                                        mob.mp = 0;
                                    }
                                }
                                else
                                {
                                    mpdelta = Utilities.round(Utilities.GetValueFromPercent(mob.mp, ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA) / 100f) + Utilities.GetValueFromPercent(damageInfo.getDamge(), ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA_BY_ATK) / 100f));
                                    if (nonPet.mp - mpdelta > 0)
                                    {
                                        nonPet.mp -= mpdelta;
                                    }
                                    else
                                    {
                                        mpdelta = nonPet.mp;
                                        nonPet.mp = 0;
                                    }
                                }
                            }
                            if (!(isMiss || damageInfo.isSkillMiss()))
                            {
                                if (isPetAttackMob())
                                {
                                    mob.addHp(damageInfo.getDamge());
                                    mob.addHp(damageInfo.getTrueDamge());
                                }
                                else
                                {
                                    nonPet.subHp(damageInfo.getDamge());
                                    nonPet.subHp(damageInfo.getTrueDamge());
                                }
                            }
                            else
                            {
                                damageInfo.setDamge(0);
                            }
                            TurnEffect turnEffect = new TurnEffect(TurnEffect.NONE, petSkill.isSkillBuff() ? getUserTurnId() : getFocus(), skillId, -(damageInfo.getDamge() + damageInfo.getTrueDamge()), -mpdelta);
                            turnEffects.add(turnEffect);
                            if (damageInfo.isSkillMiss() || isMiss)
                            {
                                turnEffects.add(new TurnEffect(TurnEffect.SKILL_MISS, getFocus(), TurnEffect.SKILL_MISS, 0, 0));
                            }
                            if (damageInfo.getHpRecovery() > 0 && !damageInfo.isSkillMiss() && !isMiss)
                            {
                                pet.addHp(damageInfo.getHpRecovery());
                                player.controller.sendMyPetInfo();
                                turnEffects.add(new TurnEffect(TurnEffect.NONE, player.user.user_id, -1, damageInfo.getHpRecovery(), 0));
                            }
                            sendPetAttack(turnEffects, TurnEffect.createWait(-petSkillLv.mpLost, getUserTurnId()));
                            nextTurn();
                            if (hasWinner())
                            {
                                win();
                            }/*
                            if (petAttackMob)
                            {
                                Thread.Sleep(GopetManager.DELAY_TURN_PET_BATTLE);
                            }*/
                        }
                        else
                        {
                            player.redDialog("Thú cưng của bạn không đủ thể lực");
                        }
                    }
                    else
                    {
                        player.redDialog("Không có kỹ năng này");
                    }
                }
                else
                {
                    player.redDialog(Utilities.Format("Chưa hồi kỹ năng xong.\n Còn %s hiệp", petBattleInfo.getTurnCoolDown(skillId)));
                }
            }
            else
            {
                player.redDialog("Không phải lượt của bạn");
            }
        }

        private bool randMiss(PetBattleInfo nonPetBattleInfo)
        {
            ItemInfo[] itemInfos = nonPetBattleInfo.getBuff();
            return ItemInfo.getValueById(itemInfos, ItemInfo.Type.MISS_IN_3_TURN) > 0 && ActiveObject.AccuracyPercent - ItemInfo.getValueById(itemInfos, ItemInfo.Type.MISS_IN_3_TURN) / 100f - PassiveObject.SkipPercent > Utilities.NextFloatPer();
        }

        private bool dotmana(PetSkillLv petSkillLv)
        {
            return ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA) > 0 ||
              ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA_BY_ATK) > 0;
        }

        private PetDamgeInfo makeDamage(PetBattleInfo petBattleInfo, PetBattleInfo nonPetBattleInfo, PetSkillLv petSkillLv)
        {
            PetDamgeInfo damgeInfo = new PetDamgeInfo();
            Pet nonPet = getNonPet();
            int trueDamge = 0;
            Pet myPet = getPet();
            int sum = isPetAttackMob() && nonPet == activePet ? mob.getAtk() : myPet.getAtk();
            bool isPassiveSKill = petSkillLv != null;
            if (petSkillLv != null)
            {
                foreach (PetSkillInfo petSkillInfo in petSkillLv.skillInfo)
                {
                    switch (petSkillInfo.id)
                    {
                        case ItemInfo.Type.SKILL_BUFF_DAMGE:
                            sum = Utilities.round(Utilities.GetValueFromPercent(petSkillInfo.getPercent(), sum));
                            isPassiveSKill = false;
                            break;
                        case ItemInfo.Type.SKILL_SKIP_DEF:
                            sum += (int)Utilities.GetValueFromPercent(petSkillInfo.getPercent(), sum);
                            damgeInfo.setSkipDef(true);
                            isPassiveSKill = false;
                            break;
                        case ItemInfo.Type.TRUE_DAMGE:
                            trueDamge += petSkillInfo.value;
                            break;
                        case ItemInfo.Type.SKILL_MISS:
                            bool isMiss = petSkillInfo.getPercent() > Utilities.NextFloatPer();
                            damgeInfo.setSkillMiss(isMiss);
                            break;
                    }
                    if (damgeInfo.isSkillMiss())
                    {
                        break;
                    }
                }
                damgeInfo.setHpRecovery((int)(damgeInfo.getHpRecovery() + Utilities.GetValueFromPercent(sum, ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.RECOVERY_HP) / 100f)));
            }



            foreach (ItemInfo itemInfo in petBattleInfo.getBuff())
            {
                if (damgeInfo.isSkillMiss())
                {
                    break;
                }
                switch (itemInfo.id)
                {
                    case ItemInfo.Type.BUFF_DAMGE:
                        sum += (int)Utilities.GetValueFromPercent(itemInfo.getPercent(), sum);
                        break;
                    case ItemInfo.Type.BUFF_STR:
                        sum += itemInfo.value;
                        break;
                }
            }

            if (nonPet != null)
            {
                sum -= nonPet.getDef();
                sum -= ItemInfo.getValueById(getNonUserPetBattleInfo().getBuff(), ItemInfo.Type.DEF);
                if (ItemInfo.getValueById(getNonUserPetBattleInfo().getBuff(), ItemInfo.Type.PHANDOAN_2_TURN) > 0)
                {
                    float damagePer = ItemInfo.getValueById(getNonUserPetBattleInfo().getBuff(), ItemInfo.Type.PHANDOAN_2_TURN) / 100f;
                    int valueDg = Utilities.round(Utilities.GetValueFromPercent(sum, damagePer));
                    sum -= valueDg;
                    getNonUserPetBattleInfo().addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.DAMAGE_PHANDOAN, valueDg) }, 2));
                }
            }
            else
            {
                sum -= mob.getDef();
            }

            if (!isPassiveSKill && sum > 0)
            {
                damgeInfo.setDamge(sum);
            }
            damgeInfo.setTrueDamge(trueDamge);
            return damgeInfo;
        }

        private void mobUseNormalAttack()
        {
            bool isStun = ItemInfo.getValueById(getUserPetBattleInfo().getBuff(), ItemInfo.Type.STUN) > 0 || Utilities.NextFloatPer() < ItemInfo.getValueById(getUserPetBattleInfo().getBuff(), ItemInfo.Type.PER_STUN_1_TURN) / 100f;
            if (!isStun)
            {
                JArrayList<TurnEffect> turnEffects = new();
                bool isMiss = randMiss(getNonUserPetBattleInfo());
                int sum = mob.getAtk();

                if (!isMiss)
                {
                    bool crit = mob.IsCrit;
                    if (crit)
                    {
                        sum *= 2;
                    }

                    foreach (ItemInfo itemInfo in passiveBattleInfo.getBuff())
                    {
                        switch (itemInfo.id)
                        {
                            case ItemInfo.Type.BUFF_DAMGE:
                                sum += (int)Utilities.GetValueFromPercent(itemInfo.getPercent(), sum);
                                break;

                        }
                    }

                    if (!(mob is Boss))
                    {
                        if (ItemInfo.getValueById(activeBattleInfo.getBuff(), ItemInfo.Type.PHANDOAN_2_TURN) > 0)
                        {
                            float damagePer = ItemInfo.getValueById(activeBattleInfo.getBuff(), ItemInfo.Type.PHANDOAN_2_TURN) / 100f;
                            int valueDg = Utilities.round(Utilities.GetValueFromPercent(sum, damagePer));
                            sum -= valueDg;
                            passiveBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.DAMAGE_PHANDOAN, valueDg) }, 2));
                        }
                    }
                    Pet pet = getNonPet();
                    int buffDef = ItemInfo.getValueById(getNonUserPetBattleInfo().getBuff(), ItemInfo.Type.DEF);
                    bool flag = true;
                    if ((mob is Boss b))
                    {
                        if (b.Template.typeBoss == 0)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        if (sum - (pet.getDef() + buffDef) < 0)
                        {
                            sum = 1;
                        }
                        else
                        {
                            sum -= pet.getDef() + buffDef;
                        }
                    }
                    if (crit)
                    {
                        turnEffects.add(new TurnEffect(TurnEffect.SKILL_CRIT, getFocus(), TurnEffect.SKILL_CRIT, -sum, 0));
                    }
                    else
                    {
                        turnEffects.add(new TurnEffect(TurnEffect.SKILL_NORMAL, getFocus(), TurnEffect.SKILL_NORMAL, -sum, 0));
                    }
                    activePet.hp -= sum;
                }
                else
                {
                    turnEffects.add(new TurnEffect(TurnEffect.SKILL_MISS, getFocus(), TurnEffect.SKILL_MISS, 0, 0));
                }
                sendPetAttack(turnEffects, TurnEffect.createNormalAttack(activePet.mp, 0, getUserTurnId()));
            }
            nextTurn();
            if (hasWinner())
            {
                win();
            }
        }

        private void mobUseSkill(PetSkill skill, PetSkillLv petSkillLv)
        {
            bool isStun = ItemInfo.getValueById(activeBattleInfo.getBuff(), ItemInfo.Type.STUN) > 0 || Utilities.NextFloatPer() < ItemInfo.getValueById(activeBattleInfo.getBuff(), ItemInfo.Type.PER_STUN_1_TURN) / 100f;
            if (!isStun)
            {
                PetBattleInfo nonPetBattleInfo = passiveBattleInfo;
                PetBattleInfo petBattleInfo = activeBattleInfo;
                if (mob.mp - petSkillLv.mpLost >= 0)
                {
                    int mpdelta = 0;
                    mob.mp -= petSkillLv.mpLost;
                    petBattleInfo.addSkillCoolDown(skill.skillID, GopetManager.MAX_SKILL_COOLDOWN);
                    JArrayList<TurnEffect> turnEffects = new();
                    bool isMiss = randMiss(nonPetBattleInfo);
                    applySkill(petSkillLv, petBattleInfo, nonPetBattleInfo);
                    PetDamgeInfo damageInfo = makeDamage(petBattleInfo, nonPetBattleInfo, petSkillLv);
                    if (dotmana(petSkillLv))
                    {
                        mpdelta = Utilities.round(Utilities.GetValueFromPercent(mob.mp, ItemInfo.getValueById(petSkillLv.skillInfo, ItemInfo.Type.DOT_MANA) / 100f));
                        if (activePet.mp - mpdelta > 0)
                        {
                            activePet.mp -= mpdelta;
                        }
                        else
                        {
                            mpdelta = activePet.mp;
                            activePet.mp = 0;
                        }
                    }
                    if (!(isMiss || damageInfo.isSkillMiss()))
                    {
                        activePet.subHp(damageInfo.getDamge());
                        activePet.subHp(damageInfo.getTrueDamge());
                    }
                    else
                    {
                        damageInfo.setDamge(0);
                    }
                    TurnEffect turnEffect = new TurnEffect(TurnEffect.NONE, skill.isSkillBuff() ? getUserTurnId() : getFocus(), skill.skillID, -(damageInfo.getDamge() + damageInfo.getTrueDamge()), -mpdelta);
                    turnEffects.add(turnEffect);
                    if (damageInfo.isSkillMiss() || isMiss)
                    {
                        turnEffects.add(new TurnEffect(TurnEffect.SKILL_MISS, getFocus(), TurnEffect.SKILL_MISS, 0, 0));
                    }
                    if (damageInfo.getHpRecovery() > 0 && !damageInfo.isSkillMiss() && !isMiss)
                    {
                        mob.addHp(damageInfo.getHpRecovery());
                        activePlayer.controller.sendMyPetInfo();
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, mob.getMobId(), -1, damageInfo.getHpRecovery(), 0));
                    }
                    sendPetAttack(turnEffects, TurnEffect.createWait(-petSkillLv.mpLost, getUserTurnId()));
                    nextTurn();
                    if (hasWinner())
                    {
                        win();
                    }
                    return;
                }
                nextTurn();
                if (hasWinner())
                {
                    win();
                }
            }
            else
            {
                nextTurn();
                if (hasWinner())
                {
                    win();
                }
            }
        }

        private void mobAttack()
        {
            mobUseNormalAttack();
            //        if (this.mob.getMobLvInfo().getLvl() > 3) {
            //            bool isUseSkill = Utilities.NextFloatPer() <= 200f;
            //            if (isUseSkill) {
            //                ArrayList<PetSkill> listSkill = GopetManager.NCLASS_PETSKILL_HASH_MAP.get(this.mob.getPetTemplate().getNclass());
            //                if (listSkill != null) {
            ////                    System.out.println("data.battle.PetBattle.mobAttack() list skill not null");
            //                    if (!listSkill.isEmpty()) {
            ////                        System.out.println("data.battle.PetBattle.mobAttack() list skill not empty");
            //                        PetSkill petSkill = listSkill.get(Utilities.nextInt(listSkill.Count));
            //                        int skillLv = Utilities.nextInt(0, Math.min(7, this.mob.getMobLvInfo().getLvl() / 7));
            //                        PetSkillLv petSkillLv = petSkill.skillLv.get(skillLv);
            ////                        System.out.println("data.battle.PetBattle.mobAttack() mp mob " + this.mob.mp);
            //                        if ((passiveBattleInfo.isCoolDown(petSkill.skillID) || this.mob.mp < petSkillLv.mpLost)) {
            //                            mobUseNormalAttack();
            //                        } else {
            //                            mobUseSkill(petSkill, petSkillLv);
            //                        }
            //                    } else {
            //                        mobUseNormalAttack();
            //                    }
            //                } else {
            //                    mobUseNormalAttack();
            //                }
            //            } else {
            //                mobUseNormalAttack();
            //            }
            //        } else {
            //            mobUseNormalAttack();
            //        }
        }

        private void applySkill(PetSkillLv petSkillLv, PetBattleInfo petBattleInfo, PetBattleInfo nonBattleInfo)
        {
            foreach (ItemInfo i in petSkillLv.skillInfo)
            {
                switch (i.id)
                {
                    case ItemInfo.Type.MISS_IN_3_TURN:
                        petBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 3));
                        break;
                    case ItemInfo.Type.POWER_DOWN_4_TURN:
                        nonBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.BUFF_DAMGE, -i.value) }, 4));
                        break;
                    case ItemInfo.Type.POWER_DOWN_3_TURN:
                        nonBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.BUFF_DAMGE, -i.value) }, 3));
                        break;
                    case ItemInfo.Type.POWER_DOWN_1_TURN:
                        nonBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.BUFF_DAMGE, -i.value) }, 1));
                        break;
                    case ItemInfo.Type.DAMGE_TOXIC_IN_3_TURN_PER:
                        nonBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 3));
                        break;
                    case ItemInfo.Type.BUFF_STR:
                        petBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 2));
                        break;
                    case ItemInfo.Type.DEF:
                        petBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 1));
                        break;
                    case ItemInfo.Type.SELECT_DEF_IN_3_TURN:
                        if (!petAttackMob)
                        {
                            Pet nonPet = getNonPet();
                            petBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.DEF, (int)Utilities.GetValueFromPercent(nonPet.getDef(), i.getPercent())) }, 3));
                            nonBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.DEF, -(int)Utilities.GetValueFromPercent(nonPet.getDef(), i.getPercent())) }, 3));
                        }
                        break;
                    case ItemInfo.Type.PER_DEF_BUFF_3_TURN:
                        {
                            petBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.DEF, (int)Utilities.GetValueFromPercent(this.ActiveObject.getDef(), i.getPercent())) }, 3));
                        }
                        break;
                    case ItemInfo.Type.STUN:
                        nonBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 2));
                        break;
                    case ItemInfo.Type.PER_STUN_1_TURN:
                        nonBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 2));
                        break;
                    case ItemInfo.Type.BUFF_ATK_3_TURN:
                        petBattleInfo.addBuff(new Buff(new ItemInfo[] { new ItemInfo(ItemInfo.Type.BUFF_DAMGE, i.value) }, 4));
                        break;
                    case ItemInfo.Type.PHANDOAN_2_TURN:
                        petBattleInfo.addBuff(new Buff(new ItemInfo[] { i }, 1));
                        break;
                }
            }
        }

        private void updateDamagePhanDoan()
        {
            Pet pet = getPet();
            Pet nonPet = getNonPet();
            JArrayList<TurnEffect> turnEffects = new();
            int damagePhandoan = ItemInfo.getValueById(getNonUserPetBattleInfo().getBuff(), ItemInfo.Type.DAMAGE_PHANDOAN);
            if (damagePhandoan > 0)
            {
                if (petAttackMob)
                {
                    if (pet != null)
                    {
                        activePet.subHp(damagePhandoan);
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, mob.getMobId(), PetSkill.GetTPhanDonSkill(activePet), -damagePhandoan, 0));
                    }
                    else
                    {
                        mob.addHp(damagePhandoan);
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, activePlayer.playerData.user_id, PetSkill.GetTPhanDonSkill(mob), -damagePhandoan, 0));
                    }
                }
                else
                {
                    if (pet != null)
                    {
                        getNonPet().subHp(damagePhandoan);
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, getFocus(), PetSkill.GetTPhanDonSkill(getNonPet()), -damagePhandoan, 0));
                    }
                }
                sendPetAttack(turnEffects, new TurnEffect(TurnEffect.NONE, -1, 0, 0, 0));
            }
        }

        private void updateDamageToxic()
        {
            Pet pet = getPet();
            Pet nonPet = getNonPet();
            JArrayList<TurnEffect> turnEffects = new();
            float damagePer = ItemInfo.getValueById(getNonUserPetBattleInfo().getBuff(), ItemInfo.Type.DAMGE_TOXIC_IN_3_TURN_PER) / 100f;

            if (damagePer > 0)
            {
                if (petAttackMob)
                {
                    if (pet != null)
                    {
                        int damage = (int)Utilities.GetValueFromPercent(PassiveObject.maxHp, damagePer);
                        mob.subHp(damage);
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, mob.getMobId(), PetSkill.GetToxicSkill(activePet), -damage, 0));
                    }
                    else
                    {
                        int damage = (int)Utilities.GetValueFromPercent(ActiveObject.maxHp, damagePer);
                        activePet.addHp(damage);
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, activePlayer.playerData.user_id, PetSkill.GetToxicSkill(mob), -damage, 0));
                    }
                }
                else
                {
                    if (pet != null)
                    {
                        int damage = (int)Utilities.GetValueFromPercent(ActiveObject.maxHp, damagePer);
                        getPet().subHp(damage);
                        turnEffects.add(new TurnEffect(TurnEffect.NONE, getFocus(), PetSkill.GetToxicSkill(getNonPet()), -damage, 0));
                    }
                }
                sendPetAttack(turnEffects, new TurnEffect(TurnEffect.NONE, -1, 0, 0, 0));
            }
        }

        public void useItem(Player player, Item itemSelect)
        {
            if (checkWhoseTurn(player))
            {
                if (GameController.checkCount(itemSelect, 1))
                {
                    Pet p = getPet();
                    int oldHp = p.hp;
                    int oldMp = p.mp;
                    for (int i = 0; i < itemSelect.getTemp().getOption().Length; i++)
                    {
                        int j = itemSelect.getTemp().getOption()[i];
                        int opValue = itemSelect.getTemp().getOptionValue()[i];
                        switch (j)
                        {
                            case GopetManager.ITEM_OP_HP:
                                p.addHp(opValue);
                                break;
                            case GopetManager.ITEM_OP_MP:
                                p.addMp(opValue);
                                break;
                        }
                    }
                    sendPetAttack(new(), TurnEffect.createWait(p.hp - oldHp, p.mp - oldMp, getUserTurnId()));
                    player.controller.subCountItem(itemSelect, 1, GopetManager.NORMAL_INVENTORY);
                    if (!petAttackMob)
                    {
                        string str = Utilities.Format("Người chơi %s đã sử dụng %s", player.playerData.name, itemSelect.getTemp().getName());
                        Player playerNeedSend = player == activePlayer ? passivePlayer : activePlayer;
                        playerNeedSend.Popup(str);
                    }
                    player.controller.sendMyPetInfo();
                }
            }
            else
            {
                player.redDialog("Không phải lượt của bạn");
            }
        }

        public static int genGemWhenMobDie(Player player, Pet p, Mob mob)
        {
            int begin = mob.getMobLvInfo().lvl * 10;
            bool minus = Math.Abs(p.lvl - mob.getMobLvInfo().lvl) >= 5;
            if (minus)
            {
                //            System.out.println("data.battle.PetBattle.genGemWhenMobDie()" + begin);
                begin = (int)Utilities.GetValueFromPercent(begin, Math.Max(0, 100 - Math.Abs(p.lvl - mob.getMobLvInfo().lvl) * 3));
                //            System.out.println("data.battle.PetBattle.genGemWhenMobDie()" + begin);
            }
            begin = Math.Max(0, (int)Utilities.GetValueFromPercent(begin, 100 - Utilities.nextInt(-10, 10)));
            //        System.out.println("data.battle.PetBattle.genGemWhenMobDie()" + begin);
            return Utilities.round(Utilities.GetValueFromPercent(begin, FieldManager.PERCENT_GEM));
        }

        public static int genExpWhenMobDie(Player player, Pet p, Mob mob, int exp)
        {
            int begin = exp;
            int deltaLvl = p.lvl - mob.getMobLvInfo().lvl;
            if (deltaLvl >= 0)
            {
                if (deltaLvl <= 15)
                {
                    begin = (int)Math.Max(0, exp + Utilities.GetValueFromPercent(exp, deltaLvl * 2));
                }
                else
                {
                    begin = 0;
                }
            }
            else
            {
                if (deltaLvl >= -15)
                {
                    begin = (int)Math.Max(0, exp - Utilities.GetValueFromPercent(exp, deltaLvl * 8));
                }
                else
                {
                    begin = 0;
                }
            }
            begin = Math.Max(0, (int)Utilities.GetValueFromPercent(begin, 100 - Utilities.nextInt(-10, 10)));
            return Utilities.round(Utilities.GetValueFromPercent(begin, FieldManager.PERCENT_EXP));
        }
    }
}