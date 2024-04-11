
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Pangoo.Core.VisualScripting;
using Pangoo.Core.Characters;
using System;
using UnityEngine.UI;
using Pangoo.Core.Common;
using Pangoo.MetaTable;
using UnityEditor.VersionControl;
using System.Linq;
using UnityEngine.UIElements;

namespace Pangoo
{

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public class CharacterBornEditor : MonoBehaviour
    {
        public CharacterBornInfo BornInfo;
        [ShowInInspector]
        public Action SaveAction;

        [Button("重设到出生点设置")]
        public void UpdateTransformByBornInfo()
        {
            transform.position = BornInfo.Pose.Position;
            transform.rotation = Quaternion.Euler(BornInfo.Pose.Rotation);
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            Clear();
        }

        public void Clear()
        {
            GizmosColor = new Color(1f, 1f, 0f);
        }

        [Button("设置出身点姿势")]
        public void SetTransform()
        {
            BornInfo.Pose.Position = transform.position;
            BornInfo.Pose.Rotation = transform.rotation.eulerAngles;
            Save();
        }

        Color GizmosColor = new Color(1f, 1f, 0f);

        private void OnDrawGizmos()
        {
            var oldColor = Gizmos.color;
            Gizmos.color = GizmosColor;
            Gizmos.DrawCube(transform.position, new Vector3(0.5f, 2, 0.5f));
            Gizmos.DrawSphere(transform.TransformPoint(Vector3.forward * 0.3f), 0.3f);
            Gizmos.color = oldColor;
        }

        void Save()
        {
            SaveAction?.Invoke();
        }

    }


}
#endif