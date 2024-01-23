
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using DG.Tweening;

namespace Pangoo.Common
{
    public static class TweenerUtility
    {

        public static void TweenerSetFlashEase(Tweener tweener, TweenFlashType tweenFlashType)
        {
            switch (tweenFlashType)
            {
                case TweenFlashType.Flash:
                    tweener.SetEase(Ease.Flash, 2, 0);
                    break;
                case TweenFlashType.InFlash:
                    tweener.SetEase(Ease.InFlash, 2, 0);
                    break;
                case TweenFlashType.OutFlash:
                    tweener.SetEase(Ease.OutFlash, 2, 0);
                    break;
                case TweenFlashType.InOutFlash:
                    tweener.SetEase(Ease.InOutFlash, 2, 0);
                    break;
            }
        }

        public static void TweenerSetNormalEase(Tweener tweener, TweenNormalEaseType easeType)
        {
            switch (easeType)
            {
                case TweenNormalEaseType.InSine:
                    tweener.SetEase(Ease.InSine);
                    break;
                case TweenNormalEaseType.OutSine:
                    tweener.SetEase(Ease.OutSine);
                    break;
                case TweenNormalEaseType.InOutSine:
                    tweener.SetEase(Ease.InOutSine);
                    break;

                case TweenNormalEaseType.InQuad:
                    tweener.SetEase(Ease.InQuad);
                    break;
                case TweenNormalEaseType.OutQuad:
                    tweener.SetEase(Ease.OutQuad);
                    break;
                case TweenNormalEaseType.InOutQuad:
                    tweener.SetEase(Ease.InOutQuad);
                    break;

                case TweenNormalEaseType.InCubic:
                    tweener.SetEase(Ease.InCubic);
                    break;
                case TweenNormalEaseType.OutCubic:
                    tweener.SetEase(Ease.OutCubic);
                    break;
                case TweenNormalEaseType.InOutCubic:
                    tweener.SetEase(Ease.InOutCubic);
                    break;

                case TweenNormalEaseType.InQuart:
                    tweener.SetEase(Ease.InQuad);
                    break;
                case TweenNormalEaseType.OutQuart:
                    tweener.SetEase(Ease.OutQuart);
                    break;
                case TweenNormalEaseType.InOuQuart:
                    tweener.SetEase(Ease.InOutQuart);
                    break;

                case TweenNormalEaseType.InExpo:
                    tweener.SetEase(Ease.InExpo);
                    break;
                case TweenNormalEaseType.OutExpo:
                    tweener.SetEase(Ease.OutExpo);
                    break;
                case TweenNormalEaseType.InOutExpo:
                    tweener.SetEase(Ease.InOutExpo);
                    break;
            }
        }

        public static (float, float) GetTweenValue(float startOrigin, TweenTransformStartTypeEnum startType, TweenTransformEndTypeEnum endType, float max, float min = 0)
        {
            float start = 0;
            switch (startType)
            {
                case TweenTransformStartTypeEnum.RelativeOrigin:
                    start = startOrigin + (float)min;
                    break;
                case TweenTransformStartTypeEnum.ConfigValue:
                    start = (float)min;
                    break;
            }

            float end = 0;
            switch (endType)
            {
                case TweenTransformEndTypeEnum.RelativeStart:
                    end = start + (float)max;
                    break;
                case TweenTransformEndTypeEnum.ConfigValue:
                    end = (float)max;
                    break;
            }


            return (start, end);
        }

        public static bool HasTweenTransformType(TweenTransformType inputType, TweenTransformType checkType)
        {
            if (((int)inputType & (int)checkType) > 0)
            {
                return true;
            }
            return false;
        }


        public static bool HasTweenTransformPositionType(TweenTransformType inputType)
        {
            if (HasTweenTransformType(inputType, TweenTransformType.PostionX))
            {
                return true;
            }

            if (HasTweenTransformType(inputType, TweenTransformType.PostionY))
            {
                return true;
            }

            if (HasTweenTransformType(inputType, TweenTransformType.PostionZ))
            {
                return true;
            }
            return false;
        }

        public static bool HasTweenTransformRotationType(TweenTransformType inputType)
        {

            if (HasTweenTransformType(inputType, TweenTransformType.RotationX))
            {
                return true;
            }

            if (HasTweenTransformType(inputType, TweenTransformType.RotationY))
            {
                return true;
            }

            if (HasTweenTransformType(inputType, TweenTransformType.RotationZ))
            {
                return true;
            }

            return false;
        }


        public static (Vector3, Vector3) TweenPositionStartEnd(Vector3 startOrigin, TweenTransformType inputType, TweenTransformStartTypeEnum startType, TweenTransformEndTypeEnum endType, float max, float min = 0)
        {
            Vector3 start = startOrigin;
            Vector3 end = start;


            if (HasTweenTransformType(inputType, TweenTransformType.PostionX))
            {
                (start.x, end.x) = GetTweenValue(startOrigin.x, startType, endType, max, min);
            }

            if (HasTweenTransformType(inputType, TweenTransformType.PostionY))
            {
                (start.y, end.y) = GetTweenValue(startOrigin.y, startType, endType, max, min);
            }

            if (HasTweenTransformType(inputType, TweenTransformType.PostionZ))
            {
                (start.z, end.z) = GetTweenValue(startOrigin.z, startType, endType, max, min);
            }

            return (start, end);
        }


        public static (Vector3, Vector3) TweenRotationStartEnd(Vector3 startOrigin, TweenTransformType inputType, TweenTransformStartTypeEnum startType, TweenTransformEndTypeEnum endType, float max, float min = 0)
        {
            Vector3 start = startOrigin;
            Vector3 end = start;


            if (HasTweenTransformType(inputType, TweenTransformType.RotationX))
            {
                (start.x, end.x) = GetTweenValue(startOrigin.x, startType, endType, max, min);
            }

            if (HasTweenTransformType(inputType, TweenTransformType.RotationY))
            {
                (start.y, end.y) = GetTweenValue(startOrigin.y, startType, endType, max, min);
            }

            if (HasTweenTransformType(inputType, TweenTransformType.RotationZ))
            {
                (start.z, end.z) = GetTweenValue(startOrigin.z, startType, endType, max, min);
            }

            return (start, end);
        }



    }
}