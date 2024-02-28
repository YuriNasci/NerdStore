using System.Collections.Generic;

namespace NerdStore.Core.Communication
{
    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public string SuccessMessage { get; set; }
        public ResponseErrorMessages Errors { get; set; }

        public ResponseResult()
        {
            Errors = new ResponseErrorMessages();
        }
        
    }

    public class ResponseErrorMessages
    {
        public List<string> Messages { get; set; }

         public ResponseErrorMessages()
        {
            Messages = new List<string>();
        }
    }
}