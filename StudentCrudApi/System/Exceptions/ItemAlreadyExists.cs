namespace StudentCrudApi.System.Exceptions
{
    public class ItemAlreadyExists:Exception
    {
        public ItemAlreadyExists(string? message) : base(message)
        {

        }
    }
}
