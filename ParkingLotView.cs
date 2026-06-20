using UnityEngine;
using UnityEngine.UI;

namespace AndroidParkingIdle.Gameplay
{
    // Bu dosya, seviye ilerledikce ekranda daha dolu gorunen basit otopark gorselini olusturur.
    public class ParkingLotView
    {
        private readonly Transform parent;
        private readonly Color emptyColor = new Color(0.18f, 0.2f, 0.22f);
        private readonly Color carColor = new Color(0.2f, 0.72f, 0.5f);

        public ParkingLotView(Transform parent)
        {
            this.parent = parent;
        }

        public void Rebuild(int level)
        {
            foreach (Transform child in parent)
            {
                Object.Destroy(child.gameObject);
            }

            int slots = Mathf.Clamp(4 + level, 5, 12);
            for (int i = 0; i < slots; i++)
            {
                GameObject slot = new GameObject($"ParkingSlot_{i + 1}", typeof(Image));
                slot.transform.SetParent(parent, false);
                Image image = slot.GetComponent<Image>();
                image.color = i < Mathf.Min(level, slots) ? carColor : emptyColor;

                RectTransform rect = slot.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(150, 90);
            }
        }
    }
}
