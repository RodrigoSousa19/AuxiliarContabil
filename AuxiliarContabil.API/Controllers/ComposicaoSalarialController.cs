using Microsoft.AspNetCore.Mvc;

namespace AuxiliarContabil.API.Controllers;

public class ComposicaoSalarialController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}