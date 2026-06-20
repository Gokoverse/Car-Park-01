using AndroidParkingIdle.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AndroidParkingIdle.UI
{
    // Bu dosya, ana ekran arayuzunu kurar; oyuncu buradan para kazanabilir, oyuna girebilir veya kaydi sifirlayabilir.
    public class MainMenuController : MonoBehaviour
    {
        private GameData data;
        private EconomyService economy;
        private Text moneyText;

        private void Start()
        {
            data = SaveSystem.Load();
            economy = new EconomyService(data);
            BuildInterface();
            Refresh();
        }

        private void BuildInterface()
        {
            EnsureEventSystem();
            Canvas canvas = ResponsiveCanvasScaler.CreateCanvas("MainMenuCanvas");
            VerticalLayoutGroup layout = CreatePanel(canvas.transform, 80, 80, 80, 80);

            CreateText(layout.transform, "ARABA PARK", 72, TextAnchor.MiddleCenter);
            moneyText = CreateText(layout.transform, "", 42, TextAnchor.MiddleCenter);
            CreateButton(layout.transform, "Para Kazan", OnEarnMoney);
            CreateButton(layout.transform, "Oyuna Basla", () => SceneManager.LoadScene("ParkingGame"));
            CreateButton(layout.transform, "Kaydi Sifirla", OnResetSave);
        }

        private void OnEarnMoney()
        {
            economy.EarnTapReward();
            SaveSystem.Save(data);
            Refresh();
        }

        private void OnResetSave()
        {
            SaveSystem.Reset();
            data = new GameData();
            economy = new EconomyService(data);
            Refresh();
        }

        private void Refresh()
        {
            moneyText.text = $"Para: {data.money} | Seviye: {data.level}";
        }

        private static void EnsureEventSystem()
        {
            if (FindObjectOfType<EventSystem>() != null)
            {
                return;
            }

            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        private static VerticalLayoutGroup CreatePanel(Transform parent, int left, int right, int top, int bottom)
        {
            GameObject panel = new GameObject("MenuPanel", typeof(RectTransform), typeof(VerticalLayoutGroup));
            panel.transform.SetParent(parent, false);
            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(left, bottom);
            rect.offsetMax = new Vector2(-right, -top);

            VerticalLayoutGroup layout = panel.GetComponent<VerticalLayoutGroup>();
            layout.spacing = 28;
            layout.padding = new RectOffset(24, 24, 120, 120);
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childControlHeight = false;
            layout.childControlWidth = true;
            return layout;
        }

        private static Text CreateText(Transform parent, string value, int size, TextAnchor anchor)
        {
            GameObject item = new GameObject("Text", typeof(Text));
            item.transform.SetParent(parent, false);
            Text text = item.GetComponent<Text>();
            text.text = value;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = size;
            text.alignment = anchor;
            text.color = new Color(0.08f, 0.09f, 0.11f);
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 120);
            return text;
        }

        private static void CreateButton(Transform parent, string label, UnityEngine.Events.UnityAction action)
        {
            GameObject buttonObject = new GameObject(label, typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);
            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.14f, 0.42f, 0.9f);
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(action);
            buttonObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 120);

            Text text = CreateText(buttonObject.transform, label, 40, TextAnchor.MiddleCenter);
            text.color = Color.white;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
        }
    }
}
