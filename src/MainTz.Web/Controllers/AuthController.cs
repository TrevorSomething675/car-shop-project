using MainTz.Application.Models.SittingsModels;
using MainTz.Application.Models.UserEntities;
using MainTz.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.ViewModels;
using FluentValidation;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly IUserService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IValidator<LoginFormRequest> _loginFormValidator;
        private readonly IValidator<RegisterFormRequest> _registerFormValidator;
        private readonly IValidator<RestoreEmailRequest> _retoreEmailValidator;
        public AuthController(IUserService usersService, ITokenService tokenService, IMapper mapper, IMailService mailService,
            IValidator<RegisterFormRequest> registerFormValidator, IValidator<LoginFormRequest> loginFormValidator, IValidator<RestoreEmailRequest> retoreEmailValidator)
        {
            _registerFormValidator = registerFormValidator;
            _retoreEmailValidator = retoreEmailValidator;
            _loginFormValidator = loginFormValidator;
            _usersService = usersService;
            _tokenService = tokenService;
            _mailService = mailService;
            _mapper = mapper;
        }
        /// <summary>
        /// Получение токена из сервиса, путём отправки запроса с ролью
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> GetToken([FromBody] string role, string name) // отправка сообщения и получение токена из AuthApi
        {
            var tokensModel = new TokensModel
            {
                RefreshToken = _tokenService.CreateRefreshToken(role, name),
                AccessToken = _tokenService.CreateAccessToken(role, name),
                Role = role
            };

            return Results.Json(tokensModel);
        }
        /// <summary>
        /// Получение разметки с формой логина
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> RestoreAccountOnEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IResult> RestoreAccountOnEmail(RestoreEmailRequest restoreEmailRequest)
        {
            var restoreEmailValidator = _retoreEmailValidator.Validate(restoreEmailRequest);
            if (!restoreEmailValidator.IsValid)
                return Results.Json(new ErrorViewModel { ErrorMessage = "Неверный адрес почты" });

            //var isValidEmail = await _mailService.CheckEmailAsync("soalone999@mail.ru");
            return Results.Json(new MessageViewModel { Message = "Если эта почта ассоциирована с пользователем, то на нее была отправлена инструкция по сбросу" });
        }
        [HttpGet]
        public async Task<IActionResult> ErrorViewPartial(string error)
        {
            return PartialView("ErrorViewPartial", error);
        }
        [HttpGet]
        public async Task<IActionResult> InfoViewPartial(string message)
        {
            return PartialView("InfoViewPartial", message);
        }
		/// <summary>
		/// Отправка формы логина, она возрвращает json, js код в FormScripts.js 
		/// получает её и в зависимости от роли перенаправляет пользователя на страницу роли.
		/// </summary>
		/// <param name="userDto">Модель, которая приходит с фронта</param>
		/// <returns></returns>
		[HttpPost]
        public async Task<IResult> LoginNormal(LoginFormRequest loginFormRequest)
        {
            var formValid = _loginFormValidator.Validate(loginFormRequest);
            if (!formValid.IsValid)
                return Results.Json(new ErrorViewModel { ErrorMessage = "Введите логи и пароль" });

            var user = await _usersService.GetUserByNameAsync(loginFormRequest.Name);

            if (loginFormRequest.Password != user?.Password || user == null)
                return Results.Json(new ErrorViewModel { ErrorMessage = "Неверный логин или пароль" });

            var tokens = await GetToken(user.Role.RoleName, user.Name);

            return Results.Json(tokens);
        }
        [HttpPost]
        public async Task<IResult> LoginMail(LoginFormRequest loginFormRequest)
        {
            var formValid = _loginFormValidator.Validate(loginFormRequest);
            if (!formValid.IsValid)
                return Results.Json(new ErrorViewModel { ErrorMessage = "Введите логи и пароль" });

            var user = await _usersService.GetUserByEmailAsync(loginFormRequest.Name);

            if (loginFormRequest.Password != user?.Password || user == null)
                return Results.Json(new ErrorViewModel { ErrorMessage = "Неверный логин или пароль" });

            var tokens = await GetToken(user.Role.RoleName, user.Name);

			return Results.Json(tokens);
		}
		/// <summary>
		/// Получение разметки с формой регистрации
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        /// <summary>
        /// Регистрация пользователя, по умолчанию роль всегда 'User', 
        /// мы регистрируем пользователя, а после вызываем метод Login, результат обрабатывает RegisterFormScript.js
        /// и перенаправляет пользователя на нужную страницу после регистрации.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Register(RegisterFormRequest registerFormRequest)
        {
            var registerFormModel = await _registerFormValidator.ValidateAsync(registerFormRequest);

            if (!registerFormModel.IsValid)
                return Results.BadRequest(new ErrorViewModel { ErrorMessage = string.Join(" ", registerFormModel.Errors.Select(err => err.ErrorMessage))});

            var user = await _usersService.GetUserByNameAsync(registerFormRequest.Name);

            if (user != null)
                return Results.BadRequest("Пользователь уже существует");

            var userDomainEntity = _mapper.Map<User>(registerFormRequest);
            await _usersService.CreateAsync(userDomainEntity);
            var result = await LoginNormal(registerFormRequest);
            
            return result;
        }
    }
}
