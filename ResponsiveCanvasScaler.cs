using UnityEngine;
using UnityEngine.UI;

namespace AndroidParkingIdle.UI
{
    // Bu dosya, Android telefon ekran oranlarinda UI'nin okunabilir kalmasi icin CanvasScaler ayarlarini yapar.
    public static class ResponsiveCanvasScaler
    {
        public static Canvas CreateCanvas(string name)
        {
            GameObject canvasObject = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            Canvas canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.matchWidthOrHeight = 0.5f;

            GameObject backgroundObject = new GameObject("Background", typeof(Image));
            backgroundObject.transform.SetParent(canvasObject.transform, false);
            Image background = backgroundObject.GetComponent<Image>();
            background.color = new Color(0.94f, 0.96f, 0.98f);

            RectTransform backgroundRect = backgroundObject.GetComponent<RectTransform>();
            backgroundRect.anchorMin = Vector2.zero;
            backgroundRect.anchorMax = Vector2.one;
            backgroundRect.offsetMin = Vector2.zero;
            backgroundRect.offsetMax = Vector2.zero;
            backgroundObject.transform.SetAsFirstSibling();

            return canvas;
        }
    }
}
