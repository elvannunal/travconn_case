# 🚀 Microservices Solution

Bu proje, modern bir **.NET 8** ekosisteminde tasarlanmış, **API Gateway** ve üç temel mikroservisten (**Identity**, **Hobbies**, **Logs**) oluşan, işlevsellik ve ölçeklenebilirlik odaklı bir çözümdür.

---

## 🌟 Proje Mimarisi

Çözüm, dört ana bileşenden oluşmaktadır:

| Servis Adı | Teknoloji | Temel Sorumluluk |
|-------------|------------|------------------|
| **Gateway** | Ocelot / .NET 8 | API trafiğini yönetmek ve Rate Limiting uygulamak |
| **Identity** | EF Core / .NET 8 | Kullanıcı kimlik doğrulama (JWT) ve kullanıcı yönetimi |
| **Hobbies** | EF Core / .NET 8 | Hobi ve kullanıcı-hobi ilişkisi CRUD operasyonları |
| **Logs** | EF Core / .NET 8 | Servis işlem ve token istek loglarını toplamak ve sorgulamak |

---

## ⚙️ Servis Gereksinimleri ve İşlevsellik

### 🔹 Gateway Servisi
- API yönlendirmesi için tüm gelen istekler tek giriş noktası üzerinden yönetildi.  
- **Rate Limiting** yapılandırması eklendi: Tüm API trafiğine 5 saniyede 2 istek kuralı uygulandı.

---

### 🔹 Identity Servisi

#### Veritabanı Yapısı:
- EF Core ile **IdentityDb** adlı bir veritabanı oluşturuldu.  
- Kullanıcı şifreleri hash’lenerek güvenli bir şekilde saklandı.

#### Kullanıcı Yönetimi:
- `Accounts` tablosu oluşturuldu.  
- Migration işlemi sırasında tabloya 3 adet başlangıç kullanıcısı eklendi.

#### Kimlik Doğrulama:
- **JWT (JSON Web Token)** mekanizması kuruldu ve Authorization sağlandı.  
- Diğer API’ler token’ı doğrulayarak yetkilendirme işlemlerini gerçekleştirebiliyor.  
- Kullanıcılar için `Get Users` endpoint’i eklendi.

---

### 🔹 Hobbies Servisi

#### Veritabanı Yapısı:
- EF Core tabanlı bir veritabanı kuruldu.  
- Aşağıdaki tablolar oluşturuldu:
  - `Hobbies`  
  - `UserHobbies` (Kullanıcı–Hobi ilişkisi)

#### CRUD Operasyonları:
- Her iki tablo için tam CRUD (Create, Read, Update, Delete) işlemleri geliştirildi.

#### Loglama:
- Create ve Update işlemleri tamamlandıktan sonra bu işlemler **Logs API**’ye gönderilerek kaydediliyor.

---

### 🔹 Logs Servisi

#### Sorgulama Endpoint’i:
- Diğer servislerden gelen log kayıtlarını sorgulamak için bir endpoint geliştirildi.

Kabul edilen parametreler:
- Tarih aralığı  
- Log tipi (`Token` ve `Hobbies` işlemlerini kapsayacak şekilde)

---

## 🧩 Token

- Hobbies parametreleri ve kullanıcı doğrulama işlemleri **JWT Token** yapısıyla gerçekleştirildi.  
- Tüm API’lerde Authorization mekanizması entegre edildi.

---

## 🧰 Kurulum ve Çalıştırma

Proje, yerel ortamda mikroservislerin doğru sırayla başlatılmasıyla birlikte sorunsuz bir şekilde çalışmaktadır.

### 🔧 Veritabanı Kurulumu:
- Her servisin (Identity, Hobbies, Logs) kendi veritabanı bağlantı ayarları yapılandırıldı.

### 🧱 Migration Uygulaması:
- Identity ve Hobbies projelerinde EF Core migration’ları çalıştırılarak tablolar oluşturuldu.  
- Identity tablosuna başlangıç kullanıcıları eklendi.

### ▶️ Servisleri Başlatma Sırası (Bağımlılıklara göre):
1. **Logs API**  
2. **Identity API**  
3. **Hobbies API**  
4. **Gateway API** (Tüm servisler hazır olduğunda)

---

## 💬 İletişim

Sorularınız veya geri bildirimleriniz için benimle iletişime geçebilirsiniz.
