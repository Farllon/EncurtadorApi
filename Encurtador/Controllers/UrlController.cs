using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Encurtador.DTO;
using Encurtador.Intefaces;
using Encurtador.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Name.Controllers
{
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUrlService _urlService;
        private readonly IUrlRepository _urlRepository;
        private readonly IHashRepository _hashRepository;
        private int _timeoutRetry = 60;

        public UrlController(IConfiguration configuration, 
                              IUrlService urlService,
                              IUrlRepository urlRepository,
                              IHashRepository hashRepository)
        {
            _configuration = configuration;
            _urlService = urlService;
            _urlRepository = urlRepository;
            _hashRepository = hashRepository;
        }

        [HttpPost]
        [Authorize]
        [Route("urls")]
        public async Task<ActionResult<dynamic>> GetHashUrl([FromBody]UrlDto model)
        {
            int i = 0;
            string hash;
            
            while (i < _timeoutRetry)
            {
                hash = _urlService.GenerateHash();

                if (await _hashRepository.GetHash(hash) == null)
                {
                    var urlModel = new Url
                    {
                        Hash = hash,
                        OriginalUrl = model.OriginalUrl,
                        CreatedAt = DateTime.Now,
                        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    };

                    // await _hashRepository.Create(new Hash { HashCode = hash, IsAvaible = false });
                    await _urlRepository.Create(urlModel);

                    return new
                    {
                        hash = hash
                    };
                }

                Thread.Sleep(320);
            }

            return new 
            {
                erro = "Tempo limite de conexão excedido!"
            };
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{hash}")]
        public async Task RedirectTo(string hash)
        {
            var originalUrl = await _urlRepository.GetUrl(hash);

            if (originalUrl == null)
                Response.Redirect("error");

            else
                Response.Redirect(originalUrl.OriginalUrl);
        }

        [HttpGet]
        [Route("error")]
        public ActionResult<dynamic> ToError()
        {
            return new
            {
                erro = "Página não encontrada!"
            };
        }
    }
}