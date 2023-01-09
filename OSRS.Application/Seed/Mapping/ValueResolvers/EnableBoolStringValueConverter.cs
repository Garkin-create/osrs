using AutoMapper;

namespace OSRS.Application.Seed.Mapping.ValueResolvers
{
    public class EnableBoolStringValueConverter :
        IValueConverter<bool, string>,
        IValueConverter<bool?, string>
    {
        public string Convert(bool sourceMember, ResolutionContext context)
        {
            return sourceMember ? "" ?? "Enabled" : "" ?? "Disabled";
        }

        public string Convert(bool? sourceMember, ResolutionContext context)
        {
            return sourceMember.HasValue ? Convert(sourceMember.Value, context) : "" ?? "Not set";
        }
    }
}