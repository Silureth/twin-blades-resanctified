// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Tools.StringHelper
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System.Text;

namespace PSS.GameEngine.Tools
{
  public static class StringHelper
  {
    private static readonly char[] s_digits = new char[19]
    {
      '9',
      '8',
      '7',
      '6',
      '5',
      '4',
      '3',
      '2',
      '1',
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9'
    };

    public static StringBuilder Append(StringBuilder sb, int value, int padding)
    {
      if (value < 0)
        sb.Append('-');
      int length = sb.Length;
      do
      {
        sb.Insert(length, StringHelper.s_digits, value % 10 + 9, 1);
        value /= 10;
        --padding;
      }
      while (value > 0 || padding > 0);
      return sb;
    }

    public static StringBuilder Append(StringBuilder sb, int value) => StringHelper.Append(sb, value, 0);

    public static StringBuilder Append(
      StringBuilder sb,
      float value,
      int precision,
      char separator)
    {
      if ((double) value < 0.0)
      {
        sb.Append('-');
        value = -value;
      }
      long num1 = (long) value;
      long num2 = num1;
      int length1 = sb.Length;
      do
      {
        sb.Insert(length1, StringHelper.s_digits, (int) (num2 % 10L) + 9, 1);
        num2 /= 10L;
      }
      while (num2 > 0L);
      if (precision == 0)
        return sb;
      sb.Append(separator);
      long num3 = 1;
      float num4 = 1f;
      for (int index = 0; index < precision; ++index)
      {
        num3 *= 10L;
        num4 *= 10f;
      }
      long num5 = num1 * num3;
      long num6 = (long) ((double) value * (double) num4) - num5;
      int length2 = sb.Length;
      do
      {
        sb.Insert(length2, StringHelper.s_digits, (int) (num6 % 10L) + 9, 1);
        num6 /= 10L;
      }
      while (num6 > 0L);
      return sb;
    }
  }
}
