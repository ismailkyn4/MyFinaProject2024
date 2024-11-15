﻿using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages //sürekli newlememek için ve sabit bir mesaj olduğu için static yaptık.
                                 //Temel mesajlarımızı buraya koyacağız magic string denilen karmaşadan kurtulacağız.
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductListed = "Ürünler Listelendi";
        public static string GetAllByCategoryId = "Category Idler getirildi.";
        public static string CategoryListed = "Kategoriler listelendi.";
        public static string ProductUpdated = "Ürünler Güncellendi.";

        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir.";

        public static string ProductNameAlreadExists = "Bu isimde zaten başka bir ürün var";

        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor.";

        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kayıt oldu.";

        public static string UserNotFound = "Kullanıcı Bulunamadı.";
        public static string PasswordError = "Parola hatası.";

        public static string SuccessfulLogin = "Başarılı giriş.";

        public static string UserAlreadyExists = "Kullanıcı mevcut";

        public static string AccessTokenCreated = "Token oluşturuldu.";

    }
}
