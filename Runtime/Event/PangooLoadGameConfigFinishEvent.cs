using GameFramework;
using GameFramework.Event;


namespace Pangoo
{
    public sealed class PangooLoadGameConfigFinishEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(PangooLoadGameConfigFinishEventArgs).GetHashCode();

        public override int Id => EventId;

        public PangooLoadGameConfigFinishEventArgs()
        {
            Clear();
        }

        public static PangooLoadGameConfigFinishEventArgs Create()
        {
            var args = ReferencePool.Acquire<PangooLoadGameConfigFinishEventArgs>();
            return args;
        }

        public override void Clear()
        {

        }

    }
}