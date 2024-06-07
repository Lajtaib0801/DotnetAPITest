using DotnetAPITest.Models;

namespace DotnetAPITest.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
    }
}
