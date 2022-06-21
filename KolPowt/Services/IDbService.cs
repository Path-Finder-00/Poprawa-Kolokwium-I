using KolPowt.Models;
using KolPowt.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KolPowt.Services
{
    public interface IDbService
    {
        public GetMedicament GetMedicament(int id);
        public bool DeletePatient(int id);
    }
}
