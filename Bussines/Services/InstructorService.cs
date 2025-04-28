using Persistence.Entities;
using Persistence.Repositories;

namespace Bussines.Services
{
    public interface IInstructorService
    {
        Task<Instructor?> GetInstructor(string username, string password);
        Task<Instructor?> GetInstructor(string name);
    }
    
    class InstructorService : IInstructorService
    {
        private readonly IMiAppRepository db;

        public InstructorService(IMiAppRepository db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Instructor?> GetInstructor(string username, string password)
        {
            return await db.GetInstructor(username, password);
        }

        public async Task<Instructor?> GetInstructor(string name)
        {
            return await db.GetInstructor(name);
        }
    }
}