namespace Resanctified.Engine.FSM
{
    public class DrawableFiniteStateMachine :
      FiniteStateMachine,
      IDrawableFiniteStateMachine,
      IFiniteStateMachine
    {
        public virtual void Draw() => ((IDrawableState)this.CurrentState).Draw(this);
    }
}
