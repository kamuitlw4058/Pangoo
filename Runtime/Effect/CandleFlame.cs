using System.Collections;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Pangoo.Effect
{



    public class CandleFlame : MonoBehaviour
    {

        Material material;

        [LabelText("固定高度的间隔")]
        [HideInEditorMode]
        public float FlameHeightInterval = 2;

        [LabelText("固定高度的间隔区间")]
        public Vector2 FlameHeightIntervalRange = new Vector2(3, 10);

        [LabelText("变动高度的间隔")]
        [HideInEditorMode]
        public float FlameHeightLerpInterval = 0.5f;

        [LabelText("变动高度的间隔区间")]

        public Vector2 FlameHeightLerpIntervalRange = new Vector2(1, 3);


        float FlameHeightLastest;

        float FlameHeightNext;

        [ReadOnly]
        public float FlameHeightCurrent;


        float FlameHeightProgressTime;
        float FlameHeightLerpProgressTime;

        [ReadOnly]
        public bool InLerpHeight;

        [LabelText("变动高度的范围")]
        public Vector2 FlameHeightRange;


        float LastestX;

        [Range(0.01f, 2)]
        [LabelText("横向移动导致的偏移响应速度")]
        public float Factor = 1;

        float XSpeedNext;

        float XSpeed;

        [LabelText("横向移动最大偏移量")]
        public Vector2 FlameXRange = new Vector2(-2.5f, 2.5f);



        // Start is called before the first frame update
        void Start()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                material = renderer.material;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (material == null)
            {
                return;
            }


            if (!InLerpHeight)
            {
                if (FlameHeightProgressTime > FlameHeightInterval)
                {
                    InLerpHeight = true;
                    FlameHeightLerpProgressTime = 0;
                    FlameHeightNext = UnityEngine.Random.Range(FlameHeightRange.x, FlameHeightRange.y);
                    FlameHeightLerpInterval = UnityEngine.Random.Range(FlameHeightLerpIntervalRange.x, FlameHeightLerpIntervalRange.y);
                }
                else
                {
                    FlameHeightProgressTime += Time.deltaTime;
                }
            }



            if (InLerpHeight)
            {
                FlameHeightLerpProgressTime += Time.deltaTime;
                var FlameHeightLerpProgress = Mathf.Min(1, FlameHeightLerpProgressTime / FlameHeightLerpInterval);
                FlameHeightCurrent = Mathf.Lerp(FlameHeightLastest, FlameHeightNext, FlameHeightLerpProgress);
                material.SetFloat("_NoiseOffsetY", FlameHeightCurrent);

                if (FlameHeightLerpProgressTime >= FlameHeightLerpInterval)
                {
                    FlameHeightProgressTime = 0;
                    FlameHeightLastest = FlameHeightNext;
                    InLerpHeight = false;
                    FlameHeightInterval = UnityEngine.Random.Range(FlameHeightIntervalRange.x, FlameHeightIntervalRange.y);
                }
            }

            if (LastestX != transform.position.x)
            {
                var diff = transform.position.x - LastestX;
                XSpeedNext += diff * Factor * -100 * Mathf.Abs(FlameHeightCurrent);
                XSpeedNext = Mathf.Clamp(XSpeedNext, FlameXRange.x, FlameXRange.y);
                XSpeed = Mathf.Lerp(XSpeed, XSpeedNext, 0.5f);
                material.SetFloat("_NoiseOffsetX", XSpeed);
                LastestX = transform.position.x;
            }
            else
            {
                XSpeedNext = 0;
                XSpeed = Mathf.Lerp(XSpeed, XSpeedNext, 0.5f);
                material.SetFloat("_NoiseOffsetX", XSpeed);
            }
        }
    }
}