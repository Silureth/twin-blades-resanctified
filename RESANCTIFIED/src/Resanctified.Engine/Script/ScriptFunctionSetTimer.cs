namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetTimer : IScriptFunction
    {
        public ScriptFunctionSetTimer(float time, World.World world, ScriptManager.ScriptDelegate script) => ScriptManager.Instance.SetTimerEvent(time, world, script);

        public bool Update(float time) => true;

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
