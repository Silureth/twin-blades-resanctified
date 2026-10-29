// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.GameLocalisation
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

namespace Resanctified.Game
{
  public class GameLocalisation
  {
    public const GameLocalisation.LanguageId LOCALISATION_LANGUAGE_DEFAULT = GameLocalisation.LanguageId.English;
    private static readonly GameLocalisation m_instance = new GameLocalisation();
    private string[] m_localisationArray = new string[324];

    public static GameLocalisation Instance => GameLocalisation.m_instance;

    public static GameLocalisation GetInstance() => GameLocalisation.m_instance;

    public string GetString(GameLocalisation.TextId textId) => this.m_localisationArray[(int) textId];

    public string GetString(int textId) => this.m_localisationArray[textId];

    public string GetLanguageName(GameLocalisation.LanguageId language)
    {
      switch (language)
      {
        case GameLocalisation.LanguageId.English:
          return "English";
        case GameLocalisation.LanguageId.French:
          return "Français";
        case GameLocalisation.LanguageId.Italian:
          return "Italiano";
        case GameLocalisation.LanguageId.German:
          return "Deutsch";
        case GameLocalisation.LanguageId.Spanish:
          return "Español";
        default:
          return "?";
      }
    }

    public void Initialize(GameLocalisation.LanguageId language)
    {
      switch (language)
      {
        case GameLocalisation.LanguageId.English:
          this.Initialize_English();
          break;
        case GameLocalisation.LanguageId.French:
          this.Initialize_French();
          break;
        case GameLocalisation.LanguageId.Italian:
          this.Initialize_Italian();
          break;
        case GameLocalisation.LanguageId.German:
          this.Initialize_German();
          break;
        case GameLocalisation.LanguageId.Spanish:
          this.Initialize_Spanish();
          break;
      }
    }

    private void Initialize_English()
    {
      this.m_localisationArray[0] = this.GetLanguageName(GameLocalisation.LanguageId.English);
      this.m_localisationArray[1] = "Ok";
      this.m_localisationArray[2] = "Cancel";
      this.m_localisationArray[3] = "Yes";
      this.m_localisationArray[4] = "No";
      this.m_localisationArray[5] = "On";
      this.m_localisationArray[6] = "Off";
      this.m_localisationArray[9] = "Start New Game";
      this.m_localisationArray[10] = "Continue";
      this.m_localisationArray[11] = "Help & Options";
      this.m_localisationArray[12] = "How to Play";
      this.m_localisationArray[13] = "Settings";
      this.m_localisationArray[14] = "About Twin Blades";
      this.m_localisationArray[15] = "Music:";
      this.m_localisationArray[16] = "Sfx:";
      this.m_localisationArray[17] = "Flip screen:";
      this.m_localisationArray[18] = "Language:";
      this.m_localisationArray[19] = "Leaderboards";
      this.m_localisationArray[20] = "Credits";
      this.m_localisationArray[21] = "Exit Game";
      this.m_localisationArray[24] = "STORY";
      this.m_localisationArray[25] = "SURVIVAL";
      this.m_localisationArray[26] = "OPTIONS";
      this.m_localisationArray[27] = "SETTINGS";
      this.m_localisationArray[28] = "LEADERBOARDS";
      this.m_localisationArray[29] = "ACHIEVEMENTS";
      this.m_localisationArray[30] = "Shooting guns consumes energy.\nSlash zombies to recover some of it.";
      this.m_localisationArray[31] = "Swipe up or down to change gun types.\nUse zombie hearts to buy upgrades\nat the end of the day.";
      this.m_localisationArray[33] = "Life";
      this.m_localisationArray[34] = "Energy";
      this.m_localisationArray[35] = "Zombie\nhearts";
      this.m_localisationArray[36] = "Gun type";
      this.m_localisationArray[37] = "Shoot";
      this.m_localisationArray[38] = "Slash";
      this.m_localisationArray[39] = "Move\nand jump";
      this.m_localisationArray[43] = "Are you sure?";
      this.m_localisationArray[44] = "This will overwrite your existing game.";
      this.m_localisationArray[45] = "All unsaved progress will be lost.";
      this.m_localisationArray[46] = "Exit the game?";
      this.m_localisationArray[47] = "Character upgrades";
      this.m_localisationArray[48] = "Weapon upgrades";
      this.m_localisationArray[49] = "Buy the machine gun";
      this.m_localisationArray[50] = "Buy the flame thrower";
      this.m_localisationArray[51] = "Buy the ice gun";
      this.m_localisationArray[52] = "Buy the explosive gun";
      this.m_localisationArray[53] = "Buy the holy beam";
      this.m_localisationArray[54] = "Upgrade the gun";
      this.m_localisationArray[55] = "Upgrade the machine gun";
      this.m_localisationArray[56] = "Upgrade the flame thrower";
      this.m_localisationArray[57] = "Upgrade the ice gun";
      this.m_localisationArray[58] = "Upgrade the explosive gun";
      this.m_localisationArray[59] = "Upgrade the holy beam";
      this.m_localisationArray[60] = "Buy air slash ability";
      this.m_localisationArray[61] = "Upgrade air slash ability";
      this.m_localisationArray[62] = "Upgrade slash damage";
      this.m_localisationArray[63] = "Upgrade slash energy drain";
      this.m_localisationArray[64] = "Upgrade the amount of life";
      this.m_localisationArray[65] = "Upgrade energy storage";
      this.m_localisationArray[66] = "Pause";
      this.m_localisationArray[67] = "Resume game";
      this.m_localisationArray[68] = "Help";
      this.m_localisationArray[69] = "Back to main menu";
      this.m_localisationArray[71] = "Back";
      this.m_localisationArray[72] = "Loading...";
      this.m_localisationArray[73] = "Press Start Studio";
      this.m_localisationArray[74] = "Programming";
      this.m_localisationArray[75] = "Fabien Kautzmann";
      this.m_localisationArray[76] = "Jean-François Moser";
      this.m_localisationArray[77] = "Graphics";
      this.m_localisationArray[78] = "Mickael Balloul";
      this.m_localisationArray[79] = "David Chauderlot";
      this.m_localisationArray[80] = "Music and sounds";
      this.m_localisationArray[81] = "Nicolas Signat";
      this.m_localisationArray[82] = "Marketing & Business Development";
      this.m_localisationArray[83] = "Philippe Rapin";
      this.m_localisationArray[84] = "";
      this.m_localisationArray[85] = "MGS Mobile";
      this.m_localisationArray[86] = "Production Team";
      this.m_localisationArray[87] = "Travis Howland";
      this.m_localisationArray[88] = "Maja Persson";
      this.m_localisationArray[89] = "Matt Roberts";
      this.m_localisationArray[90] = "Cassie Townsend";
      this.m_localisationArray[91] = "Test Team";
      this.m_localisationArray[92] = "Chad Long";
      this.m_localisationArray[93] = "Chris Hanson";
      this.m_localisationArray[94] = "Justin Davis (VMC)";
      this.m_localisationArray[95] = "Joseph Sheehan (VMC)";
      this.m_localisationArray[96] = "Product Planning & Business Team";
      this.m_localisationArray[97] = "Cherie Lutz";
      this.m_localisationArray[98] = "Jeff Buckingham";
      this.m_localisationArray[99] = "Marketing";
      this.m_localisationArray[100] = "Kathy Richardson";
      this.m_localisationArray[101] = "Special Thanks";
      this.m_localisationArray[102] = "Matt Booty";
      this.m_localisationArray[103] = "David Boker";
      this.m_localisationArray[104] = "Michael Klucher";
      this.m_localisationArray[105] = "Oliver Miyashita";
      this.m_localisationArray[106] = "Andy Grove";
      this.m_localisationArray[107] = "Matt Golz";
      this.m_localisationArray[108] = "Twin Blades is a registered trade mark.";
      this.m_localisationArray[109] = "©2010 Press Start Studio. All rights reserved.";
      this.m_localisationArray[110] = "http://www.press-start-studio.com";
      this.m_localisationArray[111] = "info@press-start-studio.com";
      this.m_localisationArray[112] = "Thank you for playing!";
      this.m_localisationArray[22] = "";
      this.m_localisationArray[23] = "";
      this.m_localisationArray[7] = "Story";
      this.m_localisationArray[8] = "Survival";
      this.m_localisationArray[217] = "Choose difficulty";
      this.m_localisationArray[214] = "Difficulty:";
      this.m_localisationArray[215] = "Normal";
      this.m_localisationArray[216] = "Hard";
      this.m_localisationArray[32] = "Swipe up or down to change gun types.\nUse zombie hearts to buy upgrades\nat the monastery.";
      this.m_localisationArray[41] = "Change gun";
      this.m_localisationArray[40] = "Move and choose the zone to attack the undead.\nWatch out the infestation icons, the undeads strength\nis increasing with the infestation level.";
      this.m_localisationArray[42] = "Day";
      this.m_localisationArray[70] = "Back to map";
      this.m_localisationArray[310] = "Buy or upgrade weapons and abilities by trading zombie hearts.";
      this.m_localisationArray[311] = "Do you really want to exit the shop?";
      this.m_localisationArray[113] = "Sister Angelika,\n\nthe town of Jalombe is facing a serious malevolent threat, and with the ongoing crusade in the West we have very limited means for which to counter this agression.\n\nI hereby put you under Father Richiardo's command who is supervising the surrounding area. I have faith your old mentor will be delighted to reunite with you.\n\nNow, make haste Sister Angelika. I wish for you to depart within the hour.";
      this.m_localisationArray[114] = "Inquisitor Del'Mar";
      this.m_localisationArray[115] = "Tutorial";
      this.m_localisationArray[116] = "I have to clean the city from the zombie plague.";
      this.m_localisationArray[117] = "I must find the zombies' leader. Maybe I should check around the marketplace.";
      this.m_localisationArray[118] = "I have spotted their leader, I'll have to defeat it.";
      this.m_localisationArray[119] = "I still have to clean the city from the zombie plague.";
      this.m_localisationArray[120] = "I must find the missing missionary. I should look around the forest.";
      this.m_localisationArray[121] = "I have found no trace of the missionary, nor of the zombie master. I should take advice at the monastery.";
      this.m_localisationArray[122] = "I have to go to the cliff at nightfall.";
      this.m_localisationArray[123] = "I have found the root of all of evil. I must secate it!";
      this.m_localisationArray[124] = "Rescue.";
      this.m_localisationArray[125] = "I have to explore the old graveyard.";
      this.m_localisationArray[126] = "I have to explore the old chapel.";
      this.m_localisationArray[(int) sbyte.MaxValue] = "I should go back to the monastery and ask Father Richiardo about this magical field blocking the way to the crypt.";
      this.m_localisationArray[128] = "I must kill all the undead.";
      this.m_localisationArray[129] = "I will avenge the death of Magdalena and kill this evil spirit!";
      this.m_localisationArray[130] = "Looks like my duty is not over yet.";
      this.m_localisationArray[133] = "Father Richiardo";
      this.m_localisationArray[131] = "Angelika";
      this.m_localisationArray[132] = "Magdalena";
      this.m_localisationArray[134] = "Undead Butcher";
      this.m_localisationArray[135] = "Necromancer";
      this.m_localisationArray[136] = "Evil Spirit";
      this.m_localisationArray[137] = "It is good to see you again Sister Angelika. It has been quite some time since you last graced us with your presence, and I am proud to say that you have become a fine nun. Well, I see the Order has entrusted you with a Soul Redeemer.";
      this.m_localisationArray[138] = "As I am sure you already know, the energy of the Redeemer is resplenished by reaping the tainted souls of your victims using your mighty scythe.";
      this.m_localisationArray[139] = "Make good use of it in the name of the Lord and repel the zombies out ofthe monastery! Godspeed Sister Angelika";
      this.m_localisationArray[140] = "Sharply handled, sister. Now give me their blackened hearts so that I can purify the magic within which will allow you to upgrade your weapons and abilities.";
      this.m_localisationArray[141] = "You may visit me anytime you wish. I will welcome you with open arms, always. But for now you must hurry to the town and eradicate the undead plague before it spreads to every living soul.";
      this.m_localisationArray[142] = "The threat has been eliminated...For now.";
      this.m_localisationArray[143] = "The night is falling.\nMore of the undead are rising to attack the city!";
      this.m_localisationArray[144] = "These creatures are mindless and yet they attack in groups... .\nCould it be possible that someone or something is luring them here?";
      this.m_localisationArray[145] = "They have found a way to protect themselves from my bullets.\nI should head to the monastery and grab a more powerful weapon.";
      this.m_localisationArray[146] = "The undead grow stronger every day.\nThis isn't a begining uprising Sister Angelika, we are facing a full-scale infestation!\nI fear a powerful malevolent spirit may be behind all this.";
      this.m_localisationArray[147] = "I must find their maker and put an end to this nightmare.";
      this.m_localisationArray[148] = "These tracks lead to a much bigger being. It must be...their master!";
      this.m_localisationArray[151] = "This vile beast wasn't intelligent enough to summon all these zombies by himself.\nI have to find the brains behind all of this.";
      this.m_localisationArray[149] = "FRESH MEEEAT...";
      this.m_localisationArray[152] = "The city is clear for now...\nI should report back to Father Richiardo at the monastery.";
      this.m_localisationArray[153] = "You fought a very large creature indeed. Your skills have improved much Angelika,\nyou could very well be the pride of the order if only...";
      this.m_localisationArray[154] = "What is it Father?";
      this.m_localisationArray[155] = "...\nYou must know of the conflicting rumors which are rampant among the Order with regards to the...";
      this.m_localisationArray[156] = "...Nature of your relationship with Sister Magdalena.";
      this.m_localisationArray[157] = "...";
      this.m_localisationArray[158] = "What are your orders Father?";
      this.m_localisationArray[159] = "It has been one week since we have received a report from our field missionary. I want you to investigate the forest nearby where she was last seen.";
      this.m_localisationArray[160] = "Remember Angelika, I want you to report the moment you come across any new elements. Do not undertake any initiative without my express consent.";
      this.m_localisationArray[161] = "I have found no trace of the missionary, nor of the zombie master, and now the city is under attack again!";
      this.m_localisationArray[162] = "I have sensed a strong magical presence emanating from the cliffs when the attack started. I suspect some kind of dark ritual is taking place and I want you to scout the area at the favor of the night.";
      this.m_localisationArray[163] = "There is a scent leading into the depth of the forest... It is enveloped in blood, this must lead to their master's lair!";
      this.m_localisationArray[164] = "Ah, you are the lover I assume? I am so glad to finally meet you in person. She spoke very highly of you. Unfortunately I only managed to catch half of what she said due to the buckets of blood she was coughing up.";
      this.m_localisationArray[165] = "M...Mag...Magdalena?! You monster! What have you done to her?";
      this.m_localisationArray[166] = "...";
      this.m_localisationArray[167] = "What have I done? My dear, she did it to herself. This one barely needed any temptation. I thought you might be interested to know she offered herself to me...willingly";
      this.m_localisationArray[168] = "His defense... weaker...when the dead...rising.";
      this.m_localisationArray[169] = "Magdalena...";
      this.m_localisationArray[170] = "These dark marks on your body... \nYour heart was always true to me, I know you could never have betrayed us!";
      this.m_localisationArray[171] = "Hold on Magdalena. I will bring you back home.";
      this.m_localisationArray[172] = "Father! Magdalena has fallen into the hands of a necromancer, you must save her!";
      this.m_localisationArray[173] = "I am sorry Angelika...she appears to have expired her last breath already...";
      this.m_localisationArray[174] = "Wait are these...Marks of corruption?! What were you thinking Angelika! You should never have brought this soilure here!";
      this.m_localisationArray[175] = "Soilure?! How can you say that? She is a missionary! Her body needs to be cleansed!";
      this.m_localisationArray[176] = "And so shall it be. But now isn't the time to mourn your fellow, Angelika.";
      this.m_localisationArray[177] = "You must carry on your duties until the undead are eradicated. If you do not, there is no telling what will happen of this region and it's inhabitants.";
      this.m_localisationArray[178] = "But, Father...";
      this.m_localisationArray[179] = "Enough! Your work is not over yet. Fear not, I will prepare all the arrangement for Magdalena's funeral.";
      this.m_localisationArray[180] = "The necromancer you encountered was merely a puppet possessed by an evil spirit belonging to a much greater force. You must find this creature and destroy its very essence.";
      this.m_localisationArray[181] = "But I don't even know where to look!";
      this.m_localisationArray[182] = "One of my scouts spotted some troubling activities in the old graveyard. Who knows what this creature is after. ";
      this.m_localisationArray[183] = "An old chapel lies ahead. The surrounding dark energy is sending chills down my spine.";
      this.m_localisationArray[184] = "Oh my...What have I done? The dead are rising by the hundreds!";
      this.m_localisationArray[185] = "There are a set of stairs leading down to the crypt, but I can not enter. A strong magical field is blocking the way. I should go back to the monastery and ask Father Richiardo about this.";
      this.m_localisationArray[186] = "This dark magic takes its strength from the undead. You must clear the island of all the undead. Only the holy beam can destroy this shield which hinders your entry into the crypt.";
      this.m_localisationArray[187] = "I cannot enter the crypt. I need the holy beam to force my way through!";
      this.m_localisationArray[188] = "The path to the crypt is now clear. Evil beware, for the arm of the almighty is soon to be unleashed upon thee!";
      this.m_localisationArray[189] = "Turn back while you still can, mortal. Creep back to your short-lived existence and enjoy your last breaths before my master delivers you from the pain of living.";
      this.m_localisationArray[190] = "My spirit is stronger than that, demon. Swallow your hopes of domination, for your uprising ends here!";
      this.m_localisationArray[191] = "You are too late mortal, the awakening has already begun. Nothing can stop his arisal now.";
      this.m_localisationArray[192] = "What's happening? My energy is being drained!";
      this.m_localisationArray[193] = "The time has come for Magdalena's funeral.";
      this.m_localisationArray[194] = "It is done Father. However, the spirit mentioned something else: a mast...";
      this.m_localisationArray[195] = "We will look into it Angelika. For now we must proceed with Magdalena's corpse immediately. I am afraid her soul has gone beyond redemption.";
      this.m_localisationArray[196] = "What do you mean Father? You said you would handle the funeral?";
      this.m_localisationArray[197] = "You have seen the marks of corruption with your own eyes Angelika. She has left me no choice, I must purify her tainted flesh by fire.";
      this.m_localisationArray[198] = "Father, no! She deserves a proper burial. For she has earned her place next to the Lord!";
      this.m_localisationArray[199] = "The threat is too severe Angelika, my decision is irrevocable. I will give you a moment for your valediction while the funeral pyre is being prepared.";
      this.m_localisationArray[200] = "Oh Magdalena...how could I ever bear living without you?";
      this.m_localisationArray[201] = "What should I do?";
      this.m_localisationArray[202] = "1. Life goes on. I have devoted myself to the order.";
      this.m_localisationArray[203] = "2. I can not bare to live without Magdalena.";
      this.m_localisationArray[204] = "Rest in peace, my love.";
      this.m_localisationArray[205] = "They will never set us apart. \nI will be with you, forever....";
      this.m_localisationArray[206] = "";
      this.m_localisationArray[207] = "Epilog";
      this.m_localisationArray[208] = "-Unfinished business-";
      this.m_localisationArray[209] = "Inquisitor,\nThe contamination in Jalombe has been halted thanks to the courageous efforts of Sister Angelika. \nAlthough the circumstances of her death remains an unforgivable sin, I recommend her name to be cited amongst the Fighting Martyrs.\nYour suspicion regarding the seeds of corruption within Sister Magdalena were funded and although I have found no evidence of a dark ritual the disappearance of her corpse is rather disturbing. Yet I canno't spare the resources to investigate any further.\nMay your quest in the pacification of the West shine high upon the Order.";
      this.m_localisationArray[210] = "Father Richiardo";
      this.m_localisationArray[211] = "You may continue playing to raise your score.\nAll levels are unlocked with maximal infestation.";
      this.m_localisationArray[218] = "Killing zombies earns you zombie hearts. Trade hearts for weapons and abilities at the end of a day.";
      this.m_localisationArray[219] = "Killing zombies earns you zombie hearts. Trade hearts for weapons and abilities at the monastery.";
      this.m_localisationArray[220] = "Zombies get tougher every day, so make sure you upgrade your skills to increase your chances of survival!";
      this.m_localisationArray[221] = "Armored zombies take less damage from bullets.";
      this.m_localisationArray[222] = "Upgrade the nun's abilities before upgrading her weapons if you want to make it to the last day.";
      this.m_localisationArray[223] = "Jumping can get you out of tricky situations, but mind the landing as you will be vulnerable for a short period of time!";
      this.m_localisationArray[224] = "Will you survive 31 days of zombie onslaught?";
      this.m_localisationArray[225] = "Did you know there is a second tab in the shop where you can buy upgrades for the nun's health and energy?";
      this.m_localisationArray[226] = "Buy the air slash ability at the end of the day. When acquired, press 'jump' and 'slash' to do the air slash move.";
      this.m_localisationArray[227] = "Buy the air slash ability at the monastery. When acquired, press 'jump' and 'slash' to do the air slash move.";
      this.m_localisationArray[228] = "Try to group zombies into big packs for a more efficient use of your ammo energy.";
      this.m_localisationArray[229] = "Upgrade slash damage to deal more damage to zombies when using the scythe.";
      this.m_localisationArray[230] = "You can perform an air slash combo after jumping; buy the skill in the second tab of the shop!";
      this.m_localisationArray[231] = "Upgrade the amount of life to be more resistant to zombie attacks.";
      this.m_localisationArray[232] = "Twin Blades is a scoring survival game: if you die you'll keep your weapons and progress but your score will be reset to 0!";
      this.m_localisationArray[233] = "When using the flame thrower, do a 'quick burn' of the zombies then finish them off with the scythe. That way you can still drain their energy while using your weapon longer.";
      this.m_localisationArray[234] = "Upgrading the scythe's damage also increases the amount of energy retrieved with each slash!";
      this.m_localisationArray[235] = "";
      this.m_localisationArray[236] = "If you like Twin Blades visual style, you can find freebies to download at www.press-start-studio.com";
      this.m_localisationArray[237] = "Watch out the zombie infestation progress, new zones may be infected during the night. If you don't clear a zone, the undead will get stronger.";
      this.m_localisationArray[238] = "You can come back anytime to the monastery, and talk to Father Richiardo to upgrade your weapons and abilities.";
      this.m_localisationArray[239] = "You can have informations about your current mission, by pressing 'i' on the map screen.";
      this.m_localisationArray[240] = "Some bosses have shields with rotating vulnerability; so try out another weapon, if the one you are using deals no damage.";
      this.m_localisationArray[212] = "Will you survive 31 days of zombie onslaught?";
      this.m_localisationArray[213] = "The story of Sister Angelika.";
      this.m_localisationArray[241] = "Buy Full Game";
      this.m_localisationArray[243] = "Also available on XBox Live Indie Games.";
      this.m_localisationArray[242] = "Achievements";
      this.m_localisationArray[244] = "Achievement unlocked:";
      this.m_localisationArray[245] = "Pistolera";
      this.m_localisationArray[246] = "Kill 500 zombies using only the base gun\nor the machine gun in survival mode.";
      this.m_localisationArray[247] = "You killed 500 zombies using only the base gun\nor the machine gun in survival mode.";
      this.m_localisationArray[248] = "Inquisitor";
      this.m_localisationArray[249] = "Kill 500 zombies using only\nthe flamethrower in survival mode.";
      this.m_localisationArray[250] = "You killed 500 zombies using only\nthe flamethrower in survival mode.";
      this.m_localisationArray[251] = "Ice Cold Killer";
      this.m_localisationArray[252] = "Kill 500 zombies using only\nthe ice gun in survival mode.";
      this.m_localisationArray[253] = "You killed 500 zombies using only\nthe ice gun in survival mode.";
      this.m_localisationArray[254] = "Fallout";
      this.m_localisationArray[(int) byte.MaxValue] = "Kill 500 zombies using only\nthe explosive gun in survival mode.";
      this.m_localisationArray[256] = "You killed 500 zombies using only\nthe explosive gun in survival mode.";
      this.m_localisationArray[257] = "Wrath of God";
      this.m_localisationArray[258] = "Kill 1000 zombies using only\nthe holy beam in survival mode.";
      this.m_localisationArray[259] = "You killed 1000 zombies using only\nthe holy beam in survival mode.";
      this.m_localisationArray[260] = "The Falcon";
      this.m_localisationArray[261] = "Kill 500 zombies using only\nthe air slash in survival mode.";
      this.m_localisationArray[262] = "You killed 500 zombies using only\nthe air slash in survival mode.";
      this.m_localisationArray[263] = "Love Me Tender";
      this.m_localisationArray[264] = "Shake off 100 zombies in survival mode.";
      this.m_localisationArray[265] = "You shook off 100 zombies in survival mode.";
      this.m_localisationArray[266] = "One Nun Army";
      this.m_localisationArray[267] = "Buy all possible upgrades for\nall the weapons in survival mode.";
      this.m_localisationArray[268] = "You bought all possible upgrades for\nall the weapons in survival mode.";
      this.m_localisationArray[269] = "Way Of The Nunja";
      this.m_localisationArray[270] = "Upgrade all of your Nun's abilities in survival mode.";
      this.m_localisationArray[271] = "You upgraded all of your Nun's abilities in survival mode.";
      this.m_localisationArray[272] = "Sector Clear";
      this.m_localisationArray[273] = "Finish the survival mode.";
      this.m_localisationArray[274] = "You finished the survival mode.";
      this.m_localisationArray[275] = "It's a hard life";
      this.m_localisationArray[276] = "Finish the survival mode in hard difficulty.";
      this.m_localisationArray[277] = "You finished the survival mode in hard difficulty.";
      this.m_localisationArray[278] = "1st class nun";
      this.m_localisationArray[279] = "Kill 100 zombies in story mode.";
      this.m_localisationArray[280] = "You killed 100 zombies in story mode.";
      this.m_localisationArray[281] = "Nun Corporal";
      this.m_localisationArray[282] = "Kill 500 zombies in story mode.";
      this.m_localisationArray[283] = "You killed 500 zombies in story mode.";
      this.m_localisationArray[284] = "Nun Sergeant";
      this.m_localisationArray[285] = "Kill 1000 zombies in story mode.";
      this.m_localisationArray[286] = "You killed 1000 zombies in story mode.";
      this.m_localisationArray[287] = "The revenge of the meat";
      this.m_localisationArray[288] = "Defeat the undead butcher.";
      this.m_localisationArray[289] = "You killed the undead butcher.";
      this.m_localisationArray[290] = "Did I break your concentration?";
      this.m_localisationArray[291] = "Defeat the necromancer";
      this.m_localisationArray[292] = "You killed the necromancer.";
      this.m_localisationArray[293] = "See thee in Hell";
      this.m_localisationArray[294] = "Defeat the evil spirit.";
      this.m_localisationArray[295] = "You killed the evil spirit.";
      this.m_localisationArray[296] = "Sold out";
      this.m_localisationArray[297] = "Buy and upgrade all guns and abilities\nto the max in story mode.";
      this.m_localisationArray[298] = "You bought and upgraded all guns and abilities\nto the max in story mode.";
      this.m_localisationArray[299] = "Guardian angel";
      this.m_localisationArray[300] = "Finish the story mode.";
      this.m_localisationArray[301] = "You finished the story mode.";
      this.m_localisationArray[302] = "Iron Lady";
      this.m_localisationArray[303] = "Finish the story mode in hard difficulty.";
      this.m_localisationArray[304] = "You finished the story mode in hard difficulty.";
      this.m_localisationArray[305] = "The full version of Twin Blades features:";
      this.m_localisationArray[306] = "An immersive storyline with epic boss encounters!";
      this.m_localisationArray[307] = "6 range weapons\nranging from machinegun to explosive nukes";
      this.m_localisationArray[308] = "Dozens of death animations related to your artillery";
      this.m_localisationArray[309] = "9 environments discoverable\nat different times of the day or night";
      this.m_localisationArray[312] = "HUD:";
      this.m_localisationArray[313] = "Friends";
      this.m_localisationArray[314] = "Weekly";
      this.m_localisationArray[315] = "To earn achievements you must purchase the full version of the game.";
      this.m_localisationArray[316] = "Purchase";
      this.m_localisationArray[317] = "Not now";
      this.m_localisationArray[318] = "Update available";
      this.m_localisationArray[319] = "An update is available! This update is required to connect to Xbox LIVE.\nUpdate now?";
      this.m_localisationArray[320] = "Server unavailable";
      this.m_localisationArray[321] = "The Twin Blades server is not available at this time. Please try again later.";
      this.m_localisationArray[322] = "Music";
      this.m_localisationArray[323] = "Another music is playing, do you want to stop this music?";
    }

