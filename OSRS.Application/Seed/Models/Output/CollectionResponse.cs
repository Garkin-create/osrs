using System.Collections.Generic;

namespace OSRS.Application.Seed.Models.Output
{
    public class CollectionResponse<T> : Response<IEnumerable<T>>
    {
        public CollectionResponse()
        {

        }
        public CollectionResponse(bool success = false, IEnumerable<T> data = null) : base(success, data)
        {
        }

        public CollectionResponse(bool success = false, string message = "", IEnumerable<T> data = null) : base(success, message, data)
        {
        }
    }
}
