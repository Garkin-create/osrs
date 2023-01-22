using OSRS.Application.Models.Alchemy.Model.Input;
using OSRS.Application.Seed.Command;

namespace OSRS.Application.Models.Alchemy.Command
{
    public class AlchemyBaseCommand <T> : CommandBase<T>
    {
        public AddAlchemyInputModel Model { get; set; }

        public AlchemyBaseCommand(AddAlchemyInputModel model)
        {
            Model = model;
        }
    }
}