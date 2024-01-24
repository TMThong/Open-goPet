
public class GopetCaptcha : DataVersion
{

    public sbyte[] bufferImg;
    public String key;
    public int numShow = 0;
    public sbyte[] getBufferImg()
    {
        return this.bufferImg;
    }

    public String getKey()
    {
        return this.key;
    }

    public int getNumShow()
    {
        return this.numShow;
    }

    public GopetCaptcha()
    {

    }
}
