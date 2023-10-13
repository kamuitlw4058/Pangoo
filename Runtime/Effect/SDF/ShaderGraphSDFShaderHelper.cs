using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
[ExecuteAlways]
public class ShaderGraphSDFShaderHelper : MonoBehaviour
{
    public enum SDFType
    {
        sphere,
        roundedBox,
        capsule
    }

    public SDFType sDFType = SDFType.sphere;


    public float Range = 1f;
    [ShowIf("@this.sDFType == SDFType.roundedBox")]
    [Range(0f, 1f)] public float boxRoundness = 0f;
    public float SoftEdge = 0f;
    public float NoiseScale = 20f;


    public List<Material> materials;


    void Update()
    {
        foreach (var mat in materials)
        {
            mat.SetVector("_SDFWorldPostion", transform.position);
            mat.SetFloat("_SDFRange", Range);
            mat.SetFloat("_SDFSoftEdge", SoftEdge);
            mat.SetFloat("_NoiseScale", NoiseScale);
        }
    }

    [SerializeField, HideInInspector] private Mesh capsuleMesh;
    [SerializeField, HideInInspector] private Mesh sphereMesh;
    [SerializeField, HideInInspector] private Mesh cubeMesh;


    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow * 0.2f;

        switch (sDFType)
        {
            case SDFType.sphere:
                if (!sphereMesh)
                    sphereMesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
                if (sphereMesh)
                {
                    var scale = Range * 2;
                    Gizmos.DrawWireMesh(sphereMesh, Vector3.zero, Quaternion.identity, new Vector3(scale, scale, scale));
                }
                break;

            case SDFType.roundedBox:
                if (!cubeMesh)
                    cubeMesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
                if (cubeMesh)
                    Gizmos.DrawWireMesh(cubeMesh);
                break;

            case SDFType.capsule:
                if (!capsuleMesh)
                    capsuleMesh = Resources.GetBuiltinResource<Mesh>("New-Capsule.fbx");
                if (capsuleMesh)
                    Gizmos.DrawWireMesh(capsuleMesh);
                break;
        }
    }
}