namespace HangFireTest
{
    public interface IJustMakeMeHappy
    {
        string Message();
    }

    public class JustMakeMeHappy : IJustMakeMeHappy
    {
        public string Message() => "Hello";
    }
}