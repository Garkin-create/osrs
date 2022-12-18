using System;

namespace OSRS.Persistance.Helper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegister : Attribute
    {
        public AutoRegister(Type interfaceType)
        {

        }
    }
}
