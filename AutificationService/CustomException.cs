namespace AutificationService
{
    public class CustomException : Exception
    {
        private readonly string _message;
        public CustomException(string message)
        {
            _message = message;
        }
    }
}
