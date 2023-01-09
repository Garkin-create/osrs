using System;
using AutoMapper;

namespace Stu.Cubatel.Application.Seed.Mapping.ValueResolvers
{
    public class EnumCamelCaseConverter<T> : ITypeConverter<T, string> where T : Enum
    {
        public string Convert(T source, string destination, ResolutionContext context)
        {
            char[] answer = source.ToString().ToCharArray();
            answer[0] = char.ToLower(answer[0]);
            return string.Join("", answer);
        }
    }
}
