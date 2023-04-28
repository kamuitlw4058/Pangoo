using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Pangoo
{


    public class TableTitleGroupAttributeDrawer : OdinGroupDrawer<TableTitleGroupAttribute>
    {



        protected override void DrawPropertyLayout(GUIContent label)
        {
            SirenixEditorGUI.BeginHorizontalPropertyLayout(label);
            foreach (var child in this.Property.Children)
            {
                child.Draw();
            }
            SirenixEditorGUI.EndHorizontalPropertyLayout();
            // SirenixEditorGUI.BeginBox();
            // SirenixEditorGUI.BeginBoxHeader();
            // GUIHelper.PopColor();
            // this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, label);
            // SirenixEditorGUI.EndBoxHeader();

            // if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
            // {
            //     for (int i = 0; i < this.Property.Children.Count; i++)
            //     {
            //         this.Property.Children[i].Draw();
            //     }
            // }

            // SirenixEditorGUI.EndFadeGroup();
            // SirenixEditorGUI.EndBox();
        }
    }
}