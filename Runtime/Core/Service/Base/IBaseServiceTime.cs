namespace Pangoo.Core.Services
{
    public interface IBaseServiceTime
    {
        float DeltaTime { get; }

        float Time { get; }
    }
}