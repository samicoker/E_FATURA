using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constantcs
{
    public static class Messages
    {
        public static string NotFoundClass = "Görevi gerçekleştirecek sınıf bulunamadı.";

        public static string NotFoundMethod = "Görevi gerçekleştirecek metot bulunamadı.";

        public static string NotFoundRep = "Görevi gerçekleştirecek repository bulunamadı.";

        public static string NotZero(string v)
        {
            return string.Format("{0} 0 olamaz.", v);
        }

        public static string NotEmpty(string v)
        {
            return string.Format("{0} boş olamaz.", v);
        }

        public static string NotNull(string v)
        {
            return string.Format("{0} null olamaz.", v);
        }

        public static string TableColumnNotFound(string Table, string Column)
        {
            return string.Format("{0} tablosunda {1} bulunamadı.", Table, Column);
        }

        public static string NotFoundAbstarct = "Class da Abstarct kalıtımı bulunamadı.";

        public static string NotFound = "Herhangi bir olay örgüsüne girmedi";

        public static string NotFound2(string text)
        {
            return string.Format("{0} bulunamadı.", text);
        }

        public static string CantAddNewTask = "Yeni bir görev eklenemedi";

        public static string CantGetInformationFromSOAP = "Veri alınamadı.Logları kontrol ediniz.";

        public static string AnErrorOccurred = "Entegrasyon ile ilgili hata oluştu.";

        public static string CantTransformObjects = "Nesne dönüştürülemedi.";

        public static string NotFoundKurumSube = "Kurum şube bulunamadı.";

        public static string InvoiceAnErrorOccurred = "Fatura gönderilirken hata oluştu.";

        public static string Successful = "Görev başarılı bir şekilde gerçekleşti.";


        public static string NotFoundDataByTableRowID() //string tableRowID
        {
            //return string.Format("Table Row Id : {0} olan data bulunamadı.", tableRowID);
            return string.Format("Data bulunamadı.");
        }

        public static string CantMakeTask(string tableRowID, string v)
        {
            return string.Format("Table Row Id : {0} olan {1} görevi yapılamadı.Hata tablosuna bakın.", tableRowID, v);
        }

        public static string FileNo(string dosyaNo)
        {
            return string.Format("Dosya - Kart No : {0} .", dosyaNo);
        }

        public static string ErrorAndErrorCode(string hatakodu1, string hatakodu2)
        {
            return string.Format("Hata Kodu - Açıklama = {0} - {1}.", hatakodu1, hatakodu2);
        }
    }
}
