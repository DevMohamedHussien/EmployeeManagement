﻿using MyAssessment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<bool> HasEmployeesAsync(int departmentId);
    }
}
