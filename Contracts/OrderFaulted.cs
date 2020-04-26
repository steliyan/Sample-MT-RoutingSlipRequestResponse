namespace Contracts
{
    public interface OrderFaulted
    {
        string Id { get; }

        string Reason { get; }
    }
}
