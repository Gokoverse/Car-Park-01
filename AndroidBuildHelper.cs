using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace AndroidParkingIdle.EditorTools
{
    // Bu dosya, Android test build'i almadan once sahneleri ve temel mobil ayarlari tek tikla hazirlar.
    public static class AndroidBuildHelper
    {
        [MenuItem("Parking Game/Android Ayarlarini Hazirla")]
        public static void ConfigureAndroidBuild()
        {
            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene("Assets/Scenes/MainMenu.unity", true),
                new EditorBuildSettingsScene("Assets/Scenes/ParkingGame.unity", true)
            };

            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.companyName = "Codex";
            PlayerSettings.productName = "Android Parking Idle";
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.codex.androidparkingidle");

            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("Hazir", "Android sahneleri ve temel mobil ayarlar hazirlandi.", "Tamam");
        }
    }
}
