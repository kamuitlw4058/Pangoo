using UnityEngine;

namespace Pangoo.Core.Common
{
    public interface ISpatialHash
    {
        Vector3 Position { get; }
        int UniqueCode { get; }
    }
}