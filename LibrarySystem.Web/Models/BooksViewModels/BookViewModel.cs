using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web.Models.BooksViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; } //se refiere al codigo de barra o numeros unicos de serie. //palabra clave tmb
        public string CategoryName { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; } //copias disponibles

    }
}
