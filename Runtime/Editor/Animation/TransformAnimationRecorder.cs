using System;
using System.Collections.Generic;
using System.IO;
using Pangoo;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Pangoo
{
    public class TransformAnimationRecorder : MonoBehaviour
    {
        public UnityEvent InitRecorderEvent;
        public UnityEvent StartRecorderEvent;

        public Transform RootTransform;
        public string path = "Assets/Temp";

        public string clipName = "test";

        [ReadOnly]
        public bool LastestStarted = false;

        [ReadOnly]
        public bool Started = false;

        public float DelayStart = 0.1f;

        public int KeyframeCount = 0;
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        [Button("Init")]
        public void Init()
        {
            if (RootTransform == null) return;

            transforms.Add(RootTransform);
            CurveDict.Add(RootTransform, new TransformAllCurve(""));


            transforms.AddRange(RootTransform.Children());
            foreach (var trans in RootTransform.Children())
            {
                CurveDict.Add(trans, new TransformAllCurve(trans.name, RootTransform));
            }

            InitRecorderEvent?.Invoke();
        }

        public List<Transform> transforms = new List<Transform>();

        [ShowInInspector]
        public Dictionary<Transform, TransformAllCurve> CurveDict = new Dictionary<Transform, TransformAllCurve>();


        [Button("StartRecord")]
        [EnableIf("@!this.Started")]
        public void StartRecord()
        {

            Started = true;
            StartRecorderEvent?.Invoke();
        }

        [Button("EndRecord")]
        [EnableIf("@this.Started")]
        public void EndRecord()
        {
            Started = false;
            SaveAnimationClip();
        }

        AnimationClip SaveAnimationClip()
        {
            AnimationClip clip = new AnimationClip();
            clip.frameRate = 60;
            foreach (var kv in CurveDict)
            {
                kv.Value.Bind(clip);
            }
            int index = 2;

            var animPath = path + "/" + clipName + ".anim";
            while (File.Exists(animPath))
            {
                animPath = $"{path}/{clipName}_{index}.anim";
                index += 1;
            }

            AssetDatabase.CreateAsset(clip, animPath);
            AssetDatabase.SaveAssets();
            return clip;
        }

        // Update is called once per frame

        [ReadOnly]
        public float StartedTime = 0;
        void Update()
        {


            if (Started)
            {
                if (DelayStart > 0)
                {
                    DelayStart -= Time.deltaTime;
                    if (DelayStart <= 0)
                    {
                        DelayStart = 0;
                    }
                    else
                    {
                        return;
                    }
                }

                StartedTime += Time.deltaTime;
                if (LastestStarted != Started)
                {
                    StartedTime = 0;
                    KeyframeCount = 0;
                    foreach (var kv in CurveDict)
                    {
                        kv.Value.Clear();
                        kv.Value.Init(kv.Key);
                    }
                }


                foreach (var trans in transforms)
                {
                    CurveDict[trans].AddKeyFrame(trans, StartedTime);
                }
                KeyframeCount += 1;

            }
            LastestStarted = Started;

        }

        [Serializable]
        public class TransformKeyFrame
        {
            public string name;
            public float time;
            public float x;

            public float y;
        }
        public class TransformAllCurve
        {
            Transform parent;
            public TransformCurve PostionX;
            public TransformCurve PostionY;
            public TransformCurve PostionZ;

            public TransformCurve EulerAnglesX;
            public TransformCurve EulerAnglesY;
            public TransformCurve EulerAnglesZ;

            public TransformAllCurve(string path, Transform parent = null)
            {
                this.parent = parent;
                PostionX = TransformCurve.CreatePositionX(path);
                PostionY = TransformCurve.CreatePositionY(path);
                PostionZ = TransformCurve.CreatePositionZ(path);
                EulerAnglesX = TransformCurve.CreatelocalEulerAnglesRawX(path);
                EulerAnglesY = TransformCurve.CreatelocalEulerAnglesRawY(path);
                EulerAnglesZ = TransformCurve.CreatelocalEulerAnglesRawZ(path);
            }

            public void Init(Transform trans)
            {
                PostionX.Init(trans.localPosition.x);
                PostionY.Init(trans.localPosition.y);
                PostionZ.Init(trans.localPosition.z);
                EulerAnglesX.Init(trans.localEulerAngles.x);
                EulerAnglesY.Init(trans.localEulerAngles.y);
                EulerAnglesZ.Init(trans.localEulerAngles.z);
            }

            public void AddKeyFrame(Transform trans, float StartedTime)
            {
                if (parent != null)
                {
                    PostionX.AddKeyFrame(StartedTime, trans.position.x - parent.position.x);
                    PostionY.AddKeyFrame(StartedTime, trans.position.y - parent.position.y);
                    PostionZ.AddKeyFrame(StartedTime, trans.position.z - parent.position.z);
                }
                else
                {
                    PostionX.AddKeyFrame(StartedTime, trans.localPosition.x);
                    PostionY.AddKeyFrame(StartedTime, trans.localPosition.y);
                    PostionZ.AddKeyFrame(StartedTime, trans.localPosition.z);

                }

                EulerAnglesX.AddKeyFrame(StartedTime, trans.localEulerAngles.x);
                EulerAnglesY.AddKeyFrame(StartedTime, trans.localEulerAngles.y);
                EulerAnglesZ.AddKeyFrame(StartedTime, trans.localEulerAngles.z);

            }

            public void Bind(AnimationClip clip)
            {
                PostionX.Bind(clip);
                PostionY.Bind(clip);
                PostionZ.Bind(clip);
                EulerAnglesX.Bind(clip);
                EulerAnglesY.Bind(clip);
                EulerAnglesZ.Bind(clip);
            }

            public void Clear()
            {
                PostionX.Clear();
                PostionY.Clear();
                PostionZ.Clear();
                EulerAnglesX.Clear();
                EulerAnglesY.Clear();
                EulerAnglesZ.Clear();
            }
        }



        public class TransformCurve
        {
            EditorCurveBinding curveBinding;
            public AnimationCurve curve = new AnimationCurve();

            float lastestVal = 0;

            public TransformCurve(string path, string propertyName, Type type)
            {
                curveBinding = new EditorCurveBinding();
                curveBinding.path = path;
                curveBinding.propertyName = propertyName;
                curveBinding.type = type;
            }

            public void Init(float val)
            {
                // lastestVal = val;
            }

            public static TransformCurve CreatePositionX(string path)
            {
                return new TransformCurve(path, "m_LocalPosition.x", typeof(Transform));
            }

            public static TransformCurve CreatePositionY(string path)
            {
                return new TransformCurve(path, "m_LocalPosition.y", typeof(Transform));
            }

            public static TransformCurve CreatePositionZ(string path)
            {
                return new TransformCurve(path, "m_LocalPosition.z", typeof(Transform));
            }

            public static TransformCurve CreatelocalEulerAnglesRawX(string path)
            {
                return new TransformCurve(path, "localEulerAnglesRaw.x", typeof(Transform));
            }

            public static TransformCurve CreatelocalEulerAnglesRawY(string path)
            {
                return new TransformCurve(path, "localEulerAnglesRaw.y", typeof(Transform));
            }

            public static TransformCurve CreatelocalEulerAnglesRawZ(string path)
            {
                return new TransformCurve(path, "localEulerAnglesRaw.z", typeof(Transform));
            }





            public void AddKeyFrame(float time, float val)
            {

                curve.AddKey(time, val - lastestVal);
                // lastestVal = val;
            }

            public void Bind(AnimationClip clip)
            {
                AnimationUtility.SetEditorCurve(clip, curveBinding, curve);
            }

            public void Clear()
            {
                curve = new AnimationCurve();
            }

        }
    }
}