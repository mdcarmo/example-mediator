using SmallApi.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmallApi.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person> GetById(object id);
        Task<int> Create(Person entity);
        Task<bool> Update(Person entity);
        Task<bool> Delete(int id);
        Task<bool> ExistWithThisRegister(int registerId);
    }
}
