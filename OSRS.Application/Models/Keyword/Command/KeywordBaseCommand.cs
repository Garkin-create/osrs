using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Seed.Command;

namespace OSRS.Application.Models.Alchemy.Command
{
    public class KeywordBaseCommand <T> : CommandBase<T>
    {
        public AddKeywordInputModel Model { get; set; }

        public KeywordBaseCommand(AddKeywordInputModel model)
        {
            Model = model;
        }
    }
}