using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        /// <summary>
        /// GetByEmployeeId calls the CompensationRepository to grab a Compensation record for an employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CompensationDto GetByEmployeeId(string id);
        
        /// <summary>
        /// Create creates and saves a new Compensation record to the database.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        Compensation Create(Compensation compensation);
    }
}