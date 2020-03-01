﻿using APPDTOCOREHORTIQUERY.SIGNATURE;
using DOMAINCOREHORTICOMMAND;
using System.Threading.Tasks;

namespace DATACOREHORTIQUERY.IQUERIES
{
    public interface IProducerRepository
    {
        Task<Producer> GetProducerByEmail(ConsultProducerSignature signature);
    }
}
