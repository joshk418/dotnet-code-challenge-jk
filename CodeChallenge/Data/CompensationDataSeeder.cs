using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Newtonsoft.Json;

namespace CodeChallenge.Data
{
    public class CompensationDataSeeder
    {
        private CompensationContext _compensationContext;
        private const string COMPENSATION_SEED_DATA_FILE = "resources/CompensationSeedData.json";

        public CompensationDataSeeder(CompensationContext compensationContext)
        {
            _compensationContext = compensationContext;
        }
        
        public async Task Seed()
        {
            if(!_compensationContext.Compensation.Any())
            {
                var comp = LoadCompensation();
                _compensationContext.Compensation.AddRange(comp);

                await _compensationContext.SaveChangesAsync();
            }
        }
        
        private List<Compensation> LoadCompensation()
        {
            using var fs = new FileStream(COMPENSATION_SEED_DATA_FILE, FileMode.Open);
            using var sr = new StreamReader(fs);
            using var jr = new JsonTextReader(sr);
            
            var serializer = new JsonSerializer();
            var compensation = serializer.Deserialize<List<Compensation>>(jr);

            return compensation;
        }
    }
}