using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DormMS.Web.Repositories
{
    public class StudentFeeRepository : IStudentFeeRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentFeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentFee>> GetAllAsync()
            => await _context.StudentFees.ToListAsync();

        public async Task<IEnumerable<StudentFee>> GetByStudentIdAsync(int studentId) 
            => await _context.StudentFees.Include(f => f.Fee).Where(f => f.studentId == studentId).ToListAsync();

        public async Task<StudentFee?> GetByIdAsync(int id) => await _context.StudentFees.FindAsync(id);

        public async Task<bool> AddAsync(StudentFee fee)
        {
            _context.StudentFees.Add(fee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(StudentFee fee)
        {
            _context.StudentFees.Update(fee);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;
    }
}

