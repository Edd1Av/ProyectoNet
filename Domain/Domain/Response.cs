using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain
{
    public class Response
    {
        public bool Success { get; set; }
        public object? Content { get; set; }
        public string? Error { get; set; }
    }
}
