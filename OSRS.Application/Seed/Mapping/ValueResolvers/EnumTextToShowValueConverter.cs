using System;
using AutoMapper;

namespace OSRS.Application.Seed.Mapping.ValueResolvers
{
    public class EnumTextToShowValueConverter : IValueConverter<Enum, string>
    {
        public string Convert(Enum sourceMember, ResolutionContext context)
        {
            return "";// I18n.ResourceManager.GetString($"{sourceMember.GetType().Name}_{sourceMember}") ?? sourceMember.ToString();
        }
    }
}
