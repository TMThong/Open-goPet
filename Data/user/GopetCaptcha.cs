 
public class GopetCaptcha : DataVersion {

    private sbyte[] bufferImg;
    private String key;
    public int numShow = 0;

    public GopetCaptcha()   {
        // Tạo captcha
        DefaultKaptcha kaptcha = new DefaultKaptcha();
        kaptcha.setConfig(new Config(new Properties() {
            {
                setProperty("kaptcha.textproducer.char.Length", "6"); // Độ dài của captcha
                setProperty("kaptcha.image.width", "160"); // Chiều rộng captcha (đơn vị pixel)
                setProperty("kaptcha.image.height", "80"); // Chiều cao captcha (đơn vị pixel)
            }
        }));
        key = kaptcha.createText();
        sbyteArrayOutputStream fArrayOutputStream = new sbyteArrayOutputStream();
        ImageIO.write(kaptcha.createImage(key), "png", fArrayOutputStream);
        fArrayOutputStream.close();
        bufferImg = fArrayOutputStream.tosbyteArray();
        fArrayOutputStream = null;
    }
}
