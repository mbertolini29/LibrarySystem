using AutoMapper;
using LibrarySystem.Models;
using LibrarySystem.Web.Models.BooksViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.ViewModels.Utility
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.CategoryName, 
                            opt => opt.MapFrom(src => src.Category.Title));
        }
    }
}
