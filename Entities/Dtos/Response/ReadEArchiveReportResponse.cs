using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Response
{
    public class ReadEArchiveReportResponse
    {
        public string EARCHIVEREPORT { get; set; }
        public REQUEST_RETURN RequestReturn { get; set; }
        public eArsivRaporu eArsivRaporu { get; set; }
    }
    public class eArsivRaporu
    {
        public baslik baslik { get; set; }
        public serbestMeslekMakbuzIptal serbestMeslekMakbuzIptal { get; set; }
    }
    public class baslik
    {
        public string versiyon { get; set; }
        public mukellef mukellef { get; set; }
        public hazirlayan hazirlayan { get; set; }
        public string raporNo { get; set; }
        public string donemBaslangicTarihi { get; set; }
        public string donemBitisTarihi { get; set; }
        public string bolumBaslangicTarihi { get; set; }
        public string bolumBitisTarihi { get; set; }
        public string bolumNo { get; set; }
    }
    public class hazirlayan
    {
        public string vkn { get; set; }
    }
    public class mukellef
    {
        public string vkn { get; set; }
    }
    public class serbestMeslekMakbuzIptal
    {
        public string makbuzNo { get; set; }
        public string iptalTarihi { get; set; }
        public string toplamTutar { get; set; }
    }
}
