using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HidalgoCastro.WebInterface.Models
{
    public enum ResponseStatus
    {
        SUCCESS,
        ERROR,
        INVALID_REQUEST
    }

    public class Response<T>
    {
        public Response() { }

        public Response(T data, ResponseStatus status)
        {
            this.Data = data;
            this.Status = status;
        }
        
        public Response(T data, ResponseStatus status, string message)
        {
            this.Data = data;
            this.Status = status;
            this.Message = message;
        }

        public T Data { get; set; }
        
        public ResponseStatus Status { get; set; }

        public string Message { get; set; }

    }
}