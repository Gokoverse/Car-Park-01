namespace AndroidParkingIdle.Core
{
    // Bu dosya, para kazanma ve otomatik gelir upgrade hesaplarini tek yerde toplar.
    public class EconomyService
    {
        private readonly GameData data;

        public EconomyService(GameData data)
        {
            this.data = data;
        }

        public void EarnTapReward()
        {
            data.money += data.TapReward;
        }

        public void AddAutoIncomeTick()
        {
            data.money += data.AutoIncomePerSecond;
        }

        public bool TryBuyAutoIncomeUpgrade()
        {
            int cost = data.UpgradeCost;
            if (data.money < cost)
            {
                return false;
            }

            data.money -= cost;
            data.autoIncomeLevel++;
            return true;
        }
    }
}
