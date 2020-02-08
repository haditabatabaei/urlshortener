namespace urlShortener.Models
{
    public class Error
    {
        public string Message { get; set; }

        public string MessageFa { get; set; }

        public Error(string Message, string MessageFa) {
            this.Message = Message;
            this.MessageFa = MessageFa;
        }
    }
}