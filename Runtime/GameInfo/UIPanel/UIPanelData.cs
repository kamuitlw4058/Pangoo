
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using GameFramework;
using Pangoo.Core.Services;

namespace Pangoo
{
    [Serializable]
    public sealed class UIPanelData : IReference
    {

        public object UserData;

        public UIInfoRow InfoRow;

        public UIService UI { get; set; }
        public MainService Main { get; set; }

        public string AssetPath
        {
            get
            {
                return InfoRow.m_AssetPathRow.ToPrefabPath();
            }
        }

        public static UIPanelData Create(UIInfoRow InfoRow, object userData)
        {
            var info = ReferencePool.Acquire<UIPanelData>();
            info.InfoRow = InfoRow;
            info.UserData = userData;
            return info;
        }

        public void Clear()
        {
            InfoRow = null;
        }

    }
}
