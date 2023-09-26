using System;
using System.Collections.Generic;
using Pangoo.Core.Common;
using UnityEngine;

namespace Pangoo.Core.Character
{
    public static class SpatialHashInteractionItems
    {
        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] private static SpatialHash Handler { get; set; }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public static void Insert(ISpatialHash spatialHash)
        {
            Handler ??= new SpatialHash();
            Handler.Insert(spatialHash);
        }

        public static void Remove(ISpatialHash spatialHash)
        {
            Handler ??= new SpatialHash();
            Handler.Remove(spatialHash);
        }

        public static void Find(Vector3 point, float radius, List<ISpatialHash> results, ISpatialHash except = null)
        {
            Handler ??= new SpatialHash();
            Handler.Find(point, radius, results, except);
        }

        public static int Count()
        {
            return Handler?.Count ?? 0;
        }

    }
}