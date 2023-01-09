using System;

namespace OSRS.Application.Seed
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegister : Attribute
    {
        public AutoRegister(Type interfaceType)
        {

        }
    }
}
