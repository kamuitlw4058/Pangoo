using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ShaderUtility
{

    public static void SetColor(Renderer render, string prop, Color value, MaterialPropertyBlock propertyBlock = null)
    {
        SetColor(new List<Renderer>() { render }, prop, value, propertyBlock);
    }

    public static void SetColor(List<Renderer> renderers, string prop, Color value, MaterialPropertyBlock propertyBlock = null)
    {
        if (renderers == null || renderers.Count == 0)
        {
            return;
        }

        Renderer firstRenderer = renderers[0];
        if (propertyBlock == null)
        {
            propertyBlock = new MaterialPropertyBlock();
            firstRenderer.GetPropertyBlock(propertyBlock);
        }

        foreach (var render in renderers)
        {
            propertyBlock.SetColor(prop, value);
            render.SetPropertyBlock(propertyBlock);
        }

    }


    public static void SetFloat(Renderer render, string prop, float value, MaterialPropertyBlock propertyBlock = null)
    {
        SetFloat(new List<Renderer>() { render }, prop, value, propertyBlock);
    }

    public static void SetFloat(List<Renderer> renderers, string prop, float value, MaterialPropertyBlock propertyBlock = null)
    {
        if (renderers == null || renderers.Count == 0)
        {
            return;
        }

        Renderer firstRenderer = renderers[0];
        if (propertyBlock == null)
        {
            propertyBlock = new MaterialPropertyBlock();
            firstRenderer.GetPropertyBlock(propertyBlock);
        }

        foreach (var render in renderers)
        {
            propertyBlock.SetFloat(prop, value);
            render.SetPropertyBlock(propertyBlock);
        }

    }


}
