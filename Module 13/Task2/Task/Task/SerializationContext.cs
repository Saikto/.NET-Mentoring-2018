using System;
using System.Data.Entity.Core.Objects;

namespace Task
{
    public class SerializationContext
    {
        public ObjectContext ObjectContext { get; set; }
        public Type TypeToSerialize { get; set; }
    }
}
