# 🚀  Microservices Solution

Bu proje, modern bir **.NET 8** ekosisteminde tasarlanmış, **API Gateway** ve üç temel mikroservisten (**Identity**, **Hobbies**, **Logs**) oluşan, işlevsellik ve ölçeklenebilirlik odaklı bir çözümdür.

---

## 🌟 Proje Mimarisi

Çözüm, temel olarak dört ana bileşenden oluşmaktadır:

| Servis Adı | Teknoloji | Temel Sorumluluk |
|-------------|------------|------------------|
| **Gateway** | Ocelot / .NET 8 | API trafiğini yönetme ve Rate Limiting uygulama |
| **Identity** | EF Core / .NET 8 | Kullanıcı kimlik doğrulama (JWT) ve kullanıcı yönetimi |
| **Hobbies** | EF Core / .NET 8 | Hobi ve kullanıcı-hobi ilişkisi CRUD operasyonları |
| **Logs** | EF Core / .NET 8 | Servis işlem ve token istek loglarını toplama ve sorgulama |

---

## ⚙️ Servis Gereksinimleri ve İşlevsellik

### 🔹 Gateway Servisi
- API yönlendirme: Tüm gelen istekler için giriş noktası olarak görev yapar.  
- **Rate Limiting**: Güvenlik ve kaynak yönetimi amacıyla, tüm API trafiğine 5 saniyede 2 istek kuralı uygulanacaktır.

---

### 🔹 Identity Servisi

#### Veritabanı Yapısı:
- EF Core ile bir veritabanı kurulacak ve **IdentityDb** kullanılacaktır.  
- Kullanıcı şifreleri hashlenerek güvenli bir şekilde saklanacaktır.

#### Kullanıcı Yönetimi:
- `Accounts` adında bir tablo bulunacak.
- Migration işlemi sırasında tabloya 3 adet kullanıcı eklenecektir.

#### Kimlik Doğrulama:
- **JWT (JSON Web Token)** mekanizması kurulacak, Authorization sağlanacaktır.  
- Diğer API’ler token’ı doğrulayarak yetkilendirme işlemlerini yapabilecektir.  
- Kullanıcılar için `Get Users` endpoint’i ile listeleme yapılabilecektir.

---

### 🔹 Hobbies Servisi

#### Veritabanı Yapısı:
- EF Core tabanlı bir veritabanı oluşturulacaktır.  
- Tablolar:
  - `Hobbies` tablosu  
  - `UserHobbies` tablosu (Kullanıcı–Hobi ilişkisi)

#### CRUD Operasyonları:
- Her iki tablo için tam CRUD (Create, Read, Update, Delete) endpoint’leri sağlanacaktır.

#### Loglama:
- Create ve Update işlemleri tamamlandıktan sonra bu işlemler **Logs API**’ye yazılacaktır.

---

### 🔹 Logs Servisi

#### Sorgulama Endpoint’i:
Diğer servislerden gelen log kayıtlarının sorgulanabileceği bir endpoint sunulacaktır.

Kabul edilecek parametreler:
- Tarih aralığı  
- Log tipi (`Token` ve `Hobbies` işlemlerini içerecek şekilde)

---

## 🧩 Token

Hobbies parametreleri ve kullanıcı doğrulama işlemleri JWT Token mekanizmasıyla yapılacaktır.

---

## 🧰 Kurulum ve Çalıştırma

Projenin yerel ortamda başarılı bir şekilde çalıştırılabilmesi için mikroservislerin doğru sırayla başlatılması gerekmektedir.

### 🔧 Veritabanı Kurulumu:
Her servisin (Identity, Hobbies, Logs) kendi veritabanı bağlantı ayarlarını yapın.

### 🧱 Migration Uygulama:
Identity ve Hobbies projelerinde EF Core migration’larını çalıştırarak tabloları oluşturun ve Identity tablosuna başlangıç kullanıcılarını ekleyin.

### ▶️ Servisleri Başlatma Sırası (Bağımlılıklara göre):
1. **Logs API**  
2. **Identity API**  
3. **Hobbies API**  
4. **Gateway API** (Tüm servisler hazır olduğunda)

---

## 💬 İletişim

Sorularınız veya geri bildirimleriniz için lütfen iletişime geçiniz.
