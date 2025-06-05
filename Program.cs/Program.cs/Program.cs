using System;
using System.Collections.Generic;

// Bu sınıf tüm karakterlerin ortak özelliklerini içeriyor
class Karakter
{
    public string Isim { get; set; }
    public int Can { get; set; }
    public int Guç { get; set; }
    public int Mana { get; set; }

    public static int ToplamSaldiriSayisi = 0;

    // Yapıcı metot - karakter oluşturulurken çağrılır
    public Karakter(string isim, int can, int guc, int mana)
    {
        Isim = isim;
        Can = can;
        Guç = guc;
        Mana = mana;
    }

    // Sanal saldırı metodu - kalıtım alan sınıflar burayıı ezebilir
    public virtual void Saldir(Karakter hedef)
    {
        Console.WriteLine($"{Isim}, {hedef.Isim}'e normal saldırı yaptı! ({Guç} hasar)");
        hedef.Can -= Guç;
        ToplamSaldiriSayisi++;
    }
}

// Oyuncu sınıfı, Karakter sınıfından kalıtım alır
class Oyuncu : Karakter
{
    public Oyuncu(string isim) : base(isim, 100, 20, 50) { }

    public override void Saldir(Karakter hedef)
    {
        base.Saldir(hedef);
    }

    // Özel saldırı  - mana kontrolü yapar
    public void OzelSaldiri(Karakter hedef)
    {
        if (Mana >= 20)
        {
            int ozelHasar = Guç + 20;
            Console.WriteLine($"{Isim}, özel saldırı yaptı! ({ozelHasar} hasar)");
            hedef.Can -= ozelHasar;
            Mana -= 20;
            ToplamSaldiriSayisi++;
        }
        else
        {
            Console.WriteLine("Yetersiz mana! Özel saldırı başarısız.");
        }
    }

    // Mana yenileme metodu
    public void ManaYenile()
    {
        Mana += 15;
        Console.WriteLine($"{Isim}, meditasyon yaparak manasını yeniledi. (+15 Mana)");
    }
}

// Düşman sınıfı, karakter sınıfından kalıtım alır
class Dusman : Karakter
{
    public Dusman(string isim, int can, int guc) : base(isim, can, guc, 0) { }

    public override void Saldir(Karakter hedef)
    {
        Console.WriteLine($"{Isim}, {hedef.Isim}'e saldırıyor! ({Guç} hasar)");
        hedef.Can -= Guç;
        ToplamSaldiriSayisi++;
    }
}

class Program
{
    static List<string> SaldiriGecmisi = new List<string>();
    static Random rnd = new Random();

    // Yeni düşman oluşturan metot
    static Dusman DusmanSec()
    {
        int secim = rnd.Next(0, 3);
        if (secim == 0)
        {
            Dusman d = new Dusman("Zombi", 40, 10);
            return d;
        }
        if (secim == 1)
        {
            Dusman d = new Dusman("Goblin", 50, 15);
            return d;
        }
        Dusman e = new Dusman("Ejderha", 80, 25);
        return e;
    }

    // Ana oyun döngüsü
    static void Main()
    {
        // Oyuncu ismi alınıyor
        Console.Write("Karakter ismini girin: ");
        string isim = Console.ReadLine();
        Oyuncu oyuncu = new Oyuncu(isim);
        Dusman dusman = DusmanSec();

        // Oyun başladığında skor sıfırlanır
        int skor = 0;

        // Oyun döngüsü - oyuncunun canı olduğu sürece devameder
        while (oyuncu.Can > 0)
        {
            Console.WriteLine($"\n🧑‍🎮 {oyuncu.Isim} - Can: {oyuncu.Can} | Mana: {oyuncu.Mana}");
            Console.WriteLine($"👾 {dusman.Isim} - Can: {dusman.Can}");

            Console.WriteLine("\n1 - Normal Saldırı\n2 - Özel Saldırı\n3 - Mana Yenile");
            Console.Write("Seçim: ");
            string secim = Console.ReadLine();

            // Oyuncunun seçimine göre işlem yapılıyor
            if (secim == "1")
            {
                oyuncu.Saldir(dusman);
                SaldiriGecmisi.Add(oyuncu.Isim + " -> " + dusman.Isim + " [Normal]");
            }
            else if (secim == "2")
            {
                oyuncu.OzelSaldiri(dusman);
                SaldiriGecmisi.Add(oyuncu.Isim + " -> " + dusman.Isim + " [Özel]");
            }
            else if (secim == "3")
            {
                oyuncu.ManaYenile();
            }

            // Düşman öldüyse yeni düşman geliyor
            if (dusman.Can <= 0)
            {
                Console.WriteLine("✅ " + dusman.Isim + " yenildi!");
                skor += 10; // Skor +10
                dusman = DusmanSec();
            }
            else
            {
                dusman.Saldir(oyuncu);
                SaldiriGecmisi.Add(dusman.Isim + " -> " + oyuncu.Isim + " [Normal]");
            }
        }

        // Oyun bitti mesajı ve skor bilgisi
        Console.WriteLine("\n☠️ Oyuncu öldü! Oyun bitti.");
        Console.WriteLine("🎯 Skorunuz: " + skor);
        Console.WriteLine("🔢 Toplam saldırı sayısı: " + Karakter.ToplamSaldiriSayisi);
        Console.WriteLine("📜 Saldırı Geçmişi:");

        // Saldırı geçmişini yazdırıyor
        int i = 0;
        while (i < SaldiriGecmisi.Count)
        {
            Console.WriteLine(" - " + SaldiriGecmisi[i]);
            i++;
        }
    }
}
