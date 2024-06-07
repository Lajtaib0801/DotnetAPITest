using DotnetAPITest.Data;
using DotnetAPITest.Interfaces;
using DotnetAPITest.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPITest.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }
    }
}
