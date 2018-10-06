using ShopService.Common.Infrastructure;
using ShopService.Common.Interfaces;

namespace ShopService.Web.Helpers
{
    public class Message : IMessage
    {
        public object Data { get; set; }
        public string Description { get; set; }
        public IError Error { get; set; }

        public bool IsSuccess { get; set; }
        public IPaging Paging { get; set; }

        internal Message()
        {
        }

        public Message(Error error) 
        {
            Data = null;
            Error = error;
            Description = "error";
            IsSuccess = false;
        }

        public Message(object data)
        {
            Data = data;
            IsSuccess = true;
        }

        public Message(QueryResult queryResult)
        {
            Data = queryResult.GetData();
            Paging = queryResult.Paging;
            IsSuccess = true;
        }
    }
}
