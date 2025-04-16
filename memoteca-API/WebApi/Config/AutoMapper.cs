using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace WebApi.Config;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<QuoteDTO, QuoteModel>().ReverseMap();
        CreateMap<UpdateQuoteDTO, QuoteModel>().ReverseMap();

    }
}