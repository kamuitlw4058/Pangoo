
namespace Pangoo.MetaTable
{
    public partial class CharacterOverview
    {
#if UNITY_EDITOR
        public static UnityCharacterRow GetCharacterRowByUuid(string uuid)
        {
            if (uuid.IsNullOrWhiteSpace() || (!uuid.IsNullOrWhiteSpace() && uuid.Equals(ConstString.LatestPlayer)))
            {
                var config = GameSupportEditorUtility.GetGameMainConfig();
                return GetUnityRowByUuid(config.DefaultPlayer);
            }

            return GetUnityRowByUuid(uuid);
        }
#endif
    }
}
