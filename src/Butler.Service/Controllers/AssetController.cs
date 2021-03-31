using Butler.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Butler.Service.Controllers
{
    [ApiController]
    [Route("Asset")]
    public class AssetController : Controller
    {
        [HttpPost("AddFundPosition")]
        public bool AddFundPosition(string fundCode, decimal avgCost, decimal share) => Asset.AddFundPosition(fundCode, avgCost, share);

        [HttpPost("RemoveFundPosition")]
        public bool RemoveFundPosition(string fundCode) => Asset.RemoveFundPosition(fundCode);

        [HttpGet("Analyse")]
        public async Task<string> Analyse() => (await Asset.Analyse()).GetDescription();

        [HttpPost("Clear")]
        public void Clear() => Asset.Clear();
    }
}
