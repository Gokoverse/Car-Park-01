using AndroidParkingIdle.Core;
using AndroidParkingIdle.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AndroidParkingIdle.UI
{
    // Bu dosya, oyun ekranini, otomatik gelir dongusunu, upgrade butonlarini ve seviye ilerlemesini yonetir.
    public class GameUIController : MonoBehaviour
    {
        private GameData data;
        private EconomyService economy;
        private LevelService levels;
        private ParkingLotView parkingLot;
        private Text statsText;
        private Text levelText;
        private Text upgradeText;
        private float autoIncomeTimer;

        private void Start()
        {
            data = SaveSystem.Load();
            economy = new EconomyService(data);
            levels = new LevelService(data);
            BuildInterface();
            Refresh();
        }

        private void Update()
        {
            autoIncomeTimer += Time.deltaTime;
            if (autoIncomeTimer < 1f || data.AutoIncomePerSecond <= 0)
            {
                return;
            }

            autoIncomeTimer = 0f;
            economy.AddAutoIncomeTick();
            SaveSystem.Save(data);
            Refresh();
        }

        private void BuildInterface()
        {
            EnsureEventSystem();
            Canvas canvas = ResponsiveCanvasScaler.CreateCanvas("GameCanvas");
            VerticalLayoutGroup root = CreatePanel(canvas.transform);

            levelText = CreateText(root.transform, "", 48, TextAnchor.MiddleCenter);
            statsText = CreateText(root.transform, "", 34, TextAnchor.MiddleCenter);

            GridLayoutGroup lotGrid = CreateLotGrid(root.transform);
            parkingLot = new ParkingLotView(lotGrid.transform);

            CreateButton(root.transform, "Para Kazan", OnEarnMoney);
            upgradeText = CreateButton(root.transform, "", OnUpgradeAutoIncome);
            CreateButton(root.transform, "Araba Park Et", OnCompleteParking);
            CreateButton(root.transform, "Ana Ekran", () => SceneManager.LoadScene("MainMenu"));
        }

        private void OnEarnMoney()
        {
            economy.EarnTapReward();
            SaveSystem.Save(data);
            Refresh();
        }

        private void OnUpgradeAutoIncome()
        {
            economy.TryBuyAutoIncomeUpgrade();
            SaveSystem.Save(data);
            Refresh();
        }

        private void OnCompleteParking()
        {
            levels.CompleteParkingJob();
            SaveSystem.Save(data);
            Refresh();
        }

        private void Refresh()
        {
            levelText.text = $"Seviye {data.level}";
            statsText.text = $"Para: {data.money}\nXP: {data.experience}/{data.ExperienceToNextLevel}\nOtomatik Gelir: {data.AutoIncomePerSecond}/sn\nPark Edilen: {data.totalParkedCars}";
            upgradeText.text = $"Otomatik Gelir Upgrade ({data.UpgradeCost})";
            parkingLot.Rebuild(data.level);
        }

        private static void EnsureEventSystem()
        {
            if (FindObjectOfType<EventSystem>() != null)
            {
                return;
            }

            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        private static VerticalLayoutGroup CreatePanel(Transform parent)
        {
            GameObject panel = new GameObject("GamePanel", typeof(RectTransform), typeof(VerticalLayoutGroup));
            panel.transform.SetParent(parent, false);
            RectTransform rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(56, 56);
            rect.offsetMax = new Vector2(-56, -56);

            VerticalLayoutGroup layout = panel.GetComponent<VerticalLayoutGroup>();
            layout.spacing = 18;
            layout.padding = new RectOffset(12, 12, 48, 48);
            layout.childAlignment = TextAnchor.UpperCenter;
            layout.childControlHeight = false;
            layout.childControlWidth = true;
            return layout;
        }

        private static GridLayoutGroup CreateLotGrid(Transform parent)
        {
            GameObject gridObject = new GameObject("ParkingLot", typeof(RectTransform), typeof(GridLayoutGroup));
            gridObject.transform.SetParent(parent, false);
            RectTransform rect = gridObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 360);

            GridLayoutGroup grid = gridObject.GetComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(150, 90);
            grid.spacing = new Vector2(18, 18);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 3;
            grid.childAlignment = TextAnchor.MiddleCenter;
            return grid;
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
            item.GetComponent<RectTransform>().sizeDelta = new Vector2(0, size * 3);
            return text;
        }

        private static Text CreateButton(Transform parent, string label, UnityEngine.Events.UnityAction action)
        {
            GameObject buttonObject = new GameObject(label, typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);
            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.14f, 0.42f, 0.9f);
            Button button = buttonObject.GetComponent<Button>();
            button.onClick.AddListener(action);
            buttonObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 108);

            Text text = CreateText(buttonObject.transform, label, 34, TextAnchor.MiddleCenter);
            text.color = Color.white;
            RectTransform textRect = text.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;
            return text;
        }
    }
}
