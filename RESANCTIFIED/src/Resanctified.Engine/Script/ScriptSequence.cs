using System.Collections.Generic;

namespace Resanctified.Engine.Script
{
    public class ScriptSequence
    {
        private List<IScriptFunction> m_scriptFunctionList;
        private int m_currentFunctionIndex;

        public ScriptSequence()
        {
            this.m_scriptFunctionList = new List<IScriptFunction>();
            this.m_currentFunctionIndex = -1;
        }

        public bool Update(float gameTime)
        {
            if (this.m_scriptFunctionList.Count == 0)
                return true;
            if (this.m_currentFunctionIndex < 0)
            {
                this.m_currentFunctionIndex = 0;
                this.m_scriptFunctionList[this.m_currentFunctionIndex].Initialize();
            }
            while (this.m_scriptFunctionList[this.m_currentFunctionIndex].Update(gameTime))
            {
                this.m_scriptFunctionList[this.m_currentFunctionIndex].Cleanup();
                ++this.m_currentFunctionIndex;
                if (this.m_currentFunctionIndex >= this.m_scriptFunctionList.Count)
                    return true;
                this.m_scriptFunctionList[this.m_currentFunctionIndex].Initialize();
            }
            return false;
        }

        public void AddScriptFunction(IScriptFunction function) => this.m_scriptFunctionList.Add(function);
    }
}
