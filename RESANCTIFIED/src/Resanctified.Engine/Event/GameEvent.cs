using Resanctified.Engine.Script;

namespace Resanctified.Engine.Event
{
    public class GameEvent
    {
        protected ScriptManager.ScriptDelegate m_script;
        private World.World m_world;

        public GameEvent(ScriptManager.ScriptDelegate script, World.World world)
        {
            this.m_script = script;
            this.m_world = world;
        }

        public void RaiseEvent() => GameEventManager.Instance.RaisedEventList.Add(this);

        public void ExecuteScript() => ScriptManager.Instance.AddScriptSequence(this.m_script, this.m_world);
    }
}
