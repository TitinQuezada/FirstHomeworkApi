using Core.Contracts;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Managers
{
    public sealed class MunicipityManager
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        public MunicipityManager(IMunicipalityRepository municipalityRepository)
        {
            _municipalityRepository = municipalityRepository;
        }

        public async Task<IOperationResult<IEnumerable<Municipality>>> GetMunicipalities()
        {
            try
            {
                List<Municipality> municipalities = await _municipalityRepository.FindAllAsync(x => !string.IsNullOrEmpty(x.Description));
                return BasicOperationResult<IEnumerable<Municipality>>.Ok(municipalities);
            }
            catch (Exception ex)
            {
                return BasicOperationResult<IEnumerable<Municipality>>.Fail("Ha ocurrido un error obteniedo los municipios", ex.ToString());
            }
        }
    }
}
