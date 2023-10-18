using System;
using UnityEngine;
using Pangoo.Core.Common;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Pangoo;

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


        [Space][SerializeField] protected GameObject m_Prefab;

        // MEMBERS: -------------------------------------------------------------------------------

        [ShowInInspector]
        private GameObject m_Tooltip;

        private Image m_Image;
        [NonSerialized] private Text m_TooltipText;
        [NonSerialized] private TMP_Text m_TooltipTMPText;

        // PROPERTIES: ----------------------------------------------------------------------------

        // public override string Title => $"Show {this.m_Text}";

        // OVERRIDE METHODS: ----------------------------------------------------------------------

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
                dynamicObject.HotspotPosition + offset,
                ShortcutMainCamera.Transform.rotation
            );

            bool isActive = this.EnableInstance();
            instance.SetActive(isActive);
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
                    dynamicObject.HotspotPosition + dynamicObject.CachedTransfrom.TransformDirection(this.m_Params.Offset),
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

                RectTransform image = this.ConfigureImage(canvasTransform);

                // RectTransform background = this.ConfigureBackground(canvasTransform);
                // this.ConfigureText(background);

                // this.m_Tooltip.hideFlags = HideFlags.HideAndDontSave;

                Args args = new Args(dynamicObject);
                args.ChangeTarget(dynamicObject.Target);

                if (this.m_TooltipText != null) this.m_TooltipText.text = this.m_Params.Text;
                if (this.m_TooltipTMPText != null) this.m_TooltipTMPText.text = this.m_Params.Text;
            }

            return this.m_Tooltip;
        }

        private RectTransform ConfigureBackground(RectTransform parent)
        {
            GameObject gameObject = new GameObject("Background");

            Image image = gameObject.AddComponent<Image>();
            image.color = COLOR_BACKGROUND;

            VerticalLayoutGroup layoutGroup = gameObject.AddComponent<VerticalLayoutGroup>();
            layoutGroup.padding = new RectOffset(PADDING, PADDING, PADDING, PADDING);
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childScaleWidth = true;
            layoutGroup.childScaleHeight = true;
            layoutGroup.childForceExpandWidth = true;
            layoutGroup.childForceExpandHeight = true;

            ContentSizeFitter sizeFitter = gameObject.AddComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            PangooRectTransformUtility.SetAndCenterToParent(rectTransform, parent);

            return rectTransform;
        }
        private RectTransform ConfigureImage(RectTransform parent)
        {
            GameObject gameObject = new GameObject("Image");
            this.m_Image = gameObject.AddComponent<Image>();
            var Eye = Resources.Load<Sprite>("UI/UI_Eye");
            m_Image.sprite = Eye;

            // Debug.Log($"Point:{Eye}");
            // this.m_TooltipText = gameObject.AddComponent<Text>();

            // Font font = (Font)Resources.GetBuiltinResource(typeof(Font), FONT_NAME);
            // this.m_TooltipText.font = font;
            // this.m_TooltipText.fontSize = FONT_SIZE;

            RectTransform imageTransform = gameObject.GetComponent<RectTransform>();
            PangooRectTransformUtility.SetAndCenterToParent(imageTransform, parent);
            imageTransform.sizeDelta = new Vector2(30, 30);

            // Shadow shadow = gameObject.AddComponent<Shadow>();
            // shadow.effectColor = COLOR_BACKGROUND;
            // shadow.effectDistance = Vector2.one;

            return imageTransform;
        }


        private GameObject ConfigureText(RectTransform parent)
        {
            GameObject gameObject = new GameObject("Text");
            this.m_TooltipText = gameObject.AddComponent<Text>();
            var Point = Resources.Load<Sprite>("UI/UI_Point");
            Debug.Log($"Point:{Point}");
            // Resources.GetBuiltinResource(typeof())

            // Font font = (Font)Resources.GetBuiltinResource(typeof(Font), FONT_NAME);
            // this.m_TooltipText.font = font;
            // this.m_TooltipText.fontSize = FONT_SIZE;

            RectTransform textTransform = gameObject.GetComponent<RectTransform>();
            PangooRectTransformUtility.SetAndCenterToParent(textTransform, parent);

            Shadow shadow = gameObject.AddComponent<Shadow>();
            shadow.effectColor = COLOR_BACKGROUND;
            shadow.effectDistance = Vector2.one;

            return gameObject;
        }

        public override void LoadParamsFromJson(string val)
        {
            m_Params.LoadFromJson(val);
        }
        public override string ParamsToJson()
        {
            return m_Params.ToJson();
        }
    }
}