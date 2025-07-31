package vn.me.network;

import java.io.IOException;
import java.util.Vector;

/* renamed from: Class163  reason: default package */
 /* loaded from: gopet_repackage.jar:Class163.class */
public final class MsgSender implements Runnable {

    protected MobileClient session;
   protected final Vector sendingMessage = new Vector();

   public MsgSender(MobileClient session) {
      this.session = session;
   }

   public void addMessage(Message message) {
      synchronized(this.sendingMessage) {
         this.sendingMessage.addElement(message);
         this.sendingMessage.notifyAll();
      }
   }

   public void run() {
      while(true) {
         try {
            if (this.session.isConnected) {
               synchronized(this.sendingMessage) {
                  while(this.sendingMessage.size() > 0) {
                     if (this.session.isConnected) {
                        Message m = (Message)this.sendingMessage.elementAt(0);
                        this.sendingMessage.removeElementAt(0);
                        this.doSendMessage(m);
                     }
                  }

                  try {
                     this.sendingMessage.wait();
                  } catch (InterruptedException var4) {
                  }
                  continue;
               }
            }
         } catch (Exception var6) {
         }

         return;
      }
   }

   public void doSendMessage(Message m) throws IOException {
       
      byte[] data = m.getBuffer();
      MobileClient var10000;
      if (data != null) {
         if (m.isEncrypted) {
            data = this.session.tea.encrypt(data);
         }

         this.session.dos.writeInt(data.length + 1);
         this.session.dos.writeByte(m.isEncrypted ? 1 : 0);
         this.session.dos.write(data);
         var10000 = this.session;
         var10000.sendByteCount += data.length;
      } else {
         this.session.dos.writeInt(0);
      }

      var10000 = this.session;
      var10000.sendByteCount += 4;
      this.session.dos.flush();
   }

   public void stop() {
      synchronized(this.sendingMessage) {
         this.sendingMessage.removeAllElements();
         this.sendingMessage.notifyAll();
      }
   }

   protected void writeKey(long key) throws IOException {
      byte[] var10000 = new byte[9];
      this.session.getClass();
      var10000[0] = 9;
      var10000[1] = (byte)((int)(key >>> 56));
      var10000[2] = (byte)((int)(key >>> 48));
      var10000[3] = (byte)((int)(key >>> 40));
      var10000[4] = (byte)((int)(key >>> 32));
      var10000[5] = (byte)((int)(key >>> 24));
      var10000[6] = (byte)((int)(key >>> 16));
      var10000[7] = (byte)((int)(key >>> 8));
      var10000[8] = (byte)((int)key);
      byte[] buf = var10000;
      this.session.dos.write(buf);
      this.session.dos.flush();
   }
}
