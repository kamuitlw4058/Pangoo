using UnityEngine;

namespace Pangoo.Core.Common
{
    public interface ILogger
    {
        string LogModule { get; }

        void Log(string message);

        void LogError(string message);
    }
}