    private void Initialize_French()
    {
      this.m_localisationArray[0] = this.GetLanguageName(GameLocalisation.LanguageId.French);
      this.m_localisationArray[1] = "Ok";
      this.m_localisationArray[2] = "Annuler";
      this.m_localisationArray[3] = "Oui";
      this.m_localisationArray[4] = "Non";
      this.m_localisationArray[5] = "Oui";
      this.m_localisationArray[6] = "Non";
      this.m_localisationArray[9] = "Nouvelle partie";
      this.m_localisationArray[10] = "Continuer";
      this.m_localisationArray[11] = "Aide et options";
      this.m_localisationArray[12] = "Comment jouer";
      this.m_localisationArray[13] = "Paramètres";
      this.m_localisationArray[14] = "A propos de Twin Blades";
      this.m_localisationArray[15] = "Musique :";
      this.m_localisationArray[16] = "Effets :";
      this.m_localisationArray[17] = "Inverser écran :";
      this.m_localisationArray[18] = "Langue :";
      this.m_localisationArray[19] = "Classements";
      this.m_localisationArray[20] = "Crédits";
      this.m_localisationArray[21] = "Quitter le jeu";
      this.m_localisationArray[24] = "HISTOIRE";
      this.m_localisationArray[25] = "SURVIE";
      this.m_localisationArray[26] = "OPTIONS";
      this.m_localisationArray[27] = "PARAMETRES";
      this.m_localisationArray[28] = "CLASSEMENTS";
      this.m_localisationArray[29] = "SUCCES";
      this.m_localisationArray[30] = "Tirer consomme de l'énergie.\nFaucher un zombie permet d'en récupérer.";
      this.m_localisationArray[31] = "Glisser vers le haut ou le bas permet de\nchanger le type d'arme.\nLes coeurs de zombie permettent d'acheter\ndes améliorations à la fin de la journée.";
      this.m_localisationArray[33] = "Vie";
      this.m_localisationArray[34] = "Energie";
      this.m_localisationArray[35] = "Coeurs de\nzombie";
      this.m_localisationArray[36] = "Type d'arme";
      this.m_localisationArray[37] = "Tirer";
      this.m_localisationArray[38] = "Faucher";
      this.m_localisationArray[39] = "Se déplacer\net sauter";
      this.m_localisationArray[43] = "Êtes-vous sûr?";
      this.m_localisationArray[44] = "Cela effacera votre partie existante.";
      this.m_localisationArray[45] = "La progression non sauvegardée sera perdue.";
      this.m_localisationArray[46] = "Quitter le jeu ?";
      this.m_localisationArray[47] = "Améliorations du personnage";
      this.m_localisationArray[48] = "Améliorations des armes";
      this.m_localisationArray[49] = "Acheter la mitrailleuse";
      this.m_localisationArray[50] = "Acheter le lance-flamme";
      this.m_localisationArray[51] = "Acheter le lance-glace";
      this.m_localisationArray[52] = "Acheter le pistolet explosif";
      this.m_localisationArray[53] = "Acheter le rayon tout-puissant";
      this.m_localisationArray[54] = "Améliorer le pistolet";
      this.m_localisationArray[55] = "Améliorer la mitrailleuse";
      this.m_localisationArray[56] = "Améliorer le lance-flamme";
      this.m_localisationArray[57] = "Améliorer le lance-glace";
      this.m_localisationArray[58] = "Améliorer le pistolet explosif";
      this.m_localisationArray[59] = "Améliorer le rayon tout-puissant";
      this.m_localisationArray[60] = "Acheter la faux aérienne";
      this.m_localisationArray[61] = "Améliorer la faux aérienne";
      this.m_localisationArray[62] = "Améliorer les dommages de la faux";
      this.m_localisationArray[63] = "Améliorer l'énergie récupérée par la faux";
      this.m_localisationArray[64] = "Améliorer la quantité de vie";
      this.m_localisationArray[65] = "Améliorer la quantité d'énergie";
      this.m_localisationArray[66] = "Pause";
      this.m_localisationArray[67] = "Retour au jeu";
      this.m_localisationArray[68] = "Aide";
      this.m_localisationArray[69] = "Retour au menu principal";
      this.m_localisationArray[71] = "Retour";
      this.m_localisationArray[72] = "Chargement...";
      this.m_localisationArray[73] = "Press Start Studio";
      this.m_localisationArray[74] = "Programmation";
      this.m_localisationArray[75] = "Fabien Kautzmann";
      this.m_localisationArray[76] = "Jean-François Moser";
      this.m_localisationArray[77] = "Graphisme";
      this.m_localisationArray[78] = "Mickael Balloul";
      this.m_localisationArray[79] = "David Chauderlot";
      this.m_localisationArray[80] = "Musique et sons";
      this.m_localisationArray[81] = "Nicolas Signat";
      this.m_localisationArray[82] = "Marketing & Développement d'affaires";
      this.m_localisationArray[83] = "Philippe Rapin";
      this.m_localisationArray[84] = "";
      this.m_localisationArray[85] = "MGS Mobile";
      this.m_localisationArray[86] = "Production Team";
      this.m_localisationArray[87] = "Travis Howland";
      this.m_localisationArray[88] = "Maja Persson";
      this.m_localisationArray[89] = "Matt Roberts";
      this.m_localisationArray[90] = "Cassie Townsend";
      this.m_localisationArray[91] = "Test Team";
      this.m_localisationArray[92] = "Chad Long";
      this.m_localisationArray[93] = "Chris Hanson";
      this.m_localisationArray[94] = "Justin Davis (VMC)";
      this.m_localisationArray[95] = "Joseph Sheehan (VMC)";
      this.m_localisationArray[96] = "Product Planning & Business Team";
      this.m_localisationArray[97] = "Cherie Lutz";
      this.m_localisationArray[98] = "Jeff Buckingham";
      this.m_localisationArray[99] = "Marketing";
      this.m_localisationArray[100] = "Kathy Richardson";
      this.m_localisationArray[101] = "Special Thanks";
      this.m_localisationArray[102] = "Matt Booty";
      this.m_localisationArray[103] = "David Boker";
      this.m_localisationArray[104] = "Michael Klucher";
      this.m_localisationArray[105] = "Oliver Miyashita";
      this.m_localisationArray[106] = "Andy Grove";
      this.m_localisationArray[107] = "Matt Golz";
      this.m_localisationArray[108] = "Twin Blades is a registered trade mark.";
      this.m_localisationArray[109] = "©2010 Press Start Studio. All rights reserved.";
      this.m_localisationArray[110] = "http://www.press-start-studio.com";
      this.m_localisationArray[111] = "info@press-start-studio.com";
      this.m_localisationArray[112] = "Merci d'avoir joué!";
      this.m_localisationArray[22] = "";
      this.m_localisationArray[23] = "";
      this.m_localisationArray[7] = "Histoire";
      this.m_localisationArray[8] = "Survie";
      this.m_localisationArray[217] = "Sélectionnez la difficulté";
      this.m_localisationArray[214] = "Difficulté:";
      this.m_localisationArray[215] = "Normal";
      this.m_localisationArray[216] = "Difficile";
      this.m_localisationArray[32] = "Faites glisser votre doigt de haut en bas\npour changer d'arme.\nUtilisez les coeurs de zombie pour acheter\ndes améliorations au monastère.";
      this.m_localisationArray[41] = "Changer d'arme";
      this.m_localisationArray[40] = "Déplacez la nonne et choisissez la zone où attaquer\nles mort-vivants.\nPrenez garde aux icônes d'infestation, la force\ndes zombies augmente en fonction du niveau d'infestation.";
      this.m_localisationArray[42] = "Jour";
      this.m_localisationArray[70] = "Retour à la carte";
      this.m_localisationArray[310] = "Acheter ou améliorer les armes et aptitudes\nen troquant des coeurs de zombie.";
      this.m_localisationArray[311] = "Êtes-vous sûr de vouloir quitter le magasin?";
      this.m_localisationArray[113] = "Soeur Angelika,\n\nla ville de Jalombe fait face à une sérieuse menace maléfique, or la croisade en cours à l'Ouest limite nos moyens pour y faire face.\n\nJe vous mets de ce pas sous le commandement du Père Richiardo qui supervise la région. Je suis certain que votre ancien mentor sera très heureux de vous retrouver.\n\nPour l'heure hâtez-vous Soeur Angelika, je vous veux sur la route dès à présent.";
      this.m_localisationArray[114] = "Inquisiteur Del'Mar";
      this.m_localisationArray[115] = "Tutorial";
      this.m_localisationArray[116] = "Je dois purger la ville de l'invasion zombie.";
      this.m_localisationArray[117] = "Je dois trouver le meneur des zombies. Peut-être devrais-je vérifier près du marché.";
      this.m_localisationArray[118] = "J'ai trouvé leur meneur, il va me falloir le vaincre.";
      this.m_localisationArray[119] = "Il me faut encore purger la ville de l'invasion zombie.";
      this.m_localisationArray[120] = "Je dois trouver le missionnaire perdu. Je devrais regarder aux alentours de la forêt.";
      this.m_localisationArray[121] = "Je n'ai trouvé aucune trace du missionnaire, ni du chef des zombies. Je devrais demander conseil au monastère.";
      this.m_localisationArray[122] = "Je dois me rendre sur la falaise à la nuit tombée.";
      this.m_localisationArray[123] = "J'ai trouvé la racine du mal, maintenant je dois l'arracher!";
      this.m_localisationArray[124] = "Secourir.";
      this.m_localisationArray[125] = "Je dois explorer l'ancien cimetière.";
      this.m_localisationArray[126] = "Je dois explorer la vieille chapelle.";
      this.m_localisationArray[(int) sbyte.MaxValue] = "Je devrais retourner au monastère et demander au Père Richiardo ce qu'il pense de ce champ magique bloquant l'accès à la crypte.";
      this.m_localisationArray[128] = "Je dois tuer tous les mort-vivants.";
      this.m_localisationArray[129] = "Je vengerai la mort de Magdalena et tuerai cet esprit maléfique!";
      this.m_localisationArray[130] = "Il semblerait que mon devoir ne soit pas encore totalement rempli.";
      this.m_localisationArray[133] = "Père Richiardo";
      this.m_localisationArray[131] = "Angelika";
      this.m_localisationArray[132] = "Magdalena";
      this.m_localisationArray[134] = "Boucher Mort-vivant";
      this.m_localisationArray[135] = "Nécromancien";
      this.m_localisationArray[136] = "Esprit Maléfique";
      this.m_localisationArray[137] = "Soeur Angelika, cela fait bien trop longtemps que je n'ai pas eu l'occasion d'apprécier ta compagnie. Je suis fier de voir que tu es devenue une nonne accomplie.\nJe vois en outre que l'Ordre t'as confié un de ses Rédempteurs d'Âme.";
      this.m_localisationArray[138] = "Comme tu le sais certainement déjà, l'énergie du Rédempteur est récupérée en fauchant les âmes impures avec ta Faux Sacrée.";
      this.m_localisationArray[139] = "Fais-en bon usage au nom du Seigneur et repousse les zombies hors du monastère!\nBonne chasse Soeur Angelika.";
      this.m_localisationArray[140] = "Joli travail ma soeur. Maintenant donne-moi ces coeurs de zombie afin que j'en purifie la magie qui les imprègne. Cela te permettra d'améliorer tes armes et aptitudes.";
      this.m_localisationArray[141] = "Tu peux venir me voir à n'importe quelle heure du jour ou de la nuit, je t'accueillerai toujours à bras ouverts. Mais pour l'heure tu dois te hâter vers la cité et éradiquer l'invasion des zombies avant qu'elle ne se propage à chaque être vivant.";
      this.m_localisationArray[142] = "La menace a été éliminée...Pour l'instant.";
      this.m_localisationArray[143] = "La nuit tombe. De nouveaux mort-vivants se lèvent pour attaquer la cité!";
      this.m_localisationArray[144] = "Ces créatures sont dépourvues d'intelligence et pourtant elles attaquent en groupe...\nSerait-il possible que quelqu'un ou quelque chose les attire en ce lieu?";
      this.m_localisationArray[145] = "Ils ont trouvé un moyen de se protéger de mes balles. Je devrais me rendre au monastère et me procurer une arme plus puissante.";
      this.m_localisationArray[146] = "Les mort-vivants deviennent plus puissants chaque jour. Ce n'est pas une infestation bénigne Soeur Angelika, nous faisons face à une contamination de grande envergure! Je crains qu'un puissant esprit maléfique ne se cache derrière tout ceci.";
      this.m_localisationArray[147] = "Je dois trouver leur maître et mettre un terme à ce cauchemar.";
      this.m_localisationArray[148] = "Cette piste indique qu'un specimen bien plus gros est passé par là. Ce doit être...Leur maître!";
      this.m_localisationArray[151] = "Cet ignoble monstre n'était pas suffisamment intelligent pour conjurer tous ces zombies de lui-même. Je dois trouver l'instigateur de toute cette histoire.";
      this.m_localisationArray[149] = "VIANDE FRAAAÎCHE !";
      this.m_localisationArray[152] = "La cité est sûre pour le moment...\nJe devrais faire mon rapport auprès du Père Richiardo au monastère.";
      this.m_localisationArray[153] = "La créature dont tu as triomphé était particulièrement puissante. Tes compétences se sont beaucoup développées Angelika, tu pourrais incarner l'élite de l'Ordre si seulement...";
      this.m_localisationArray[154] = "Qu'y a t-il mon Père?";
      this.m_localisationArray[155] = "...\nTu dois savoir qu'il y a un certain nombre de rumeurs rampantes dans l'Ordre à propos de...";
      this.m_localisationArray[156] = "...La nature de ta relation avec Soeur Magdalena.";
      this.m_localisationArray[157] = "...";
      this.m_localisationArray[158] = "Quels sont vos ordres mon Père?";
      this.m_localisationArray[159] = "Je vois.\nCela fait une semaine que nous n'avons pas eu de nouvelles de notre missionnaire. Je veux que tu ailles enquêter dans la forêt environnante où elle a été vue pour la dernière fois.";
      this.m_localisationArray[160] = "Souviens-toi Angelika, je veux que tu me rapportes aussitôt tout nouvel élément de ton enquête. Ne prends aucune initiative sans mon consentement averti.";
      this.m_localisationArray[161] = "Je n'ai trouvé aucune trace du missionnaire, ni du maître des zombies, et maintenant la cité est de nouveau attaquée!";
      this.m_localisationArray[162] = "J'ai perçu une puissante présence magique émanant de la falaise lorsque l'attaque a débutée.\nJe suspecte qu'une sorte de rituel démoniaque s'y déroule et je veux que tu y effectue une reconnaissance des lieux à la faveur de la nuit.";
      this.m_localisationArray[163] = "Il y a une piste menant vers les profondeurs de la forêt... Elle est couverte de sang, ce doit être l'antre de leur maître!";
      this.m_localisationArray[164] = "Ah, vous-êtes l'amante je suppose? Vous me voyez ravi de faire enfin votre connaissance, elle a beaucoup parlé de vous. J'ai bien peur de n'en avoir compris que la moitié toutefois, à cause des flots de sang jaillissant de sa bouche.";
      this.m_localisationArray[165] = "M...Mag...Magdalena?! Espèce de monstre! Que lui avez-vous fait?";
      this.m_localisationArray[166] = "...";
      this.m_localisationArray[167] = "Ce que je lui ai fait? Ma chère, elle se l'est fait à elle-même. Celle-ci n'avait presque pas besoin d'être tentée. J'ai pensé que vous seriez intéressée de savoir qu'elle s'est offerte à moi... De son plein gré.";
      this.m_localisationArray[168] = "Ses défenses... faiblissent... quand les morts... se lèvent...";
      this.m_localisationArray[169] = "Magdalena...";
      this.m_localisationArray[170] = "Ces marques sombres sur ton corps... Ton coeur est pur, je sais que tu n'aurais jamais pu nous trahir!";
      this.m_localisationArray[171] = "Tiens-bon Magdalena, je vais te ramener chez les nôtres.";
      this.m_localisationArray[172] = "Père! Magdalena est tombée entre les mains d'un nécromant, vous devez la sauver!";
      this.m_localisationArray[173] = "Je suis navré Angelika... Elle a déjà expiré son dernier souffle...";
      this.m_localisationArray[174] = "Mais serait-ce... Des marques de corruption?! A quoi pensais-tu Angelika! Tu n'aurais jamais du ramener cette souillure ici!";
      this.m_localisationArray[175] = "Souillure?! Comment pouvez-vous dire cela? C'est une missionnaire! Son corps doit être purifié!";
      this.m_localisationArray[176] = "Et il le sera. Mais l'heure n'est pas à faire le deuil de nos compagnons Angelika.";
      this.m_localisationArray[177] = "Tu dois poursuivre ton devoir jusqu'à ce que les mort-vivants soient éradiqués. Si tu ne le fais pas, Dieu seul sait ce qu'il adviendra de la région et de ses habitants.";
      this.m_localisationArray[178] = "Mais, Père...";
      this.m_localisationArray[179] = "Assez! Ton travail est loin d'être terminé. Ne t'inquiète pas, je préparerai tous les arrangements pour les funérailles de Magdalena.";
      this.m_localisationArray[180] = "Le nécromant que tu as combattu n'était qu'une marionnette possédée par un esprit malveillant appartenant à une puissance bien supérieure. Tu dois trouver cette créature et en détruire l'essence même.";
      this.m_localisationArray[181] = "Mais je ne sais même pas où la trouver!";
      this.m_localisationArray[182] = "Un de mes éclaireurs a repéré des activités troublantes dans le vieux cimetière. Qui sait ce que cette créature y recherche.";
      this.m_localisationArray[183] = "Une ancienne chapelle se tient juste en face. L'énergie maléfique qui en émane me glace les sangs.";
      this.m_localisationArray[184] = "Oh mon Dieu... Qu'ai-je fait? Les morts se lèvent par centaines!";
      this.m_localisationArray[185] = "Il y a des escaliers qui descendent vers la crypte, mais je ne peux pas y rentrer. Un fort champ magique en bloque l'accès. Je devrais retourner au monastère et demander l'avis de Père Richiardo à ce propos.";
      this.m_localisationArray[186] = "Cette magie démoniaque puise sa force des mort-vivants. Tu dois purger l'île de tous ses revenants Soeur Angelika. Seul le Rayon tout-puissant peut détruire le bouclier qui bloque l'accès à la crypte. ";
      this.m_localisationArray[187] = "Je ne peux pas entrer dans la crypte. Il me faut le Rayon tout-puissant pour m'y frayer un passage!";
      this.m_localisationArray[188] = "La voie vers la crypte est dégagée. Tremblez forces maléfiques, le bras vengeur du Seigneur est sur le point de s'abattre!";
      this.m_localisationArray[189] = "Fuis tant que tu le peux encore, mortel. Retourne à ton existence éphémère et profite de tes dernières bouffées d'air avant que mon maître ne vienne te délivrer des souffrances de la vie.";
      this.m_localisationArray[190] = "Mon esprit est plus fort que tes sournoiseries, démon. Ravale tes espoirs de domination, ton insurrection s'arrête ici!";
      this.m_localisationArray[191] = "Tu arrives trop tard mortel, son éveil à déjà commencé. Rien ne pourra arrêter son incarnation désormais.";
      this.m_localisationArray[192] = "Que se passe t-il? Quelque chose draine mon énergie!";
      this.m_localisationArray[193] = "Le temps est venu pour les funérailles de Magdalena.";
      this.m_localisationArray[194] = "Mon devoir est terminé mon Père. Toutefois l'esprit a mentionné quelque chose d'autre, un maî...";
      this.m_localisationArray[195] = "Cela peut attendre Angelika. Pour l'heure nous devons nous occuper du corps de Magdalena immédiatement. J'ai bien peur que son âme ne soit par delà toute rédemption.";
      this.m_localisationArray[196] = "Que voulez-vous dire mon Père? Vous aviez dit que vous vous occuperiez des funérailles.";
      this.m_localisationArray[197] = "Tu as vu les marques de corruption de tes propres yeux Angelika. Elle ne m'a guère laissé de choix, je dois purifier sa chair par le feu.";
      this.m_localisationArray[198] = "Mon Père, non! \nElle mérite des funérailles décentes, elle a gagné sa place aux côtés du Seigneur!";
      this.m_localisationArray[199] = "La menace est trop grave Angelika, ma décision est irrévocable. Je te laisse un moment pour lui faire tes derniers adieux pendant que le bûcher est assemblé.";
      this.m_localisationArray[200] = "Oh Magdalena... Comment pourrais-je jamais vivre sans toi?";
      this.m_localisationArray[201] = "Que dois-je faire?";
      this.m_localisationArray[202] = "1. La vie continue. J'ai voué ma vie à l'Ordre.";
      this.m_localisationArray[203] = "2. Je ne pourrai jamais supporter de vivre sans Magdalena.";
      this.m_localisationArray[204] = "Que ton repos sois doux, mon amour.";
      this.m_localisationArray[205] = "Ils ne nous séparerons jamais.\nJe t'accompagnerai, pour toujours...";
      this.m_localisationArray[206] = "";
      this.m_localisationArray[207] = "Epilogue";
      this.m_localisationArray[208] = "-Derniers Adieux-";
      this.m_localisationArray[209] = "Inquisiteur,\nLa contamination a été stoppée net à Jalombe grâce aux courageux efforts de Soeur Angelika.\nBien que les circonstances de sa mort restent un pêché impardonnable, je recommande que son nom soit cité parmi les Martyrs Combattants. Votre suspicion à propos des graines de corruption pénétrant Soeur Magdalena se sont avérées fondées et bien que je n'ai pas trouvé trace d'un rituel maléfique la disparition de son corps demeure une inquiétante énigme. Malgré tout, je ne dispose pas des ressources nécessaires pour investiguer l'affaire plus en profondeur. Puisse votre quête dans la pacification de l'Ouest hisser haut la bannière de l'Ordre.";
      this.m_localisationArray[210] = "Père Richiardo";
      this.m_localisationArray[211] = "Vous pouvez continuer de jouer pour augmenter votre score.\nTous les niveaux sont débloqués au niveau maximum d'infestation.";
      this.m_localisationArray[218] = "Tuer des zombies rapporte des coeurs de zombies. Echangez ces coeurs contre des armes et des aptitudes à la fin du jour.";
      this.m_localisationArray[219] = "Tuer des zombies rapporte des coeurs de zombies. Echangez ces coeurs contre des armes et des aptitudes au monastère.";
      this.m_localisationArray[220] = "Les zombies deviennent plus forts chaque jour, assurez-vous d'améliorer vos armes et aptitudes pour augmenter vos chances de survie!";
      this.m_localisationArray[221] = "Les zombies en armure encaissent moins de dégâts de vos balles.";
      this.m_localisationArray[222] = "Améliorez les aptitudes de la nonne avant d'acheter des armes onéreuses si vous voulez survivre jusqu'au dernier jour.";
      this.m_localisationArray[223] = "Sauter peut vous tirer d'une situation difficile, mais faites attention où vous retombez car vous serez vulnérable pendant un court instant!";
      this.m_localisationArray[224] = "Survivrez-vous à 31 jours d'infestation zombie?";
      this.m_localisationArray[225] = "Saviez-vous qu'il y a un deuxième onglet dans le magasin où vous pouvez améliorer la santé et l'énergie de la nonne?";
      this.m_localisationArray[226] = "Achetez ou améliorez vos armes et aptitudes en échangeant les coeurs de zombies.";
      this.m_localisationArray[227] = "Achetez la faux aérienne au monastère. Une fois acquis, appuyez sur 'saut' puis sur 'faucher' pour réaliser une attaque dévastatrice!";
      this.m_localisationArray[228] = "Essayez de grouper les zombies en gros tas pour mieux gérer votre énergie.";
      this.m_localisationArray[229] = "Vous pouvez améliorer la faux pour infliger plus de dégâts aux zombies.";
      this.m_localisationArray[230] = "Vous pouvez effectuer une attaque aérienne avec la faux en achetant la compétence appropriée au monastère!";
      this.m_localisationArray[231] = "Améliorez la vie de la nonne pour être plus résistant aux attaques de zombies.";
      this.m_localisationArray[232] = "Twin Blades est un jeu de survie: en périssant vous conservez vos armes et votre progression mais votre score est remis à 0!";
      this.m_localisationArray[233] = "Lorsque vous utilisez le lance-flamme, faites un jet rapide sur les zombies puis finissez-les avec la faux. Cela permet de drainer leur energie pour utiliser l'arme plus longtemps.";
      this.m_localisationArray[234] = "Améliorer les dégâts de la faux augmente également la quantité d'énergie recupérée à chaque fauchage!";
      this.m_localisationArray[235] = "";
      this.m_localisationArray[236] = "Si vous aimez le style visuel de Twin Blades, vous pouvez télécharger des cadeaux sur www.press-start-studio.com";
      this.m_localisationArray[237] = "Prenez-garde aux niveaux d'infestation des zombies sur la mini-carte, de nouvelles zones peuvent s'infecter durant la nuit. Si vous ne purgez pas une zone, les mort-vivants y deviennent plus forts.";
      this.m_localisationArray[238] = "Vous pouvez retourner au monastère quand vous le souhaitez, et parler au Père Richiardo pour améliorer vos armes et aptitudes.";
      this.m_localisationArray[239] = "Vous pouvez voir les informations de votre mission actuelle, en appuyant sur la touche \"i\" sur la mini-carte.";
      this.m_localisationArray[240] = "Certains boss ont des boucliers aux vulnérabilités tournantes, essayez une arme différente si celle que vous utilisez ne leur causent aucun dégâts.";
      this.m_localisationArray[212] = "Survivrez-vous à 31 jours d'infestation zombie?";
      this.m_localisationArray[213] = "L'histoire de Soeur Angelika.";
      this.m_localisationArray[241] = "Acheter le jeu complet";
      this.m_localisationArray[243] = "Aussi disponible sur XBox Live Indie Games.";
      this.m_localisationArray[242] = "Succès";
      this.m_localisationArray[244] = "Succès déverrouillé:";
      this.m_localisationArray[245] = "Pistolera";
      this.m_localisationArray[246] = "Tuez 500 zombies en utilisant uniquement\nle pistolet de base ou la mitrailleuse en mode survie.";
      this.m_localisationArray[247] = "Vous avez tué 500 zombies en utilisant uniquement\nle pistolet de base ou la mitrailleuse en mode survie.";
      this.m_localisationArray[248] = "Inquisitrice";
      this.m_localisationArray[249] = "Tuez 500 zombies en utilisant uniquement\nle lance-flamme en mode survie.";
      this.m_localisationArray[250] = "Vous avez tué 500 zombies en utilisant uniquement\nle lance-flamme en mode survie.";
      this.m_localisationArray[251] = "Tueuse au sang-froid";
      this.m_localisationArray[252] = "Tuez 500 zombies en utilisant uniquement\nle lance-glace en mode survie.";
      this.m_localisationArray[253] = "Vous avez tué 500 zombies en utilisant uniquement\nle lance-glace en mode survie.";
      this.m_localisationArray[254] = "Retombées radioactives";
      this.m_localisationArray[(int) byte.MaxValue] = "Tuez 500 zombies en utilisant uniquement\nle pistolet explosif en mode survie.";
      this.m_localisationArray[256] = "Vous avez tué 500 zombies en utilisant uniquement\nle pistolet explosif en mode survie.";
      this.m_localisationArray[257] = "La colère de Dieu";
      this.m_localisationArray[258] = "Tuez 1000 zombies en utilisant uniquement\nle rayon tout-puissant en mode survie.";
      this.m_localisationArray[259] = "Vous avez tué 1000 zombies en utilisant uniquement\nle rayon tout-puissant en mode survie.";
      this.m_localisationArray[260] = "Le Faucon";
      this.m_localisationArray[261] = "Tuez 500 zombies en utilisant uniquement\nla faux aérienne en mode survie.";
      this.m_localisationArray[262] = "Vous avez tué 500 zombies en utilisant uniquement\nla faux aérienne en mode survie.";
      this.m_localisationArray[263] = "Je t'aime... moi non plus";
      this.m_localisationArray[264] = "Repoussez 100 zombies en mode survie.";
      this.m_localisationArray[265] = "Vous avez repoussé 100 zombies en mode survie.";
      this.m_localisationArray[266] = "La Sainte-Armée";
      this.m_localisationArray[267] = "Achetez toutes les améliorations possibles\npour toutes les armes en mode survie.";
      this.m_localisationArray[268] = "Vous avez acheté toutes les améliorations possibles\npour toutes les armes en mode survie.";
      this.m_localisationArray[269] = "La voie du Nunja";
      this.m_localisationArray[270] = "Achetez toutes les améliorations possibles\npour votre Nonne en mode survie.";
      this.m_localisationArray[271] = "Vous avez acheté toutes les améliorations possibles\npour votre Nonne en mode survie.";
      this.m_localisationArray[272] = "Zone nettoyée";
      this.m_localisationArray[273] = "Terminez le jeu en mode survie.";
      this.m_localisationArray[274] = "Vous avez terminé le jeu en mode survie.";
      this.m_localisationArray[275] = "La vie est dure";
      this.m_localisationArray[276] = "Terminez le mode survie en difficile.";
      this.m_localisationArray[277] = "Vous avez terminé le mode survie en difficile.";
      this.m_localisationArray[278] = "Nonne 1ère classe";
      this.m_localisationArray[279] = "Tuez 100 zombies en mode histoire.";
      this.m_localisationArray[280] = "Vous avez tué 100 zombies en mode histoire.";
      this.m_localisationArray[281] = "Nonne Caporal";
      this.m_localisationArray[282] = "Tuez 500 zombies en mode histoire.";
      this.m_localisationArray[283] = "Vous avez tué 500 zombies en mode histoire.";
      this.m_localisationArray[284] = "Nonne Sergent";
      this.m_localisationArray[285] = "Tuez 1000 zombies en mode histoire.";
      this.m_localisationArray[286] = "Vous avez tué 1000 zombies en mode histoire.";
      this.m_localisationArray[287] = "La revanche de la viande";
      this.m_localisationArray[288] = "Tuez le zombie boucher.";
      this.m_localisationArray[289] = "Vous avez tué le zombie boucher.";
      this.m_localisationArray[290] = "Déconcentré?";
      this.m_localisationArray[291] = "Tuez le nécromancien.";
      this.m_localisationArray[292] = "Vous avez tué le nécromancien.";
      this.m_localisationArray[293] = "Rendez-vous en enfer";
      this.m_localisationArray[294] = "Tuez l'esprit maléfique.";
      this.m_localisationArray[295] = "Vous avez tué l'esprit maléfique.";
      this.m_localisationArray[296] = "Soldes";
      this.m_localisationArray[297] = "Achetez et améliorer toutes les armes\net aptitudes au maximum en mode histoire.";
      this.m_localisationArray[298] = "Vous avez acheté et amélioré toutes les armes\net aptitudes au maximum en mode histoire.";
      this.m_localisationArray[299] = "Ange gardien";
      this.m_localisationArray[300] = "Terminez le jeu en mode histoire.";
      this.m_localisationArray[301] = "Vous avez terminé le jeu en mode histoire.";
      this.m_localisationArray[302] = "La dame de fer";
      this.m_localisationArray[303] = "Terminez le mode histoire en difficile.";
      this.m_localisationArray[304] = "Vous avez terminé le mode histoire en difficile.";
      this.m_localisationArray[305] = "La version complète de Twin Blades dispose de:";
      this.m_localisationArray[306] = "Une histoire immersive avec des combats épiques\ncontre de puissants ennemis!";
      this.m_localisationArray[307] = "6 armes de jet différentes allant de la mitraillette\naux explosions atomiques";
      this.m_localisationArray[308] = "Des dizaines d'animations différentes\nen fonction des armes utilisées";
      this.m_localisationArray[309] = "9 environnements différents à explorer\nà différents moments du jour et de la nuit.";
      this.m_localisationArray[312] = "HUD :";
      this.m_localisationArray[313] = "Amis";
      this.m_localisationArray[314] = "Hebdomadaire";
      this.m_localisationArray[315] = "Pour obtenir des succès, vous devez acheter le jeu complet.";
      this.m_localisationArray[316] = "Acheter";
      this.m_localisationArray[317] = "Pas maintenant";
      this.m_localisationArray[318] = "Mise à jour disponible";
      this.m_localisationArray[319] = "Une mise à jour est disponible! Cette mise à jour est requise pour se connecter à Xbox LIVE.\nMettre à jour maintenant?";
      this.m_localisationArray[320] = "Serveur indisponible";
      this.m_localisationArray[321] = "Le serveur de Twin Blades n'est pas disponible actuellement. Veuillez réessayer ultérieurement.";
      this.m_localisationArray[322] = "Musique";
      this.m_localisationArray[323] = "Une autre musique est jouée, voulez-vous arrêter cette musique?";
    }

