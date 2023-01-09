using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Seed.Interface
{
    public interface IConfigurationResponse
    {
        public ConfigurationModel Configuration { get; set; }
    }
}
