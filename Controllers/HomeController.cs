using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using RandomPasscode.Models;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        int codeCount = HttpContext.Session.GetInt32("CodeCount") ?? 0;

        // Genera un código de acceso aleatorio de 14 caracteres
        string randomCode = GenerateRandomCode(14);

        // Almacena el código en la sesión
        HttpContext.Session.SetString("RandomCode", randomCode);

        // Incrementa el contador de códigos
        HttpContext.Session.SetInt32("CodeCount", codeCount + 1);

        return View(new PasscodeViewModel
        {
            RandomCode = randomCode,
            CodeCount = codeCount + 1
        });
    }

    private string GenerateRandomCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
