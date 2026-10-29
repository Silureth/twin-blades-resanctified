using Resanctified.Engine.Collections;
using Resanctified.Engine.Event;
using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
    public class ScriptManager : IGameComponent
    {
        private double m_timer;
        private GameEvent m_timeUpEvent;
        private PList<ScriptSequence> m_scriptSequenceList;
        private PList<ScriptSequence> m_scriptSequenceListSecondary;
        private static ScriptManager m_instance = new ScriptManager();
        private PList<Sound> m_scriptSoundList;
        private bool m_firstUpdate;
        public World.World WorldForSequenceInit;

        public static ScriptManager Instance => m_instance;

        public bool IsScriptExecuting => this.m_scriptSequenceList.Count != 0 || this.m_scriptSequenceListSecondary.Count != 0;

        public PList<Sound> ScriptSoundList => this.m_scriptSoundList;

        public ScriptManager()
        {
            this.m_timer = 0.0;
            this.m_timeUpEvent = null;
            this.WorldForSequenceInit = null;
            this.m_scriptSequenceList = new PList<ScriptSequence>();
            this.m_scriptSequenceListSecondary = new PList<ScriptSequence>();
            this.m_scriptSoundList = new PList<Sound>();
        }

        public void Update(float gameTime)
        {
            if (this.m_firstUpdate)
                this.m_firstUpdate = false;
            if (this.m_timer > 0.0)
            {
                this.m_timer -= gameTime;
                if (this.m_timer <= 0.0)
                {
                    this.m_timer = 0.0;
                    if (this.m_timeUpEvent != null)
                        this.m_timeUpEvent.RaiseEvent();
                }
            }
            PList<ScriptSequence> plist1 = new PList<ScriptSequence>();
            PList<ScriptSequence> plist2 = new PList<ScriptSequence>();
            foreach (ScriptSequence scriptSequence in this.m_scriptSequenceList)
            {
                if (scriptSequence.Update(gameTime))
                    plist1.Add(scriptSequence);
            }
            foreach (ScriptSequence scriptSequence in this.m_scriptSequenceListSecondary)
            {
                if (scriptSequence.Update(gameTime))
                    plist2.Add(scriptSequence);
            }
            foreach (ScriptSequence scriptSequence in plist1)
                this.m_scriptSequenceList.Remove(scriptSequence);
            foreach (ScriptSequence scriptSequence in plist2)
                this.m_scriptSequenceListSecondary.Remove(scriptSequence);
            plist1.Clear();
            plist2.Clear();
        }

        public void Initialize() => this.m_firstUpdate = true;

        public void Cleanup()
        {
            this.m_scriptSequenceList.Clear();
            this.m_scriptSequenceListSecondary.Clear();
            this.m_scriptSoundList.Clear();
        }

        public void SetTimerEvent(float time, World.World world, ScriptDelegate timeUpScript)
        {
            this.m_timer = time;
            this.m_timeUpEvent = new GameEvent(timeUpScript, world);
        }

        public void AddScriptSequence(ScriptDelegate script, World.World world)
        {
            this.WorldForSequenceInit = world;
            this.m_scriptSequenceList.Add(new ScriptSequence());
            script();
            this.WorldForSequenceInit = null;
        }

        public void AddScriptFunction(IScriptFunction function) => this.m_scriptSequenceList[this.m_scriptSequenceList.Count - 1].AddScriptFunction(function);

        public void AddScriptFunctionSecondary(IScriptFunction function)
        {
            ScriptSequence scriptSequence = new ScriptSequence();
            scriptSequence.AddScriptFunction(function);
            this.m_scriptSequenceListSecondary.Add(scriptSequence);
        }

        public delegate void ScriptDelegate();
    }
}
