namespace INPDS_Core.Interfaces
{
    public interface IObserver<T>
    {
        void Update(T data);
    }
}