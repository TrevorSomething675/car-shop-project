using Microsoft.AspNetCore.Mvc;

namespace MainTz.Web.Controllers
{
    public class TestController : Controller
    {
        public IResult Index()
        {
            return Results.Ok("Ok");
        }
    }
}
