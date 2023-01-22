# CampaignCode.App

Projede tek bir algoritma ile kod üretme mantığının üzerine çıkılmış ve istenilen şekilde kod üretebilme yeteneği kazandırılmıştır.  
Kodun kaç karakterli olacağı seçilebilir.  
Kodun karakter setinin yapısı değiştirilebilir ve bu yapıdan çok sayıda üretilebilir.  
Kod içerisinde kullanılan karakterlerin tipleri belirlenebilir (Sesli harf, sessiz harf, rakam ve özel karakter). Bu sayede farklı yapılarda kodlar üretilebilir.  

Statik olarak karakterler tiplerine göre sisteme tanıtılmıştır. Bu tipler sessiz harfler, sesli harfler, rakamlar, özel karakterlerdir.  
  
```csharp
public static class AllChars
{
  public static string consonants = "BCÇDFGĞHJKLMNPRSŞTVXYZ";
  public static string vovels = "AEIİOÖUÜ";
  public static string numbers = "1234567890";
  public static string specialCharacters = @".:,;/*-+!'^%&/()=?_-\}][{½$#£><@|";
  public static string allCharacters = consonants + vovels + numbers + specialCharacters;
}
```  

Karakterlerin tiplerini belirtmek için bir enum tanımlanmıştır:  

```csharp
public enum CharProperties
{
  Consonant = 1,
  Vovel = 2,
  Number = 3,
  SpecialCharacter = 4,
  All = 5
}
``` 

Karakter modeli:  

```csharp
public class CharModel
{
  public char Char { get; set; }
  public byte Property { get; set; }

  public CharModel(char @char, byte property)
  {
    Char = @char;
    Property = property;
  }
}
```    

## Nasıl Kullanılır  
Programı çalıştırdığınızda bir menü sizi karşılar. Yardım almak için "e" yazıp enter'a basabilirsiniz.   
```csharp
Console.WriteLine("1: Pdf'de ki örnek ile kampanya kodu üretmek istiyorum");
Console.WriteLine("2: Kendi patternim ile kendi kodlarımı üreteceğim.");
Console.WriteLine("3: Kod kontrol etmek istiyorum");
Console.WriteLine("e: Kullanım klavuzu");
Console.WriteLine("0: Çıkış");
```      
    
### Menü:   
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
