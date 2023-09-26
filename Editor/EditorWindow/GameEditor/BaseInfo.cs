using UnityEngine;


namespace Pangoo.Editor
{
    [CreateAssetMenu(fileName = "BaseInfo", menuName = "Lost_In_Abyss2_HDRP/BaseInfo", order = 0)]
    public class BaseInfo : ScriptableObject
    {
        [SerializeReference]
        ExcelRowBase row;
    }
}