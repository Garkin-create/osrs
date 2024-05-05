using AutoMapper;
using HtmlAgilityPack;
using OpenAI_API.Images;
using OSRS.Application.Models.OpenAI.Model.Input;
using OSRS.Application.Models.OpenAI.Model.Output;
using OSRS.Core.Models.Contratos;
using Data = OpenAI_API.Embedding.Data;

namespace OSRS.Application.Models.OpenAI.MapProfile
{
    public class OpenAIMapProfile: Profile
    {
        public OpenAIMapProfile()
        {
            CreateMap<ImageResult, ImageOutputModel>()
                .ForMember(dest => dest.Url, opt=> opt.MapFrom(x => x.Data[0].Url));
            CreateMap<ArticleInputModel, ArticleModelObject>();
            CreateMap<ArticleH2, ArticleH2Object>();
            
            
            
            CreateMap<HtmlNodeCollection, ArticleModelObject>()
                .AfterMap<NodeCollectionAfterMaps>();
            
            
            
        }
    }
}