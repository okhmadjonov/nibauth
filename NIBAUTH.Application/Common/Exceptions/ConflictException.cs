namespace NIBAUTH.Application.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string name, object key) : base($"Entity \"{name}\" ({key}) already exist.")
        {

        }

        public ConflictException(string message) : base(message)
        {

        }

    }
}
