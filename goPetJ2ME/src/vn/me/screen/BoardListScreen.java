package vn.me.screen;

import defpackage.ActorFactory;
import vn.me.core.BaseCanvas;
import defpackage.Class141;
import vn.me.ui.InputDialog;
import defpackage.Class66;
import defpackage.Command;
import defpackage.GameController;
import defpackage.GameResourceManager;
import defpackage.GlobalService;
import vn.me.ui.common.LAF;
import defpackage.SceneManage;
import defpackage.mMap;
import vn.me.ui.WidgetGroup;
import vn.me.screen.Screen;
import java.util.Vector;

/* renamed from: Class64  reason: default package */
 /* loaded from: gopet_repackage.jar:Class64.class */
public class BoardListScreen extends Screen {

    public int Field418;
    public WidgetGroup Field419;
    public int Field420;
    public int Field421;

    public BoardListScreen(String str) {
        super(true);
        this.title = str;
        this.screenId = "BOARDLIST";
        this.cmdLeft = GameController.Field467;
        this.cmdRight = new Command(1000, ActorFactory.gL(189), this);
    }

    ///////@Override // defpackage.Screen
    public final void paintBackground() {
        BaseCanvas.g.setColor(2425856);
        BaseCanvas.g.fillRect(0, 0, BaseCanvas.w, BaseCanvas.h);
    }

    public final void Method19(Vector vector) {
        this.container.removeWidget(this.Field419);
        int size = vector.size();
        this.Field420 = Math.max(GameResourceManager.Field600.getHeight(), GameResourceManager.Field600.getWidth()) + (LAF.LOT_PADDING << 1);
        this.Field419 = new Class66(this, 2, vector);
        this.Field419.setMetrics(0, LAF.Field1292, BaseCanvas.w, BaseCanvas.h - (LAF.Field1292 << 1));
        this.Field419.isScrollableY = true;
        this.Field419.isLoop = true;
        this.Field419.padding = LAF.LOT_PADDING;
        this.Field419.columns = (this.Field419.w - (2 * this.Field419.padding)) / this.Field420;
        this.Field419.spacing = ((this.Field419.w - (2 * this.Field419.padding)) - (this.Field420 * this.Field419.columns)) / (this.Field419.columns + 1);
        int i = size / this.Field419.columns;
        if (vector.size() % this.Field419.columns != 0) {
            i++;
        }
        this.Field419.preferredSize.width = this.Field419.columns * this.Field420;
        this.Field419.preferredSize.height = i * this.Field420;
        this.container.addWidget(this.Field419);
        this.Field419.cmdCenter = new Command(5, ActorFactory.gL(231), vector, this);
        this.Field418 = 0;
        this.Field421 = ((this.Field419.h / this.Field420) + 2) * this.Field419.columns;
        this.Field419.requestFocus();
    }

    ///////@Override // defpackage.Screen, defpackage.Class200
    public final void actionPerformed(Object obj) {
        Command Command = (Command) ((Object[]) obj)[0];
        switch (Command.cmdId) {
            case 5:
                GameController.waitDialog();
                GlobalService.Method245(24, new int[]{GameController.myInfo.mGoMapId, ((Class141) ((Vector) Command.objPerfomed).elementAt(this.Field418)).Field973, 100, mMap.getMapVersion(GameController.myInfo.mGoMapId)});
                return;
            case 1000:
                GameController.Method57(ActorFactory.gL(189), new Command(1001, ActorFactory.gL(337), this), GameController.Field464, 1);
                return;
            case 1001:
                GameController.waitDialog();
                GlobalService.Method245(24, new int[]{GameController.myInfo.mGoMapId, Integer.parseInt(((InputDialog) Screen.currentDialog).Method327(0)), SceneManage.myCharacter.xChar, SceneManage.myCharacter.yChar});
                return;
            default:
                return;
        }
    }

    /* JADX INFO: Access modifiers changed from: package-private */
    public static int Method20(BoardListScreen class64) {
        return class64.Field421;
    }
}
