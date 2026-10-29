using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;

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
                            this.lineHeight = int.Parse(xmlReader["lineHeight"]);
                            this.baseLine = int.Parse(xmlReader["base"]);
                            this.scaleW = int.Parse(xmlReader["scaleW"]);
                            this.scaleH = int.Parse(xmlReader["scaleH"]);
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
                            this.charData[index].x = int.Parse(xmlReader["x"]);
                            this.charData[index].y = int.Parse(xmlReader["y"]);
                            this.charData[index].width = int.Parse(xmlReader["width"]);
                            this.charData[index].height = int.Parse(xmlReader["height"]);
                            this.charData[index].xoffset = int.Parse(xmlReader["xoffset"]);
                            this.charData[index].yoffset = int.Parse(xmlReader["yoffset"]);
                            this.charData[index].xadvance = int.Parse(xmlReader["xadvance"]);
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

        public void Cleanup() => this.charData = null;
    }
}
