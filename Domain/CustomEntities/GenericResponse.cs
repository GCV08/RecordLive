using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomEntities
{
    public class GenericResponse<T>
    {
        public GenericResponse(T data)
        {
            this.errors = new Dictionary<string, string>();
            this.Data = data;
        }

        public GenericResponse()
        {
            this.errors = new Dictionary<string, string>();
            this.Data = default;
        }

        public T Data { get; set; }
        public Metadata Meta { get; set; } = null;
        public bool success { get; set; }
        public int? ErrorCode { get; set; } = null;
        public Dictionary<string, string> errors { get; set; }

        public void AddError(string errorTitle, string errorDescription, int? errorCode = null)
        {
            errors.Add(errorTitle, errorDescription);
            ErrorCode = errorCode ?? 0;
        }
    }
}

