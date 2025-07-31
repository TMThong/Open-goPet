package defpackage;

import vn.me.ui.interfaces.IActionListener;

/* JADX INFO: Access modifiers changed from: package-private */
 /* renamed from: Class80  reason: default package */
 /* loaded from: gopet_repackage.jar:Class80.class */
public final class Class80 implements Runnable {

    /* JADX INFO: Access modifiers changed from: package-private */
    public Class80(GameController class74) {
    }

    ///////@Override // java.lang.Runnable
    public final void run() {
        GlobalService.Method244(GlobalService.Field1010, new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                GameController.Method55("INTRO", ActorFactory.gL(222), obj == null ? ActorFactory.gL(223) : (String) obj);
            }
        }, new IActionListener() {
            ///////@Override
            public void actionPerformed(Object obj) {
                GameController.Method55("INTRO", ActorFactory.gL(222), ActorFactory.gL(223));
            }
        });
    }
}