    private void Initialize_Italian()
    {
      this.m_localisationArray[0] = this.GetLanguageName(GameLocalisation.LanguageId.Italian);
      this.m_localisationArray[1] = "Ok";
      this.m_localisationArray[2] = "Cancella";
      this.m_localisationArray[3] = "Si";
      this.m_localisationArray[4] = "No";
      this.m_localisationArray[5] = "Si";
      this.m_localisationArray[6] = "No";
      this.m_localisationArray[9] = "Nuova partita";
      this.m_localisationArray[10] = "Continua";
      this.m_localisationArray[11] = "Guida e opzioni";
      this.m_localisationArray[12] = "Come giocare";
      this.m_localisationArray[13] = "Impostazioni";
      this.m_localisationArray[14] = "A proposito di Twin Blades";
      this.m_localisationArray[15] = "Musica";
      this.m_localisationArray[16] = "Effetti";
      this.m_localisationArray[17] = "Gira lo schermo";
      this.m_localisationArray[18] = "Lingua";
      this.m_localisationArray[19] = "Classifiche";
      this.m_localisationArray[20] = "Riconoscimenti";
      this.m_localisationArray[21] = "Esci dal gioco";
      this.m_localisationArray[24] = "STORIA";
      this.m_localisationArray[25] = "SOPRAVVIVENZA";
      this.m_localisationArray[26] = "OPZIONI";
      this.m_localisationArray[27] = "IMPOSTAZIONI";
      this.m_localisationArray[28] = "CLASSIFICHE";
      this.m_localisationArray[29] = "OBIETTIVI";
      this.m_localisationArray[30] = "Sparare costa energia.\nFalciare uno zombie per recuperare energia.";
      this.m_localisationArray[31] = "Scorrere verso l'alto o verso il basso per\ncambiar e il tipo di arma.\nUtilizzare i cuor i degli zombie per acquistare\nmiglioramenti a fine giornata.";
      this.m_localisationArray[33] = "Vita";
      this.m_localisationArray[34] = "Energia";
      this.m_localisationArray[35] = "Cuori degli\nzombie";
      this.m_localisationArray[36] = "Tipo di arma";
      this.m_localisationArray[37] = "Sparare";
      this.m_localisationArray[38] = "Falciare";
      this.m_localisationArray[39] = "Spostarsi\ne saltare";
      this.m_localisationArray[43] = "Sei sicuro?";
      this.m_localisationArray[44] = "Questo cancellerà la partita esistente";
      this.m_localisationArray[45] = "La progressione non salvata sarà persa.";
      this.m_localisationArray[46] = "Uscire dal gioco?";
      this.m_localisationArray[47] = "Miglioramenti del personaggio";
      this.m_localisationArray[48] = "Miglioramenti delle armi";
      this.m_localisationArray[49] = "Acquistare la mitragliatrice";
      this.m_localisationArray[50] = "Acquistare il lancia-fiamme";
      this.m_localisationArray[51] = "Acquistare il lancia-ghiaccio";
      this.m_localisationArray[52] = "Acquistare la pistola esplosiva";
      this.m_localisationArray[53] = "Acquistare il raggio sacro";
      this.m_localisationArray[54] = "Migliorare l'arma";
      this.m_localisationArray[55] = "Migliorare la mitragliatrice";
      this.m_localisationArray[56] = "Migliorare il lancia-fiamme";
      this.m_localisationArray[57] = "Migliorare il lancia-ghiaccio";
      this.m_localisationArray[58] = "Migliorare la pistola esplosiva";
      this.m_localisationArray[59] = "Migliorare il raggio sacro";
      this.m_localisationArray[60] = "Acquistare la falce aerea";
      this.m_localisationArray[61] = "Migliorare la falce aerea";
      this.m_localisationArray[62] = "Migliorare i danni della falce";
      this.m_localisationArray[63] = "Migliorare l'energia recuperata con la falce";
      this.m_localisationArray[64] = "Migliorare il totale della vita";
      this.m_localisationArray[65] = "Migliorare la quantità di energia";
      this.m_localisationArray[66] = "Pausa";
      this.m_localisationArray[67] = "Torna al gioco";
      this.m_localisationArray[68] = "Guida";
      this.m_localisationArray[69] = "Torna al menu principale";
      this.m_localisationArray[71] = "Indietro";
      this.m_localisationArray[72] = "Caricamento...";
      this.m_localisationArray[73] = "Press Start Studio";
      this.m_localisationArray[74] = "Programming";
      this.m_localisationArray[75] = "Fabien Kautzmann";
      this.m_localisationArray[76] = "Jean-François Moser";
      this.m_localisationArray[77] = "Graphics";
      this.m_localisationArray[78] = "Mickael Balloul";
      this.m_localisationArray[79] = "David Chauderlot";
      this.m_localisationArray[80] = "Music and sounds";
      this.m_localisationArray[81] = "Nicolas Signat";
      this.m_localisationArray[82] = "Business development & Marketing";
      this.m_localisationArray[83] = "Philippe Rapin";
      this.m_localisationArray[84] = "";
      this.m_localisationArray[85] = "MGS Mobile";
      this.m_localisationArray[86] = "Production Team";
      this.m_localisationArray[87] = "Travis Howland";
      this.m_localisationArray[88] = "Maja Persson";
      this.m_localisationArray[89] = "Matt Roberts";
      this.m_localisationArray[90] = "Cassie Townsend";
      this.m_localisationArray[91] = "Test Team";
      this.m_localisationArray[92] = "Chad Long";
      this.m_localisationArray[93] = "Chris Hanson";
      this.m_localisationArray[94] = "Justin Davis (VMC)";
      this.m_localisationArray[95] = "Joseph Sheehan (VMC)";
      this.m_localisationArray[96] = "Product Planning & Business Team";
      this.m_localisationArray[97] = "Cherie Lutz";
      this.m_localisationArray[98] = "Jeff Buckingham";
      this.m_localisationArray[99] = "Marketing";
      this.m_localisationArray[100] = "Kathy Richardson";
      this.m_localisationArray[101] = "Special Thanks";
      this.m_localisationArray[102] = "Matt Booty";
      this.m_localisationArray[103] = "David Boker";
      this.m_localisationArray[104] = "Michael Klucher";
      this.m_localisationArray[105] = "Oliver Miyashita";
      this.m_localisationArray[106] = "Andy Grove";
      this.m_localisationArray[107] = "Matt Golz";
      this.m_localisationArray[108] = "Twin Blades is a registered trade mark.";
      this.m_localisationArray[109] = "©2010 Press Start Studio. All rights reserved.";
      this.m_localisationArray[110] = "http://www.press-start-studio.com";
      this.m_localisationArray[111] = "info@press-start-studio.com";
      this.m_localisationArray[112] = "Grazie di aver giocato!";
      this.m_localisationArray[22] = "";
      this.m_localisationArray[23] = "";
      this.m_localisationArray[7] = "Storia";
      this.m_localisationArray[8] = "Sopravvivenza";
      this.m_localisationArray[217] = "Scegliere difficoltà";
      this.m_localisationArray[214] = "Difficoltà";
      this.m_localisationArray[215] = "Normale";
      this.m_localisationArray[216] = "Elevata";
      this.m_localisationArray[32] = "Scorrere verso l'alto o verso il basso per cambiar.\nUsare i cuori degli zombie per acquistare\nmiglioramenti al monastero.";
      this.m_localisationArray[41] = "Cambiare arma";
      this.m_localisationArray[40] = "Spostarsi e scegliere la zona per attaccare i non-morti.\nOsservare le icone relative all'infestazione.\nLa forza dei non-morti cresce\nall'aumentare del livello di infestazione.";
      this.m_localisationArray[42] = "Giorno";
      this.m_localisationArray[70] = "Tornare alla mappa";
      this.m_localisationArray[310] = "Acquistare o migliorare le armi e\nle abilità barattando i cuori di zombie.";
      this.m_localisationArray[311] = "Vuoi uscire dallo shop?";
      this.m_localisationArray[113] = "Suor Angelika, \n\nla città di Jalombe si trova di fronte ad una grave e infida minaccia; inoltre, con la crociata in corso in Occidente, disponiamo di mezzi molto limitati per contrastare questa aggressione.\n\nPerciò, ti invito a metterti agli ordini di Padre Richiardo, che si occupa della supervisione dell'area circostante. \nConfido che il tuo vecchio mentore sarà lieto di riunirsi con te. \n\nEd ora, affrettati, Suor Angelika. Desidero che tu parta nentro un'ora.";
      this.m_localisationArray[114] = "Inquisitore Del'Mar";
      this.m_localisationArray[115] = "Tutorial";
      this.m_localisationArray[116] = "Devo ripulire la città della piaga degli zombie.";
      this.m_localisationArray[117] = "Devo trovare il capo degli zombie. Forse dovrei perlustrare il mercato.";
      this.m_localisationArray[118] = "Ho individuato il loro capo. Ora devo sconfiggerlo.";
      this.m_localisationArray[119] = "Devo ancora ripulire la città della piaga degli zombie.";
      this.m_localisationArray[120] = "Devo trovare la missionaria scomparsa. Dovrei cercare nella foresta.";
      this.m_localisationArray[121] = "Non ho trovato traccia della missionaria né del capo degli zombie. Devo recarmi al monastero per farmi consigliare.";
      this.m_localisationArray[122] = "Devo andare alla scogliera al calar della notte.";
      this.m_localisationArray[123] = "Ho trovato la radice di tutto il male. Devo eliminarla!";
      this.m_localisationArray[124] = "Soccorso";
      this.m_localisationArray[125] = "Devo esplorare il vecchio cimitero.";
      this.m_localisationArray[126] = "Devo esplorare la vecchia cappella.";
      this.m_localisationArray[(int) sbyte.MaxValue] = "Dovrei tornare al monastero e chiedere a Padre Richiardo informazioni su questo campo magico che blocca la strada per la cripta.";
      this.m_localisationArray[128] = "Devo uccidere tutti i non-morti.";
      this.m_localisationArray[129] = "Vendicherò la morte di Magdalena e ucciderò questo spirito malvagio!";
      this.m_localisationArray[130] = "Sembra che la mia missione non sia ancora terminata.";
      this.m_localisationArray[133] = "Padre Richiardo";
      this.m_localisationArray[131] = "Angelika";
      this.m_localisationArray[132] = "Magdalena";
      this.m_localisationArray[134] = "Macellaio zombie";
      this.m_localisationArray[135] = "Negromante";
      this.m_localisationArray[136] = "Spirito malvagio";
      this.m_localisationArray[137] = "È bello rivederti, Suor Angelika. È passato molto tempo dall'ultima volta che ci hai onorato con la tua presenza; sono lieto di vedere che sei diventata una brava suora. Bene, vedo che l'Ordine ti ha affidato il ruolo di Redentrice di anime.";
      this.m_localisationArray[138] = "Sono sicuro che lo sai già: puoi reintegrare l'energia di redenzione mietendo le anime corrotte delle tue vittime con l'imponente falce di cui disponi.";
      this.m_localisationArray[139] = "Fanne buon uso nel nome del Signore e ricaccia gli zombie fuori dal monastero! Che Dio ti aiuti, Suor Angelika.";
      this.m_localisationArray[140] = "Ben fatto, Sorella. Ora dammi i loro cuori invasi dalla tenebra cosicché io possa purificarli dalla magia che li ha posseduti, cosa che ti consentirà di migliorare le tue armi e abilità.";
      this.m_localisationArray[141] = "Puoi venire a farmi visita quando vuoi. Ti accoglierò a braccia aperte. Sempre. Ma adesso devi affrettarti ad andare in città per estirpare la piaga dei non-morti prima che contagi ogni anima vivente.";
      this.m_localisationArray[142] = "La minaccia è stata sconfitta... almeno per ora.";
      this.m_localisationArray[143] = "Sta scendendo la notte. Un numero sempre più grande di non-morti sta sorgendo per attaccare la città.";
      this.m_localisationArray[144] = "Queste creature sono prive di intelligenza, eppure attaccano in gruppi... È possibile che ci sia qualcuno o qualcosa che li attrae qui?";
      this.m_localisationArray[145] = "Hanno trovato un modo per proteggersi dalle mie pallottole. Dovrei recarmi al monastero e procurarmi un'arma più potente.";
      this.m_localisationArray[146] = "I non-morti diventano più forti ogni giorno che passa. Non siamo di fronte ad un'innocua sollevazione, ma ad un'infestazione della massima gravità! Temo che dietro tutto questo possa esserci un potente spirito maligno.";
      this.m_localisationArray[147] = "Devo trovare chi li ha creati e mettere fine a questo terribile incubo.";
      this.m_localisationArray[148] = "Queste tracce portano a una creatura ben più grande. Deve essere... il loro capo!";
      this.m_localisationArray[151] = "Questa vile bestia non era abbastanza intelligente per radunare da sola tutti questi zombie. Devo trovare le menti che stanno dietro a tutto questo.";
      this.m_localisationArray[149] = "CARNE FREEESCA...";
      this.m_localisationArray[152] = "Per adesso la città è libera... Dovrei recarmi al monastero per fare rapporto a Padre Richiardo.";
      this.m_localisationArray[153] = "Hai combattuto con una creatura davvero grande. Le tue abilità sono molto migliorate, Angelika. Potresti essere l'orgoglio dell'Ordine, se solo...";
      this.m_localisationArray[154] = "Che c'è, Padre?";
      this.m_localisationArray[155] = "...\nDevi essere a conoscenza delle voci contraddittorie che stanno prendendo piede all'interno dell'Ordine in merito alla...";
      this.m_localisationArray[156] = " ...natura del tuo rapporto con Suor Magdalena.";
      this.m_localisationArray[157] = "...";
      this.m_localisationArray[158] = "Quali sono i tuoi ordini, Padre?";
      this.m_localisationArray[159] = "L'ultimo rapporto dalla nostra missionaria impegnata sul campo lo abbiamo ricevuto ormai una settimana fa. Voglio che tu perlustri la foresta che si trova vicino al luogo in cui è stata vista l'ultima volta.";
      this.m_localisationArray[160] = "Ricordati, Angelika, voglio che tu mi tenga informato su qualsiasi nuovo elemento. Non prendere nessuna iniziativa senza il mio esplicito consenso.";
      this.m_localisationArray[161] = "Non ho trovato traccia della missionaria né del capo degli zombie e ora la città è di nuovo sotto assedio!";
      this.m_localisationArray[162] = "Ho percepito una forte presenza magica diffondersi dalle scogliere quando è cominciato l'attacco. Sospetto che vi si stia svolgendo un qualche oscuro rituale, quindi voglio che tu esplori quell'area con il favore delle tenebre.";
      this.m_localisationArray[163] = "C'è una scia di odore che porta dritto nelle profondità della foresta... È avvolta nel sangue. Deve condurre al nascondiglio del loro capo.";
      this.m_localisationArray[164] = "Ah, tu devi essere la sua amante... Finalmente! Sono proprio contento di incontrarti di persona. Mi ha parlato molto bene di te. Sfortunatamente sono riuscito a capire solo la metà di quello che ha detto per via di tutto il sangue che sputava tossendo.";
      this.m_localisationArray[165] = "M... Mag...Magdalena?! Brutto mostro! Cosa le hai fatto?";
      this.m_localisationArray[166] = "...";
      this.m_localisationArray[167] = "Cosa le ho fatto io?! Mia cara, vuoi dire cosa lei ha fatto a se stessa... Non quasi dovuto neppure indurla in tentazione. Pensavo che potesse interessarti sapere che lei si è offerta a me... spontaneamente.";
      this.m_localisationArray[168] = "La sua difesa... sempre più debole... quando invoca... i morti.";
      this.m_localisationArray[169] = "Magdalena...";
      this.m_localisationArray[170] = "Questi segni scuri sul tuo corpo... Il tuo cuore mi è sempre stato fedele. Ero certa che non ci avresti mai traditi!";
      this.m_localisationArray[171] = "Tieni duro, Magdalena. Ti riporterò a casa.";
      this.m_localisationArray[172] = "Padre! Magdalena è caduta nelle mani di un negromante, devi salvarla!";
      this.m_localisationArray[173] = "Mi dispiace, Angelika... sembra che ormai abbia esalato il suo ultimo respiro...";
      this.m_localisationArray[174] = "Aspetta... ma questi sono... segni di corruzione?! Cosa pensavi, Angelika?! Non avresti mai dovuto portare qui questa lordura!";
      this.m_localisationArray[175] = "Lordura?! Come puoi dire questo? È una missionaria! Il suo corpo deve essere purificato!";
      this.m_localisationArray[176] = "E così sarà. Ma ora non è il momento di piangere per la tua compagna, Angelika.";
      this.m_localisationArray[177] = "Devi portare avanti la tua missione fino ad estirpare completamente la piaga dei non-morti. Altrimenti, nessuno può sapere cosa accadrà a questa regione e ai suoi abitanti.";
      this.m_localisationArray[178] = "Ma, Padre...";
      this.m_localisationArray[179] = "Basta! Il tuo lavoro non è ancora finito. Non temere, mi occuperò di tutti i preparativi per il funerale di Magdalena.";
      this.m_localisationArray[180] = "Il negromante che hai incontrato era solo un fantoccio posseduto da uno spirito malvagio appartenente ad una forza molto più grande. Devi trovare questa creatura e distruggerne la vera essenza.";
      this.m_localisationArray[181] = "Ma non so neppure dove cercare!";
      this.m_localisationArray[182] = "Uno dei miei esploratori ha individuato delle strane attività nel vecchio cimitero. Chissà che intenzioni ha questa creatura.";
      this.m_localisationArray[183] = "Una vecchia cappella si trova là più avanti. L'energia oscura che la circonda mi fa venire i brividi.";
      this.m_localisationArray[184] = "Santo cielo... Cosa ho fatto?! I morti stanno risorgendo a centinaia!";
      this.m_localisationArray[185] = "C'è una serie di scale che conducono giù alla cripta, ma io non posso entrare. Un forte campo magico mi blocca la strada. Dovrei tornare al monastero e chiedere a Padre Richiardo.";
      this.m_localisationArray[186] = "Questa oscura magia prende la sua forza dai non-morti. Devi liberare l'isola da tutti gli zombie. Solo il raggio sacro può distruggere questo scudo che ti impedisce di entrare nella cripta.";
      this.m_localisationArray[187] = "Non posso entrare nella cripta. Ho bisogno del raggio sacro per aprirmi la strada!";
      this.m_localisationArray[188] = "Finalmente la strada per la cripta è libera. Stai attento, spirito malvagio! Perché il braccio dell'Onnipotente sta per colpirti!";
      this.m_localisationArray[189] = "Indietro finché puoi, mortale! Torna strisciando alla tua breve vita e goditi i tuoi ultimi respiri prima che il mio padrone ti liberi dal fardello dell'esistenza.";
      this.m_localisationArray[190] = "Il mio spirito è più forte, demone. Rimangiati le tue speranze di dominazione, perché la tua ribellione finisce qui!";
      this.m_localisationArray[191] = "Sei in ritardo, mortale. Il risveglio è già cominciato. Niente può più fermarne l'avanzata ormai.";
      this.m_localisationArray[192] = "Cosa sta succedendo? La mia energia si sta esaurendo!";
      this.m_localisationArray[193] = "È giunto il momento del funerale di Magdalena.";
      this.m_localisationArray[194] = "È tutto finito, Padre. Anche se lo spirito ha menzionato qualcos'altro: un cap...";
      this.m_localisationArray[195] = "Indagheremo anche su questo, Angelika. Per ora dobbiamo procedere subito con il cadavere di Magdalena. Temo che la sua anima abbia superato il confine per la redenzione.";
      this.m_localisationArray[196] = "Cosa vuoi dire, Padre? Hai detto che avresti organizzato il suo funerale!";
      this.m_localisationArray[197] = "Hai visto i segni della corruzione con i tuoi stessi occhi, Angelika. Lei non mi ha lasciato scelta, devo purificare con il fuoco la sua carne corrotta dalla tentazione.";
      this.m_localisationArray[198] = "No, Padre! Lei si merita un vero funerale! Perché si è guadagnata un posto accanto al Signore!";
      this.m_localisationArray[199] = "La minaccia è troppo grande, Angelika. La mia decisione è irrevocabile. Ti lascerò un momento per dirle addio mentre la pira funeraria viene preparata.";
      this.m_localisationArray[200] = "Oh, Magdalena... come potrei mai sopportare di vivere senza di te?";
      this.m_localisationArray[201] = "Cosa devo fare?";
      this.m_localisationArray[202] = "1. La vita va avanti. E io mi sono votata all'Ordine.";
      this.m_localisationArray[203] = "2. Non posso sopportare di vivere senza Magdalena.";
      this.m_localisationArray[204] = "Riposa in pace, amore mio.";
      this.m_localisationArray[205] = "Non ci divideranno mai. Io resterò con te... per sempre...";
      this.m_localisationArray[206] = "";
      this.m_localisationArray[207] = "Epilogo";
      this.m_localisationArray[208] = "-Una questione in sospeso-";
      this.m_localisationArray[209] = "Inquisitore,\nLa contaminazione di Jalombe è stata fermata grazie ai coraggiosi sforzi di Suor Angelika.\nSebbene le circostanze della sua morte rimangano un peccato imperdonabile, consiglio di menzionare il suo nome tra quello dei Martiri Combattenti. I tuoi sospetti riguardo i semi della corruzione in Suor Magdalena erano fondati e, anche se non ho trovato prove di un oscuro rituale, la scomparsa del suo cadavere è alquanto preoccupante. Eppure non posso concedere le risorse necessarie per investigare oltre. Possa la tua ricerca nella pacificazione dell'Occidente risplendere sull'Ordine.";
      this.m_localisationArray[210] = "Padre Richiardo";
      this.m_localisationArray[211] = "Puoi continuare a giocare per aumentare il tuo punteggio.\nTutti i livelli sono sbloccati con la massima infestazione.";
      this.m_localisationArray[218] = "Uccidere gli zombie ti fa guadagnare i loro cuori. Scambia i cuori con armi e abilità a fine giornata.";
      this.m_localisationArray[219] = "Uccidere gli zombie ti fa guadagnare i loro cuori. Scambia i cuori con armi e abilità al monastero.";
      this.m_localisationArray[220] = "Gli zombie diventano più forti ogni giorno, perciò accertati di migliorare le tue abilità per aumentare le tue possibilità di sopravvivenza!";
      this.m_localisationArray[221] = "Gli zombie con l'armatura subiscono meno danni dalle pallottole.";
      this.m_localisationArray[222] = "Migliora le abilità della monaca prima di migliorare le sue armi per poter resistere fino all'ultimo giorno.";
      this.m_localisationArray[223] = "Saltare può aiutarti ad uscire da situazioni difficili, ma stai attento all'atterraggio perché dopo sarai vulnerabile per un breve periodo di tempo!";
      this.m_localisationArray[224] = "Sopravviverai per 31 giorni all'assalto furioso degli zombie?";
      this.m_localisationArray[225] = "Sapevi che c'è una seconda scheda nello shop dove puoi acquistare miglioramenti per la vita e l'energia della monaca?";
      this.m_localisationArray[226] = "Acquista l'abilità di falce aerea a fine giornata. Una volta acquisita l'abilità, premi \"Saltare\" e \"Falciare\" per eseguire la mossa della falce aerea.";
      this.m_localisationArray[227] = "Acquista l'abilità di falce aerea al monastero. Una volta acquisita l'abilità, premi \"Saltare\" e \"Falciare\" per eseguire la mossa della falce aerea.";
      this.m_localisationArray[228] = "Tenta di riunire gli zombie in grandi gruppi per usare in modo più efficiente l'energia delle tue munizioni.";
      this.m_localisationArray[229] = "Migliora i danni della falce per arrecare più danni agli zombie quando usi la falce.";
      this.m_localisationArray[230] = "Puoi eseguire una falce aerea combinata dopo aver saltato; acquista l'abilità nella seconda scheda dello shop!";
      this.m_localisationArray[231] = "Migliora la quantità di vita per aumentare la tua resistenza agli attacchi degli zombie.";
      this.m_localisationArray[232] = "Twin Blades è un gioco di sopravvivenza a punti: se muori, conservi le tue armi e gli obiettivi raggiunti, ma il tuo punteggio viene riportato a 0!";
      this.m_localisationArray[233] = "Quando usi il lancia-fiamme, dai una \"rapida scottata\" agli zombie, poi finiscili con la falce. In questo modo, puoi prosciugare la loro energia usando la tua arma per più tempo.";
      this.m_localisationArray[234] = "Migliorando i danni della falce, aumenti anche la quantità di energia recuperata con ogni falciata!";
      this.m_localisationArray[235] = "";
      this.m_localisationArray[236] = "Se ti piace lo stile grafico di Twin Blades, puoi trovare dei download gratuiti all'indirizzo www.press-start-studio.com";
      this.m_localisationArray[237] = "Stai attento all'avanzamento dell'infestazione degli zombie. Durante la notte, potrebbero essere infestate nuove zone. Se non liberi una zona, i non-morti diventeranno più forti.";
      this.m_localisationArray[238] = "Puoi tornare in qualsiasi momento al monastero e parlare con Padre Richiardo per migliorare le tue armi e le tue abilità.";
      this.m_localisationArray[239] = "Puoi avere delle informazioni sulla tua missione corrente premendo \"i\" sulla schermata della mappa.";
      this.m_localisationArray[240] = "Alcuni capi hanno degli scudi con vulnerabilità a rotazione; perciò prova con un'altra arma, se quella che stai usando non riesce a procurare nessun danno.";
      this.m_localisationArray[212] = "Sopravviverai per 31 giorni all'assalto furioso degli zombie?";
      this.m_localisationArray[213] = "La storia di Suor Angelika.";
      this.m_localisationArray[241] = "Acquista gioco completo";
      this.m_localisationArray[243] = "Disponibile anche su XBox Live Indie Games.";
      this.m_localisationArray[242] = "Obiettivi";
      this.m_localisationArray[244] = "Obiettivi sbloccate:";
      this.m_localisationArray[245] = "Pistolera";
      this.m_localisationArray[246] = "Uccidere 500 zombie usando solo l'arma base\no la mitragliatrice, in modalità sopravvivenza.";
      this.m_localisationArray[247] = "Hai ucciso 500 zombie usando solo l'arma base\no la mitragliatrice, in modalità sopravvivenza.";
      this.m_localisationArray[248] = "Inquisitore";
      this.m_localisationArray[249] = "Uccidere 500 zombie usando solo\nil lancia-fiamme, in modalità sopravvivenza.";
      this.m_localisationArray[250] = "Hai ucciso 500 zombie usando solo\nil lancia-fiamme, in modalità sopravvivenza.";
      this.m_localisationArray[251] = "Killer glaciale";
      this.m_localisationArray[252] = "Uccidere 500 zombie usando solo\nil lancia-ghiaccio, in modalità sopravvivenza.";
      this.m_localisationArray[253] = "Hai ucciso 500 zombie usando solo\nil lancia-ghiaccio, in modalità sopravvivenza.";
      this.m_localisationArray[254] = "Ricaduta radioattiva";
      this.m_localisationArray[(int) byte.MaxValue] = "Uccidere 500 zombie usando solo\nla pistola esplosiva, in modalità sopravvivenza.";
      this.m_localisationArray[256] = "Hai ucciso 500 zombie usando solo\nla pistola esplosiva, in modalità sopravvivenza.";
      this.m_localisationArray[257] = "Collera di Dio";
      this.m_localisationArray[258] = "Uccidere 1000 zombie usando solo\nil raggio sacro, in modalità sopravvivenza.";
      this.m_localisationArray[259] = "Hai ucciso 1000 zombie usando solo\nil raggio sacro, in modalità sopravvivenza.";
      this.m_localisationArray[260] = "Il falcone";
      this.m_localisationArray[261] = "Uccidere 500 zombie usando solo\nla falce aerea, in modalità sopravvivenza.";
      this.m_localisationArray[262] = "Hai ucciso 500 zombie usando solo\nla falce aerea, in modalità sopravvivenza.";
      this.m_localisationArray[263] = "Amami teneramente";
      this.m_localisationArray[264] = "Respingi 100 zombie in modalità sopravvivenza.";
      this.m_localisationArray[265] = "Hai respinto 100 zombie in modalità sopravvivenza.";
      this.m_localisationArray[266] = "La Sacra Armata";
      this.m_localisationArray[267] = "Acquistare tutti i possibili miglioramenti\nper le armi, in modalità sopravvivenza.";
      this.m_localisationArray[268] = "Hai acquistato tutti i possibili miglioramenti\nper le armi, in modalità sopravvivenza.";
      this.m_localisationArray[269] = "La Strada del Nunja";
      this.m_localisationArray[270] = "Acquistare tutti i possibili miglioramenti\nper la monaca, in modalità sopravvivenza.";
      this.m_localisationArray[271] = "Hai acquistato tutti i possibili miglioramenti\nper la monaca, in modalità sopravvivenza.";
      this.m_localisationArray[272] = "Settore liberato";
      this.m_localisationArray[273] = "Terminare il gioco in modalità sopravvivenza.";
      this.m_localisationArray[274] = "Hai terminato il gioco in modalità sopravvivenza.";
      this.m_localisationArray[275] = "Dura è la vita!";
      this.m_localisationArray[276] = "Terminare la modalità sopravvivenza difficile.";
      this.m_localisationArray[277] = "Hai terminato la modalità sopravvivenza difficile.";
      this.m_localisationArray[278] = "Monaca di 1a classe";
      this.m_localisationArray[279] = "Uccidere 100 zombie in modalità storia.";
      this.m_localisationArray[280] = "Hai ucciso 100 zombie in modalità storia.";
      this.m_localisationArray[281] = "Monaca caporale";
      this.m_localisationArray[282] = "Uccidere 500 zombie in modalità storia.";
      this.m_localisationArray[283] = "Hai ucciso 500 zombie in modalità storia.";
      this.m_localisationArray[284] = "Monaca sergente";
      this.m_localisationArray[285] = "Uccidere 1000 zombie in modalità storia.";
      this.m_localisationArray[286] = "Hai ucciso 1000 zombie in modalità storia.";
      this.m_localisationArray[287] = "La vendetta della carne";
      this.m_localisationArray[288] = "Uccidere il macellaio zombie.";
      this.m_localisationArray[289] = "Hai ucciso il macellaio zombie.";
      this.m_localisationArray[290] = "Ti sei deconcentrato?";
      this.m_localisationArray[291] = "Uccidere il negromante.";
      this.m_localisationArray[292] = "Hai ucciso il negromante.";
      this.m_localisationArray[293] = "Appuntamento all'inferno";
      this.m_localisationArray[294] = "Uccidere lo spirito maligno.";
      this.m_localisationArray[295] = "Hai ucciso lo spirito maligno.";
      this.m_localisationArray[296] = "Esauriti";
      this.m_localisationArray[297] = "Acquistare e migliorare al massimo\ntutte le armi e le abilità in modalità storia.";
      this.m_localisationArray[298] = "Hai acquistato e migliorato al massimo\ntutte le armi e le abilità in modalità storia.";
      this.m_localisationArray[299] = "Angelo guardiano";
      this.m_localisationArray[300] = "Terminare il gioco in modalità storia.";
      this.m_localisationArray[301] = "Hai terminato il gioco in modalità storia.";
      this.m_localisationArray[302] = "Dama di ferro";
      this.m_localisationArray[303] = "Terminare la modalità storia difficile.";
      this.m_localisationArray[304] = "Hai terminato la modalità storia difficile.";
      this.m_localisationArray[305] = "La versione completa di Twin Blades offre:";
      this.m_localisationArray[306] = "Una storia coinvolgente con combattimenti\nepici contro nemici potenti!";
      this.m_localisationArray[307] = "6 armi da fuoco diverse,\ndalla mitragliatrice alle esplosioni atomiche";
      this.m_localisationArray[308] = "Dozzine di animazioni differenti in funzione delle armi utilizzate";
      this.m_localisationArray[309] = "9 ambientazioni differenti da esplorare\nin momenti diversi del giorno e della notte.";
      this.m_localisationArray[312] = "HUD";
      this.m_localisationArray[313] = "Amici";
      this.m_localisationArray[314] = "Settimanale";
      this.m_localisationArray[315] = "Per ottenere delle conquiste, devi sbloccare il gioco completo.";
      this.m_localisationArray[316] = "Sbloccare";
      this.m_localisationArray[317] = "Non ora";
      this.m_localisationArray[318] = "Aggiornamento disponibile";
      this.m_localisationArray[319] = "Un aggiornamento è disponibile! Questo aggiornamento è necessario per connettersi a Xbox LIVE.\nAggiorna ora?";
      this.m_localisationArray[320] = "Server non disponibile";
      this.m_localisationArray[321] = "Il server di Twin Blades non è al momento disponibile. Riprova più tardi.";
      this.m_localisationArray[322] = "Musica";
      this.m_localisationArray[323] = "Un'altra musica è suonata, vi fermerà questa musica?";
    }

