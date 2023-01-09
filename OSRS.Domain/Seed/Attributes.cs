using System;

namespace OSRS.Domain.Seed
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegister : Attribute
    {
        public AutoRegister(Type interfaceType)
        {

        }
    }
}
