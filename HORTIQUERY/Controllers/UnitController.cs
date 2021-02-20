﻿using HORTIQUERY.DOMAIN.INTERFACE.APP;
using HORTIQUERY.DOMAIN.MODEL.SIGNATURE;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HORTIQUERY.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class UnitController : ControllerBase
    {
        private readonly IUnitQueryApp _consultUnitApp;
        public UnitController(IUnitQueryApp consultUnitApp)
        {
            _consultUnitApp = consultUnitApp;
        }

        [HttpPost(nameof(GetUnitById))]
        public async Task<IActionResult> GetUnitById([FromBody] UnitQuerySignature signature)
        {
            return Ok(await _consultUnitApp.GetUnitById(signature));
        }

        [HttpGet(nameof(GetFullListOfUnits))]
        public async Task<IActionResult> GetFullListOfUnits()
        {
            return Ok(await _consultUnitApp.GetFullListOfUnits());
        }

        [HttpPost(nameof(GetListOfUnits))]
        public async Task<IActionResult> GetListOfUnits([FromBody] UnitQuerySignature signature)
        {
            return Ok(await _consultUnitApp.GetListOfUnits(signature));
        }
    }
}