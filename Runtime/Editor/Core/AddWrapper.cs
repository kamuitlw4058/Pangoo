#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Pangoo.Editor
{

    public class AddWrapper<TOverview> where TOverview : ExcelTableOverview
    {
        [ValueDropdown("GetId", ExpandAllMenuItems = true)]
        public int RowId;
        public delegate void ConfirmAddHandler(int id);

        public ConfirmAddHandler ConfirmAdd;
        List<int> ExcludedIds;

        public AddWrapper(List<int> ids)
        {
            ExcludedIds = ids;
        }

        public IEnumerable GetId()
        {
            return GameSupportEditorUtility.GetExcelTableOverviewNamedIds<TOverview>(excludeIds: ExcludedIds); ;
        }

        [Button("添加", ButtonSizes.Large)]
        public virtual void Create()
        {
            if (RowId == 0)
            {
                EditorUtility.DisplayDialog("错误", "Id必须填写.不能为0", "确定");
                return;
            }

            if (ConfirmAdd != null)
            {
                ConfirmAdd(RowId);
            }

        }

    }
}
#endif