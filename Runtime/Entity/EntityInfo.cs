
using System;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Pangoo
{
    [Serializable]
     public sealed class EntityInfo
    {
        [SerializeField] private int m_Id;
        [SerializeField] string m_Name;

        [SerializeField] string m_AssetPath;

        [SerializeField] string m_GroupName;
        // private DREntity dREntity;
        // private DRAssetsPath dRAssetsPath;
        // private EntityGroupData entityGroupData;

        public int Id
        {
            set{
                m_Id= value;
            }
            get
            {
                return m_Id;
            }
        }

        public string Name
        {
                        set{
                m_Name= value;
        }
            get
            {
                return m_Name;
            }
        }


        public string AssetPath
        {
                        set{
                m_AssetPath= value;
            }
            get
            {
                return m_AssetPath;
            }
        }

        public string GroupName{
                        set{
                m_GroupName= value;
            }
            get{
                return m_GroupName;
            }
        }

        // public EntityGroupData EntityGroupData
        // {
        //     get
        //     {
        //         return entityGroupData;
        //     }
        // }

        // public EntityInfo(int id,string name,string path,string groupName)
        // {
        //     m_Id = id;
        //     m_Name = name;
        //     m_AssetPath = path;
        //     m_GroupName = groupName;
        // }

    }
}
