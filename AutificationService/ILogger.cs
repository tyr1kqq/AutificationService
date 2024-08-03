namespace AutificationService
{
    public interface ILogger
    {
        void WriteEvent(string eventMessage);
        void WriteError(string errorMesage);
    }
}
