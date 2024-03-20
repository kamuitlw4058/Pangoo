

namespace Pangoo.Core.VisualScripting
{

    public partial class DynamicObject
    {

        public override string ServiceName => "DynamicObject";

        public override void Log(string message)
        {
            base.Log($"{Row?.Name}[{Row?.UuidShort}]:{message}");
        }

        public override void LogError(string message)
        {
            base.LogError($"{Row.Name}[{Row.UuidShort}]:{message}");
        }


    }


}