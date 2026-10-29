// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.Font
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace Resanctified.Engine.Resources
{
  public sealed class Font : Resource
  {
    private FontData m_data;
    private Texture m_texture;
    private Dictionary<char, int> m_unicodeMap;
    private int[] m_asciiMap;

    internal FontCharData? GetCharData(char unicode)
    {
      int index = -1;
      if (unicode < 'Ā')
        index = this.m_asciiMap[(int) unicode];
      else
        this.m_unicodeMap.TryGetValue(unicode, out index);
      return index >= 0 ? new FontCharData?(this.m_data.charData[index]) : new FontCharData?();
    }

    internal FontData GetFontData() => this.m_data;

    public Texture GetTexture() => this.m_texture;

    public float GetCharWidth(char c)
    {
      FontCharData? charData = this.GetCharData(c);
      return charData.HasValue ? charData.Value.xadvance : 0.0f;
    }

    public float GetStringWidth(string s)
    {
      float num = 0.0f;
      float stringWidth = 0.0f;
      for (int index = 0; index < s.Length; ++index)
      {
        char unicode = s[index];
        if (unicode == '\n')
        {
          if ((double) num > (double) stringWidth)
            stringWidth = num;
          num = 0.0f;
        }
        else
        {
          FontCharData? charData = this.GetCharData(unicode);
          if (charData.HasValue)
            num += charData.Value.xadvance;
        }
      }
      if ((double) num > (double) stringWidth)
        stringWidth = num;
      return stringWidth;
    }

    public float GetStringWidth(StringBuilder sb)
    {
      float num = 0.0f;
      float stringWidth = 0.0f;
      for (int index = 0; index < sb.Length; ++index)
      {
        char unicode = sb[index];
        if (unicode == '\n')
        {
          if ((double) num > (double) stringWidth)
            stringWidth = num;
          num = 0.0f;
        }
        else
        {
          FontCharData? charData = this.GetCharData(unicode);
          if (charData.HasValue)
            num += charData.Value.xadvance;
        }
      }
      if ((double) num > (double) stringWidth)
        stringWidth = num;
      return stringWidth;
    }

    public float GetLineHeight() => this.m_data.lineHeight;

    public float GetBaselinePosition() => this.m_data.baseLine;

    public Font()
      : base(Resource.ResourceType.Font)
    {
      this.m_asciiMap = new int[256];
      this.m_unicodeMap = new Dictionary<char, int>(16);
      this.m_data = new FontData();
    }

    internal override void Load()
    {
      if (this.m_isLoaded)
        return;
      this.m_data.InitFromXML(this.m_filename);
      this.m_texture = ResourceManager.Instance.LoadTexture(this.m_data.file);
      for (int index = 0; index < this.m_asciiMap.Length; ++index)
        this.m_asciiMap[index] = -1;
      this.m_unicodeMap.Clear();
      for (int index = 0; index < this.m_data.charData.Length; ++index)
      {
        if (this.m_data.charData[index].charId < 256U)
          this.m_asciiMap[(int)(IntPtr) this.m_data.charData[index].charId] = index;
        else
          this.m_unicodeMap.Add((char) this.m_data.charData[index].charId, index);
      }
      this.m_isLoaded = true;
    }

    internal override void Unload()
    {
      if (!this.m_isLoaded)
        return;
      Resource.SafeRelease<Texture>(ref this.m_texture);
      this.m_unicodeMap.Clear();
      this.m_data.Cleanup();
      this.m_isLoaded = false;
    }
  }
}
