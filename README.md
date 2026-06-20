# Android Parking Idle

Unity ile Android odakli, sade ve moduler bir araba park idle/progression prototipi.

## Icerik

- Ana ekran
- Para kazanma butonu
- Otomatik gelir sistemi
- Otomatik gelir upgrade sistemi
- Park etme ile XP ve seviye ilerleme
- PlayerPrefs tabanli basit kayit sistemi
- Android portre ekran oranlarina uyumlu CanvasScaler

## Unity Editor'da Test

1. Unity Hub ile `AndroidParkingIdle` klasorunu ac.
2. `Assets/Scenes/MainMenu.unity` sahnesini ac.
3. Play tusuna bas.
4. Ana ekranda:
   - `Para Kazan` ile para artisini test et.
   - `Oyuna Basla` ile oyun sahnesine gec.
   - `Kaydi Sifirla` ile PlayerPrefs kaydini temizle.
5. Oyun ekraninda:
   - `Para Kazan` anlik para verir.
   - `Otomatik Gelir Upgrade` otomatik geliri artirir.
   - `Araba Park Et` XP, seviye ve otopark doluluk gorselini ilerletir.
   - `Ana Ekran` geri doner.

Alternatif olarak dogrudan `Assets/Scenes/ParkingGame.unity` sahnesini acip oyun ekranini test edebilirsin.

## Android Build Alma

1. Unity'de `File > Build Settings` menusu ac.
2. Platform olarak `Android` sec ve `Switch Platform` tusuna bas.
3. `Parking Game > Android Ayarlarini Hazirla` menusunu calistir.
   - Bu arac `MainMenu` ve `ParkingGame` sahnelerini build listesine ekler.
   - Uygulamayi portre moda ayarlar.
   - Paket adini `com.codex.androidparkingidle` olarak hazirlar.
4. `Build Settings` ekraninda `Build` veya `Build And Run` sec.
5. APK/AAB cikisini istedigin klasore kaydet.

## Dosya Yapisi

- `Assets/Scenes/MainMenu.unity`: Ana ekran sahnesi.
- `Assets/Scenes/ParkingGame.unity`: Oyun sahnesi.
- `Assets/Scripts/Core/GameData.cs`: Oyuncu para, seviye, XP ve upgrade verisi.
- `Assets/Scripts/Core/SaveSystem.cs`: Basit kayit/yukleme/sifirlama sistemi.
- `Assets/Scripts/Core/EconomyService.cs`: Para kazanma ve otomatik gelir upgrade mantigi.
- `Assets/Scripts/Core/LevelService.cs`: XP ve seviye atlama mantigi.
- `Assets/Scripts/UI/MainMenuController.cs`: Ana ekran UI ve buton aksiyonlari.
- `Assets/Scripts/UI/GameUIController.cs`: Oyun UI, otomatik gelir dongusu ve gameplay butonlari.
- `Assets/Scripts/UI/ResponsiveCanvasScaler.cs`: Mobil ekranlara uyumlu Canvas ayarlari.
- `Assets/Scripts/Gameplay/ParkingLotView.cs`: Basit otopark doluluk gorseli.
- `Assets/Editor/AndroidBuildHelper.cs`: Android build sahne ve mobil ayarlarini hazirlayan Editor araci.

## Notlar

- Prototip, ekstra asset veya paket gerektirmeden Unity UI ile calisir.
- UI koddan olusturuldugu icin sahneler temiz ve hafiftir.
- Kayit sistemi ilk surum icin PlayerPrefs kullanir; daha buyuk oyunda dosya tabanli veya bulut kayit sistemine gecilebilir.
