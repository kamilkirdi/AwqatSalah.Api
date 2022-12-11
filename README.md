# ![](https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Diyanet_%C4%B0%C5%9Fleri_Ba%C5%9Fkanl%C4%B1%C4%9F%C4%B1_yeni_logo.svg/64px-Diyanet_%C4%B0%C5%9Fleri_Ba%C5%9Fkanl%C4%B1%C4%9F%C4%B1_yeni_logo.svg.png) Diyanet Namaz Vakitleri 

Merhaba. Ben Kamil KIRDI. Yazılım dünyasına 2005 yılında klasik asp ile başladım. 2015 yılına kadar kendi çapımda bir hobi olarak sürdürdüğüm çalışmalarıma bu yıldan sonra .Net ve C# teknolojileri, veri tabanı tarafında ise MsSql server ve SQL Server Reporting Services ve Analysis Services teknolojileri ile  projelerin geliştirildiği (şuanda PostgreSql’e de geçilmiştir), proje ve kullanıcı sayısının hatrı sayılır ölçüde fazla olduğu Diyanet İşleri Başkanlığı yazılım ekibinde devam ettim. 2022 yılından itibaren ise Din İşleri Yüksek Kurulunda .Net Core, C#, Angular, PostgreSql ve Sql Server teknolojileri ile uygulamalar geliştirmekte ve mevcut uygulamalara destek vermekteyim.

[Diyanet İşleri Başkanlığı](https://www.diyanet.gov.tr/) namaz vakitleri; [Din İşleri Yüksek Kurulu](https://kurul.diyanet.gov.tr/) Üye ve Uzmanları tarafından, İslam dininin kuralları çerçevesinde sınırı çizilmiş olan kurallar çerçevesinde, Vakit Hesaplama Uzmanı ve Astronomlar tarafından gerekli teknik aletlerin yanı sıra gözlemler ile bilimsel ve dini olarak hesaplanmaktadır. Hesaplanan namaz vakitlerinin, geliştiricilerin erişimine https://awqatsalah.diyanet.gov.tr adresinden bir api aracılığıyla açmamızla birlikte ilgili Api servisinin etkin ve nasıl kullanılacağına yönelik bu örnek çalışmayı yapmış bulunmaktayım. 

Uygulama API servisi olarak tasarlanmıştır. Yazılım dili olarak C# ile yazılmıştır. Ortam olarak ise açık kaynak yapıya sahip .Net Core  teknolojisi kullanılmıştır. Ayrıca uygulama geliştirme sürecinde SOLID kurallarına riayet edilmiş, tasarım deseni olarak Repository Pattern kullanılarak bütün bağımlılıklar soyut bir yapıya kavuşturulmuştur. Uygulama varsayılan bir biçimde Exception Handler mekanizmasına sahiptir. Yine basit bir kullanıcı doğrulaması yazılmış ancak aktif edilmemiştir. Program.cs içerisinden bunu aktif edebilirsiniz. 
```sh
builder.Services
    .AddControllers()
    //.AddControllers(opt => opt.Filters.Add<ClientAtionFilter>())
```
> Not: `.AddControllers() satırını kapatarak .AddControllers(opt => opt.Filters.Add<ClientAtionFilter>()) satırını açınız.`

Yapmanız gereken tek şey bu projeyi kendi Github reponuza çekmek. Ardından appsettings.json dosyasındaki size verilen kullanıcı adı ve şifreyi aşağıdaki örnekteki yere yazmak.

```sh
"AwqatSalahSettings": {
    "ApiUri": "https://awqatsalah.diyanet.gov.tr/",
    "TokenLifetimeMinutes": "45",
    "RefreshTokenLifetimeMinutes": "15",
    "UserName": "",
    "Password": "
  },
```
> Not: `Unutmayın Awqat Salah Apisinin her bir endpointi için 5 istek hakkınız bulunuyor.`
---------------------------------------------

## Diyanet Awqat Salah

Hello. I'm Kamil KIRDI. I started the software world in 2005 with classic asp. I continued my work as a hobby until 2015, and after this year, projects were developed with .Net and C# technologies, and on the database side with MsSql server and SQL Server Reporting Services and Analysis Services technologies (currently PostgreSql has also been used), the number of projects and users. I continued in the software team of the Presidency of Religious Affairs, which is considerably more. Since 2022, I have been developing applications and supporting existing applications with .Net Core, C#, Angular, PostgreSql and Sql Server technologies at the High Council of Religious Affairs.

Prayer times of the [Presidency of Religious Affairs](https://www.diyanet.gov.tr/) are calculated scientifically and religiously by the Members and Experts of the [High Council of Religious Affairs](https://kurul.diyanet.gov.tr/), within the framework of the rules of the Islamic religion, by the Time Calculator Specialist and Astronomers, as well as the necessary technical tools, as well as observations. 
I have made this case study on how to use the relevant Api service effectively, after we opened the calculated prayer times to developers' access via an API at https://awqatsalah.diyanet.gov.tr.

The application is designed as an API service. It is written in C# as the programming language. As the environment, .Net Core technology with open source structure was used. In addition, SOLID rules were complied with during the application development process, and all dependencies were made abstract by using Repository Pattern as the design pattern. The application has the Exception Handler mechanism by default. Again, a simple user authentication was written but not activated. You can enable it in Program.cs.
```sh
builder.Services
    .AddControllers()
    //.AddControllers(opt => opt.Filters.Add<ClientAtionFilter>())
```
> Note: `.AddControllers() closing the line .AddControllers(opt => opt.Filters.Add<ClientAtionFilter>()) open the line.`

All you have to do is pull this project to your own Github repo. Then write the username and password given to you in the appsettings.json file to the place in the example below

```sh
"AwqatSalahSettings": {
    "ApiUri": "https://awqatsalah.diyanet.gov.tr/",
    "TokenLifetimeMinutes": "45",
    "RefreshTokenLifetimeMinutes": "15",
    "UserName": "",
    "Password": "
  },
```
> Note: ` Remember, you have 5 requests for each endpoint of the Awqat Salah API.`

## License
MIT
