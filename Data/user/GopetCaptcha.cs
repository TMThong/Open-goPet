/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package data.user;

import base.DataVersion;
import com.google.code.kaptcha.impl.DefaultKaptcha;
import com.google.code.kaptcha.util.Config;
import java.io.ByteArrayOutputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.Properties;
import javax.imageio.ImageIO;
import lombok.Getter;

/**
 *
 * @author MINH THONG
 */
@Getter
public class GopetCaptcha extends DataVersion {

    private byte[] bufferImg;
    private String key;
    public int numShow = 0;

    public GopetCaptcha() throws FileNotFoundException, IOException {
        // Tạo captcha
        DefaultKaptcha kaptcha = new DefaultKaptcha();
        kaptcha.setConfig(new Config(new Properties() {
            {
                setProperty("kaptcha.textproducer.char.length", "6"); // Độ dài của captcha
                setProperty("kaptcha.image.width", "160"); // Chiều rộng captcha (đơn vị pixel)
                setProperty("kaptcha.image.height", "80"); // Chiều cao captcha (đơn vị pixel)
            }
        }));
        key = kaptcha.createText();
        ByteArrayOutputStream fArrayOutputStream = new ByteArrayOutputStream();
        ImageIO.write(kaptcha.createImage(key), "png", fArrayOutputStream);
        fArrayOutputStream.close();
        bufferImg = fArrayOutputStream.toByteArray();
        fArrayOutputStream = null;
    }
}
