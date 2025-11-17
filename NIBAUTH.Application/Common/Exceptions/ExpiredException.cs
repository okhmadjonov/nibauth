namespace NIBAUTH.Application.Common.Exceptions
{
    public class ExpiredException : Exception
    {
        public ExpiredException(string key) : base($"Your {key} has been expired")
        {

        }
    }
}
