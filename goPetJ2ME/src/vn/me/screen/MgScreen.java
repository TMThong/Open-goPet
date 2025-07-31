package vn.me.screen;

import defpackage.PetUI;
import defpackage.Class118;
import defpackage.MEService;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.PetGameModel;
import defpackage.PetInfo;
import vn.me.ui.common.LAF;
import vn.me.ui.common.ResourceManager;
import vn.me.core.BaseCanvas;
import vn.me.ui.WidgetGroup;
import vn.me.ui.common.T;


public final class MgScreen extends Screen {
    private boolean Field733;
    private PetUI Field734;
    private PetGameModel Field735;
    private WidgetGroup Field736;
    private PetInfo pet;

    public MgScreen(PetGameModel class43, boolean z) {
        super(true);
        this.Field1184 = true;
        this.Field1185 = GameResourceManager.Method116();
        this.Field735 = class43;
        this.Field733 = z;
        this.Field734 = new PetUI(this.Field735);
        this.Field734.setPosition(10 + ((64 - this.Field734.w) / 2), 37);
        this.container.addWidget(this.Field734);
        this.Field734.requestFocus();
        this.Field736 = new WidgetGroup(0, 132, BaseCanvas.w, ((BaseCanvas.h - LAF.Field1293) - 5) - 132);
        this.Field736.isScrollableY = true;
        this.Field736.isLoop = true;
        this.Field736.spacing = 0;
        this.container.addWidget(this.Field736);
        this.cmdLeft = GameController.Field467;
    }

    public final void paintBackground() {
        BaseCanvas.g.setColor(0);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
        PetGameModel.Field283.Method448(this.pet.tiemnang);
        LAF.Method413(10, 20, BaseCanvas.w - 20, 22);
        PetGameModel.Method461(this.pet.petName, this.pet.nClass, BaseCanvas.w >> 1, 22);
        LAF.Method413(10, 42, 64, 80);
        LAF.Method413(74, 42, (BaseCanvas.w - 20) - 64, 80);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(str) ").append(String.valueOf(this.pet.str)).toString(), 85, 46, 0);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(agi) ").append(String.valueOf(this.pet.agi)).toString(), 85, 64, 0);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(int) ").append(String.valueOf(this.pet._int)).toString(), 85, 82, 0);
        int i = 85 + (((BaseCanvas.w - 85) - 20) >> 1);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(atk) ").append(String.valueOf(this.pet.atk)).toString(), i, 46, 0);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(def) ").append(String.valueOf(this.pet.def)).toString(), i, 64, 0);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(hp) ").append(String.valueOf(this.pet.hp)).append("/").append(String.valueOf(this.pet.maxHp)).toString(), i, 82, 0);
        ResourceManager.boldFont.drawString(BaseCanvas.g, new StringBuffer("(mp) ").append(String.valueOf(this.pet.mp)).append("/").append(String.valueOf(this.pet.maxMp)).toString(), i, 100, 0);
        LAF.Method413(10, 122, BaseCanvas.w - 20, ((BaseCanvas.h - 122) - 5) - LAF.Field1293);
    }

    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 0:
                GameController.startOKDlg(((Class118) this.Field736.getWidgetAt(((Integer) Command.objPerfomed).intValue())).Field767);
                return;
            case 1:
                GameController.Method45(T.gL(T.SELECT_MATERIAL), new Command(2, T.gL(T.OK), Command.objPerfomed, this), GameController.Field464);
                return;
            case 2:
                MEService.Method29(this.pet.skillId[((Integer) Command.objPerfomed).intValue()], 9);
                GameController.waitDialog();
                return;
            default:
                super.actionPerformed(obj);
                return;
        }
    }

    public final void setInfo(PetInfo petInfo) {
        this.pet = petInfo;
        this.Field734.setPetInfo(petInfo);
        this.Field736.removeAll();
        int i = 0;
        for (int i2 = 0; i2 < this.pet.skillName.length; i2++) {
            Class118 class118 = new Class118(ResourceManager.boldFont, this.pet.skillId[i2], this.pet.skillName[i2], this.pet.skillDesc[i2], this.pet.mpLost[i2]);
            i = i2 * 20;
            class118.setPosition(17, i);
            this.Field736.addWidget(class118);
            class118.cmdCenter = new Command(0, T.gL(T.VIEW), new Integer(i2), this);
            if (this.Field733) {
                class118.cmdLeft = new Command(1, T.gL(T.UPGRADE_STR), new Integer(i2), this);
            }
        }
        int i3 = i + 20;
        for (int i4 = 0; i4 < this.pet.Field395.length; i4++) {
            Class118 class1182 = new Class118(ResourceManager.boldFont, 0, this.pet.Field395[i4], this.pet.Field395[i4], 0);
            class1182.setPosition(17, i3 + (i4 * 20));
            this.Field736.addWidget(class1182);
        }
        requestFocus(this.Field736.getWidgetAt(0));
        this.Field736.setPreferredSize(0, 250);
        this.Field736.setFocusWithParents(true);
    }

    public final void Method139(int i, int i2, String str, String str2, int i3) {
        for (int i4 = 0; i4 < 3; i4++) {
            Class118 class118 = (Class118) this.Field736.getWidgetAt(i4);
            if (class118.Field766 == i) {
                class118.Field766 = i2;
                class118.Method158(str);
                class118.Field767 = str2;
                class118.Field768 = i3;
            }
            if (this.pet.skillId[i4] == i) {
                this.pet.skillId[i4] = i2;
                this.pet.skillDesc[i4] = str2;
                this.pet.skillName[i4] = str;
                this.pet.mpLost[i4] = i3;
            }
        }
    }
}
