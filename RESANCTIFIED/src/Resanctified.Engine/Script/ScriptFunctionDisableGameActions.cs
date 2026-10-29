using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionDisableGameActions : IScriptFunction
    {
        private bool m_disable;
        private PlayerIndex m_playerId;

        public ScriptFunctionDisableGameActions(bool disable, PlayerIndex playerId)
        {
            this.m_disable = disable;
            this.m_playerId = playerId;
        }

        public bool Update(float gameTime) => true;

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
