using OSRS.Application.Models.User.Model.Input;
using OSRS.Application.Seed.Interfaces;
using OSRS.Application.Seed.Models.Output;

namespace OSRS.Application.Models.User.Command
{
    public class AddUserCommand : UserCommandBase<Response>, ITraceableRequest
    {
        public AddUserCommand(AddUserInputModel model) : base(model)
        {
        }
    }
}