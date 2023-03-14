﻿using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public IActionResult Register(UserAndCompanyRegisterDto userAndCompanyRegister)
        {

            var userExists = _authService.UserExists(userAndCompanyRegister.UserForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            //sirket kontrolu yap
            var companyExists = _authService.CompanyExists(userAndCompanyRegister.Company);
            if (!companyExists.Success)
            {
                return BadRequest(companyExists.Message);
            }


            var registerResult = _authService.Register(userAndCompanyRegister.UserForRegister, userAndCompanyRegister.UserForRegister.Password, userAndCompanyRegister.Company);
            var result = _authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId); //kullanici ile sirket baglantisi yapinca CreateAccessToken metodunu kullan
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);

        }


        [HttpPost("registerSecondAccount")]
        public IActionResult RegisterSecondAccount(UserForRegister userForRegister, int companyId)
        {
            var userExists = _authService.UserExists(userForRegister.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.RegisterSecondAccount(userForRegister, userForRegister.Password);
            var result = _authService.CreateAccessToken(registerResult.Data, companyId); //kullanici ile sirket baglantisi yapinca CreateAccessToken metodunu kullan
            if (result.Success)
            {
                return Ok(result.Data);
            }

            //if (registerResult.Success)
            //{
            //    return Ok(registerResult);
            //}
            return BadRequest(registerResult.Message);

        }
        [HttpPost("login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authService.Login(userForLogin);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data, 0);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
