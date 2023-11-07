using Pangoo;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameFramework;
using Pangoo.Core.Services;


namespace Pangoo
{
    public class UIInfoRow : BaseInfoRow
    {
        public SimpleUITable.SimpleUIRow m_SimpleUIRow;
        public AssetPathTable.AssetPathRow m_AssetPathRow;

        private UIPanelData m_UIPanelInfo;


        public UIInfoRow(SimpleUITable.SimpleUIRow row, AssetPathTable.AssetPathRow assetPathRow)
        {
            this.m_SimpleUIRow = row;
            this.m_AssetPathRow = assetPathRow;
        }


        public EntityGroupTable.EntityGroupRow EntityGroupRow;


        List<int> m_LoadSceneIds = null;

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
            if (m_UIPanelInfo == null)
            {
                m_UIPanelInfo = UIPanelData.Create(this, userData);
            }

            return m_UIPanelInfo;
        }




    }
}