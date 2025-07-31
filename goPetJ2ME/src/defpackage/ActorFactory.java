package defpackage;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Vector;

import javax.microedition.rms.RecordStore;
import vn.me.core.BaseCanvas;

/* renamed from: Class59  reason: default package */
 /* loaded from: gopet_repackage.jar:Class59.class */
public class ActorFactory {

    public static final int VI = 0;
    public static final int EN = 1;
    public static int langueCode = EN;
    public short[] Field398;
    public short[] Field399;
    public short[] Field400;
    public short[] Field401;
    public int[] Field402;
    public int[] Field403;
    public int[] Field404;
    public int[] Field405;
    public short[] Field406;
    short[] Field407;
    boolean Field408;
    Vector Field409 = new Vector();
    public static String languageRMS = "mLanguage";

    protected ActorFactory(boolean z) {
        this.Field408 = z;
    }

    public static ActorFactory Method485(String str, boolean z, Class62 class62) throws IOException {
        if (class62 == null) {
            System.out.println("defpackage.ActorFactory.Method485()");
            throw new IllegalArgumentException("Image Loader cannot be null");
        }
        ActorFactory class59 = new ActorFactory(false);

        InputStream inputStream = null;
        try {
            if (BaseCanvas.iPlatformSDK == null) {
                inputStream = ActorFactory.class.getResourceAsStream(str);
            } else {
                inputStream = BaseCanvas.iPlatformSDK.getAssetSDK().load(str);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        DataInputStream dataInputStream = new DataInputStream(inputStream);
        try {
            try {
                dataInputStream.readShort();
                dataInputStream.readUTF();
                byte readByte = dataInputStream.readByte();
                short[] sArr = new short[readByte << 1];
                for (int i = 0; i < readByte; i++) {
                    sArr[2 * i] = dataInputStream.readShort();
                    sArr[(2 * i) + 1] = dataInputStream.readShort();
                }
                short readShort = dataInputStream.readShort();
                short[] sArr2 = new short[readShort << 2];
                for (int i2 = 0; i2 < readShort; i2++) {
                    sArr2[4 * i2] = dataInputStream.readShort();
                    sArr2[(4 * i2) + 1] = dataInputStream.readByte();
                    sArr2[(4 * i2) + 2] = dataInputStream.readShort();
                    sArr2[(4 * i2) + 3] = dataInputStream.readShort();
                }
                short[] sArr3 = new short[dataInputStream.readShort()];
                short readShort2 = dataInputStream.readShort();
                short s = 0;
                short[] sArr4 = new short[readShort2 << 1];
                for (int i3 = 0; i3 < readShort2; i3++) {
                    sArr4[2 * i3] = s;
                    short readShort3 = dataInputStream.readShort();
                    for (short s2 = 0; s2 < readShort3; s2++) {
                        short s3 = (short) (s + 1);
                        sArr3[s] = dataInputStream.readShort();
                        short s4 = (short) (s3 + 1);
                        sArr3[s3] = dataInputStream.readShort();
                        short s5 = (short) (s4 + 1);
                        sArr3[s4] = dataInputStream.readShort();
                        s = (short) (s5 + 1);
                        sArr3[s5] = dataInputStream.readByte();
                    }
                    sArr4[(2 * i3) + 1] = (short) (s - 1);
                }
                short readShort4 = dataInputStream.readShort();
                int readByte2 = dataInputStream.readByte();
                short[] sArr5 = new short[readShort4 << 2];
                short s6 = 0;
                short[] sArr6 = new short[readByte2];
                short s7 = 0;
                for (int i4 = 0; i4 < readByte2; i4++) {
                    sArr6[i4] = s7;
                    short readShort5 = dataInputStream.readShort();
                    for (short s8 = 0; s8 < readShort5; s8++) {
                        short s9 = (short) (s6 + 1);
                        sArr5[s6] = dataInputStream.readShort();
                        short s10 = (short) (s9 + 1);
                        sArr5[s9] = dataInputStream.readShort();
                        short s11 = (short) (s10 + 1);
                        sArr5[s10] = dataInputStream.readShort();
                        s6 = (short) (s11 + 1);
                        sArr5[s11] = dataInputStream.readShort();
                    }
                    s7 = (short) (s7 + readShort5);
                    class59.Field409.addElement(class62.Method17(str));
                }
                short readShort6 = dataInputStream.readShort();
                int[] iArr = new int[readShort6 * 5];
                for (int i5 = 0; i5 < readShort6; i5++) {
                    iArr[5 * i5] = dataInputStream.readShort();
                    iArr[(5 * i5) + 1] = dataInputStream.readShort();
                    iArr[(5 * i5) + 2] = dataInputStream.readShort();
                    iArr[(5 * i5) + 3] = dataInputStream.readShort();
                    iArr[(5 * i5) + 4] = dataInputStream.readInt();
                }
                short readShort7 = dataInputStream.readShort();
                int[] iArr2 = new int[readShort7 * 3];
                for (int i6 = 0; i6 < readShort7; i6++) {
                    iArr2[3 * i6] = dataInputStream.readShort();
                    iArr2[(3 * i6) + 1] = dataInputStream.readShort();
                    iArr2[(3 * i6) + 2] = dataInputStream.readInt();
                }
                short readShort8 = dataInputStream.readShort();
                int[] iArr3 = new int[readShort8 * 3];
                for (int i7 = 0; i7 < readShort8; i7++) {
                    iArr3[3 * i7] = dataInputStream.readShort();
                    iArr3[(3 * i7) + 1] = dataInputStream.readShort();
                    iArr3[(3 * i7) + 2] = dataInputStream.readInt();
                }
                short readShort9 = dataInputStream.readShort();
                int[] iArr4 = new int[readShort9 * 5];
                for (int i8 = 0; i8 < readShort9; i8++) {
                    iArr4[5 * i8] = dataInputStream.readShort();
                    iArr4[(5 * i8) + 1] = dataInputStream.readShort();
                    iArr4[(5 * i8) + 2] = dataInputStream.readShort();
                    iArr4[(5 * i8) + 3] = dataInputStream.readShort();
                    iArr4[(5 * i8) + 4] = dataInputStream.readInt();
                }
                short readShort10 = dataInputStream.readShort();
                short[] sArr7 = new short[readShort10 << 1];
                for (int i9 = 0; i9 < readShort10; i9++) {
                    sArr7[2 * i9] = dataInputStream.readShort();

                }
                dataInputStream.close();
                class59.Field398 = sArr;
                class59.Field399 = sArr2;
                class59.Field407 = sArr4;
                class59.Field400 = sArr3;
                class59.Field401 = sArr5;
                class59.Field402 = iArr;
                class59.Field403 = iArr2;
                class59.Field404 = iArr3;
                class59.Field405 = iArr4;
                class59.Field406 = sArr6;
                System.out.println("defpackage.ActorFactory.Method485() z");
                return class59;
            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Throwable th) {
            try {
                dataInputStream.close();
            } catch (IOException ex) {
                ex.printStackTrace();
            }
        }

        return null;
    }

    public static final int ACCEPT_STR = 4;
    public static final int REFUSE_STR = 101;

    public static String gL(int i) {
        switch (langueCode) {
            case VI: {
                switch (i) {
                    case 3:
                        return "Tiện ích";
                    case ACCEPT_STR:
                        return "Chấp nhận";
                    case 5:
                        return "T.Khoản";
                    case 9:
                        return "Thêm bạn";
                    case 18:
                        return "Nhiệm vụ";
                    case 21:
                        return "Quay lại";
                    case 24:
                        return "Ngân hàng";
                    case 25:
                        return "Đậu";
                    case 30:
                        return "Game cần mở trình duyệt web. Hãy thoát game nếu như không thấy hiển thị trang web.";
                    case 40:
                        return "Gọi trợ giúp";
                    case 41:
                        return "Thôi";
                    case 46:
                        return "Không thể gởi tin nhắn đăng ký. Xin kiểm tra tiền và thử khởi động lại game.";
                    case 51:
                        return "Đổi";
                    case 55:
                        return "Nạp";
                    case 58:
                        return "Chat";
                    case 63:
                        return "Xóa dữ liệu";
                    case 64:
                        return "Đóng";
                    case 73:
                        return "Bạn muốn xoá dữ liệu?";
                    case 77:
                        return "Bạn muốn thoát?";
                    case 82:
                        return "Kết nối thất bại. Xin kiểm tra lại GPRS, 3G, Wifi hoặc cập nhật lại máy chủ.";
                    case 83:
                        return "Đang kết nối...";
                    case 87:
                        return "Tiếp tục";
                    case 93:
                        return "ngày";
                    case 94:
                        return "Xóa hết";
                    case 95:
                        return "Xóa";
                    case REFUSE_STR:
                        return "Từ chối";
                    case 103:
                        return "Hủy";
                    case 116:
                        return "Tải";
                    case 123:
                        return "Cảm xúc";
                    case 129:
                        return "English";
                    case 130:
                        return "Nhập tên nhân vật";
                    case 132:
                        return "Vui lòng nhập Nick ID muốn đăng ký vào ô trên.";
                    case 133:
                        return "Bạn phải nhập password đăng ký.";
                    case 134:
                        return "Khu giải trí";
                    case 138:
                        return "Sự kiện";
                    case 139:
                        return "Thoát";
                    case 140:
                        return "Kb, bạn có muốn thoát khỏi ứng dụng không?";
                    case 154:
                        return "Quên mật khẩu";
                    case 159:
                        return "Bạn bè";
                    case 167:
                        return "đã được người khác sử dụng. Xin chọn tên khác.";
                    case 174:
                        return "Bạn phải dùng số điện thoại đăng ký nick để lấy mật khẩu.";
                    case 178:
                        return "Tên muốn lấy mật khẩu không được rỗng!";
                    case 184:
                        return "mGold";
                    case 185:
                        return "Đổi vàng lấy đậu";
                    case 186:
                        return "Đổi vàng lấy thóc";
                    case 189:
                        return "Đến khu số";
                    case 203:
                        return "Bạn có tin nhắn mới.";
                    case 205:
                        return "Hỗ trợ";
                    case 209:
                        return "giờ";
                    case 212:
                        return "Bạn phải có 1 mã số để nạp.";
                    case 213:
                        return "Bạn phải có thẻ cào có 2 mã số để nạp.";
                    case 220:
                        return "Thông tin";
                    case 221:
                        return "vào danh sách bạn bè?";
                    case 222:
                        return "Giới thiệu";
                    case 223:
                        return "Không thể lấy thông tin\nVui lòng xem thông tin tại đia chỉ\nhttps://gopetmoi.com.";
                    case 231:
                        return "Vào";
                    case 247:
                        return "Ngôn ngữ";
                    case 249:
                        return "Quy định";
                    case 265:
                        return "Đang đăng nhập...";
                    case 266:
                        return "Đăng nhập";
                    case 267:
                        return "Đăng xuất";
                    case 275:
                        return "Menu";
                    case 276:
                        return "Tin nhắn";
                    case 278:
                        return "phút";
                    case 284:
                        return "Di chuyển";
                    case 291:
                        return "Bạn cần có ít nhất 1000 đồng trong tài khoản chính để lấy mật khẩu.";
                    case 298:
                        return "Tên";
                    case 299:
                        return "Chuyển cho";
                    case 300:
                        return "Không";
                    case 307:
                        return "Bạn không có tin nhắn mới.";
                    case 322:
                        return "Password bạn vừa gõ không khớp với password phía trên.";
                    case 330:
                        return "Nếu chưa có tên,";
                    case 331:
                        return "Nếu chưa có tên, xin đăng ký";
                    case 337:
                        return "OK";
                    case 344:
                        return "Hoặc soạn tin nhắn ";
                    case 348:
                        return "M.Kh:";
                    case 350:
                        return "Thành công, xin vui lòng chờ hệ thống gởi tin nhắn mật khẩu mới cho bạn";
                    case 352:
                        return "Xin chờ...";
                    case 353:
                        return "\nBản quyền 2011 ME Corp.\nGiấy phép cung cấp MXH số 35/GXN-TTĐT. Cấp ngày 06/05/2011.";
                    case 356:
                        return "Số điện thoại này có phải là số dùng để đăng ký nick không?";
                    case 362:
                        return "xin đăng ký.";
                    case 363:
                        return "Xin chờ";
                    case 364:
                        return "Trước";
                    case 371:
                        return "Tên chương trình";
                    case 375:
                        return "Mật khẩu";
                    case 376:
                        return "Số lượng";
                    case 384:
                        return "Khu vực";
                    case 385:
                        return "Đăng ký";
                    case 386:
                        return "Đang đăng ký...";
                    case 387:
                        return "Đã gửi thông tin đăng ký thành công. Xin thoát game và chờ giây lát.";
                    case 390:
                        return "Xóa bộ nhớ tạm thành công.";
                    case 391:
                        return "Thông báo";
                    case 393:
                        return "Tên không được trống.";
                    case 394:
                        return "Mật khẩu không được rỗng.";
                    case 397:
                        return "Nhập lại";
                    case 400:
                        return "Thóc";
                    case 419:
                        return "Chọn";
                    case 422:
                        return "Chọn nhân vật";
                    case 428:
                        return "Chọn máy chủ";
                    case 435:
                        return "Đang gửi tin nhắn...";
                    case 439:
                        return "Không thể gởi tin nhắn nạp tiền. Xin kiểm tra tiền và thử khởi động lại game.";
                    case 440:
                        return "Đã nạp tiền xong. Xin chờ tin nhắn xác nhận. Lưu ý bạn chỉ nạp được 3 lần trong 5 phút.";
                    case 441:
                        return " Gửi tới ";
                    case 459:
                        return "Cấu hình";
                    case 471:
                        return "Khu mua sắm";
                    case 473:
                        return "Chưa hoàn thành nhập";
                    case 482:
                        return "Gõ lại";
                    case 488:
                        return "Nhớ thông tin";
                    case 491:
                        return "Thành công!";
                    case 495:
                        return "Góp ý";
                    case 496:
                        return "Không hỗ trợ phương thức này.";
                    case 503:
                        return "Tặng bạn bè";
                    case 504:
                        return "Cảm ơn bạn đã góp ý!";
                    case 519:
                        return "Nhập tên cần lấy lại mật khẩu";
                    case 532:
                        return "Tặng mGold";
                    case 548:
                        return "Cập nhật";
                    case 552:
                        return "Kết nối thất bại. Bạn có muốn cập nhật thông tin máy chủ không?";
                    case 553:
                        return "Không thể cập nhật danh sách máy chủ. Mời bạn thử lại sau";
                    case 557:
                        return "Phiên bản";
                    case 558:
                        return "Rung";
                    case 559:
                        return "Tiếng Việt";
                    case 560:
                        return "Xem";
                    case 574:
                        return "Bạn muốn đi đâu?";
                    case 580:
                        return "Có";
                    case 595:
                        return "của bạn";
                    case 596:
                        return "mGold hiện tại của bạn";
                    case 604:
                        return "Bạn đã dùng ";
                    case 611:
                        return "Chế độ rung";
                    case 664:
                        return "Sau";
                    case 666:
                        return "KHU VỰC";
                    case 667:
                        return "Thách đấu";
                    case 668:
                        return "Chuyển map";
                    case 670:
                        return "Trò chơi trong nhà";
                    case 671:
                        return "Thú cưng";
                    default:
                        return String.valueOf(i);
                }

            }
            case EN: {
                switch (i) {
                    case 3:
                        return "Utilities";
                    case ACCEPT_STR:
                        return "Accept";
                    case 5:
                        return "Account";
                    case 9:
                        return "Add friend";
                    case 18:
                        return "Task";
                    case 21:
                        return "Back";
                    case 24:
                        return "Bank";
                    case 25:
                        return "Đậu";
                    case 30:
                        return "The game needs to open a web browser. Quit the game if you don't see the website displayed.";
                    case 40:
                        return "Need helps";
                    case 41:
                        return "Stop";
                    case 46:
                        return "Unable to send registration messages. Please check the money and try to restart the game.";
                    case 51:
                        return "Change";
                    case 55:
                        return "Nạp";
                    case 58:
                        return "Chat";
                    case 63:
                        return "Clear cache";
                    case 64:
                        return "Close";
                    case 73:
                        return "Do you want clear cache?";
                    case 77:
                        return "Do you want exit?";
                    case 82:
                        return "... Connection failed. Please check GPRS, 3G, Wifi or update the server again.";
                    case 83:
                        return "Conecting.....";
                    case 87:
                        return "Continue...";
                    case 93:
                        return "day";
                    case 94:
                        return "Remove all";
                    case 95:
                        return "remove";
                    case REFUSE_STR:
                        return "Refuse";
                    case 103:
                        return "Cancel";
                    case 116:
                        return "Download";
                    case 123:
                        return "Emotion";
                    case 129:
                        return "English";
                    case 130:
                        return "Type your character name";
                    case 132:
                        return "Please enter the Nick ID you wish to register in the box above.";
                    case 133:
                        return "You must enter your registration password.";
                    case 134:
                        return "Recreation area";
                    case 138:
                        return "Events";
                    case 139:
                        return "Exit";
                    case 140:
                        return "Kb, do you want to quit the app?";
                    case 154:
                        return "Forgot password";
                    case 159:
                        return "Friend";
                    case 167:
                        return "have been used by others. Please choose a different name.";
                    case 174:
                        return "You must use the nick registration phone number to get the password.";
                    case 178:
                        return "The name you want to get the password must not be empty!";
                    case 184:
                        return "mGold";
                    case 185:
                        return "Đổi vàng lấy đậu";
                    case 186:
                        return "Đổi vàng lấy thóc";
                    case 189:
                        return "Go to place";
                    case 203:
                        return "You have new message.";
                    case 205:
                        return "Help";
                    case 209:
                        return "hour";
                    case 212:
                        return "You must have 1 code to deposit.";
                    case 213:
                        return "You must have a scratch card with 2 numbers to load.";
                    case 220:
                        return "Information";
                    case 221:
                        return "to your friends list?";
                    case 222:
                        return "Introduce";
                    case 223:
                        return "Unable to obtain informationn\nPlease see the information at the addressn\nhttp://gopetmoi.com.";
                    case 231:
                        return "Enter";
                    case 247:
                        return "Language";
                    case 249:
                        return "Regulation";
                    case 265:
                        return "Logging in...";
                    case 266:
                        return "Log";
                    case 267:
                        return "Logout";
                    case 275:
                        return "Menu";
                    case 276:
                        return "Message";
                    case 278:
                        return "phút";
                    case 284:
                        return "Move";
                    case 291:
                        return "You need to have at least 1000 dong in the main account to get the password.";
                    case 298:
                        return "Name";
                    case 299:
                        return "Transfer to";
                    case 300:
                        return "No";
                    case 307:
                        return "You don't have new messages.";
                    case 322:
                        return "The password you just typed does not match the password above.";
                    case 330:
                        return "If you dont have a name,";
                    case 331:
                        return "If you don't have a name, please register";
                    case 337:
                        return "OK";
                    case 344:
                        return "Or compose a message ";
                    case 348:
                        return "Pass:";
                    case 350:
                        return "Success, please wait for the system to send you a new password message";
                    case 352:
                        return "Wating...";
                    case 353:
                        return "";
                    case 356:
                        return "Is this phone number used to register a nick?";
                    case 362:
                        return "please register.";
                    case 363:
                        return "Please wait";
                    case 364:
                        return "Before";
                    case 371:
                        return "Program Name";
                    case 375:
                        return "Password";
                    case 376:
                        return "Amount";
                    case 384:
                        return "Zones";
                    case 385:
                        return "Register";
                    case 386:
                        return "Registing...";
                    case 387:
                        return "Registration information has been successfully submitted. Please quit the game and wait a moment.";
                    case 390:
                        return "Cache cleared successfully.";
                    case 391:
                        return "Announcement";
                    case 393:
                        return "Names must not be blank.";
                    case 394:
                        return "The password must not be empty.";
                    case 397:
                        return "Re-enter";
                    case 400:
                        return "Rice grains";
                    case 419:
                        return "Select";
                    case 422:
                        return "Select character";
                    case 428:
                        return "Select server";
                    case 435:
                        return "Đang gửi tin nhắn...";
                    case 439:
                        return "Unable to send a recharge message. Please check the money and try to restart the game.";
                    case 440:
                        return "The deposit has been completed. Please wait for the confirmation message. Note that you can only reload 3 times in 5 minutes.";
                    case 441:
                        return " Send to ";
                    case 459:
                        return "Config";
                    case 471:
                        return "Market place";
                    case 473:
                        return "Incomplete input in the cells";
                    case 482:
                        return "Re-type";
                    case 488:
                        return "Remember the information";
                    case 491:
                        return "Successful!";
                    case 495:
                        return "Comments";
                    case 496:
                        return "This method is not supported.";
                    case 503:
                        return "Give to Friends";
                    case 504:
                        return "Thank you for your feedback!";
                    case 519:
                        return "Enter the name you want to retrieve the password";
                    case 532:
                        return "Tặng mGold";
                    case 548:
                        return "Update";
                    case 552:
                        return "Connection failed. Do you want to update the server information?";
                    case 553:
                        return "The server list cannot be updated. Please try again later";
                    case 557:
                        return "Version";
                    case 558:
                        return "Rung";
                    case 559:
                        return "Vietnamese";
                    case 560:
                        return "View";
                    case 574:
                        return "Where do you want to go?";
                    case 580:
                        return "Yes";
                    case 595:
                        return "of you";
                    case 596:
                        return "mGold hiện tại của bạn";
                    case 604:
                        return "Bạn đã dùng ";
                    case 611:
                        return "Chế độ rung";
                    case 664:
                        return "Sau";
                    case 666:
                        return "ZONE";
                    case 667:
                        return "Challenge";
                    case 668:
                        return "Map transfer";
                    case 670:
                        return "Indoor Games";
                    case 671:
                        return "Pet";
                    default:
                        return String.valueOf(i);
                }
            }
        }
        return "";
    }

    public static mCharacter Method487(int i, byte b, SceneManage class140) {
        return new MyCharacter(i, b, class140);
    }

    public static byte[] loadBuffer(String str, int i) {
        RecordStore recordStore = null;
        try {
            RecordStore openRecordStore = RecordStore.openRecordStore(str, false);
            recordStore = openRecordStore;
            byte[] record = openRecordStore.getRecord(1);
            if (recordStore != null) {
                try {
                    recordStore.closeRecordStore();
                } catch (Exception unused) {
                }
            }
            return record;
        } catch (Exception unused2) {
            if (recordStore != null) {
                try {
                    recordStore.closeRecordStore();
                    return null;
                } catch (Exception unused3) {
                    return null;
                }
            }
            return null;
        } catch (Throwable th) {
            if (recordStore != null) {
                try {
                    recordStore.closeRecordStore();
                } catch (Exception unused4) {
                }
            }
        }
        return null;
    }

    public static void saveBuffer(String str, byte[] bArr) {
        try {
            RecordStore openRecordStore = RecordStore.openRecordStore(str, true);
            if (openRecordStore.getNumRecords() > 0) {
                openRecordStore.setRecord(1, bArr, 0, bArr.length);
            } else {
                openRecordStore.addRecord(bArr, 0, bArr.length);
            }
            openRecordStore.closeRecordStore();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void saveUTF(String str, String str2) {
        try {
            saveBuffer(str, str2.getBytes("UTF-8"));
        } catch (Exception unused) {
        }
    }

    public static String loadUTF(String str) {
        byte[] Method488 = loadBuffer(str, 1);
        if (Method488 == null) {
            return null;
        }
        try {
            return new String(Method488, "UTF-8");
        } catch (Exception unused) {
            return new String(Method488);
        }
    }

    public static void saveInt(String str, int i) {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
        try {
            dataOutputStream.writeInt(i);
            saveBuffer(str, byteArrayOutputStream.toByteArray());
            try {
                dataOutputStream.close();
            } catch (Exception unused) {
            }
        } catch (Exception unused2) {
            try {
                dataOutputStream.close();
            } catch (Exception unused3) {
            }
        } catch (Throwable th) {
            try {
                dataOutputStream.close();
            } catch (Exception unused4) {
            }

        }
    }

    public static boolean loadBoolean(String str, boolean defaultValue) {
        byte[] Method488 = loadBuffer(str, 1);
        if (Method488 != null) {
            DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(Method488));
            try {
                boolean read = dataInputStream.readBoolean();
                try {
                    dataInputStream.close();
                } catch (Exception unused) {
                }
                return read;
            } catch (Exception unused2) {
                try {
                    dataInputStream.close();
                    return defaultValue;
                } catch (Exception unused3) {
                    return defaultValue;
                }
            } catch (Throwable th) {
                try {
                    dataInputStream.close();
                } catch (Exception unused4) {
                }

            }
        }
        return defaultValue;
    }

    public static void saveBool(String str, boolean i) {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        DataOutputStream dataOutputStream = new DataOutputStream(byteArrayOutputStream);
        try {
            dataOutputStream.writeBoolean(i);
            saveBuffer(str, byteArrayOutputStream.toByteArray());
            try {
                dataOutputStream.close();
            } catch (Exception unused) {
            }
        } catch (Exception unused2) {
            try {
                dataOutputStream.close();
            } catch (Exception unused3) {
            }
        } catch (Throwable th) {
            try {
                dataOutputStream.close();
            } catch (Exception unused4) {
            }
        }
    }

    public static Integer loadInt(String str) {
        byte[] Method488 = loadBuffer(str, 1);
        if (Method488 != null) {
            DataInputStream dataInputStream = new DataInputStream(new ByteArrayInputStream(Method488));
            try {
                int readInt = dataInputStream.readInt();
                try {
                    dataInputStream.close();
                } catch (Exception unused) {
                }
                return new Integer(readInt);
            } catch (Exception unused2) {
                try {
                    dataInputStream.close();
                    return null;
                } catch (Exception unused3) {
                    return null;
                }
            } catch (Throwable th) {
                try {
                    dataInputStream.close();
                } catch (Exception unused4) {
                }

            }
        }
        return null;
    }

    public static void deleteRecord(String str) {
        try {
            RecordStore.deleteRecordStore(str);
        } catch (Exception unused) {
        }
    }

    public static void deleteAllRecordStore() {
        String[] listRecordStores = RecordStore.listRecordStores();
        if (listRecordStores == null) {
            return;
        }
        for (int i = 0; i < listRecordStores.length; i++) {
            String str = listRecordStores[i];
            try {
                RecordStore.deleteRecordStore(str);
            } catch (Exception unused) {
            }
        }

    }
}
