using Resanctified.Game;
using Xunit;

namespace Resanctified.Tests
{
    public class GameLocalisationTests
    {
        [Theory]
        [InlineData(GameLocalisation.LanguageId.English)]
        [InlineData(GameLocalisation.LanguageId.French)]
        [InlineData(GameLocalisation.LanguageId.Italian)]
        [InlineData(GameLocalisation.LanguageId.German)]
        [InlineData(GameLocalisation.LanguageId.Spanish)]
        public void KeyboardPageIsTranslated(GameLocalisation.LanguageId language)
        {
            GameLocalisation localisation = GameLocalisation.Instance;
            localisation.Initialize(language);
            // The language tables assign by number, so a slip would leave a gap here.
            for (GameLocalisation.TextId id = GameLocalisation.TextId.Keyboard; id <= GameLocalisation.TextId.Key_Esc; ++id)
                Assert.False(string.IsNullOrEmpty(localisation.GetString(id)), $"{language}: {id} has no text");
            // The big title font only has the letters A-Z and the space.
            Assert.Matches("^[A-Z ]+$", localisation.GetString(GameLocalisation.TextId.KeyboardBig));
        }
    }
}
