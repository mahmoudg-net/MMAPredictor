using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.Core.ProcessingResult
{
    public class EntityProcessingResult<T> where T:class
    {
        public ProcessingStatus Status { get; set; }
        public string? Message { get; set; }
        public T? Entity { get; set; }

        public static EntityProcessingResult<T> BadInput(string message) => new EntityProcessingResult<T> { Status = ProcessingStatus.BadInput, Message = message };
        public static EntityProcessingResult<T> NotFound(string message) => new EntityProcessingResult<T> { Status = ProcessingStatus.NotFound, Message = message };
        public static EntityProcessingResult<T> Created(string message, T entity) => new EntityProcessingResult<T> { Status = ProcessingStatus.Created, Message = message };
        public static EntityProcessingResult<T> Updated(string message, T entity) => new EntityProcessingResult<T> { Status = ProcessingStatus.Updated, Message = message };
        public static EntityProcessingResult<T> Exception(string message) => new EntityProcessingResult<T> { Status = ProcessingStatus.Exception, Message = message };
    }

    public enum ProcessingStatus
    {
        Created,
        Updated,
        BadInput,
        NotFound,
        Exception,
    }
}
