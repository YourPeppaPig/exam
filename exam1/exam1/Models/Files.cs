using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exam1.Models
{
    public class Files
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string FIleName { get; set; }
        public string Extension { get; set; }
        public string PathToFile { get; set; }
        public string Link { get; set; }
        [MaxLength(30, ErrorMessage = "Максимум 30 символов")]
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public DateTime UploadedDate { get; set; }
        public int DownloadedCount { get; set; }
        public string ContentType { get; set; }
        public string UserDownloadString { get; set; }
    }
}
