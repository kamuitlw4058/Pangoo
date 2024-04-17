
using UnityEngine;
using Cinemachine;


namespace Pangoo.Core.Characters
{
    public partial class Character
    {
        CharacterConfigGetRowByUuidHandler m_CharacterConfigHandler;
        void DoAwakeConfig()
        {
            if (Main.MetaTable != null)
            {
                m_CharacterConfigHandler = Main.MetaTable.GetCharacterConfigRow;
            }

            var row = CharacterConfigRowExtension.GetByUuid(Row.CharacterConfigUuid, m_CharacterConfigHandler);
            if (Row.CharacterConfigUuid.IsNullOrWhiteSpace() || row == null)
            {
                CharacterCamera.SetCameraNoise(false);
            }
            else
            {
                NoiseSettings noiseSetting = Resources.Load<NoiseSettings>("NoiseSettings/" + row.NoiseProfile);
                if (noiseSetting == null)
                {
                    CharacterCamera.SetCameraNoise(false);
                }
                else
                {
                    CharacterCamera.SetCameraNoise(true, noiseSetting, 1, 1);
                }

            }

        }



    }
}