namespace ExadelMentorship.BusinessLogic.Features
{
    public interface IRWOperation
    {
        void WriteLine(string text);
        void WriteLine(string format, params object[] arg);
        string ReadLine();
    }
}
