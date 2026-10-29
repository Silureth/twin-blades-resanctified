// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.FontData
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using System.IO;
using System.Xml;

namespace Resanctified.Engine.Resources
{
  internal class FontData
  {
    public string face;
    public int size;
    public bool bold;
    public bool italic;
    public string charset;
    public bool unicode;
    public int stretchH;
    public bool smooth;
    public bool aa;
    public int paddingUp;
    public int paddingRight;
    public int paddingDown;
    public int paddingLeft;
    public int spacingH;
    public int spacingV;
    public int outline;
    public float lineHeight;
    public float baseLine;
    public float scaleW;
    public float scaleH;
    public bool packed;
    public int alphaChannel;
    public int redChannel;
    public int greenChannel;
    public int blueChannel;
    public string file;
    public FontCharData[] charData;

    public void InitFromXML(string filename)
    {
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.IgnoreComments = true;
      settings.IgnoreWhitespace = true;
      settings.IgnoreProcessingInstructions = true;
      Stream input = TitleContainer.OpenStream(Path.Combine(Application.Instance.Content.RootDirectory, filename));
      XmlReader xmlReader = XmlReader.Create(input, settings);
      int index = 0;
      while (xmlReader.Read())
      {
        if (xmlReader.IsStartElement())
        {
          switch (xmlReader.Name)
          {
            case "info":
              this.face = xmlReader["face"];
              this.size = int.Parse(xmlReader["size"]);
              this.bold = int.Parse(xmlReader["bold"]) != 0;
              this.italic = int.Parse(xmlReader["italic"]) != 0;
              this.charset = xmlReader["charset"];
              this.unicode = int.Parse(xmlReader["unicode"]) != 0;
              this.stretchH = int.Parse(xmlReader["stretchH"]);
              this.smooth = int.Parse(xmlReader["smooth"]) != 0;
              this.aa = int.Parse(xmlReader["aa"]) != 0;
              string[] strArray1 = xmlReader["padding"].Split(',');
              this.paddingUp = int.Parse(strArray1[0]);
              this.paddingRight = int.Parse(strArray1[1]);
              this.paddingDown = int.Parse(strArray1[2]);
              this.paddingLeft = int.Parse(strArray1[3]);
              string[] strArray2 = xmlReader["spacing"].Split(',');
              this.spacingH = int.Parse(strArray2[0]);
              this.spacingV = int.Parse(strArray2[1]);
              this.outline = int.Parse(xmlReader["outline"]);
              continue;
            case "common":
              this.lineHeight = (float) int.Parse(xmlReader["lineHeight"]);
              this.baseLine = (float) int.Parse(xmlReader["base"]);
              this.scaleW = (float) int.Parse(xmlReader["scaleW"]);
              this.scaleH = (float) int.Parse(xmlReader["scaleH"]);
              this.packed = int.Parse(xmlReader["packed"]) != 0;
              this.alphaChannel = int.Parse(xmlReader["alphaChnl"]);
              this.redChannel = int.Parse(xmlReader["redChnl"]);
              this.greenChannel = int.Parse(xmlReader["greenChnl"]);
              this.blueChannel = int.Parse(xmlReader["blueChnl"]);
              continue;
            case "page":
              this.file = xmlReader["file"];
              continue;
            case "chars":
              this.charData = new FontCharData[int.Parse(xmlReader["count"])];
              continue;
            case "char":
              this.charData[index].charId = uint.Parse(xmlReader["id"]);
              this.charData[index].x = (float) int.Parse(xmlReader["x"]);
              this.charData[index].y = (float) int.Parse(xmlReader["y"]);
              this.charData[index].width = (float) int.Parse(xmlReader["width"]);
              this.charData[index].height = (float) int.Parse(xmlReader["height"]);
              this.charData[index].xoffset = (float) int.Parse(xmlReader["xoffset"]);
              this.charData[index].yoffset = (float) int.Parse(xmlReader["yoffset"]);
              this.charData[index].xadvance = (float) int.Parse(xmlReader["xadvance"]);
              ++index;
              continue;
            default:
              continue;
          }
        }
      }
      xmlReader.Close();
      input.Close();
    }

    public void Cleanup() => this.charData = (FontCharData[]) null;
  }
}