    private void Initialize_German()
    {
      this.m_localisationArray[0] = this.GetLanguageName(GameLocalisation.LanguageId.German);
      this.m_localisationArray[1] = "OK";
      this.m_localisationArray[2] = "Abbrechen";
      this.m_localisationArray[3] = "Ja";
      this.m_localisationArray[4] = "Nein";
      this.m_localisationArray[5] = "Ein";
      this.m_localisationArray[6] = "Aus";
      this.m_localisationArray[9] = "Neues Spiel";
      this.m_localisationArray[10] = "Weiterspielen";
      this.m_localisationArray[11] = "Hilfe und Optionen";
      this.m_localisationArray[12] = "So wird gespielt";
      this.m_localisationArray[13] = "Einstellungen";
      this.m_localisationArray[14] = "Über Twin Blades";
      this.m_localisationArray[15] = "Musik:";
      this.m_localisationArray[16] = "Effekte:";
      this.m_localisationArray[17] = "Bild umdrehen";
      this.m_localisationArray[18] = "Sprache:";
      this.m_localisationArray[19] = "Bestenlisten";
      this.m_localisationArray[20] = "Mitwirkende";
      this.m_localisationArray[21] = "Spiel verlassen";
      this.m_localisationArray[24] = "STORY MODUS";
      this.m_localisationArray[25] = "UBERLEBENSMODUS";
      this.m_localisationArray[26] = "OPTIONEN";
      this.m_localisationArray[27] = "EINSTELLUNGEN";
      this.m_localisationArray[28] = "BESTENLISTEN";
      this.m_localisationArray[29] = "ERFOLGE";
      this.m_localisationArray[30] = "Schiessen verbraucht Energie.\nEinen Zombie niedermähen bringt neue Energie ein.";
      this.m_localisationArray[31] = "Eine Aufwärts-/Abwärtsbewegung ermöglicht\neinen Waffenwechsel.\nMit den Herzen von Zombies können am Ende\ndes Tages Verbesserungen erworben werden.";
      this.m_localisationArray[33] = "Leben";
      this.m_localisationArray[34] = "Energie";
      this.m_localisationArray[35] = "Zombie-\nherzen";
      this.m_localisationArray[36] = "Waffentyp";
      this.m_localisationArray[37] = "Schiessen";
      this.m_localisationArray[38] = "Niedermähen";
      this.m_localisationArray[39] = "Sich fortbewegen\nund springen";
      this.m_localisationArray[43] = "Sind Sie sicher?";
      this.m_localisationArray[44] = "Dadurch wird die vorhandene Spielpartie gelöscht.";
      this.m_localisationArray[45] = "Nicht gespeichertes Fortschreiten geht verloren.";
      this.m_localisationArray[46] = "Wollen Sie das Spiel verlassen?";
      this.m_localisationArray[47] = "Verbesserungen für die Figur";
      this.m_localisationArray[48] = "Verbesserungen für Waffen";
      this.m_localisationArray[49] = "Maschinengewehr kaufen";
      this.m_localisationArray[50] = "Flammenwerfer kaufen";
      this.m_localisationArray[51] = "Eiswerfer kaufen";
      this.m_localisationArray[52] = "Sprengstoffwaffe kaufen";
      this.m_localisationArray[53] = "Heiligen-Strahl kaufen";
      this.m_localisationArray[54] = "Waffe verbessern";
      this.m_localisationArray[55] = "Maschinengewehr verbessern";
      this.m_localisationArray[56] = "Flammenwerfer verbessern";
      this.m_localisationArray[57] = "Eiswerfer verbessern";
      this.m_localisationArray[58] = "Sprengstoffwaffe verbessern";
      this.m_localisationArray[59] = "Heiligen-Strahl verbessern";
      this.m_localisationArray[60] = "Luft-Sense kaufen";
      this.m_localisationArray[61] = "Luft-Sense verbessern";
      this.m_localisationArray[62] = "Beschädigung der Sense verbessern";
      this.m_localisationArray[63] = "Sensenenergie verbessern";
      this.m_localisationArray[64] = "Lebensqualität verbessern";
      this.m_localisationArray[65] = "Energiespeicher verbessern";
      this.m_localisationArray[66] = "Pause";
      this.m_localisationArray[67] = "Zurück zum Spiel";
      this.m_localisationArray[68] = "Hilfe";
      this.m_localisationArray[69] = "Weiter zum Hauptmenü";
      this.m_localisationArray[71] = "Zurück";
      this.m_localisationArray[72] = "Lädt...";
      this.m_localisationArray[73] = "Press Start Studio";
      this.m_localisationArray[74] = "Programming";
      this.m_localisationArray[75] = "Fabien Kautzmann";
      this.m_localisationArray[76] = "Jean-François Moser";
      this.m_localisationArray[77] = "Graphics";
      this.m_localisationArray[78] = "Mickael Balloul";
      this.m_localisationArray[79] = "David Chauderlot";
      this.m_localisationArray[80] = "Music and sounds";
      this.m_localisationArray[81] = "Nicolas Signat";
      this.m_localisationArray[82] = "Business development & Marketing";
      this.m_localisationArray[83] = "Philippe Rapin";
      this.m_localisationArray[84] = "";
      this.m_localisationArray[85] = "MGS Mobile";
      this.m_localisationArray[86] = "Production Team";
      this.m_localisationArray[87] = "Travis Howland";
      this.m_localisationArray[88] = "Maja Persson";
      this.m_localisationArray[89] = "Matt Roberts";
      this.m_localisationArray[90] = "Cassie Townsend";
      this.m_localisationArray[91] = "Test Team";
      this.m_localisationArray[92] = "Chad Long";
      this.m_localisationArray[93] = "Chris Hanson";
      this.m_localisationArray[94] = "Justin Davis (VMC)";
      this.m_localisationArray[95] = "Joseph Sheehan (VMC)";
      this.m_localisationArray[96] = "Product Planning & Business Team";
      this.m_localisationArray[97] = "Cherie Lutz";
      this.m_localisationArray[98] = "Jeff Buckingham";
      this.m_localisationArray[99] = "Marketing";
      this.m_localisationArray[100] = "Kathy Richardson";
      this.m_localisationArray[101] = "Special Thanks";
      this.m_localisationArray[102] = "Matt Booty";
      this.m_localisationArray[103] = "David Boker";
      this.m_localisationArray[104] = "Michael Klucher";
      this.m_localisationArray[105] = "Oliver Miyashita";
      this.m_localisationArray[106] = "Andy Grove";
      this.m_localisationArray[107] = "Matt Golz";
      this.m_localisationArray[108] = "Twin Blades is a registered trade mark.";
      this.m_localisationArray[109] = "©2010 Press Start Studio. All rights reserved.";
      this.m_localisationArray[110] = "http://www.press-start-studio.com";
      this.m_localisationArray[111] = "info@press-start-studio.com";
      this.m_localisationArray[112] = "Danke fürs Spielen!";
      this.m_localisationArray[22] = "";
      this.m_localisationArray[23] = "";
      this.m_localisationArray[7] = "Story-Modus";
      this.m_localisationArray[8] = "Überlebensmodus";
      this.m_localisationArray[217] = "Schwierigkeitsgrad auswählen";
      this.m_localisationArray[214] = "Schwierigkeitsgrad:";
      this.m_localisationArray[215] = "Normal";
      this.m_localisationArray[216] = "Schwer";
      this.m_localisationArray[32] = "Eine Aufwärts-/Abwärtsbewegung ermöglicht\neinen Waffenwechsel.\nIm Kloster können Sie mit den erworbenen\nZombieherzen Verbesserungen kaufen.";
      this.m_localisationArray[41] = "Waffe wechseln";
      this.m_localisationArray[40] = "Bewegen Sie sich und wählen Sie die Zone\nfür den Kampf mit den Untoten aus.\nAchten Sie auf die Verseuchungssymbole:\nDie Stärke der Untoten\nnimmt mit der Verseuchungsstufe zu.";
      this.m_localisationArray[42] = "Tag";
      this.m_localisationArray[70] = "Zurück zur Karte";
      this.m_localisationArray[310] = "Erwerb oder Verbesserung von Waffen und\nFähigkeiten durch den Handel mit Zombieherzen.";
      this.m_localisationArray[311] = "Möchten Sie die Boutique wirklich verlassen?";
      this.m_localisationArray[113] = "Schwester Angelika, \n\ndie Stadt Jalombe wird vom Bösen bedroht und die Lage ist kritisch. Leider verfügen wir aufgrund des Kreuzzugs im Westen nur über sehr begrenzte Mittel, um den Angriff zurückzuschlagen.\n\nIch stelle Sie unter das Kommando von Vater Richiardo, der das Gebiet um die Stadt unter Kontrolle hält. Ihr ehemaliger Mentor wird über die Zusammenarbeit mit Ihnen sicher erfreut sein.\n\nNun aber schnell, Schwester Angelika. Sie müssen noch in dieser Stunde abreisen.";
      this.m_localisationArray[114] = "Inquisitor Del'Mar";
      this.m_localisationArray[115] = "Lernprogramm";
      this.m_localisationArray[116] = "Ich muss die Stadt von der Zombie-Plage befreien.";
      this.m_localisationArray[117] = "Ich muss den Chef der Zombies finden. Vielleicht sollte ich mich auf dem Marktplatz umsehen.";
      this.m_localisationArray[118] = "Ich habe den Zombie-Chef entdeckt. Ich muss ihn besiegen.";
      this.m_localisationArray[119] = "Ich muss die Stadt nach wie vor von der Zombie-Plage befreien.";
      this.m_localisationArray[120] = "Ich muss die verschwundene Missionarin finden. Ich sollte mich im Wald umsehen.";
      this.m_localisationArray[121] = "Keine Spur von der Missionarin oder vom Zombie-Meister. Ich sollte mir Rat im Kloster holen.";
      this.m_localisationArray[122] = "Bei Einbruch der Nacht muss ich zu den Klippen gehen.";
      this.m_localisationArray[123] = "Ich habe die Wurzel allen Übels entdeckt. Ich muss sie herausreißen!";
      this.m_localisationArray[124] = "Rettungsaktion";
      this.m_localisationArray[125] = "Ich muss den alten Friedhof erforschen.";
      this.m_localisationArray[126] = "Ich muss die alte Kapelle erforschen.";
      this.m_localisationArray[(int) sbyte.MaxValue] = "Ich sollte zum Kloster zurückkehren und Vater Richiardo zu dem magischen Feld befragen, das den Weg zur Gruft versperrt.";
      this.m_localisationArray[128] = "Ich muss alle Untoten töten.";
      this.m_localisationArray[129] = "Ich werde den Tod von Magdalena rächen und den bösen Geist töten!";
      this.m_localisationArray[130] = "Es sieht aus, als hätte ich meine Aufgabe noch nicht erfüllt.";
      this.m_localisationArray[133] = "Vater Richiardo";
      this.m_localisationArray[131] = "Angelika";
      this.m_localisationArray[132] = "Magdalena";
      this.m_localisationArray[134] = "Untoter Metzger";
      this.m_localisationArray[135] = "Totenbeschwörer";
      this.m_localisationArray[136] = "Böser Geist";
      this.m_localisationArray[137] = "Ich freue mich, Sie wiederzusehen, Schwester Angelika. Es ist ziemlich lange her, dass Sie uns mit Ihrer Anmut beglückt haben. Ich muss sagen, Sie haben sich zu einer prächtigen Nonne entwickelt. Nun, wie ich sehe, hat Ihnen der Orden einen Seelenerlöser anvertraut.";
      this.m_localisationArray[138] = "Wie Sie sicher wissen, können Sie die Energie des Erlösers erneuern, indem Sie Ihren Opfern mit Ihrer mächtigen Sense die befleckten Seelen entreißen.";
      this.m_localisationArray[139] = "Ziehen Sie guten Nutzen daraus im Namen des Herrn und vertreiben Sie die Zombies aus dem Kloster! Gott sei mit Ihnen, Schwester Angelika!";
      this.m_localisationArray[140] = "Eine gut geführte Aktion, Schwester. Geben Sie mir jetzt die befleckten Herzen, damit ich die darin enthaltene Magie reinigen kann. Sie können damit dann Ihre Waffen und Fähigkeiten verbessern.";
      this.m_localisationArray[141] = "Sie können mich jederzeit besuchen. Ich werde Sie stets mit offenen Armen empfangen. Aber jetzt müssen Sie eiligst in die Stadt und sie von der Plage der Untoten befreien, bevor sie auf alle lebenden Seelen übergreift.";
      this.m_localisationArray[142] = "Die Gefahr ist beseitigt… Zumindest für den Augenblick.";
      this.m_localisationArray[143] = "Die Nacht bricht herein. Neue Untote erheben sich, um die Stadt anzugreifen!";
      this.m_localisationArray[144] = "Diese Geschöpfe haben keinen Verstand und doch greifen sie in Gruppen an... Wäre es möglich, dass jemand oder etwas sie hierher lockt?";
      this.m_localisationArray[145] = "Sie haben einen Weg gefunden, sich vor meinen Kugeln zu schützen. Ich sollte zum Kloster eilen und mir eine stärkere Waffe holen.";
      this.m_localisationArray[146] = "Die Untoten werden von Tag zu Tag stärker. Das ist kein harmloser Aufstand, Schwester Angelika, wir haben es hier mit einer globalen Verseuchung zu tun! Ich befürchte, dass hinter all dem ein mächtiger, übelwollender Geist steht.";
      this.m_localisationArray[147] = "Ich muss ihren Schöpfer finden und diesem Albtraum ein Ende bereiten.";
      this.m_localisationArray[148] = "Diese Spuren führen zu einem weitaus größeren Lebewesen. Das muss… ihr Meister sein!";
      this.m_localisationArray[151] = "Dieses widerwärtige Biest war nicht intelligent genug, um die Zombies selbst herbeizurufen. Ich muss die Drahtzieher hinter all dem ausfindig machen.";
      this.m_localisationArray[149] = "FRISCHES FLEIIIIIIIIIIIIISCH...";
      this.m_localisationArray[152] = "Für den Augenblick ist die Stadt gesäubert... Ich sollte Vater Richiardo im Kloster von der Lage informieren.";
      this.m_localisationArray[153] = "Sie haben da ein beeindruckendes Geschöpf bekämpft. Ihre Fähigkeiten haben sich um einiges verbessert, Angelika. Sie wären der Stolz unseres Ordens, wenn nur...";
      this.m_localisationArray[154] = "Was ist, Vater?";
      this.m_localisationArray[155] = "...\nIch muss Sie über die widersprüchlichen Gerüchte informieren, die im Orden im Umlauf sind. Es geht um...";
      this.m_localisationArray[156] = "...die Art Ihrer Beziehung zu Schwester Magdalena.";
      this.m_localisationArray[157] = "...";
      this.m_localisationArray[158] = "Wie lauten Ihre Befehle, Vater?";
      this.m_localisationArray[159] = "Der letzte Bericht unserer Feldmissionarin reicht mittlerweile eine Woche zurück. Ich möchte, dass Sie den Wald durchsuchen, bei dem sie zuletzt gesehen wurde.";
      this.m_localisationArray[160] = "Vergessen Sie nicht, Angelika, mir sofort Bescheid zu geben, wenn Sie auf neue Elemente stoßen. Ergreifen Sie keine Initiative ohne meine ausdrückliche Zustimmung.";
      this.m_localisationArray[161] = "Ich habe weder eine Spur von der Missionarin noch vom Zombie-Meister gefunden und jetzt wird die Stadt erneut angegriffen!";
      this.m_localisationArray[162] = "Ich habe eine starke magische Präsenz auf den Klippen gespürt, als der Angriff begann. Ich vermute, dass dort eine Art finsteres Ritual abgehalten wird, und möchte, dass Sie die Zone im Schutz der Nacht auskundschaften.";
      this.m_localisationArray[163] = "Ich habe eine Fährte erfasst, die tief in den Wald hineinführt... Der Geruch ist blutgetränkt, die Fährte muss zum Schlupfwinkel des Zombie-Meisters führen!";
      this.m_localisationArray[164] = "Ah, du bist die Geliebte, nehme ich an? Ich freue mich, dich endlich kennen zu lernen, wirklich. Sie sprach in höchsten Tönen von dir. Leider konnte ich nur die Hälfte verstehen, da sie beim Sprechen literweise Blut gespuckt hat.";
      this.m_localisationArray[165] = "M...Mag...Magdalena?! Du Ungeheuer! Was hast du ihr angetan?";
      this.m_localisationArray[166] = "...";
      this.m_localisationArray[167] = "Was ich ihr angetan habe? Aber meine Liebe, sie hat sich alles selbst angetan. Es bedurfte keinerlei Verführungskünste. Es dürfte dich interessieren, dass sie sich mir dargeboten hat... freiwillig.";
      this.m_localisationArray[168] = "Seine Verteidigung... schwächer... wenn die Toten... sich erheben.";
      this.m_localisationArray[169] = "Magdalena...";
      this.m_localisationArray[170] = "Diese dunklen Male auf deinem Körper... Dein Herz war mir immer treu ergeben. Ich weiß, dass du uns niemals hättest verraten können!";
      this.m_localisationArray[171] = "Halte durch, Magdalena. Ich bringe dich nach Hause.";
      this.m_localisationArray[172] = "Vater! Magdalena ist einem Totenbeschwörer zum Opfer gefallen, Sie müssen sie retten!";
      this.m_localisationArray[173] = "Es tut mir leid, Angelika... Es sieht aus, als hätte sie bereits ihren letzten Atem ausgehaucht...";
      this.m_localisationArray[174] = "Warten Sie... Sind das... Verderbnismale?! Was haben Sie sich dabei gedacht, Angelika! Sie hätten diesen Abschaum niemals hierher bringen dürfen!";
      this.m_localisationArray[175] = "Abschaum?! Wie können Sie so etwas sagen? Sie ist eine Missionarin! Ihr Körper muss gereinigt werden!";
      this.m_localisationArray[176] = "Und so soll es auch geschehen. Aber jetzt ist nicht der richtige Augenblick, um über den Verlust Ihrer Gefährtin zu trauern, Angelika.";
      this.m_localisationArray[177] = "Sie müssen Ihre Pflicht erfüllen, bis die Untoten ausgelöscht sind. Andernfalls weiß Gott allein, was aus dieser Gegend und ihren Einwohnern werden wird.";
      this.m_localisationArray[178] = "Aber, Vater...";
      this.m_localisationArray[179] = "Genug! Ihre Arbeit ist noch nicht getan. Keine Sorge, ich kümmere mich um die Vorbereitungen für das Begräbnis von Magdalena.";
      this.m_localisationArray[180] = "Der Totenbeschwörer, dem Sie begegnet sind, war lediglich eine Marionette im Dienst eines bösen Geistes, der einer wesentlich größeren Macht entspringt. Sie müssen diese Kreatur ausfindig machen und bis zum letzten Wesenszug zerstören.";
      this.m_localisationArray[181] = "Aber ich weiß nicht einmal, wo ich suchen soll!";
      this.m_localisationArray[182] = "Einer meiner Späher hat im alten Friedhof eine beunruhigende Aktivität beobachtet. Wer weiß, welches Ziel dieses Geschöpf verfolgt.";
      this.m_localisationArray[183] = "Da vorn ist eine alte Kapelle. Die düstere Energie, die sie umgibt, verschafft mir Gänsehaut.";
      this.m_localisationArray[184] = "Oh nein... Was habe ich getan? Die Toten erheben sich zu Hunderten!";
      this.m_localisationArray[185] = "Eine Treppenflucht führt hinunter zur Gruft, aber ich komme nicht hinein. Ein starkes magisches Feld versperrt mir den Weg. Ich sollte zum Kloster zurückgehen und Vater Richiardo befragen.";
      this.m_localisationArray[186] = "Die schwarze Magie erhält ihre Kraft von den Untoten. Sie müssen die Insel von allen Untoten befreien. Nur der Heilige-Strahl kann das Schutzfeld zerstören, das Ihnen den Eintritt in die Gruft versperrt.";
      this.m_localisationArray[187] = "Ich kann die Gruft nicht betreten. Ich brauche den Heiligen-Strahl, um mir freien Eintritt zu verschaffen!";
      this.m_localisationArray[188] = "Der Weg in die Gruft ist jetzt frei. Böse Geister, habt acht: Bald schon wird der Arm des Allmächtigen euch niederschmettern!";
      this.m_localisationArray[189] = "Kehre um, solange es noch geht, Sterbliche. Krieche zurück zu deiner kurzen Existenz und genieße deine letzten Atemzüge, bevor mein Meister dich von den Schmerzen des Daseins erlöst.";
      this.m_localisationArray[190] = "Mein Geist ist stärker als du glaubst, Dämon. Begrabe deine Herrschaftsansprüche, der Aufstand endet hier und jetzt!";
      this.m_localisationArray[191] = "Du kommst zu spät, Sterbliche, das Erwachen hat bereits begonnen. Nichts kann den Aufstand jetzt noch aufhalten.";
      this.m_localisationArray[192] = "Was passiert? Meine Energie geht verloren!";
      this.m_localisationArray[193] = "Es ist Zeit für Magdalenas Begräbnis.";
      this.m_localisationArray[194] = "Es ist vollbracht, Vater. Allerdings erwähnte der Geist noch etwas anderes: einen Meist...";
      this.m_localisationArray[195] = "Das hat Zeit, Angelika. Jetzt müssen wir uns um Magdalenas Leichnam kümmen. Ich fürchte, ihre Seele ist verloren, ohne Hoffnung auf Erlösung.";
      this.m_localisationArray[196] = "Was meinen Sie damit, Vater? Sie haben doch gesagt, sie würden sich um das Begräbnis kümmern!";
      this.m_localisationArray[197] = "Sie haben die Verderbnismale mit eigenen Augen gesehen, Angelika. Das lässt mir keine Wahl, ich muss das befleckte Fleisch im Feuer reinigen.";
      this.m_localisationArray[198] = "Vater, nein! Sie verdient ein Begräbnis. Sie hat sich einen Platz beim Herrn erworben!";
      this.m_localisationArray[199] = "Die Gefahr ist zu groß, Angelika, meine Entscheidung ist unwiderruflich. Ich lasse Ihnen einen Augenblick, damit Sie Abschied nehmen können, solange das Bestattungsfeuer vorbereitet wird. ";
      this.m_localisationArray[200] = "Oh, Magdalena... Wie soll ich nur ohne dich leben?";
      this.m_localisationArray[201] = "Was soll ich tun?";
      this.m_localisationArray[202] = "1. Das Leben geht weiter. Ich habe mich mit Leib und Seele in den Dienst\ndes Ordens gestellt.";
      this.m_localisationArray[203] = "2. Ich kann ein Leben ohne Magdalena nicht ertragen.";
      this.m_localisationArray[204] = "Ruhe in Frieden, meine Geliebte.";
      this.m_localisationArray[205] = "Niemand kann uns trennen. Ich werde immer bei dir sein, für alle Ewigkeit...";
      this.m_localisationArray[206] = "";
      this.m_localisationArray[207] = "Epilog";
      this.m_localisationArray[208] = "- Unerledigte Aufgaben -";
      this.m_localisationArray[209] = "Inquisitor,\ndie Verseuchung von Jalombe konnte aufgehalten werden, dank des heldenhaften Einsatzes von Schwester Angelika. Zwar bilden die Umstände ihres Todes eine unverzeihliche Sünde, dennoch sollte man auch ihren Namen mit den Kämpfern und Märtyrern nennen. Ihre Mutmaßungen hinsichtlich der Saat der Verderbnis in Schwester Magdalena waren begründet, und obwohl ich keinerlei Anzeichen für ein finsteres Ritual entdecken konnte, finde ich das Verschwinden ihres Leichnams überaus beunruhigend. Aber mir fehlen Mittel und Leute, um weitere Nachforschungen anzustellen. Möge Ihre Mission der Pazifizierung im Westen ruhmreich für unseren Orden verlaufen.";
      this.m_localisationArray[210] = "Vater Richiardo";
      this.m_localisationArray[211] = "Sie können weiterspielen, um Ihren Punktestand zu verbessern.\nAlle Levels sind mit maximaler Verseuchungsstufe freigegeben.";
      this.m_localisationArray[218] = "Töten Sie Zombies und verdienen Sie Zombieherzen. Tauschen Sie die Herzen am Ende des Tages gegen Waffen und Fähigkeiten ein.";
      this.m_localisationArray[219] = "Töten Sie Zombies und verdienen Sie Zombieherzen. Tauschen Sie die Herzen im Kloster gegen Waffen und Fähigkeiten ein.";
      this.m_localisationArray[220] = "Die Zombies werden von Tag zu Tag widerstandsfähiger. Sie sollten deshalb Ihre Fähigkeiten verbessern, um Ihre Überlebenschancen zu erhöhen!";
      this.m_localisationArray[221] = "Gepanzerte Zombies erleiden weniger Schaden durch Kugeln.";
      this.m_localisationArray[222] = "Verbessern Sie die Fähigkeiten der Nonne vor einer Verbesserung der Waffen, wenn Sie bis zum letzten Tag überleben möchten.";
      this.m_localisationArray[223] = "Mit einem Sprung können Sie sich aus einer verzwickten Lage befreien, aber aufgepasst auf die Landung: Sie sind dann für kurze Zeit verletzlich!";
      this.m_localisationArray[224] = "Ob Sie 31 Tage eines ununterbrochenen Zombie-Ansturms überleben?";
      this.m_localisationArray[225] = "Wussten Sie, dass in der Boutique eine zweite Registerkarte zur Verfügung steht, auf der Sie Verbesserungen für die Gesundheit und Energie der Nonne kaufen können?";
      this.m_localisationArray[226] = "Kaufen Sie die Luft-Sense am Ende des Tages. Drücken Sie nach dem Kauf auf 'Springen' und 'Niedermähen', um die Luft-Sense einzusetzen.";
      this.m_localisationArray[227] = "Kaufen Sie die Luft-Sense im Kloster. Drücken Sie nach dem Kauf auf 'Springen' und 'Niedermähen', um die Luft-Sense einzusetzen.";
      this.m_localisationArray[228] = "Versuchen Sie, die Zombies in großen Rudeln zu gruppieren, um optimalen Nutzen aus Ihrer Munition zu ziehen.";
      this.m_localisationArray[229] = "Verbessern Sie die Beschädigungskraft der Sense, um den Zombies mit größeren Schaden zuzufügen.";
      this.m_localisationArray[230] = "Nach dem Springen können Sie eine Luft-Sensen-Kombo ausführen; kaufen Sie diese Fähigkeit auf der zweiten Registerkarte der Boutique!";
      this.m_localisationArray[231] = "Verbessern Sie die Menge an Leben, um den Zombie-Angriffen besser standhalten zu können.";
      this.m_localisationArray[232] = "Twin Blades ist ein Survival-Game mit Punkteerwerb: Wenn Sie sterben, behalten Sie Ihre Waffen und kommen weiter im Spiel, Ihr Punktstand wird jedoch auf 0 zurückgesetzt!";
      this.m_localisationArray[233] = "Wenn Sie den Flammenwerfer einsetzen, sollten Sie die Zombies nur kurz 'verkohlen' und sie dann mit der Sense erledigen. Auf diese Weise entziehen Sie ihnen ebenfalls ihre Energie, können Ihre Waffe jedoch länger verwenden.";
      this.m_localisationArray[234] = "Wenn Sie die Beschädigungskraft der Sense verbessern, wird dadurch auch die Menge an Energie erhöht, die Sie mit jedem Sensenschlag erwerben!";
      this.m_localisationArray[235] = "Sie können während des Spiels Ihre eigene Musik über das Dashboard abspielen!";
      this.m_localisationArray[236] = "Wenn Ihnen der Grafikstil von Twin Blades gefällt, finden Sie Freebie-Downloads unter www.press-start-studio.com";
      this.m_localisationArray[237] = "Behalten Sie das Fortschreiten der Zombie-Verseuchung im Auge. Nachts werden ggf. neue Zonen verseucht. Wenn Sie eine Zone nicht säubern, werden die Untoten stärker.";
      this.m_localisationArray[238] = "Sie können jederzeit zum Kloster zurückkehren und mit Vater Richiardo sprechen, um Ihre Waffen und Fähigkeiten zu verbessern.";
      this.m_localisationArray[239] = "Sie erhalten Informationen zu Ihrer derzeitigen Mission, wenn Sie auf dem Kartenbildschirm die Taste 'I' drücken.";
      this.m_localisationArray[240] = "Manche Bosse sind mit wechselnder Anfälligkeit ausgestattet; versuchen Sie es mit einer anderen Waffe, wenn die, die Sie gerade verwenden, keinen Schaden zufügt.";
      this.m_localisationArray[212] = "Ob Sie 31 Tage eines ununterbrochenen Zombie-Ansturms überleben?";
      this.m_localisationArray[213] = "Die Geschichte von Schwester Angelika";
      this.m_localisationArray[241] = "Spiele-Vollversion kaufen";
      this.m_localisationArray[243] = "Auch erhältlich auf XBox Live Indie Games.";
      this.m_localisationArray[242] = "Erfolge";
      this.m_localisationArray[244] = "Entsperrter Erfolg:";
      this.m_localisationArray[245] = "Pistolera";
      this.m_localisationArray[246] = "500 Zombies im Überlebensmodus nur mit\nder Standardwaffe oder dem Maschinengewehr töten.";
      this.m_localisationArray[247] = "Sie haben 500 Zombies im Überlebensmodus nur mit\nder Standardwaffe oder dem Maschinengewehr getötet.";
      this.m_localisationArray[248] = "Inquisitor";
      this.m_localisationArray[249] = "500 Zombies im Überlebensmodus\nnur mit dem Flammenwerfer töten.";
      this.m_localisationArray[250] = "Sie haben 500 Zombies im Überlebensmodus\nnur mit dem Flammenwerfer getötet.";
      this.m_localisationArray[251] = "Eiskalter Killer";
      this.m_localisationArray[252] = "500 Zombies im Überlebensmodus\nnur mit dem Eiswerfer töten.";
      this.m_localisationArray[253] = "Sie haben 500 Zombies im Überlebensmodus\nnur mit dem Eiswerfer getötet.";
      this.m_localisationArray[254] = "Radioaktiver Niederschlag";
      this.m_localisationArray[(int) byte.MaxValue] = "500 Zombies im Überlebensmodus\nnur mit der Sprengstoffwaffe töten.";
      this.m_localisationArray[256] = "Sie haben 500 Zombies im Überlebensmodus\nnur mit der Sprengstoffwaffe getötet.";
      this.m_localisationArray[257] = "Der Zorn Gottes";
      this.m_localisationArray[258] = "1.000 Zombies im Überlebensmodus\nnur mit dem Heiligen Strahl töten.";
      this.m_localisationArray[259] = "Sie haben 1.000 Zombies im Überlebensmodus\nnur mit dem Heiligen Strahl getötet.";
      this.m_localisationArray[260] = "Der Falke";
      this.m_localisationArray[261] = "500 Zombies im Überlebensmodus\nnur mit der Luft-Sense töten.";
      this.m_localisationArray[262] = "Sie haben 500 Zombies im Überlebensmodus\nnur mit der Luft-Sense getötet.";
      this.m_localisationArray[263] = "Ich habe dich zum Töten gern";
      this.m_localisationArray[264] = "Werfen Sie 100 Zombies im Überlebensmodus zurück.";
      this.m_localisationArray[265] = "Sie haben 100 Zombies im Überlebensmodus zurückgeworfen.";
      this.m_localisationArray[266] = "Die Armee Gottes";
      this.m_localisationArray[267] = "Alle möglichen Verbesserungen für\nalle Waffen im Überlebensmodus erwerben.";
      this.m_localisationArray[268] = "Sie haben alle möglichen Verbesserungen für\nalle Waffen im Überlebensmodus erworben.";
      this.m_localisationArray[269] = "Der Weg des Nunja";
      this.m_localisationArray[270] = "Alle Fähigkeiten der Nonne im Überlebensmodus verbessern.";
      this.m_localisationArray[271] = "Sie haben alle Fähigkeiten der Nonne im Überlebensmodus verbessert.";
      this.m_localisationArray[272] = "Sektor gesäubert";
      this.m_localisationArray[273] = "Überlebensmodus beenden.";
      this.m_localisationArray[274] = "Sie haben den Überlebensmodus beendet.";
      this.m_localisationArray[275] = "Das Leben ist hart";
      this.m_localisationArray[276] = "Überlebensmodus mit dem Härte-Challenge beenden.";
      this.m_localisationArray[277] = "Sie haben den Überlebensmodus mit dem Härte-Challenge beendet.";
      this.m_localisationArray[278] = "Nonne 1. Klasse";
      this.m_localisationArray[279] = "100 Zombies im Geschichtsmodus töten.";
      this.m_localisationArray[280] = "Sie haben 100 Zombies im Geschichtsmodus getötet.";
      this.m_localisationArray[281] = "Unteroffiziersnonne";
      this.m_localisationArray[282] = "500 Zombies im Geschichtsmodus töten.";
      this.m_localisationArray[283] = "Sie haben 500 Zombies im Geschichtsmodus getötet.";
      this.m_localisationArray[284] = "Feldwebelnonne";
      this.m_localisationArray[285] = "1.000 Zombies im Geschichtsmodus töten.";
      this.m_localisationArray[286] = "Sie haben 1.000 Zombies im Geschichtsmodus getötet.";
      this.m_localisationArray[287] = "Die Rache des Fleisches";
      this.m_localisationArray[288] = "Den untoten Metzger töten.";
      this.m_localisationArray[289] = "Sie haben den untoten Metzger getötet.";
      this.m_localisationArray[290] = "Konzentrationsschwierigkeiten?";
      this.m_localisationArray[291] = "Den Totenbeschwörer vernichten.";
      this.m_localisationArray[292] = "Sie haben den Totenbeschwörer vernichtet.";
      this.m_localisationArray[293] = "Rendez-vous in der Hölle";
      this.m_localisationArray[294] = "Den diabolischen Geist töten.";
      this.m_localisationArray[295] = "Sie haben den diabolischen Geist getötet.";
      this.m_localisationArray[296] = "Ausverkauf";
      this.m_localisationArray[297] = "Alle Waffen und Fähigkeiten bis zum Maximum\nim Geschichtsmodus erwerben und verbessern.";
      this.m_localisationArray[298] = "Sie haben alle Waffen und Fähigkeiten bis zum Maximum\nim Geschichtsmodus erworben und verbessert.";
      this.m_localisationArray[299] = "Schutzengel";
      this.m_localisationArray[300] = "Spiel im Geschichtsmodus beenden.";
      this.m_localisationArray[301] = "Sie haben das Spiel im Geschichtsmodus beendet.";
      this.m_localisationArray[302] = "Eiserne Lady";
      this.m_localisationArray[303] = "Geschichtsmodus mit Härte-Challenge beenden.";
      this.m_localisationArray[304] = "Sie haben den Geschichtsmodus mit dem Härte-Challenge beendet.";
      this.m_localisationArray[305] = "Die Vollversion von Twin Blades umfasst Folgendes:";
      this.m_localisationArray[306] = "Eine atemberaubende, fesselnde Story mit\nepischen Schlachten gegen mächtige Feinde!";
      this.m_localisationArray[307] = "6 verschiedene Waffentypen\nvom Maschinengewehr bis zur Nuklearbombe.";
      this.m_localisationArray[308] = "Dutzende Animationen je nach verwendeter Artillerie.";
      this.m_localisationArray[309] = "9 zu verschiedenen Tages- und\nNachtzeiten zu erkundende Umgebungen.";
      this.m_localisationArray[312] = "HUD:";
      this.m_localisationArray[313] = "Freunde";
      this.m_localisationArray[314] = "Wöchentlich";
      this.m_localisationArray[315] = "Für Erfolge im Spiel muss die Vollversion freigesetzt werden.";
      this.m_localisationArray[316] = "Freischalten";
      this.m_localisationArray[317] = "Nicht jetzt";
      this.m_localisationArray[318] = "Update verfügbar";
      this.m_localisationArray[319] = "Ein Update ist verfügbar! Dieses Update ist erforderlich, um auf Xbox LIVE verbinden.\nUpdate jetzt?";
      this.m_localisationArray[320] = "Server nicht verfügbar";
      this.m_localisationArray[321] = "Der Twin Blades Server ist zurzeit nicht verfügbar. Versuchen Sie es später noch einmal.";
      this.m_localisationArray[322] = "Musik";
      this.m_localisationArray[323] = "Eine andere Musik wird gespielt, wollen Sie diese Musik abschalten?";
    }

