using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Shared.Models
{
    public class CommonApiResponses
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string StatusDetails { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public CommonApiResponses()
        {

        }

        public CommonApiResponses(bool isSuccess, int statusCode, string statusDetails, string message, dynamic data)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            StatusDetails = statusDetails;
            Message = message;
            Data = data;
        }
    }
}
