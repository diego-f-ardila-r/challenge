namespace Metafar.Challenge.Model;

    public record ResponseModel<T> where T : class
    {
        /// <summary>
        /// Enum representing the types of responses.
        /// </summary>
        private enum ResponseType
        {
            Success,
            BadRequest,
            NoContent,
            FunctionalError,
            Unauthorized,
            Error
        }
        /// <summary>
        /// Request unique identifier
        /// </summary>
        public string? CorrelationId { get; set; }
        
        /// <summary>
        /// Contains the response status ["Success", "NoContent", "FunctionalError", "Error"]
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Response Languge standarized with Alpha 2 ["ES", "EN"]
        /// </summary>
        public string Lang { get; set; } = "EN";

        /// <summary>
        /// It contains the unique code for a message
        /// </summary>
        public string? MessageCode { get; set; }

        /// <summary>
        /// Contains a message with the operation's result
        /// </summary>
        public string? Message { get; set; }


        // <summary>
        // It contains an object that represents any kind of information
        // </summary>
        public T? Data { get; set; }

        // <summary>
        // Adictional information for the response such as pagination
        // </summary>
        public dynamic? Metadata { get; set; }

        // <summary>
        // It contains an object that represents any kind of error
        // </summary>
        public dynamic? Errors { get; set; }


        public void SetSuccessResponse(string messageCode, dynamic? data = null, dynamic? metadata = null)
        {
            Status = ResponseType.Success.ToString();
            MessageCode = messageCode;
            Data = data;
            Metadata = metadata;
        }

        public void SetFunctionalErrorResponse(string messageCode, dynamic? error = null)
        {
            Status =  ResponseType.FunctionalError.ToString();
            MessageCode = messageCode;
            Errors = error;
        }

        public void SetNoContentErrorResponse(string messageCode, dynamic? error = null)
        {
            Status = ResponseType.NoContent.ToString();
            MessageCode = messageCode;
            Errors = error;
        }

        public void SetInternalErrorServerResponse(dynamic? errors = null)
        {
            Status = ResponseType.Error.ToString();
            MessageCode = "INTERNAL_SERVER_ERROR";
            Errors = errors;
        }
        
        public void SetUnauthorizedErrorResponse(string messageCode, dynamic? error = null)
        {
            Status = ResponseType.Unauthorized.ToString();
            MessageCode = messageCode;
            Errors = error;
        }
        
        public void SetValidatorResponse(string messageCode, dynamic? error = null)
        {
            Status = ResponseType.BadRequest.ToString();
            MessageCode = messageCode;
            Errors = error;
        }
    
}