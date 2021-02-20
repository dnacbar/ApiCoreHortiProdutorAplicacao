﻿using HORTIQUERY.DOMAIN.INTERFACE.APP;
using HORTIQUERY.DOMAIN.MODEL.SIGNATURE;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HORTIQUERY.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class DistrictController : ControllerBase
    {
        private readonly IDistrictQueryApp _consultDistrictApp;

        public DistrictController(IDistrictQueryApp consultDistrictApp)
        {
            _consultDistrictApp = consultDistrictApp;
        }

        [HttpPost(nameof(GetDistrictByIdOrName))]
        public async Task<IActionResult> GetDistrictByIdOrName(DistrictQuerySignature signature)
        {
            return Ok(await _consultDistrictApp.GetDistrictByIdOrName(signature));
        }

        [HttpGet(nameof(GetFullListOfDistricts))]
        public async Task<IActionResult> GetFullListOfDistricts()
        {
            return Ok(await _consultDistrictApp.GetFullListOfDistricts());
        }

        [HttpPost(nameof(GetListOfDistricts))]
        public async Task<IActionResult> GetListOfDistricts(DistrictQuerySignature signature)
        {
            return Ok(await _consultDistrictApp.GetListOfDistricts(signature));
        }
    }
}