    private void Initialize_Spanish()
    {
      this.m_localisationArray[0] = this.GetLanguageName(GameLocalisation.LanguageId.Spanish);
      this.m_localisationArray[1] = "OK";
      this.m_localisationArray[2] = "Cancelar";
      this.m_localisationArray[3] = "Sí";
      this.m_localisationArray[4] = "No";
      this.m_localisationArray[5] = "Activar";
      this.m_localisationArray[6] = "Desactivar";
      this.m_localisationArray[9] = "Empezar partida nueva";
      this.m_localisationArray[10] = "Continuar";
      this.m_localisationArray[11] = "Ayuda y opciones";
      this.m_localisationArray[12] = "Cómo se juega";
      this.m_localisationArray[13] = "Configuración";
      this.m_localisationArray[14] = "Acerca de Twin Blades";
      this.m_localisationArray[15] = "Música:";
      this.m_localisationArray[16] = "Efectos:";
      this.m_localisationArray[17] = "Invertir pantalla:";
      this.m_localisationArray[18] = "Idioma:";
      this.m_localisationArray[19] = "Marcadores";
      this.m_localisationArray[20] = "Créditos";
      this.m_localisationArray[21] = "Salir del juego";
      this.m_localisationArray[7] = "Historia";
      this.m_localisationArray[8] = "Supervivencia";
      this.m_localisationArray[24] = "HISTORIA";
      this.m_localisationArray[25] = "SUPERVIVENCIA";
      this.m_localisationArray[26] = "OPCIONES";
      this.m_localisationArray[27] = "CONFIGURACION";
      this.m_localisationArray[28] = "MARCADORES";
      this.m_localisationArray[29] = "LOGROS";
      this.m_localisationArray[30] = "Disparar consume energía.\nAtravesar con la guadaña para recuperar energía.";
      this.m_localisationArray[31] = "Deslizar arriba o abajo para cambiar\nel tipo de pistola.\nUsar corazones de zombis para comprar\nmejoras al fin del día.";
      this.m_localisationArray[33] = "Vida";
      this.m_localisationArray[34] = "Energía";
      this.m_localisationArray[35] = "Corazones\nde zombis";
      this.m_localisationArray[36] = "Tipo de pistola";
      this.m_localisationArray[37] = "Disparar";
      this.m_localisationArray[38] = "Atravesar";
      this.m_localisationArray[39] = "Moverse\ny saltar";
      this.m_localisationArray[43] = "¿Está seguro?";
      this.m_localisationArray[44] = "Así borrará la partida actual";
      this.m_localisationArray[45] = "Se perderá la progresión no guardada.";
      this.m_localisationArray[46] = "¿Salir del juego?";
      this.m_localisationArray[47] = "Mejoras del personaje";
      this.m_localisationArray[48] = "Mejoras de las armas";
      this.m_localisationArray[49] = "Comprar la ametralladora";
      this.m_localisationArray[50] = "Comprar el lanzallamas";
      this.m_localisationArray[51] = "Comprar la pistola lanza hielo";
      this.m_localisationArray[52] = "Comprar la pistola explosiva";
      this.m_localisationArray[53] = "Comprar el rayo sagrado";
      this.m_localisationArray[54] = "Mejorar la pistola";
      this.m_localisationArray[55] = "Mejorar la ametralladora";
      this.m_localisationArray[56] = "Mejorar el lanzallamas";
      this.m_localisationArray[57] = "Mejorar la pistola lanza hielo";
      this.m_localisationArray[58] = "Mejorar la pistola explosiva";
      this.m_localisationArray[59] = "Mejorar el rayo sagrado";
      this.m_localisationArray[60] = "Comprar habilidad de guadaña aérea";
      this.m_localisationArray[61] = "Mejorar habilidad de guadaña aérea";
      this.m_localisationArray[62] = "Mejorar daño de la guadaña";
      this.m_localisationArray[63] = "Mejorar recuperación de energía por la guadaña";
      this.m_localisationArray[64] = "Mejorar la cantidad de vida";
      this.m_localisationArray[65] = "Mejorar la cantidad de energía";
      this.m_localisationArray[66] = "Pausa";
      this.m_localisationArray[67] = "Volver al juego";
      this.m_localisationArray[68] = "Ayuda";
      this.m_localisationArray[69] = "Volver al menú principal";
      this.m_localisationArray[71] = "Volver";
      this.m_localisationArray[72] = "Cargando...";
      this.m_localisationArray[73] = "Press Start Studio";
      this.m_localisationArray[74] = "Programming";
      this.m_localisationArray[75] = "Fabien Kautzmann";
      this.m_localisationArray[76] = "Jean-François Moser";
      this.m_localisationArray[77] = "Graphics";
      this.m_localisationArray[78] = "Mickael Balloul";
      this.m_localisationArray[79] = "David Chauderlot";
      this.m_localisationArray[80] = "Music and sounds";
      this.m_localisationArray[81] = "Nicolas Signat";
      this.m_localisationArray[82] = "Business development & Marketing";
      this.m_localisationArray[83] = "Philippe Rapin";
      this.m_localisationArray[84] = "";
      this.m_localisationArray[85] = "MGS Mobile";
      this.m_localisationArray[86] = "Production Team";
      this.m_localisationArray[87] = "Travis Howland";
      this.m_localisationArray[88] = "Maja Persson";
      this.m_localisationArray[89] = "Matt Roberts";
      this.m_localisationArray[90] = "Cassie Townsend";
      this.m_localisationArray[91] = "Test Team";
      this.m_localisationArray[92] = "Chad Long";
      this.m_localisationArray[93] = "Chris Hanson";
      this.m_localisationArray[94] = "Justin Davis (VMC)";
      this.m_localisationArray[95] = "Joseph Sheehan (VMC)";
      this.m_localisationArray[96] = "Product Planning & Business Team";
      this.m_localisationArray[97] = "Cherie Lutz";
      this.m_localisationArray[98] = "Jeff Buckingham";
      this.m_localisationArray[99] = "Marketing";
      this.m_localisationArray[100] = "Kathy Richardson";
      this.m_localisationArray[101] = "Special Thanks";
      this.m_localisationArray[102] = "Matt Booty";
      this.m_localisationArray[103] = "David Boker";
      this.m_localisationArray[104] = "Michael Klucher";
      this.m_localisationArray[105] = "Oliver Miyashita";
      this.m_localisationArray[106] = "Andy Grove";
      this.m_localisationArray[107] = "Matt Golz";
      this.m_localisationArray[108] = "Twin Blades is a registered trade mark.";
      this.m_localisationArray[109] = "©2010 Press Start Studio. All rights reserved.";
      this.m_localisationArray[110] = "http://www.press-start-studio.com";
      this.m_localisationArray[111] = "info@press-start-studio.com";
      this.m_localisationArray[112] = "Gracias por haber jugado!";
      this.m_localisationArray[22] = "";
      this.m_localisationArray[23] = "";
      this.m_localisationArray[7] = "Historia";
      this.m_localisationArray[8] = "Supervivencia";
      this.m_localisationArray[217] = "Elegir nivel de dificultad";
      this.m_localisationArray[214] = "Nivel de dificultad:";
      this.m_localisationArray[215] = "Normal";
      this.m_localisationArray[216] = "Difícil";
      this.m_localisationArray[32] = "Deslizar arriba o abajo para.\nUse corazones de zombi para comprar mejoras\nen el monasterio.";
      this.m_localisationArray[41] = "Cambiar de arma";
      this.m_localisationArray[40] = "Avance y elija la zona para atacar a los no muertos.\nNo pierda de vista los iconos de infestación,\nla fuerza de los no muertos aumenta con el nivel\nde infestación.";
      this.m_localisationArray[42] = "Día";
      this.m_localisationArray[70] = "Volver al mapa";
      this.m_localisationArray[310] = "Comprar o mejorar armas y capacidades\na cambio de corazones de zombis.";
      this.m_localisationArray[311] = "¿Realmente desea salir de la tienda?";
      this.m_localisationArray[113] = "Sor Angelika, \n\nLa ciudad de Jalombe se enfrenta a una perversa amenaza pero, \ncon la actual cruzada en el Oeste, los medios para frenarla son \nmuy escasos.\n\nEstás a las órdenes del Padre Richiardo, supervisor de zona. \nTu antiguo mentor estará encantado de reunirse contigo. \n\nAhora, date prisa Sor Angelika. Quiero que partas de inmediato.";
      this.m_localisationArray[114] = "Inquisidor Del'Mar";
      this.m_localisationArray[115] = "Tutorial";
      this.m_localisationArray[116] = "Tengo que limpiar la ciudad de la plaga de zombis.";
      this.m_localisationArray[117] = "Debo encontrar al líder de los zombis. Me voy a pasar por la plaza del mercado.";
      this.m_localisationArray[118] = "He descubierto a su líder, tengo que derrotarle.";
      this.m_localisationArray[119] = "Todavía tengo que limpiar la ciudad de la plaga de zombis.";
      this.m_localisationArray[120] = "Debo encontrar a la misionera perdida. Voy a buscar en el bosque.";
      this.m_localisationArray[121] = "No hay rastro de la misionera, ni del maestro de los zombis.Voy al monasterio a pedir consejo.";
      this.m_localisationArray[122] = "Tengo que ir al acantilado al anochecer.";
      this.m_localisationArray[123] = "He encontrado la fuente de todo mal. ¡Debo eliminarla!";
      this.m_localisationArray[124] = "Rescate";
      this.m_localisationArray[125] = "Tengo que explorar el viejo cementerio.";
      this.m_localisationArray[126] = "Tengo que explorar la vieja capilla.";
      this.m_localisationArray[(int) sbyte.MaxValue] = "Debería regresar al monasterio y preguntar al Padre Richiardo acerca de este escudo mágico que bloquea el camino a la cripta.";
      this.m_localisationArray[128] = "Debo matar a todos los no muertos.";
      this.m_localisationArray[129] = "¡Vengaré la muerte de Magdalena matando a este espíritu maligno!";
      this.m_localisationArray[130] = "Al parecer, mi deber no termina aquí.";
      this.m_localisationArray[133] = "Padre Richiardo";
      this.m_localisationArray[131] = "Angelika";
      this.m_localisationArray[132] = "Magdalena";
      this.m_localisationArray[134] = "Carnicero no muerto";
      this.m_localisationArray[135] = "Nigromante";
      this.m_localisationArray[136] = "Espíritu maligno";
      this.m_localisationArray[137] = "¡Qué alegría verte de nuevo Sor Angelika!Hace tiempo desde la última vez que nos honraste con tu presenciay me llena de orgullo decir que eres una gran monja.Veo que la Orden te ha encomendado un Redentor de Almas.";
      this.m_localisationArray[138] = "Seguro que ya sabes que la energía del Redentor se repone arrancando las almas impuras de tus víctimas con tu implacable guadaña.";
      this.m_localisationArray[139] = "¡Úsala bien en nombre del Señor para expulsar a los zombisdel monasterio! Ve con Dios Sor Angelika";
      this.m_localisationArray[140] = "Bien hecho, mi Sor.Ahora entrégame sus corazones impuros para purificar su magiay poder así mejorar tus armas y capacidades.";
      this.m_localisationArray[141] = "Puedes visitarme cuando quieras. Te recibiré siempre con los brazos abiertos. Pero ahora debes ir corriendo al pueblo y erradicar la plaga de los no muertos antes de que se extienda a toda alma viviente.";
      this.m_localisationArray[142] = "La amenaza ha sido eliminada... por ahora.";
      this.m_localisationArray[143] = "Cae la noche.¡Surgen más no muertos para atacar la ciudad!";
      this.m_localisationArray[144] = "Estas criaturas no tienen conciencia, sin embargo, atacan en grupos... ¿Podría ser que alguien o algo les esté atrayendo hacia aquí?";
      this.m_localisationArray[145] = "Han encontrado un modo de protegerse de mi balas.Debería ir al monasterio para conseguir un arma más potente.";
      this.m_localisationArray[146] = "Los no muertos se hacen más fuertes cada día.No es una revuelta pacífica, Sor Angelika, nos enfrentamos a unainfestación total. Temo que un espíritu maligno esté detrás de todo esto.";
      this.m_localisationArray[147] = "Tengo que encontrar a su creador y poner fin a esta pesadilla.";
      this.m_localisationArray[148] = "Estas huellas conducen a un ser muy superior. ¡Debe ser... su maestro!";
      this.m_localisationArray[151] = "Esta bestia sin cabeza no ha podido reunir a todos estos zombis ella sola. Tengo que encontrar al cerebro detrás de todo esto.";
      this.m_localisationArray[149] = "CARNE FREEESCA...";
      this.m_localisationArray[152] = "La ciudad está limpia… por ahora.Debería regresar al monasterio a informar al Padre Richiardo.";
      this.m_localisationArray[153] = "Te has enfrentado a una criatura gigantesca. Tus habilidades han mejorado mucho Angelika, bien podrías ser el orgullo de la Orden si no fuera por...";
      this.m_localisationArray[154] = "¿Qué es Padre?";
      this.m_localisationArray[155] = "...\nYa conoces los confusos rumores que circulan por toda la Orden respecto a la...";
      this.m_localisationArray[156] = "......Naturaleza de tu relación con Sor Magdalena.";
      this.m_localisationArray[157] = "...";
      this.m_localisationArray[158] = "¿Cuáles son sus órdenes Padre?";
      this.m_localisationArray[159] = "Ha pasado una semana desde el último informe de nuestra misionera sobre el terreno. Quiero que investigues el bosque cercano donde fue vista por última vez.";
      this.m_localisationArray[160] = "Recuerda Angelika, debes informar nada más descubrasalgo nuevo.No quiero que actúes sin mi autorización expresa.";
      this.m_localisationArray[161] = "No he encontrado rastro de la misionera, ni del maestro de los zombis, ¡y ahora la ciudad está siendo atacada de nuevo!";
      this.m_localisationArray[162] = "He sentido una fuerte presencia mágica procedente de los acantilados al iniciarse el ataque. Creo que es algún tipo de ritual oscuro. Deseo que explores la zona al abrigo de la noche.";
      this.m_localisationArray[163] = "Hay un odor que conduce al corazón del bosque... Está envuelto en sangre... ¡debe conducir a la guarida de su maestro!";
      this.m_localisationArray[164] = "Ah, ¿supongo que eres la amante?Encantado de conocerte en persona. Habló muy bien de ti. Por desgracia, sólo llegué a comprender una parte de lo que decíadebido a los cubos de sangre que escupía.";
      this.m_localisationArray[165] = "¡¿M...Mag...Magdalena?! ¡Monstruo! ¿Qué le has hecho?";
      this.m_localisationArray[166] = "...";
      this.m_localisationArray[167] = "¿Qué le he hecho? Querida, se lo hizo ella a sí misma. No tuve que tentarla demasiado. Te interesará saber que se ofreció a mí... voluntariamente";
      this.m_localisationArray[168] = "Su defensa... más débil...tras invocar a...los muertos.";
      this.m_localisationArray[169] = "Magdalena...";
      this.m_localisationArray[170] = "Estas marcas oscuras sobre tu cuerpo...Tu corazón siempre me ha sido fiel, ¡nunca nos traicionarías!";
      this.m_localisationArray[171] = "Aguanta Magdalena. Te llevaré de vuelta a casa.";
      this.m_localisationArray[172] = "¡Padre! Magdalena ha caído en las manos de un nigromante,¡tiene que salvarla!";
      this.m_localisationArray[173] = "Lo siento, Angelika... parece haber expirado su último aliento...";
      this.m_localisationArray[174] = "Espera ¡¿son éstas... marcas de corrupción?! ¡En qué estabas pensando Angelika! ¡Nunca deberías haber traído esta basura aquí!";
      this.m_localisationArray[175] = "¡¿Basura?! ¿Cómo puede decir eso? ¡Es una misionera! ¡Su cuerpo debe ser purificado!";
      this.m_localisationArray[176] = "Y así será.Pero ahora no es el momento de llorar por tu compañera, Angelika.";
      this.m_localisationArray[177] = "Debes cumplir con tu deber hasta que los no muertos queden erradicados. Si no lo haces, quién sabe lo que será de esta región y sus habitantes.";
      this.m_localisationArray[178] = "Pero, Padre...";
      this.m_localisationArray[179] = "¡Ya basta! Tu trabajo todavía no ha terminado. No temas, me encargaré de preparar el funeral de Magdalena.";
      this.m_localisationArray[180] = "El nigromante con el que te encontraste era una mera marioneta poseída por un espíritu maligno perteneciente a una fuerza muy superior. Debes encontrar a esta criatura y destruir su esencia.";
      this.m_localisationArray[181] = "¡Pero no sé por dónde empezar a buscar!";
      this.m_localisationArray[182] = "Uno de mis exploradores detectó ciertas actividades extrañas en el viejo cementerio. ¡Quién sabe qué es lo que persigue la criatura!";
      this.m_localisationArray[183] = "Justo al frente hay una vieja capilla.La oscura energía que la rodea me pone los pelos de punta.";
      this.m_localisationArray[184] = "Pero… ¿qué he hecho? ¡Centenares de muertos salen de sus tumbas!";
      this.m_localisationArray[185] = "Hay unas escaleras que conducen a la cripta,pero no puedo entrar. Un potente escudo mágico me lo impide. Debería regresar al monasterio y pedir ayuda al Padre Richiardo.";
      this.m_localisationArray[186] = "Esta magia negra toma su fuerza de los no muertos,debes liberar a la isla de todos los no muertos. Sólo el rayo sagrado puede destruir este escudo que te impide la entrada a la cripta.";
      this.m_localisationArray[187] = "No puedo acceder a la cripta. ¡Necesito el rayo sagrado para abrirme paso!";
      this.m_localisationArray[188] = "La ruta hacia la cripta ya está libre.¡Advertencia a los malignos, pronto la furia del todopoderoso caerá sobre vosotros!";
      this.m_localisationArray[189] = "Huye mientras puedas, mortal.Regresa a tu efímera existencia y disfruta de tu último alientoantes de que mi maestro te libere de la pena de vivir.";
      this.m_localisationArray[190] = "Mi espíritu es más fuerte que todo eso, demonio.Trágate tus deseos de dominación pues tu invasión toca a su fin.";
      this.m_localisationArray[191] = "Llegas tarde, mortal, el despertar ya ha comenzado.Ya nada puede impedir su progresión.";
      this.m_localisationArray[192] = "¿Qué ocurre? ¡Estoy perdiendo toda mi energía!";
      this.m_localisationArray[193] = "Ha llegado el momento del funeral de Magdalena.";
      this.m_localisationArray[194] = "Misión cumplida, Padre. No obstante, el espíritu mencionó algo más: un maest...";
      this.m_localisationArray[195] = "Lo investigaremos, Angelika.Ahora debemos encargarnos del cuerpo de Magdalena.Temo que su alma se encuentra ya más allá de toda redención.";
      this.m_localisationArray[196] = "¿Qué quiere decir, Padre? ¿Usted dijo que se encargaría del funeral?";
      this.m_localisationArray[197] = "Has visto las marcas de corrupción con tus propios ojos Angelika.No me ha dejado otra opción: debo purificar su carne con fuego.";
      this.m_localisationArray[198] = "¡Padre, no!Merece un funeral digno; se ha ganado un lugar junto al Señor.";
      this.m_localisationArray[199] = "La amenaza es demasiado seria Angelika, mi decisión es irrevocable. Te concedo unos instantes para que te despidas mientras se prepara la pira funeraria.";
      this.m_localisationArray[200] = "Oh Magdalena... ¿cómo podré soportar vivir sin ti?";
      this.m_localisationArray[201] = "¿Qué debo hacer?";
      this.m_localisationArray[202] = "1. La vida sigue. He dedicado mi vida a la orden.";
      this.m_localisationArray[203] = "2. No puedo soportar vivir sin Magdalena.";
      this.m_localisationArray[204] = "Que descanses en paz, mi amor.";
      this.m_localisationArray[205] = "Nunca conseguirán separarnos. Estaré contigo, siempre...";
      this.m_localisationArray[206] = "";
      this.m_localisationArray[207] = "Epílogo";
      this.m_localisationArray[208] = "-Cabo suelto-";
      this.m_localisationArray[209] = "Inquisidor,\nGracias a los esfuerzos valientes de Sor Angelika, Jalombe se ha salvado de la contaminación.\nA pesar de lo indigno y pecaminoso de su muerte, recomiendo que su nombre figure en la lista de Mártires Luchadores. Sus sospechas sobre las marcas de corrupción en Sor Magdalena eran fundadas; aunque no existen pruebas de un ritual oscuro, la desaparición de su cuerpo resulta bastante perturbadora. Sin embargo, no dispongo de más recursos para seguir investigando.\nQue Dios bendiga su misión de pacificación en el Oeste.";
      this.m_localisationArray[210] = "Padre Richiardo";
      this.m_localisationArray[211] = "Puede seguir jugando para incrementar su puntuación.\nTodos los niveles están desbloqueados con infestación máxima.";
      this.m_localisationArray[218] = "Matar zombis le permite ganar corazones de zombi. Canjee corazones por armas y capacidades al final del día.";
      this.m_localisationArray[219] = "Matar zombis le permite ganar corazones de zombi. Canjee corazones por armas y capacidades en el monasterio.";
      this.m_localisationArray[220] = "Los zombis se hacen más fuertes cada día, mejore sus capacidades para incrementar sus oportunidades de supervivencia.";
      this.m_localisationArray[221] = "Los zombis con armadura sufren menos daños por las balas.";
      this.m_localisationArray[222] = "Mejore las capacidades de la monja antes de mejorar sus armas si quiere aguantar hasta el último día.";
      this.m_localisationArray[223] = "Saltar le permite salir de situaciones difíciles, pero tenga cuidado dónde aterriza porque será vulnerable durante un rato.";
      this.m_localisationArray[224] = "¿Sobrevivirá a 31 días de ataque de los zombis?";
      this.m_localisationArray[225] = "¿Sabía que hay una segunda pestaña en la tienda donde puede comprar mejoras para la salud y la energía de la monja?";
      this.m_localisationArray[226] = "Compre la capacidad de guadaña aérea al final del día. Una vez comprada la capacidad, pulse \"saltar\" y \"rebanar\" para utilizar la guadaña aérea.";
      this.m_localisationArray[227] = "Compre la capacidad de guadaña aérea en el monasterio. Una vez comprada la capacidad, pulse \"saltar\" y \"rebanar\" para utilizar la guadaña aérea.";
      this.m_localisationArray[228] = "Intente agrupar a los zombis en grupos numerosos para un uso más eficaz de su munición.";
      this.m_localisationArray[229] = "Mejore la capacidad de su guadaña para infligir más daño a los zombis al utilizar su guadaña.";
      this.m_localisationArray[230] = "Puede realizar un combo de guadaña aérea después de saltar; ¡compre esta capacidad en la segunda pestaña de la tienda!";
      this.m_localisationArray[231] = "Mejore la cantidad de vida para ser más resistente a los ataques de los zombis.";
      this.m_localisationArray[232] = "Twin Blades es un juego de supervivencia por puntos: si muere, conservará sus armas y su avance, pero su puntuación se pondrá a 0.";
      this.m_localisationArray[233] = "Al utilizar el lanzallamas, haga un \"quemado rápido\" de los zombis y luego remátelos con la guadaña. De este modo, podrá extraer su energía mientras usa su arma durante más tiempo.";
      this.m_localisationArray[234] = "Mejorar el daño causado por la guadaña también incrementa la cantidad de energía recuperada con cada golpe de guadaña.";
      this.m_localisationArray[235] = "Podrá reproducir su propia música en el panel mientras juega.";
      this.m_localisationArray[236] = "Si le gusta el estilo visual de Twin Blades, podrá encontrar extras para descargar en www.press-start-studio.com";
      this.m_localisationArray[237] = "No pierda de vista el avance de la infestación, durante la noche los zombis podrían infestar nuevas zonas. Si no limpia una zona, los no muertos se harán más fuertes.";
      this.m_localisationArray[238] = "Puede regresar en cualquier momento al monasterio, y hablar con el Padre Richiardo para mejorar sus armas y capacidades.";
      this.m_localisationArray[239] = "Puede obtener información acerca de su misión actual pulsando “i” en la pantalla de mapa.";
      this.m_localisationArray[240] = "Algunos jefes tienen escudos con vulnerabilidad alterna; pruebe otra arma, si la que está utilizando no causa daños.";
      this.m_localisationArray[212] = "¿Sobrevivirá a 31 días de ataque de los zombis?";
      this.m_localisationArray[213] = "La historia de Sor Angelika.";
      this.m_localisationArray[241] = "Comprar juego completo";
      this.m_localisationArray[243] = "También disponible para XBox Live Indie Games.";
      this.m_localisationArray[242] = "Logros";
      this.m_localisationArray[244] = "Logros desbloqueada:";
      this.m_localisationArray[245] = "Pistolera";
      this.m_localisationArray[246] = "Mate a 500 zombis utilizando sólo la pistola básica\no la ametralladora en modo de supervivencia.";
      this.m_localisationArray[247] = "Ha matado a 500 zombis utilizando sólo la pistola básica\no la ametralladora en modo de supervivencia.";
      this.m_localisationArray[248] = "Inquisidora";
      this.m_localisationArray[249] = "Mate a 500 zombis utilizando sólo\nel lanzallamas en modo de supervivencia.";
      this.m_localisationArray[250] = "Ha matado a 500 zombis utilizando sólo\nel lanzallamas en modo de supervivencia.";
      this.m_localisationArray[251] = "Asesina a sangre fría";
      this.m_localisationArray[252] = "Mate a 500 zombis utilizando sólo\nla pistola lanza hielo en modo de supervivencia.";
      this.m_localisationArray[253] = "Ha matado a 500 zombis utilizando solo\nla pistola lanza hielo en modo de supervivencia.";
      this.m_localisationArray[254] = "Lluvias radioactivas";
      this.m_localisationArray[(int) byte.MaxValue] = "Mate a 500 zombis utilizando sólo\nla pistola explosiva en modo de supervivencia.";
      this.m_localisationArray[256] = "Ha matado a 500 zombis utilizando sólo\nla pistola explosiva en modo de supervivencia.";
      this.m_localisationArray[257] = "La ira de Dios";
      this.m_localisationArray[258] = "Mate a 1.000 zombis utilizando sólo\nel rayo sagrado en modo de supervivencia.";
      this.m_localisationArray[259] = "Ha matado a 1.000 zombis utilizando sólo\nel rayo sagrado en modo de supervivencia.";
      this.m_localisationArray[260] = "El Halcón";
      this.m_localisationArray[261] = "Mate a 500 zombis utilizando sólo\nla guadaña aérea en modo de supervivencia.";
      this.m_localisationArray[262] = "Ha matado a 500 zombis utilizando sólo\nla guadaña aérea en modo de supervivencia.";
      this.m_localisationArray[263] = "Amor fatal";
      this.m_localisationArray[264] = "Deshágase de 100 zombis en modo de supervivencia.";
      this.m_localisationArray[265] = "Se ha deshecho de 100 zombis en modo de supervivencia.";
      this.m_localisationArray[266] = "La Sagrada Armada";
      this.m_localisationArray[267] = "Compre todas las mejoras posibles para\ntodas las armas en modo de supervivencia.";
      this.m_localisationArray[268] = "Ha adquirido todas las mejoras posibles\npara todas las armas en modo de supervivencia.";
      this.m_localisationArray[269] = "Al estilo del Nunja";
      this.m_localisationArray[270] = "Compre todas las mejoras posibles\npara su Monja en modo de supervivencia.";
      this.m_localisationArray[271] = "Ha comprado todas las mejoras posibles\npara su Monja en modo de supervivencia.";
      this.m_localisationArray[272] = "Sector despejado";
      this.m_localisationArray[273] = "Finalice el juego en modo de supervivencia.";
      this.m_localisationArray[274] = "Ha finalizado el juego en modo de supervivencia.";
      this.m_localisationArray[275] = "La vida es dura";
      this.m_localisationArray[276] = "Finalice el modo de supervivencia\ncon un nivel alto de dificultad.";
      this.m_localisationArray[277] = "Ha finalizado el modo de supervivencia\ncon un nivel alto de dificultad.";
      this.m_localisationArray[278] = "Monja de Primera";
      this.m_localisationArray[279] = "Mate a 100 zombis en modo historia.";
      this.m_localisationArray[280] = "Ha matado a 100 zombis en modo historia.";
      this.m_localisationArray[281] = "Monja Cabo";
      this.m_localisationArray[282] = "Mate a 500 zombis en modo historia.";
      this.m_localisationArray[283] = "Ha matado a 500 zombis en modo historia.";
      this.m_localisationArray[284] = "Monja Sargento";
      this.m_localisationArray[285] = "Mate a 1.000 zombis en modo historia.";
      this.m_localisationArray[286] = "Ha matado a 1.000 zombis en modo historia.";
      this.m_localisationArray[287] = "La venganza de la carne";
      this.m_localisationArray[288] = "Mate al carnicero muerto viviente.";
      this.m_localisationArray[289] = "Ha matado al carnicero muerto viviente.";
      this.m_localisationArray[290] = "¿Distraído?";
      this.m_localisationArray[291] = "Mate al nigromante.";
      this.m_localisationArray[292] = "Ha matado al nigromante.";
      this.m_localisationArray[293] = "Nos vemos en el infierno";
      this.m_localisationArray[294] = "Mate al espíritu del mal.";
      this.m_localisationArray[295] = "Ha matado al espíritu del mal.";
      this.m_localisationArray[296] = "Rebajas";
      this.m_localisationArray[297] = "Compre y mejore todas las armas y capacidades\nhasta el máximo en modo historia.";
      this.m_localisationArray[298] = "Ha comprado y mejorado todas las armas y capacidades\nhasta el máximo en modo historia.";
      this.m_localisationArray[299] = "Ángel de la guarda";
      this.m_localisationArray[300] = "Finalice el juego en modo historia.";
      this.m_localisationArray[301] = "Ha finalizado el juego en modo historia.";
      this.m_localisationArray[302] = "La Dama de Hierro";
      this.m_localisationArray[303] = "Finalice el modo historia\nen el nivel alto de dificultad.";
      this.m_localisationArray[304] = "Ha finalizado el modo historia\nen el nivel alto de dificultad.";
      this.m_localisationArray[305] = "En la versión completa de Twin Blades encontrará:";
      this.m_localisationArray[306] = "¡Una absorbente historia con\népicos combates contra potentes enemigos!";
      this.m_localisationArray[307] = "6 armas arrojadizas distintas,\ndesde la ametralladora a explosiones atómicas.";
      this.m_localisationArray[308] = "Docenas de animaciones diferentes según las armas utilizadas.";
      this.m_localisationArray[309] = "9 entornos diferentes que descubrir\nen distintos momentos del día o de la noche.";
      this.m_localisationArray[312] = "HUD:";
      this.m_localisationArray[313] = "Amigos";
      this.m_localisationArray[314] = "Semanal";
      this.m_localisationArray[315] = "Para ir consiguiendo éxitos, debe desbloquear la versión completa del juego.";
      this.m_localisationArray[316] = "Desbloquear";
      this.m_localisationArray[317] = "Ahora no";
      this.m_localisationArray[318] = "Disponible la actualización de";
      this.m_localisationArray[319] = "Una actualización está disponible! Esta actualización es necesaria para conectarse a Xbox LIVE.\nActualizar ahora?";
      this.m_localisationArray[320] = "Servidor no disponible";
      this.m_localisationArray[321] = "El servidor de Twin Blades no está disponible en este momento. Vuelve a intentarlo más tarde.";
      this.m_localisationArray[322] = "Música";
      this.m_localisationArray[323] = "Otra música es interpretada, ¿quieres poner fin a esta música?";
    }

