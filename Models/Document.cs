using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Document
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public string Author { get; set; }

        [Display(Name = "date of publication")]
        //[DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        // https://docs.microsoft.com/de-de/aspnet/core/tutorials/razor-pages/new-field?view=aspnetcore-5.0&tabs=visual-studio
        public string DataType { get; set; }
    }
}
