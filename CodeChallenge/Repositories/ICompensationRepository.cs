using System.Threading.Tasks;
using CodeChallenge.Models;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        /// <summary>
        /// GetByEmployeeId gets the compensation for the employee using a Dto to exclude compensationId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CompensationDto GetByEmployeeId(string id);
        
        /// <summary>
        /// Add creates a new Compensation record in the database.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        public Compensation Add(Compensation compensation);
        
        /// <summary>
        /// Remove removes a Compensation record from the database.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        public Compensation Remove(Compensation compensation);
        
        /// <summary>
        /// SaveAsync is an method to save any changes made to the database.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}