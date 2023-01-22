using OSRS.Application.Models.User.Model.Input;
using OSRS.Application.Seed.Command;

namespace OSRS.Application.Models.User.Command
{
    public class UserCommandBase <T> : CommandBase<T>
    {
        public AddUserInputModel Model { get; set; }

        public UserCommandBase(AddUserInputModel model)
        {
            Model = model;
        }
        
    }
}