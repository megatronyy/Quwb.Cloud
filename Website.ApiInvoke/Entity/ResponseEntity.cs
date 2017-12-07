using System;
using System.Collections.Generic;
using System.Text;

namespace Website.ApiInvoke.Entity
{
    public class ResponseEntity<T> where T : new()
    {
        public bool isSuccess { get; set; }

        public string message { get; set; }

        public int code { get; set; }

        public string sign { get; set; }

        public string appId { get; set; }

        public T data { get; set; }
    }
}
