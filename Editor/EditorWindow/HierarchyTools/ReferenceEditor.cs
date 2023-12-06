using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Pangoo.Editor
{
    public class ReferenceEditor
    {
        bool ReadOnly = false;

        [SerializeField] GameObject gameObject;

        [LabelText("引用GameObject")]
        [ListDrawerSettings(Expanded = true)]
        [EnableIf("@!this.ReadOnly")]
        public List<GameObject> referencingObjects = new List<GameObject>();


        [LabelText("引用Transform")]
        [ListDrawerSettings(Expanded = true)]
        [EnableIf("@!this.ReadOnly")]
        public List<Transform> referencingTransform = new List<Transform>();

        OdinMenuEditorWindow m_ParentWindow;
        public ReferenceEditor(GameObject gameObject)
        {
            this.gameObject = gameObject;
            ReadOnly = true;
        }

        public ReferenceEditor(OdinMenuEditorWindow parentWindow)
        {
            m_ParentWindow = parentWindow;
            OnSelectionChange();
            Selection.selectionChanged += OnSelectionChange;
        }

        ~ReferenceEditor()
        {
            Selection.selectionChanged -= OnSelectionChange;
        }

        private void OnSelectionChange()
        {
            if (Selection.activeGameObject == null)
            {
                return;
            }

            if (Selection.activeGameObject.InScene())
            {
                gameObject = Selection.activeGameObject;
            }
        }

        [Button("查找")]
        void Search()
        {
            if (gameObject == null) return;
            referencingObjects.Clear();

            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                Component[] components = obj.GetComponents<Component>();

                foreach (Component component in components)
                {
                    if (component == null) continue;
                    // 检查组件属性是否引用了目标 GameObject
                    SerializedObject serializedObject = new SerializedObject(component);
                    SerializedProperty property = serializedObject.GetIterator();

                    while (property.Next(true))
                    {
                        if (property.propertyType == SerializedPropertyType.ObjectReference &&
                            property.objectReferenceValue == gameObject)
                        {
                            referencingObjects.Add(obj);
                        }

                        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == gameObject.transform)
                        {
                            referencingTransform.Add(obj.transform);
                        }
                    }
                }
            }

        }


        // 添加菜单项到模型的右键菜单栏
        // [MenuItem("CONTEXT/GameObject/查找引用")]
        [MenuItem("GameObject/查找/查找引用", false, 1)]

        private static void FindRefInScene(MenuCommand menuCommand)
        {
            GameObject go = menuCommand.context as GameObject;
            var editor = new ReferenceEditor(go);
            editor.Search();
            OdinEditorWindow.InspectObject(editor);


        }

        // 验证菜单项是否应该显示
        // [MenuItem("CONTEXT/GameObject/查找引用", true)]
        [MenuItem("GameObject/查找/查找引用", true)]

        private static bool ValidateFindRefInScene(MenuCommand menuCommand)
        {
            var selectGameObject = Selection.activeGameObject;
            return selectGameObject != null ? selectGameObject.InScene() : false;
        }


    }
}