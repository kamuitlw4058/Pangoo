using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SDFShaderEntity : MonoBehaviour
{
    public enum SDFType
    {
        sphere,
        roundedBox,
        capsule
    }

    public SDFType sDFType = SDFType.roundedBox;
    [SerializeField, HideInInspector] SDFType oldSDFType;

    private string spherePropertyName = "_SDF_Sphere";
    private string roundedBoxPropertyName = "_SDF_RoundedBox";
    private string capsulePropertyName = "_SDF_Capsule";

    [SerializeField, HideInInspector] Matrix4x4 transformMatrix = new Matrix4x4();

    [Range(0f, 1f)] public float boxRoundness = 0f;
    [Range(0f, 1f)] public float softness = 0f;

    int propertyId;
    public string propertyName = "_SDF_RoundedBox";
    [SerializeField, HideInInspector] private string oldPropertyName;

    public List<Material> materials;

    void Start()
    {
        Setup();
    }

    private void OnValidate()
    {
        if (sDFType != oldSDFType || propertyName != oldPropertyName)
        {
            Setup();
        }
    }
    void FixedUpdate() {
    }

    void Update()
    {
        if (propertyName != oldPropertyName)
        {
            oldPropertyName = propertyName;
            propertyId = Shader.PropertyToID(propertyName);
        }

        transformMatrix = transform.worldToLocalMatrix;
        transformMatrix[3, 0] = 1f; // set the visibility to 1
        transformMatrix[3, 2] = softness;
        transformMatrix[3, 3] = boxRoundness;

        Shader.SetGlobalMatrix(propertyId, transformMatrix);

    }

    private void OnEnable()
    {
        transformMatrix = transform.worldToLocalMatrix;
        transformMatrix[3, 0] = 1f; // set the visibility to 1

    }

    private void OnDisable()
    {
        transformMatrix[3, 0] = 0f; // set the visibility to 0
        Shader.SetGlobalMatrix(propertyId, transformMatrix);
    }

    void EnableMeaterialKeworld(SDFType sdfType){


        string EnableKeyword = "";
        string DisableKeyword = "";
        string DisableKeyword2 = "";

        switch(sdfType){
            case SDFType.sphere:
                propertyName = spherePropertyName;
                EnableKeyword = "_SDFTYPE_SPHERE";
                DisableKeyword  = "_SDFTYPE_ROUNDEDBOX";
                DisableKeyword2 = "_SDFTYPE_CAPSULE";
            break;
            case SDFType.roundedBox:
                propertyName = roundedBoxPropertyName;
                EnableKeyword = "_SDFTYPE_ROUNDEDBOX";
                DisableKeyword  = "_SDFTYPE_SPHERE";
                DisableKeyword2 = "_SDFTYPE_CAPSULE";

                break;

            case SDFType.capsule:
                propertyName = capsulePropertyName;
                EnableKeyword = "_SDFTYPE_CAPSULE";
                DisableKeyword  = "_SDFTYPE_SPHERE";
                DisableKeyword2 = "_SDFTYPE_ROUNDEDBOX";
                break;
        }

        if(materials != null &&  materials.Count > 0){
            foreach(var material in materials){
                material.EnableKeyword(EnableKeyword);
                material.DisableKeyword(DisableKeyword);
                material.DisableKeyword(DisableKeyword2);
            }
        }
    }

    void Setup()
    {

        oldSDFType = sDFType;
        switch (sDFType)
        {
            case SDFType.sphere:
                propertyName = spherePropertyName;
                
                break;
            case SDFType.roundedBox:
                propertyName = roundedBoxPropertyName;
                break;
            case SDFType.capsule:
                propertyName = capsulePropertyName;
                break;
        }
        EnableMeaterialKeworld(sDFType);

        oldPropertyName = propertyName;
        propertyId = Shader.PropertyToID(propertyName);

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
                    Gizmos.DrawWireMesh(sphereMesh);
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