    public enum LanguageId
    {
      English,
      French,
      Italian,
      German,
      Spanish,
    }

    public enum TextId
    {
      Language_Name,
      Ok,
      Cancel,
      Yes,
      No,
      On,
      Off,
      StoryMode,
      SurvivalMode,
      StartNewGame,
      ContinueGame,
      HelpAndOptions,
      HowToPlay,
      Settings,
      About,
      Music,
      Sound,
      FlipScreen,
      Language,
      Highscores,
      Credits,
      Exit,
      OpenFeint,
      GMG,
      StoryModeBig,
      SurvivalModeBig,
      OptionsBig,
      SettingsBig,
      HighscoresBig,
      AchievementsBig,
      Help_Page1,
      Help_Page2,
      Help_Page2Story,
      Help_Life,
      Help_Energy,
      Help_Heart,
      Help_GunType,
      Help_Fire,
      Help_Slash,
      Help_Move,
      Help_Map,
      Help_ChangeWeapons,
      Day,
      AskAreYouSure,
      OverwriteGame,
      ConfirmProgressErased,
      ExitConfirmation,
      EON_BuyCharacterUpgrades,
      EON_BuyWeaponUpgrades,
      EON_BuyMachineGun,
      EON_BuyFlameThrower,
      EON_BuyIceGun,
      EON_BuyExplosiveGun,
      EON_BuyHolyBeam,
      EON_UpgradeGun,
      EON_UpgradeMachineGun,
      EON_UpgradeFlameThrower,
      EON_UpgradeIceGun,
      EON_UpgradeExplosiveGun,
      EON_UpgradeHolyBeam,
      EON_BuyAirSlash,
      EON_UpgradeAirSlash,
      EON_UpgradeSlashDamage,
      EON_UpgradeSlashManaDrain,
      EON_UpgradeMaxLife,
      EON_UpgradeMana,
      PauseGame,
      ResumeGame,
      Help,
      BackToMainMenu,
      BackToMap,
      Back,
      Loading,
      PressStartStudio,
      Programming,
      ProgrammerName1,
      ProgrammerName2,
      Graphism,
      GraphistName1,
      GraphistName2,
      MusicAndSound,
      MusicAndSoundName,
      SalesMarketing,
      SalesMarketingName,
      Publisher,
      PublisherName,
      PublisherProduction,
      PublisherProductionName1,
      PublisherProductionName2,
      PublisherProductionName3,
      PublisherProductionName4,
      PublisherTest,
      PublisherTestName1,
      PublisherTestName2,
      PublisherTestName3,
      PublisherTestName4,
      PublisherBusiness,
      PublisherBusinessName1,
      PublisherBusinessName2,
      PublisherMarketing,
      PublisherMarketingName,
      PublisherThanks,
      PublisherThanksName1,
      PublisherThanksName2,
      PublisherThanksName3,
      PublisherThanksName4,
      PublisherThanksName5,
      PublisherThanksName6,
      TwinBladesTM,
      PSSCopyrights,
      PSSWeb,
      PSSSupport,
      ThankYou,
      StoryIntro,
      StoryIntroTheresa,
      MissionName_Tutorial,
      MissionName_1,
      MissionName_2,
      MissionName_3,
      MissionName_4,
      MissionName_5,
      MissionName_5b,
      MissionName_6,
      MissionName_7,
      MissionName_8,
      MissionName_9,
      MissionName_9b,
      MissionName_9c,
      MissionName_10,
      MissionName_11,
      MissionName_End,
      Dialog_Name_Nun1,
      Dialog_Name_Nun2,
      Dialog_Name_Priest,
      Dialog_Name_Butcher,
      Dialog_Name_Necro1,
      Dialog_Name_Necro2,
      Dialog_Tutorial_Father_1,
      Dialog_Tutorial_Father_1a,
      Dialog_Tutorial_Father_1b,
      Dialog_Tutorial_Father_2,
      Dialog_Tutorial_Father_3,
      Dialog_Mission_Level_Clear,
      Dialog_Mission1_Nun_NightFall,
      Dialog_Mission1_Nun_Morning,
      Dialog_Mission1_Nun_GoShop,
      Dialog_Mission1_Father_Talk,
      Dialog_Mission2_Nun_FindBoss,
      Dialog_Mission2_Nun_BossFound,
      Dialog_Mission3_Butcher_Talk,
      Dialog_Mission3_Nun_BossMissed,
      Dialog_Mission3_Nun_BossKilled,
      Dialog_Mission4_Nun_Clear,
      Dialog_Mission4_Father_Talk,
      Dialog_Mission4_Nun_Talk,
      Dialog_Mission4_Father_Talk2,
      Dialog_Mission4_Father_Talk3,
      Dialog_Mission4_Nun_Talk2,
      Dialog_Mission4_Nun_Talk3,
      Dialog_Mission4_Father_Worried,
      Dialog_Mission4_Father_Report,
      Dialog_Mission5_Nun_NotFound,
      Dialog_Mission5_Father_SenseMagic,
      Dialog_Mission6_Nun_MagicNearby,
      Dialog_Mission7_Boss_Talk1,
      Dialog_Mission7_Nun_Talk1,
      Dialog_Mission7_Sister_Talk1,
      Dialog_Mission7_Boss_Talk2,
      Dialog_Mission7_Sister_Talk_Hint,
      Dialog_Mission7_Nun_Talk2,
      Dialog_Mission7_Nun_Talk3,
      Dialog_Mission8_Nun_HoldOnMagdalena,
      Dialog_Mission8_Nun_MagIsDead,
      Dialog_Mission8_Father_Rest,
      Dialog_Mission8_Father_Restb,
      Dialog_Mission8_Nun_MagIsDead2,
      Dialog_Mission8_Father_Rest2,
      Dialog_Mission8_Father_Rest3,
      Dialog_Mission8_Nun_Talk,
      Dialog_Mission8_Father_Spirit,
      Dialog_Mission8_Father_Spirit2,
      Dialog_Mission8_Nun_Where,
      Dialog_Mission8_Father_Graveyard,
      Dialog_Mission9_Nun_Chapel,
      Dialog_Mission9_Nun_Dead_Rising,
      Dialog_Mission9_Nun_Crypt_Stair,
      Dialog_Mission9_Father_Crypt_Magic,
      Dialog_Mission10_Nun_Need_HolyBeam,
      Dialog_Mission10_Nun_Enter_Crypt,
      Dialog_Mission12_Boss_Talk,
      Dialog_Mission12_Nun_Swallow,
      Dialog_Mission12_Boss_Talk2,
      Dialog_Mission12_Nun_Talk,
      Dialog_MissionEnd_Nun_Go_Funerals,
      Dialog_MissionEnd_Nun_Talk,
      Dialog_MissionEnd_Priest_Talk,
      Dialog_MissionEnd_Nun_Talk2,
      Dialog_MissionEnd_Priest_Talk2,
      Dialog_MissionEnd_Nun_Talk3,
      Dialog_MissionEnd_Priest_Talk3,
      Dialog_MissionEnd_Nun_Body,
      Dialog_MissionEnd_Nun_Choice,
      Dialog_MissionEnd_Nun_Choice1,
      Dialog_MissionEnd_Nun_Choice2,
      Dialog_MissionEnd_Nun_RIP,
      Dialog_MissionEnd_Nun_Shoot,
      Story_EndingBoss,
      Story_Ending_EpilogTitle,
      Story_Ending_Epilog,
      Story_Ending,
      Story_Ending2,
      Story_Ending_OpenMap,
      SurvivalMode_Desc,
      StoryMode_Desc,
      Difficulty,
      DifficultyNormal,
      DifficultyHard,
      ChooseDifficulty,
      Hint_1,
      Hint_1_Story,
      Hint_2,
      Hint_3,
      Hint_4,
      Hint_5,
      Hint_6,
      Hint_7,
      Hint_8,
      Hint_8_Story,
      Hint_9,
      Hint_10,
      Hint_11,
      Hint_12,
      Hint_13,
      Hint_14,
      Hint_15,
      Hint_16,
      Hint_17,
      Hint_Story_1,
      Hint_Story_2,
      Hint_Story_3,
      Hint_Boss_1,
      UnlockFullGame,
      Achievements,
      AlsoAvailable,
      AchievementUnlocked,
      Achievement_Pistolera_Name,
      Achievement_Pistolera_Desc,
      Achievement_Pistolera_Desc2,
      Achievement_Inquisitor_Name,
      Achievement_Inquisitor_Desc,
      Achievement_Inquisitor_Desc2,
      Achievement_IceColdKiller_Name,
      Achievement_IceColdKiller_Desc,
      Achievement_IceColdKiller_Desc2,
      Achievement_Fallout_Name,
      Achievement_Fallout_Desc,
      Achievement_Fallout_Desc2,
      Achievement_WrathOfGod_Name,
      Achievement_WrathOfGod_Desc,
      Achievement_WrathOfGod_Desc2,
      Achievement_TheFalcon_Name,
      Achievement_TheFalcon_Desc,
      Achievement_TheFalcon_Desc2,
      Achievement_ShakeYourBooty_Name,
      Achievement_ShakeYourBooty_Desc,
      Achievement_ShakeYourBooty_Desc2,
      Achievement_OneNunArmy_Name,
      Achievement_OneNunArmy_Desc,
      Achievement_OneNunArmy_Desc2,
      Achievement_WayOfTheNunja_Name,
      Achievement_WayOfTheNunja_Desc,
      Achievement_WayOfTheNunja_Desc2,
      Achievement_SectorClear_Name,
      Achievement_SectorClear_Desc,
      Achievement_SectorClear_Desc2,
      Achievement_Survival_GameFinishedHard_Name,
      Achievement_Survival_GameFinishedHard_Desc,
      Achievement_Survival_GameFinishedHard_Desc2,
      Achievement_Story_100ZombiesKilled_Name,
      Achievement_Story_100ZombiesKilled_Desc,
      Achievement_Story_100ZombiesKilled_Desc2,
      Achievement_Story_500ZombiesKilled_Name,
      Achievement_Story_500ZombiesKilled_Desc,
      Achievement_Story_500ZombiesKilled_Desc2,
      Achievement_Story_1000ZombiesKilled_Name,
      Achievement_Story_1000ZombiesKilled_Desc,
      Achievement_Story_1000ZombiesKilled_Desc2,
      Achievement_Story_Boss1Killed_Name,
      Achievement_Story_Boss1Killed_Desc,
      Achievement_Story_Boss1Killed_Desc2,
      Achievement_Story_Boss2Killed_Name,
      Achievement_Story_Boss2Killed_Desc,
      Achievement_Story_Boss2Killed_Desc2,
      Achievement_Story_Boss3Killed_Name,
      Achievement_Story_Boss3Killed_Desc,
      Achievement_Story_Boss3Killed_Desc2,
      Achievement_Story_AllWeaponsAndAbilitiesUpgraded_Name,
      Achievement_Story_AllWeaponsAndAbilitiesUpgraded_Desc,
      Achievement_Story_AllWeaponsAndAbilitiesUpgraded_Desc2,
      Achievement_Story_GameFinished_Name,
      Achievement_Story_GameFinished_Desc,
      Achievement_Story_GameFinished_Desc2,
      Achievement_Story_GameFinishedHard_Name,
      Achievement_Story_GameFinishedHard_Desc,
      Achievement_Story_GameFinishedHard_Desc2,
      Features_0,
      Features_1,
      Features_2,
      Features_3,
      Features_4,
      EONHelp_Shop,
      EONExitConfirmation,
      HUD,
      Friends,
      Weekly,
      EarnAchievementUnlockGame,
      BuyGame,
      NotNow,
      GameUpdateRequired,
      GameUpdateRequiredDesc,
      NetworkLost,
      NetworkLostDesc,
      UserMusicPlaying,
      UserMusicPlayingDesc,
      ArraySize,
    }
  }
}
