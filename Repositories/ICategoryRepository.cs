using LibrarieOnline.Models;

namespace LibrarieOnline.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category?> GetById(int id);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}