using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;

namespace ORM_MINI_PROJECT.Repositories.Implementations
{
    public class UserRepository:Repository<User>,IUserRepository
    {
    }
}
