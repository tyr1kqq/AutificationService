namespace AutificationService
{
    public interface ILogerrClass
    {
        void WriteEvent(string eventMessage);
        void WriteError(string errorMesage);
    }
}
