
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Pangoo.Editor
{
    public class MaterialListTools
    {
        List<Material> m_TotalListMaterals;


        [ValueDropdown("GetTotalShaders", IsUniqueList = true, DropdownTitle = "Select Scene Object", DrawDropdownForListElements = false, ExcludeExistingValuesInList = true)]
        [SerializeField][OnValueChanged("Refresh")] List<Shader> ShaderFilterList;

        // List 《Shader

        [SerializeField][TableList(AlwaysExpanded = true, ShowPaging = true)] List<MaterialEntry> m_ShowListMaterals;

        public MaterialListTools()
        {
            m_ShowListMaterals = new List<MaterialEntry>();
            ShaderFilterList = new List<Shader>();
            RefreshTotal();
        }

        public string GetHdrpLitPropertyName(MaterialTextureType type)
        {
            switch (type)
            {
                case MaterialTextureType.BC:
                    return "_BaseColorMap";
                case MaterialTextureType.N:
                    return "_NormalMap";
                case MaterialTextureType.MASK:
                    return "_MaskMap";
                case MaterialTextureType.HT:
                    return "_HeightMap";

            }

            return "";
        }


        public string GetPropertyName(Shader shader, MaterialTextureType type)
        {
            switch (shader.name)
            {
                case "HDRP/Lit":
                    return GetHdrpLitPropertyName(type);

            }
            return null;
        }


        public Texture GetTexture(Material material, MaterialTextureType type)
        {
            if (material.HasProperty(GetPropertyName(material.shader, type)))
            {
                return material.GetTexture(GetPropertyName(material.shader, type));
            }
            return null;
        }


        void Refresh()
        {
            m_ShowListMaterals.Clear();
            foreach (var material in m_TotalListMaterals)
            {
                if (ShaderFilterList == null || ShaderFilterList.Count == 0 || ShaderFilterList.Contains(material.shader))
                {
                    var entry = new MaterialEntry();
                    entry.material = material;
                    entry.shader = material.shader;
                    entry.mainTex = GetTexture(material, MaterialTextureType.BC);
                    entry.normalTex = GetTexture(material, MaterialTextureType.N);
                    entry.heightTex = GetTexture(material, MaterialTextureType.HT);
                    entry.maskTex = GetTexture(material, MaterialTextureType.MASK);
                    var path = AssetDatabase.GetAssetPath(material);
                    if (path != null)
                    {
                        Debug.Log($"path:{path}");
                        var index = path.LastIndexOf('/');
                        if (index > 0)
                        {
                            entry.dir = path.Substring(0, index + 1);
                            var name = path.Substring(index + 1);
                            var namePointIndex = name.LastIndexOf('.');
                            if (namePointIndex > 0)
                            {
                                entry.name = name.Substring(0, namePointIndex);
                            }
                            else
                            {
                                entry.name = name;
                            }

                        }
                    }
                    path = AssetDatabase.GetAssetPath(material);
                    entry.path = path;


                    AssetDatabase.GetAssetPath(entry.mainTex);
                    m_ShowListMaterals.Add(entry);
                }
            }
        }

        [Button("刷新所有材质")]
        void RefreshTotal()
        {
            m_TotalListMaterals = AssetUtility.GetAssetListByPath<Material>("Assets");
            m_TotalListMaterals = m_TotalListMaterals.Where(o =>
            {
                var extension = AssetUtility.GetAssetFileExtension(o);
                return extension.ToLower() != "fbx";
            }).ToList();
            Refresh();
        }

        [SerializeField] Shader BuiltinStrand;

        [Button("从Hdrp转化到Builtin")]
        void Convert2Builtin()
        {
            if (BuiltinStrand == null)
            {
                BuiltinStrand = Shader.Find("Standard");
            }
            foreach (var material in m_ShowListMaterals)
            {
                material.material.shader = BuiltinStrand;
                material.material.mainTexture = material.mainTex;
                material.material.SetTexture("_BumpMap", material.normalTex);
                material.material.SetTexture("_MetallicGlossMap", material.maskTex);
                material.material.SetTexture("_ParallaxMap", material.heightTex);
            }
        }


        private IEnumerable GetTotalShaders()
        {
            return m_TotalListMaterals.Select(o => o.shader);
        }

        [Serializable]

        public class MaterialEntry
        {
            public Material material;

            public Shader shader;

            public Texture mainTex;

            public Texture normalTex;

            public Texture heightTex;


            public Texture maskTex;

            public string dir;

            public string name;

            public string path;
        }


        public enum MaterialTextureType
        {
            None,
            BC,
            N,

            MASK,
            HT,
        }

    }
}