using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Pangoo;
 
public class BuildTextureTools //扩展编辑器要继承EditorWindow
{
    [SerializeField] TextureConvertType textureConvertType;

    [SerializeField]
    [InlineEditor(InlineEditorModes.LargePreview)]
    [OnValueChanged("BuildPreivew")]
    [BoxGroup("输入")]
    [ShowIf("textureConvertType", TextureConvertType.Single2HDRPMask)]
    // [DrawWithUnity]
    private Texture2D metallic, occlusion, detailmask, smoothness;//四个纹理字段

    
    public Texture2D[] Texture2Ds{
        get{
            return new Texture2D[]{metallic, occlusion, detailmask, smoothness};
        }
    }


    public bool R2S = true;

    public bool GammaChange = true;


    [SerializeField]
    [InlineEditor(InlineEditorModes.LargePreview)]
    [OnValueChanged("BuildPreivew")]
    [BoxGroup("输入")]
    [ShowIf("textureConvertType", TextureConvertType.HDRP2Builtin)]
    // [DrawWithUnity]
    private Texture2D mask;//四个纹理字段


    private Material mat;//带有Shader的材质

   
    private bool changed;
 
    public BuildTextureTools(){
        Init();
    }

    private void Init()
    {
        if (mat == null)
        {
            mat = new Material(Shader.Find("Hidden/MaskMap"));//根据MaskMap这个Shader创建材质
            mat.hideFlags = HideFlags.HideAndDontSave;//材质不保存
        }
    }
 
    private void BuildPreivew(){
        preview = GenerageTexture();
    }

    string GetTexturePath(){
        foreach(var texture in Texture2Ds){
            var path = AssetDatabase.GetAssetPath(texture);
            if(path != null && path != string.Empty){
                return Path.GetDirectoryName(path);
            }
        }

        return Application.dataPath;
    }

    string GetTextureName(){
         foreach(var texture in Texture2Ds){
           var path = AssetDatabase.GetAssetPath(texture);
            if(path != null && path != string.Empty){
                return $"{Path.GetFileNameWithoutExtension(path)}_Mask";
            }
         }

        return  "texture.png";
    }

    [Button("生成纹理")]
    private void BuildTexture(){
        if(preview != null){
           
            string savePath = EditorUtility.SaveFilePanel("Save", GetTexturePath(), GetTextureName(), "png");
            File.WriteAllBytes(savePath,preview.EncodeToPNG());//将纹理保存为png格式，也可以是jpg、exr等格式
            AssetDatabase.Refresh();//更新，要不然在Unity当中不会看到生成的图片（在win的文件管理器中可以看到）
        }
    }

    public static Texture2D BlitHdrpMaskTexture(Texture2D metallic,Texture2D occlusion,Texture2D detailmask,Texture2D smoothness,int size = 2048, bool IsRoughness = true,bool gammaAdjust = true){
        var tempMat = new Material(Shader.Find("Hidden/MaskMap"));//根据MaskMap这个Shader创建材质
        tempMat.hideFlags = HideFlags.HideAndDontSave;//材质不保存

        tempMat.SetTexture("_R", metallic);//给Shader的属性赋值
        tempMat.SetTexture("_G", occlusion);
        tempMat.SetTexture("_B", detailmask);
        tempMat.SetTexture("_A", smoothness);
        if(IsRoughness){
                tempMat.SetFloat("_ToSmoothness",1f);
        }else{
            tempMat.SetFloat("_ToSmoothness",0f);
        }
        if(gammaAdjust){
                tempMat.SetFloat("_Gamma",0.45f);
        }else{
            tempMat.SetFloat("_Gamma",1f);
        }

        RenderTexture tempRT = new RenderTexture(size, size, 32, RenderTextureFormat.ARGB32);//生成纹理，分辨率可以自己改为1024的，也可以自己在编辑器上做出多个可供选择的分辨率，容易实现
        tempRT.Create();
        Texture2D temp2 = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);
        Graphics.Blit(temp2, tempRT, tempMat);//将temp2纹理的值通过mat赋值到tempRT，核心的代码，可以好好看看对这个方法的解释
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = tempRT;//设置当前active的纹理
 
