using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;

namespace Pangoo
{   
    public class StaticSceneInfo:IReference
    {

        public StaticSceneTable.StaticSceneRow StaticSceneRow;
        public AssetPathTable.AssetPathRow AssetPathRow;

        public EntityGroupTable.EntityGroupRow EntityGroupRow;


        EntityInfo m_EntityInfo;

        // StaticSceneTable.

        List<int> m_LoadSceneIds = null;

        public int Id{
            get{
                return StaticSceneRow.Id;
            }
        }

        public string Name{
            get{
                return StaticSceneRow.Name;
            }
        }

        public EntityInfo EntityInfo{
            get{
                if(m_EntityInfo == null){
                    m_EntityInfo = EntityInfo.Create(AssetPathRow,EntityGroupRow);
                }

                return m_EntityInfo;
            }
        }

        public List<int> LoadSceneIds{
            get{
                if(m_LoadSceneIds == null){
                    m_LoadSceneIds = StaticSceneRow.LoadSceneIds.Split("|").Select(row => int.Parse(row)).ToList();
                }
                return m_LoadSceneIds;
            }
        }

        public int AssetPathId{
            get{
                return AssetPathRow.Id;
            }
        }

        public static StaticSceneInfo Create(StaticSceneTable.StaticSceneRow staticScene, EntityGroupTable.EntityGroupRow entityGroup, AssetPathTable.AssetPathRow assetPath){
            var info = ReferencePool.Acquire<StaticSceneInfo>();
            info.EntityGroupRow = entityGroup;
            info.StaticSceneRow = staticScene;
            info.AssetPathRow = assetPath;
            return info;
            
        }

        public void Clear(){
            if(m_EntityInfo != null){
                ReferencePool.Release(m_EntityInfo);
            }
            EntityGroupRow = null;
            StaticSceneRow = null;
            AssetPathRow = null;
        }

        

 
    }
}