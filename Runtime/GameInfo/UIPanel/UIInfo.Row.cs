using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using Pangoo.Core.Services;
using Pangoo.MetaTable;


namespace Pangoo
{
    public class UIInfoRow : BaseInfoRow
    {
        public ISimpleUIRow m_SimpleUIRow;
        public IAssetPathRow m_AssetPathRow;

        private UIPanelData m_UIPanelInfo;


        public UIInfoRow(ISimpleUIRow row, IAssetPathRow assetPathRow)
        {
            this.m_SimpleUIRow = row;
            this.m_AssetPathRow = assetPathRow;
        }


        public EntityGroupTable.EntityGroupRow EntityGroupRow;



        public int Id
        {
            get
            {
                return m_SimpleUIRow.Id;
            }
        }

        public string Name
        {
            get
            {
                return m_SimpleUIRow.Name;
            }
        }


        public int AssetPathId
        {
            get
            {
                return m_AssetPathRow.Id;
            }
        }

        public string Params
        {
            get
            {
                return m_SimpleUIRow.Params;
            }
        }

        public override void Remove()
        {
            ReferencePool.Release(m_UIPanelInfo);
        }

        public UIPanelData GetPanelData(object userData)
        {
            return UIPanelData.Create(this, userData);
        }




    }
}