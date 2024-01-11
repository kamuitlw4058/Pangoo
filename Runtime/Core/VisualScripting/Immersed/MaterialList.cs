using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialList : MonoBehaviour
{
    public List<Material> materialList=new List<Material>();

    public void SetModelMaterial(Transform targetTransform,int index)
    {
        if (!targetTransform.GetComponent<MeshRenderer>())
        {
            return;
        }
        MeshRenderer meshRenderer=targetTransform.GetComponent<MeshRenderer>();
        meshRenderer.material = targetTransform.GetComponent<MaterialList>().materialList[index];
    }
}
