using System;

namespace CodeChallenge.Models
{
    public class CompensationDto
    {
        public Employee Employee { get; set; }
        public float Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
