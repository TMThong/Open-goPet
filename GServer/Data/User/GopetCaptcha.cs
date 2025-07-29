using Gopet.IO;
using SixLabors.ImageSharp;
using SixLaborsCaptcha.Core;
using System.Security.Cryptography;

public class GopetCaptcha
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


    public static SixLaborsCaptchaModule slc = new SixLaborsCaptchaModule(new SixLaborsCaptchaOptions
    {
        DrawLines = 5,
        TextColor = new Color[] { Color.Green, Color.Red, Color.Black, Color.DarkCyan, Color.SkyBlue },
        DrawLinesColor = new Color[] { Color.Gray, Color.Black, Color.DarkGrey, Color.SlateGray },
        FontFamilies = new string[] { "Arial" },
    });



    public GopetCaptcha()
    {
        key = Extensions.GetUniqueKey(6);
        bufferImg = slc.Generate(key).sbytes();
    }
}