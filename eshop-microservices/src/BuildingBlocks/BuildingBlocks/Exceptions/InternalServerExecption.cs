namespace BuildingBlocks.Exceptions
{
   public class InternalServerExecption : Exception
    {
        public InternalServerExecption(string message) : base(message)
        {

        }
        public InternalServerExecption(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {

        }
        public string? Details { get; }
    }
}
