using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HtmlAgilityPack;
using OSRS.Core.Models.Contratos;

namespace OSRS.Application.Models.OpenAI.MapProfile
{
    public class NodeCollectionAfterMaps: IMappingAction<HtmlNodeCollection, ArticleModelObject>
    {
        public void Process(HtmlNodeCollection source, ArticleModelObject destination, ResolutionContext context)
        {
            try
            {
                destination.H1Title = source.FirstOrDefault(x => x.Name == "h1").InnerText;
                for (int i = 1; i < source.Count; i++)
                {
                    if (source[i].Name == "h2")
                    {
                        List<string> H3Titles = new List<string>();
                        var h3i = i + 1;
                        if (h3i < source.Count)
                        {
                            while  (source[h3i].Name == "h3")
                            {
                                   
                                H3Titles.Add(source[h3i].InnerText);
                                if (h3i + 1 < source.Count)
                                {
                                    h3i++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                           
                        }
                        destination.ArticleH2s.Add(new ArticleH2Object(source[i].InnerText, H3Titles));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}