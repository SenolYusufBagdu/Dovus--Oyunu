
# 🎮 Mini RPG Dövüş Oyunu (C#)

## 📌 Proje Amacı
Bu projede, C# dilinde nesne tabanlı programlama (OOP) tekniklerini kullanarak konsol tabanlı bir mini RPG dövüş oyunu geliştirildi. Oyuncu, çeşitli düşmanlarla (Zombi, Goblin, Ejderha) karşılaşarak savaşıyor.

## 🧱 Kullanılan OOP Yapıları
- Sınıf(class), kalıtım, override, property, static üye ve Yapıcı Metot, liste

## 🎮 Oyun Özellikleri
- Normal saldırı
- özel saldırı
 - mana yenileme
- Düşman ölünce yenisi gelir, oyuncu ölünce oyun biter
 -3 farklı düşman var
-Her turda seçim menüsü
-Ekrana her turda Can ve Mana bilgisi yazdırılır

## 📁 Dosya Yapısı
- Program.cs
- README.md
## 📁Ekran  çıktısı 
🧑‍🎮 Ahmet - Can: 100 | Mana: 50
👾 Zombi - Can: 40

- 1 - Normal Saldırı
 Seçim: 1
Ahmet, Zombi'ye normal saldırı yaptı! (20 hasar)
👾 Zombi, Ahmet'e saldırıyor! (10 hasar)

🧑‍🎮 Ahmet - Can: 90 | Mana: 50
👾 Zombi - Can: 20

- 2 - Özel Saldırı
Seçim: 2
Ahmet, özel saldırı yaptı! (40 hasar)
✅ Zombi yenildi!

- 👾 Yeni düşman geldi: Ejderha

👾 Ejderha - Can: 80 | Güç: 25
👾 Ejderha, Ahmet'e saldırıyor! (25 hasar)

🧑‍🎮 Ahmet - Can: 20 | Mana: 25
## 📁 Sınıf diyagramı
- 
class Karakter {
  - Isim: string
  - Can: int
  - Guç: int
  - Mana: int
  - ToplamSaldiriSayisi: int <<static>>
  + Saldir(hedef: Karakter)
}

class Oyuncu {
  + OzelSaldiri(hedef: Karakter)
  + ManaYenile()
  + Saldir(hedef: Karakter)
}

class Dusman {
  + Saldir(hedef: Karakter)
}

Karakter <|-- Oyuncu
Karakter <|-- Dusman

Oyuncu : + Saldir(override)
Dusman : + Saldir(override)
Karakter : + Saldir(virtual)
## 📁 Akış Seması
- 
start
:Oyuncu ismi al;
:Dusman oluştur;
:Skor = 0;

repeat
  if (Oyuncu Can > 0?) then (evet)
    :Can, Mana ve Dusman Can yazdır;
    :Seçim al (1.Normal, 2.Özel, 3.Mana Yenile);

    if (Seçim == 1) then (evet)
      :Oyuncu normal saldırı yap;
    else if (Seçim == 2) then (evet)
      :Oyuncu özel saldırı yap (mana kontrolü);
    else
      :Oyuncu mana yenile;
    endif

    if (Dusman Can <= 0) then (evet)
      :Skor += 10;
      :Yeni düşman oluştur;
    else
      :Düşman saldırı yap;
    endif

  else (hayır)
    stop
  endif
repeat while (Oyuncu Can > 0)
stop



