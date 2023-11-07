using System;
using UnityEngine;
using Pangoo.Core.Common;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Pangoo;
using System.Collections.Generic;

namespace Pangoo.Core.VisualScripting
{
    // [Title("Show Text")]
    // [Image(typeof(IconString), ColorTheme.Type.Blue)]

    [Category("Tooltips/Show Text")]
    // [Description(
    //     "Displays a text in a world-space canvas when the Hotspot is enabled and hides it " +
    //     "when is disabled. If no Prefab is provided, a default UI is displayed"
    // )]

    [Serializable]
    public class SpotTooltipText : HotSpot
    {
        private const float CANVAS_WIDTH = 600f;
        private const float CANVAS_HEIGHT = 300f;

        private const float SIZE_X = 2f;
        private const float SIZE_Y = 1f;

        private const int PADDING = 50;

        private const string FONT_NAME = "LegacyRuntime.ttf";

        private const int FONT_SIZE = 32;

        private static readonly Color COLOR_BACKGROUND = new Color(0f, 0f, 0f, 0.5f);

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [HideReferenceObjectPicker]
        [SerializeField]
        HotSpotTipParams m_Params = new HotSpotTipParams();


        [Space][SerializeField][HideInEditorMode] protected GameObject m_Prefab;

        // MEMBERS: -------------------------------------------------------------------------------

        [ShowInInspector]
        [HideInEditorMode]
        private GameObject m_Tooltip;

        private Image m_Eye;
        private Image m_Point;

        private const float TRANSITION_SMOOTH_TIME = 0.25f;

        [NonSerialized] private Text m_TooltipText;
        [NonSerialized] private TMP_Text m_TooltipTMPText;


        [ShowInInspector]
        public HotsoptState SpotState
        {
            get
            {
                if (!dynamicObject.IsHotspotActive)
                {
                    return HotsoptState.None;
                }

                if (dynamicObject.IsHotspotBanInteractActive)
                {
                    return HotsoptState.ShowDisable;
                }

                if (dynamicObject.IsHotspotInteractActive)
                {
                    return HotsoptState.ShowInteract;
                }

                return HotsoptState.ShowUI;
            }
        }

        HotsoptState LastestSpotState = HotsoptState.None;
        HotsoptState CurrentSpotState = HotsoptState.None;


        float Transition { get; set; }

        bool IsTransitioning;

        private float m_Velocity;


        List<SpotState> states = new List<SpotState>();


        public void UpdateState(float transition, bool onlySub = false)
        {
            foreach (var state in states)
            {
                if (state.State == CurrentSpotState)
                {
                    if (onlySub)
                    {
                        state.SubAlpha(transition);
                    }
                    else
                    {
                        state.AddAlpha(transition);
                    }

                }
                else
                {
                    state.SubAlpha((1 - Transition));
                }
            }
        }




        protected override void DoUpdate()
        {
            base.DoUpdate();

            GameObject instance = this.RequireInstance();
            if (instance == null) return;

            Vector3 offset = this.m_Params.Space switch
            {
                Space.World => this.m_Params.Offset,
                Space.Self => dynamicObject.CachedTransfrom.TransformDirection(this.m_Params.Offset),
                _ => throw new ArgumentOutOfRangeException()
            };

            instance.transform.SetPositionAndRotation(
                dynamicObject.HotspotInteractPosition + offset,
                ShortcutMainCamera.Transform.rotation
            );

            bool isActive = this.EnableInstance();
            instance.SetActive(isActive);

            if (CurrentSpotState != SpotState)
            {
                Transition = 0;
                m_Velocity = 0;
                if (IsTransitioning)
                {
                    UpdateState(1, true);
                    LastestSpotState = CurrentSpotState;
                }
                CurrentSpotState = SpotState;
                IsTransitioning = true;

            }
            else
            {
                if (LastestSpotState != CurrentSpotState)
                {
                    if (IsTransitioning)
                    {
                        this.Transition = Mathf.SmoothDamp(
                            this.Transition,
                           1,
                            ref this.m_Velocity,
                            TRANSITION_SMOOTH_TIME
                        );

                        UpdateState(Transition);

                        if (Transition == 1)
                        {
                            IsTransitioning = false;
                            LastestSpotState = CurrentSpotState;
                        }
                    }
                }

            }

        }


        protected virtual bool EnableInstance()
        {
            return dynamicObject.IsHotspotActive;
        }


