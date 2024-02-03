using API_CRUD_Template.Models;

namespace API_CRUD_Template.Data.Interfaces
{
    public interface IFirstEntity
    {
        Task<List<FirstEntity>> GetAll();
        Task<FirstEntity> GetById(int id);
        Task<FirstEntity> Post(FirstEntity firstEntity);
        Task<FirstEntity> Update(int id, FirstEntity firstEntity);
        Task Delete(int id);
    }
}
