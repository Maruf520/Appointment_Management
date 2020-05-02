using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital
{
    public interface IUnitOfWork
    {
        IPatientRepository Patient { get; }
        IGenderRepository Gender { get; } 

        void Complete();
    }
}
