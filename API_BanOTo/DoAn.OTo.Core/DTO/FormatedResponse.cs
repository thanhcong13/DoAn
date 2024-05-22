using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.OTo.Core.DTO
{
    public class FormatedResponse
    {


        public EnumStatusCode StatusCode { get; set; } = EnumStatusCode.StatusCode200;

        public object? InnerBody { get; set; }

        public Exception? Exception { get; set; }
        public int? Count { get; set; }
    }

    public enum EnumStatusCode 
    {
        StatusCode200 = 200,
        StatusCode204 = 204,
        StatusCode400 = 400,
        StatusCode401 = 401,
        StatusCode404 = 404,
        StatusCode500 = 500,
        StatusCode519 = 519
    }

}
