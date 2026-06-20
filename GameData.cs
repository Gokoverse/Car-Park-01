using System;

namespace AndroidParkingIdle.Core
{
    // Bu dosya, oyuncunun para, upgrade, seviye ve XP bilgisini tutan sade veri modelidir.
    [Serializable]
    public class GameData
    {
        public int money;
        public int level = 1;
        public int experience;
        public int autoIncomeLevel;
        public int totalParkedCars;

        public int TapReward => 10 + level * 2;
        public int AutoIncomePerSecond => autoIncomeLevel * 5;
        public int UpgradeCost => 50 + autoIncomeLevel * 35;
        public int ExperienceToNextLevel => 100 + (level - 1) * 60;
    }
}