        protected GameObject RequireInstance()
        {
            if (this.m_Tooltip == null)
            {

                this.m_Tooltip = new GameObject("Tooltip");

                this.m_Tooltip.transform.SetPositionAndRotation(
                    dynamicObject.HotspotInteractPosition + dynamicObject.CachedTransfrom.TransformDirection(this.m_Params.Offset),
                    ShortcutMainCamera.Transform.rotation
                );
                this.m_Tooltip.transform.SetParent(dynamicObject.CachedTransfrom);

                Canvas canvas = this.m_Tooltip.AddComponent<Canvas>();
                this.m_Tooltip.AddComponent<CanvasScaler>();

                canvas.renderMode = RenderMode.WorldSpace;
                canvas.worldCamera = ShortcutMainCamera.Instance;

                RectTransform canvasTransform = this.m_Tooltip.GetComponent<RectTransform>();
                canvasTransform.sizeDelta = new Vector2(CANVAS_WIDTH, CANVAS_HEIGHT);
                canvasTransform.localScale = new Vector3(
                    SIZE_X / CANVAS_WIDTH,
                    SIZE_Y / CANVAS_HEIGHT,
                    1f
                );


                SpotState HandState = new SpotState();
                HandState.Enable = true;
                HandState.image = CreateHand(canvasTransform);
                HandState.State = HotsoptState.ShowInteract;

                states.Add(HandState);

                // SpotState EyeState = new SpotState();
                // EyeState.Enable = true;
                // EyeState.image = CreateEye(canvasTransform);
                // EyeState.State = HotsoptState.ShowInteract;

                // states.Add(EyeState);

                SpotState Point = new SpotState();
                Point.Enable = true;
                Point.image = CreatePoint(canvasTransform);
                Point.State = HotsoptState.ShowUI;
                states.Add(Point);

                SpotState Ban = new SpotState();
                Ban.Enable = true;
                Ban.image = CreateBan(canvasTransform);
                Ban.State = HotsoptState.ShowDisable;

                states.Add(Ban);


            }

            return this.m_Tooltip;
        }




        // private RectTransform ConfigureBackground(RectTransform parent)
        // {
        //     GameObject gameObject = new GameObject("Background");

        //     Image image = gameObject.AddComponent<Image>();
        //     image.color = COLOR_BACKGROUND;

        //     VerticalLayoutGroup layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
        //     layoutGroup.padding = new RectOffset(PADDING, PADDING, PADDING, PADDING);
        //     layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        //     layoutGroup.childControlWidth = true;
        //     layoutGroup.childControlHeight = true;
        //     layoutGroup.childScaleWidth = true;
        //     layoutGroup.childScaleHeight = true;
        //     layoutGroup.childForceExpandWidth = true;
        //     layoutGroup.childForceExpandHeight = true;

        //     ContentSizeFitter sizeFitter = gameObject.AddComponent<ContentSizeFitter>();
        //     sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        //     sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        //     RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //     PangooRectTransformUtility.SetAndCenterToParent(rectTransform, parent);

        //     return rectTransform;
        // }



        private Image ConfigureImage(RectTransform parent, string resourcePath)
        {
            GameObject imageGo = new GameObject("UI");
            var image = imageGo.AddComponent<Image>();
            var mat = Resources.Load<Material>(resourcePath);
            image.material = mat;
            RectTransform imageTransform = imageGo.GetComponent<RectTransform>();
            PangooRectTransformUtility.SetAndCenterToParent(imageTransform, parent);
            imageTransform.sizeDelta = new Vector2(30, 30);

            return image;
        }

        private Image CreateEye(RectTransform parent)
        {
            return ConfigureImage(parent, "UI/UI_Eye");
        }

        private Image CreatePoint(RectTransform parent)
        {
            return ConfigureImage(parent, "UI/UI_Point");
        }

        private Image CreateBan(RectTransform parent)
        {
            return ConfigureImage(parent, "UI/UI_Ban");
        }
        private Image CreateHand(RectTransform parent)
        {
            return ConfigureImage(parent, "UI/UI_Hand");
        }


        // private GameObject ConfigureText(RectTransform parent)
        // {
        //     GameObject gameObject = new GameObject("Text");
        //     this.m_TooltipText = gameObject.AddComponent<Text>();
        //     var Point = Resources.Load<Sprite>("UI/UI_Point");
        //     Debug.Log($"Point:{Point}");
        //     // Resources.GetBuiltinResource(typeof())

        //     // Font font = (Font)Resources.GetBuiltinResource(typeof(Font), FONT_NAME);
        //     // this.m_TooltipText.font = font;
        //     // this.m_TooltipText.fontSize = FONT_SIZE;

        //     RectTransform textTransform = gameObject.GetComponent<RectTransform>();
        //     PangooRectTransformUtility.SetAndCenterToParent(textTransform, parent);

        //     Shadow shadow = gameObject.AddComponent<Shadow>();
        //     shadow.effectColor = COLOR_BACKGROUND;
        //     shadow.effectDistance = Vector2.one;

        //     return gameObject;
        // }

        public override void LoadParamsFromJson(string val)
        {
            m_Params.Load(val);
        }
        public override string ParamsToJson()
        {
            return m_Params.Save();
        }
    }
    public enum HotsoptState
    {
        None,
        ShowUI,
        ShowInteract,

        ShowDisable,
    }


    public class SpotState
    {
        public bool Enable;

        public HotsoptState State;

        public Image image;

        public float m_Alpha = -1;

        public Material material;
        public float Alpha
        {
            get
            {
                if (image == null) return 0;

                if (m_Alpha < 0)
                {
                    m_Alpha = image.material.color.a;
                }


                return m_Alpha;
            }
            set
            {
                m_Alpha = value;
                SetImageAlpha(image, value);
            }
        }

        public void AddAlpha(float alpha)
        {
            if (Alpha < alpha)
            {
                Alpha = alpha;
            }
        }

        public void SubAlpha(float alpha)
        {
            if (Alpha > alpha)
            {
                Alpha = alpha;
            }
        }

        void SetImageAlpha(Image image, float alpha)
        {
            if (image == null) return;

            if (material == null)
            {
                material = new Material(image.material);
                image.material = material;
            }
            var color = material.color;
            if (color.a != alpha)
            {

                material.color = new Color(color.r, color.g, color.b, alpha);
            }
        }
    }
}