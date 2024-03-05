using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using Pangoo.MetaTable;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;


namespace Pangoo.Core.Common
{
    public class SaveItem : MonoBehaviour
    {
        public Action<SaveItem> Click;

        public Button ItemButton;

        public TextMeshProUGUI TitleText;

        public Tuple<DateTime, FileInfo> TupleData;

        private void Start()
        {
            ItemButton?.onClick.AddListener(() =>
            {
                Click?.Invoke(this);
            });
        }

    }
}