        Texture2D output = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);//创建一个RGBA格式的纹理
        output.ReadPixels(new Rect(0, 0, tempRT.width, tempRT.height), 0, 0);//读取当前active的纹理            
        output.Apply();//apply将改变写入
        RenderTexture.active = prev;
 
        return output;
    }



    private Texture2D GenerageTexture()
    {
        switch(textureConvertType){
            case TextureConvertType.Single2HDRPMask:
                return BlitHdrpMaskTexture(metallic,occlusion,detailmask,smoothness,IsRoughness:R2S,gammaAdjust:GammaChange);
            case TextureConvertType.HDRP2Builtin:
                break;
        }

 
        RenderTexture tempRT = new RenderTexture(2048, 2048, 32, RenderTextureFormat.ARGB32);//生成纹理，分辨率可以自己改为1024的，也可以自己在编辑器上做出多个可供选择的分辨率，容易实现
        tempRT.Create();
        Texture2D temp2 = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);
        Graphics.Blit(temp2, tempRT, mat);//将temp2纹理的值通过mat赋值到tempRT，核心的代码，可以好好看看对这个方法的解释
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = tempRT;//设置当前active的纹理
 
        Texture2D output = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);//创建一个RGBA格式的纹理
        output.ReadPixels(new Rect(0, 0, tempRT.width, tempRT.height), 0, 0);//读取当前active的纹理            
        output.Apply();//apply将改变写入
        RenderTexture.active = prev;
 
        return output;
    }
 
 
    [SerializeField][InlineEditor(InlineEditorModes.LargePreview)][BoxGroup("预览")] [ShowIf("textureConvertType", TextureConvertType.Single2HDRPMask)] private Texture2D preview = null;

    
    [SerializeField]
    [InlineEditor(InlineEditorModes.LargePreview)]
    [ShowIf("textureConvertType", TextureConvertType.HDRP2Builtin)]
    [BoxGroup("预览")]
    private Texture2D metallicPreview, occlusionPreview, detailmaskPreview, smoothnessPreview;// 拆解Hdrp纹理



    public static bool CheckSolidColor(Texture texture)
    {
            bool isSolidColor = true;
            RenderTexture _rt;
            //方法2：mipmap+泊松   blit到一张小图上
            Texture currentTexture = texture;
            int currentTextureWidth = currentTexture.width;
            int currentTextureHeight = currentTexture.height;
            int rtWidth = currentTextureWidth;
            int rtHeight = currentTextureHeight;
 
            if (currentTextureWidth>=128)
            {
                rtWidth = currentTextureWidth / 8;
            }
            if (currentTextureHeight >= 128)
            {
                rtHeight = currentTextureHeight / 8;
            }
 
            // if (_rt != null)
            // {
            //     RenderTexture.ReleaseTemporary(_rt = null);
            // }
 
            // if (_rt == null)
            // {
                _rt = RenderTexture.GetTemporary(rtWidth, rtHeight, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
                Graphics.Blit(currentTexture, _rt);
            // }
            //进行泊松分布采样判断
            return  IsSolidColor(_rt, rtWidth, rtHeight);
        // }
    }
 
 
    //泊松采样
    static bool IsSolidColor(RenderTexture rt,int rtWidth,int rtHeight)
    {
        Debug.LogError(string.Format("RT尺寸：{0} x {1}",rtWidth,rtHeight));
        int samplePointCount = rtWidth * rtHeight;
        int sampleCountLimit = samplePointCount;   //最大可采样数
        int iterLimit = 100; // 迭代上限, 100次内决定有效位置
        Vector2 sample;
        int count = 0;
        float extend = 1;
        bool avaliable;
 
        // RT像素画到一张小图上可以后面读取像素
        RenderTexture.active = rt;
        Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
 
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply();
 
        //把像素点读取存储到数组中
        List<Vector2> samplePoints = new List<Vector2>();
        for(int i=0;i< rtWidth; i++)
        {
            for (int j = 0; j < rtHeight; j++)
            {
                samplePoints.Add(new Vector2(i, j));
            }
        }
 
 
        Color initColor=new Color(0,0,0);
 
        while (samplePoints.Count > 0 && sampleCountLimit > 0 && iterLimit-- > 0)
        {
            int next = (int)Mathf.Lerp(0, samplePoints.Count - 1, UnityEngine.Random.value);
            sample = samplePoints[next]; // 在这些点中随便选一个采样点进行范围随机
 
            bool found = false;
            int kloop= 30; // 迭代30次, 找到泊松分布点
            float radius = 1;   //采样半径为1像素
            float radius2 = radius * radius;
 
            for (int j = 0; j < kloop; j++)
            {
                var angle = 2 * Mathf.PI * UnityEngine.Random.value;
                float r = Mathf.Sqrt(UnityEngine.Random.value * 3 * radius2 + radius2);
                //得到临近分布点
                Vector2 candidate = sample + r * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
 
                if (candidate.x<= rtWidth && candidate.y<=rtHeight && candidate.x>=0 && candidate.y>=0)
                {
                    found = true;
 
                    samplePoints.Add(candidate);
                    radius2 = radius * radius;
                    count++;
                    //找到采样点 进行颜色采样
                    Color candidateColor = texture.GetPixelBilinear(candidate.x, candidate.y);
                    if (sampleCountLimit == samplePointCount)
                    {
                        initColor = candidateColor;
                    }
                    else
                    {
                        initColor += candidateColor;
                        if (candidateColor != initColor / count)
                        {
                            //如果颜色有差异，说明不是纯色纹理，直接返回false
                            return false;
                        }
                    }
                    samplePointCount--;
                    break;
                }
            }
 
            if (!found)
            {
                // 如果这个点找不到周围可用点则移出采样点列表
                samplePoints[next] = samplePoints[samplePoints.Count - 1];
                samplePoints.RemoveAt(samplePoints.Count - 1);
            }
        }
        return true;
 
    }


    public enum TextureSize{
        S1024,
        S2048,
        S4096
    }

    public enum TextureConvertType{
        Single2HDRPMask,
        HDRP2Builtin,
    }
}
