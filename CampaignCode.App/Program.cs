
using CampaignCode.App;
using CampaignCode.App.Models;
using System.Text;

List<string> patterns = new List<string>();
List<string> codes = new List<string>();
string characters = "";
int codeLength = 0;
int codeNumber = 0;
List<CharModel> defaultCharacters = new List<CharModel>();
List<CharModel> myCharacters = new List<CharModel>();

string mChoice = "99";
do
{
    Console.WriteLine("1: Pdf'de ki örnek ile kampanya kodu üretmek istiyorum");
    Console.WriteLine("2: Kendi patternim ile kendi kodlarımı üreteceğim.");
    Console.WriteLine("3: Kod kontrol etmek istiyorum");
    Console.WriteLine("e: Kullanım klavuzu");
    Console.WriteLine("0: Çıkış");
    Console.WriteLine("Seçiminiz: ");
    mChoice = Console.ReadLine();

    if (mChoice == "1")
    {
        patterns.Clear();
        codes.Clear();
        defaultCharacters.Clear();
        myCharacters.Clear();

        characters = "ACDEFGHKLMNPRTXYZ234579";
        codeLength = 8;
        codeNumber = 1000;
        patterns.Add("21322113");   //SesliHarf - SessizHarf - Rakam - SesliHarf - SesliHarf - SessizHarf - SessizHarf - Rakam
                                    //Bu yapıya uygun kod üretilecektir.
        defaultCharacters = CodeService.SetDefaultCharacters();
        myCharacters = CodeService.SetMyCharacters(characters, defaultCharacters);
        codes = CodeService.GenerateKeys(patterns, myCharacters, codeLength, codeNumber);

        //Üretilen kodları listelemek için aşağıdaki satırı aktifleştirin.
        HelperFunctions.PrintProducedCodes(codes);

        //Patternleri listelemek için aşağıdaki satırı aktifleştirin.
        HelperFunctions.PrintPatterns(patterns);

        //Patternler ile kaç adet kod üretilme ihtimali olduğunu görmek için aşağıdaki satırı aktifleştirin.
        Console.WriteLine($"Mevcut patternleriniz ile toplamda {HelperFunctions.PossibilityCalculator(patterns, myCharacters)} adet kod üretilme ihtimali mevcuttur.");

        Console.WriteLine($"{codeNumber} adet unique kod üretildi.");
    }
    else if (mChoice == "2")
    {
        patterns.Clear();
        codes.Clear();
        defaultCharacters.Clear();
        myCharacters.Clear();

        Console.WriteLine("Kod üretilirken kullanılacak karakterleri giriniz: (örnek: ACDEFGHKLMNPRTXYZ234579)");
        characters = Console.ReadLine();

        Console.WriteLine("Üretilecek kodların uzunluğunu giriniz. (örnek: 8)");
        codeLength = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Kaç adet kod üretmek istiyorsunuz? (Örnek: 1000)");
        codeNumber = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Kaç farklı pattern ile kod ürettirmek istiyorsunuz? (Örnek: 1)");
        int patternCounter = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < patternCounter; i++)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < codeLength; j++)
            {
                Console.WriteLine($"Lütfen {i + 1}. patternin {j + 1}. karakterinin tipini girin: (Örnek : 1-4 arası)");
                Console.WriteLine("1: Sessiz harf");
                Console.WriteLine("2: Sesli harf");
                Console.WriteLine("3: Rakam");
                Console.WriteLine("4: Özel karakter");
                string c = Console.ReadLine();
                if (c.Length != 1 || (Convert.ToByte(c) >= 1 && Convert.ToByte(c) <= 4))
                {
                    sb.Append(c);
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim.");
                    j--;
                }
            }
            patterns.Add(sb.ToString());
        }
        defaultCharacters = CodeService.SetDefaultCharacters();
        myCharacters = CodeService.SetMyCharacters(characters, defaultCharacters);
        
        codes = CodeService.GenerateKeys(patterns, myCharacters, codeLength, codeNumber);

        //Üretilen kodları listelemek için aşağıdaki satırı aktifleştirin.
        HelperFunctions.PrintProducedCodes(codes);

        //Patternleri listelemek için aşağıdaki satırı aktifleştirin.
        HelperFunctions.PrintPatterns(patterns);

        //Patternler ile kaç adet kod üretilme ihtimali olduğunu görmek için aşağıdaki satırı aktifleştirin.
        Console.WriteLine($"Mevcut patternleriniz ile toplamda {HelperFunctions.PossibilityCalculator(patterns, myCharacters)} adet kod üretilme ihtimali mevcuttur.");

        Console.WriteLine($"{codeNumber} adet unique kod üretildi.");
    }
    else if (mChoice == "3")
    {
        if (patterns.Count > 0 && defaultCharacters.Count > 0)
        {
            Console.WriteLine($"Lütfen {codeLength} haneli kodunuzu girin.");
            string code = Console.ReadLine();
            if (CodeService.CheckKey(patterns, code, defaultCharacters))
            {
                Console.WriteLine("Kod validasyondan geçti. Lütfen db'den kontrol ediniz.");
                //Validation is success. Check from db.
            }
            else
            {
                Console.WriteLine("Kod validasyondan geçeMEdi. Db'den kontrol etmenize gerek yok.");
            }
        }
        else
        {
            Console.WriteLine("Önce pattern üretimi işlemi yapılmalıdır. Lütfen menüden 1 ya da 2 seçimi ile pattern üretiniz.");
        }
        
    }
    else if (mChoice == "e")
    {
        //explanation
        string help = @"
    Projede tek bir algoritma ile kod üretme mantığının üzerine çıkılmış ve istenilen şekilde kod üretebilme yeteneği kazandırılmıştır. 
    Kodun kaç karakterli olacağı seçilebilir. 
    Kodun karakter setinin yapısı değiştirilebilir ve bu yapıdan çok sayıda üretilebilir.
    Kod içerisinde kullanılan karakterlerin tipleri belirlenebilir (Sesli harf, sessiz harf, rakam ve özel karakter). Bu sayede farklı yapılarda kodlar üretilebilir.
    
    Menü:
    1-) PDF'deki örnek üzerinden gidilmiştir. 1 adet pattern ile 1000 adet kod üretilecektir.
    Kod 8 karakterden oluşmaktadır. Pattern şu şekildedir:
        SesliHarf - SessizHarf - Rakam - SesliHarf - SesliHarf - SessizHarf - SessizHarf - Rakam

    2-) Yukarıda belirtilen bilgiler ile kod üretimi yapabilirsiniz.
        Önce karakter setini girmeniz beklenmektedir.
        Ardından üretilecek kodların karakter uzunluğunu girmeniz beklenmektedir.
        Kaç adet kod üretmek istediğiniz beklenmektedir.
        Kaç farklı algoritma ile kod üretmek istediğiniz bilgisi beklenmektedir. 
            Ardından seçtiğiniz verilere göre döngüler yardımıyla patternlerinizi belirlemeniz beklenmektedir. Bu kısımda 1 ile 4 arası veriler girebilirsiniz.
                1-Sessiz harf
                2-Sesli harf
                3-Rakam
                4-Özel karakter.
            !Lütfen girdiğiniz karakter setine uygun seçim yapınız.

    3-) Validasyon kontrolü bu kısımda yapılmaktadır. Bu kısmı çalıştırabilmek için öncelikle 1 ya da 2 seçimleri ile patternleri oluşturmalısınız. Bu kısımda girilecek kodun patternler ile uyumlu olup olmadığı kontrol edilmektedir.

    0-) Programdan çıkış.

    Notlar:
    * Kod üretirken random yapısı kullanılmıştır. Üretilecek kodun listede olup olmadığı kontrol edilmiş ve buna göre yeniden random üretilmesi sağlanmıştır.
    * Örnekte 1000 adet kod üretimi istendiği için farklı yapılar tercih edilmemiştir. Mevcut durumda teorik olarak sonsuz döngü ihtimali mevcuttur.
    * Bu durumun önüne geçebilmek için karakterler ile tree oluşturulabilir ve backtracking methodu kullanan recursive bir fonksiyon hazırlanabilir fakat bu fonksiyon bellek kullanımını arttıracak ve performans kaybına sebep olacaktır. 1000 adet gibi bir sayıda bu yaklaşıma gidilmemiş ve random üretilen kodun listede bulunup bulunmadığı kontrol edilmiştir. Eğer üretilen kod mevcutsa tekrar random kod üretilmesi sağlanmıştır.
";
        Console.WriteLine("Yardım dökümantasyonu");
        Console.WriteLine(help);
    }
    else if (mChoice == "0")
    {
        return;
    }

} while (true);


