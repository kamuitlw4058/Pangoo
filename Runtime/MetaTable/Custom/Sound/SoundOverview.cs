#if UNITY_EDITOR

using UnityEngine;

namespace Pangoo.MetaTable
{
    public partial class SoundOverview
    {
        public static string GetSoundUuidByPath(string path)
        {

            var overviews = AssetDatabaseUtility.FindAsset<SoundOverview>();
            foreach (var overview in overviews)
            {
                foreach (var row in overview.Rows)
                {
                    var soundAssetPath = AssetUtility.GetSoundAssetPath(row.Row.PackageDir, row.Row.SoundType, row.Row.AssetPath);
                    if (path.Equals(soundAssetPath))
                    {
                        return row.Row.Uuid;
                    }

                }
            }
            return null;
        }
    }
}
#endif

