using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.DataAccess.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MyAssessmentDbContext context) : base(context) { }

        public Task<bool> HasEmployeesAsync(int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
