using System;

namespace Pangoo.Core.Common
{
    [Title("Input Button")]

    [Serializable]
    public abstract class InputButtonBase : InputBase
    {

        public event Action EventStart;
        public event Action EventCancel;
        public event Action EventPerform;


        protected void ExecuteEventStart() => this.EventStart?.Invoke();
        protected void ExecuteEventCancel() => this.EventCancel?.Invoke();
        protected void ExecuteEventPerform() => this.EventPerform?.Invoke();


        public void RegisterStart(Action callback)
        {
            this.EventStart -= callback;
            this.EventStart += callback;
        }

        public void RegisterCancel(Action callback)
        {
            this.EventCancel -= callback;
            this.EventCancel += callback;
        }

        public void RegisterPerform(Action callback)
        {
            this.EventPerform -= callback;
            this.EventPerform += callback;
        }

        public abstract bool WasPressedThisFrame();

        public abstract bool IsPressed();

        public abstract bool WasReleasedThisFrame();

    }
}