using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Images;
using OSRS.Core.Models.Contratos;
using OSRS.Domain.Entities.Post;
using OSRS.Domain.Seed;
using OSRS.Domain.Seed.UnitOfWorks;
using OSRS.Domain.Service;
using OSRS.Infrastructure.Repositories;

namespace OSRS.Infrastructure.Services
{
    public class OpenIAService : IOpenIAService
    {
        private readonly IPostRepository _postRepository;
        private readonly IDomainUnitOfWork _domainUnitOfWork;
        private readonly ISystemLogger _logger;
        public OpenAIAPI _api { get; set; }
        public string _api2 { get; set; }

        public OpenIAService(IPostRepository postRepository, IDomainUnitOfWork domainUnitOfWork, ISystemLogger logger)
        {
            _postRepository = postRepository;
            _domainUnitOfWork = domainUnitOfWork;
            _logger = logger;
            _api = new OpenAIAPI(new APIAuthentication("sk-proj-RaJtstQLarYfQme1PgRDT3BlbkFJMJZ6nltd7mOPAuijjgLn")); // create object manually
            _api2 = "sk-proj-RaJtstQLarYfQme1PgRDT3BlbkFJMJZ6nltd7mOPAuijjgLn";
        }

        public async Task<ImageResult> CreateImageAsync(string prompt)
        {
            ImageResult result = new ImageResult();
            try
            {
                result = await _api.ImageGenerations.CreateImageAsync(new ImageGenerationRequest(prompt, OpenAI_API.Models.Model.DALLE3, ImageSize._1024, "hd"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
        }
        
        public async Task<string> CreateSEOArticleAsync(ArticleModelObject prompt)
        {
            string result = null;
            try
            {
                List<ChatMessage> chatMessage = new List<ChatMessage>();
                chatMessage.Add(new ChatMessage(ChatMessageRole.System, "You are a helpful SEO assistant."));
                chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"Vamos a crear un articulo web optimizado para Seo, espera la palabra Continua para comenzar a escribir"));
                chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"escribe el artículo entre 1000 a 1200 palabras de longitud"));
                chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"Únicamente puedes usar las etiquetas HTML: <H2>, <H3>, <P>, <UL>, <LI>, <STRONG>. "));
                chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"Comienzo yo desde el título del artículo: <h1>{prompt.H1Title}</h1> (Incluye el titulo en la respuesta), con la siguiente estructura"));

                foreach (var h2 in prompt.ArticleH2s)
                {
                    chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"{h2.Title} Incluyelo en la respuesta"));
                    foreach (var h3 in h2.H3Titles)
                    {
                        chatMessage.Add(new ChatMessage(ChatMessageRole.User, h3));
                    }
                    chatMessage.Add(new ChatMessage(ChatMessageRole.User, "Continua escribiendo el artículo:"));
                    var articlePrompt = GenerateChatRequest(chatMessage.ToArray());
                    Thread.Sleep(5000); 
                    var article = await _api.Chat.CreateChatCompletionAsync(articlePrompt);
                    result += article.Choices[0].Message.TextContent;
                    chatMessage.Clear();
                    chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"Únicamente puedes usar las etiquetas HTML: <H2>, <H3>, <P>, <UL>, <LI>, <STRONG>. "));
                    chatMessage.Add(new ChatMessage(ChatMessageRole.User, $"Incluye en la respuesta los h2 y h3 a los que le des respuesta y elimina los /n"));
                    chatMessage.Add(new ChatMessage(ChatMessageRole.User, "Ahora crea el contenido para la siguiente estructura y que tenga conconrdancia con lo anterior, espera la palabra Continua para comenzar a escribir"));
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            result.Replace("\n\n", "");
            return result.Replace("\n", "");
        }

        public ChatRequest GenerateChatRequest(ChatMessage [] messages)
        {
            ChatRequest request = null;
            try
            {
                request = new ChatRequest()
                {
                    Model = "gpt-4-turbo",
                    Temperature = 0.7,
                    MaxTokens = 4096,
                    Messages = messages
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return request;
        }
    }

}