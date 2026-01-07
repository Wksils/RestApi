namespace RestAPI.Models.Abstractions
{
    public interface ICommonService<T>
    {
        bool Create(T model);
        bool Update(int id,T model);
        bool Delete(int id);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
