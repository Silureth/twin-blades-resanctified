// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Tools.TextUtils
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.Resources;
using System.Collections.Generic;

namespace Resanctified.Engine.Tools
{
  public class TextUtils
  {
    public static void WrapText(ref List<OLPair> result, string text, Font font, float width)
    {
      float num1 = 0.0f;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      string str = text;
      int index = 0;
      while (index < str.Length)
      {
        num1 += font.GetCharWidth(str[index]);
        ++num3;
        if (str[index] == ' ' && (double) num1 >= (double) width || str[index] == '\n' || index + 1 >= str.Length)
        {
          OLPair olPair = new OLPair();
          olPair.offset = num2;
          if (num4 == 0 || (double) num1 <= (double) width)
          {
            switch (str[index])
            {
              case '\n':
              case ' ':
                olPair.length = num3 - 1;
                break;
              default:
                olPair.length = num3;
                break;
            }
            num2 += num3;
          }
          else
          {
            switch (text[num2 + num4 - 1])
            {
              case '\n':
              case ' ':
                olPair.length = num4 - 1;
                break;
              default:
                olPair.length = num4;
                break;
            }
            num2 += num4;
          }
          result.Add(olPair);
          num4 = 0;
          num3 = 0;
          num1 = 0.0f;
          index = num2;
        }
        else if (str[index] == ' ')
        {
          num4 = num3;
          ++index;
        }
        else
          ++index;
      }
    }
  }
}
