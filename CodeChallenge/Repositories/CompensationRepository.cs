using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _compensationContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
            _logger = logger;
        }
        
        public CompensationDto GetByEmployeeId(string id)
        {
            return _compensationContext.Compensation
                .Include(c => c.Employee)
                .Where(c => c.Employee != null && c.Employee.EmployeeId == id)
                .Select(c => new CompensationDto()
                {
                    Employee = c.Employee,
                    Salary = c.Salary,
                    EffectiveDate = c.EffectiveDate
                })
                .FirstOrDefault();
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            compensation.EffectiveDate = DateTime.Now;
            _compensationContext.Compensation.Add(compensation);
            return compensation;
        }

        public Compensation Remove(Compensation compensation)
        {
            return _compensationContext.Remove(compensation).Entity;
        }

        public Task SaveAsync()
        {
            return _compensationContext.SaveChangesAsync();
        }
    }
}