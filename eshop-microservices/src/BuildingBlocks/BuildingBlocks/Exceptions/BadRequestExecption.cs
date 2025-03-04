namespace BuildingBlocks.Exceptions
{
    public class BadRequestExecption : Exception
    {
        public BadRequestExecption(string message) : base(message)
        {

        }
        public BadRequestExecption(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {

        }
    }
}
