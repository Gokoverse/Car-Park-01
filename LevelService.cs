namespace AndroidParkingIdle.Core
{
    // Bu dosya, park etme sonucu verilen XP'yi ve seviye atlama mantigini yonetir.
    public class LevelService
    {
        private readonly GameData data;

        public LevelService(GameData data)
        {
            this.data = data;
        }

        public bool CompleteParkingJob()
        {
            data.totalParkedCars++;
            data.experience += 35 + data.level * 5;

            bool leveledUp = false;
            while (data.experience >= data.ExperienceToNextLevel)
            {
                data.experience -= data.ExperienceToNextLevel;
                data.level++;
                data.money += 25 * data.level;
                leveledUp = true;
            }

            return leveledUp;
        }
    }
}
