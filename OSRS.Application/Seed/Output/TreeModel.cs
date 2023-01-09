using System;
using System.Collections.Generic;

namespace OSRS.Application.Seed.Output
{
    public  class TreeNodeModel<T> where T : IConvertible
    {
        public T Value { get; set; }
        public string Label { get; set; }
    }
    public class TreeModel<T>: TreeNodeModel<T> where T : IConvertible
    {
        public List<TreeNodeModel<T>> Children { get; set; }
    }
}
