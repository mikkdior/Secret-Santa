using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Secret_Santa.Controllers
{
    public class HomeController : Controller
    {
        private CEmployees EmpsModel;
        private CGifts GiftsModel;
        private CTickatus SantaModel;

        /// <summary>
        ///     Конструктор. Передаем наши сервисы в поля.
        /// </summary>
        public HomeController(CEmployees emps_model, CGifts gifts_model, CTickatus santa)
        {
            EmpsModel = emps_model;
            GiftsModel = gifts_model;
            SantaModel = santa;
        }

        /// <summary>
        ///     Главная страница
        /// </summary>
        public IActionResult Index()
        {
            var tickets = SantaModel.GetTickets(EmpsModel.GetEmployees(), GiftsModel.GetGifts());

            return View(new CHomeVM(tickets));
        }

        /// <summary>
        ///     Принимаем Ajax запрос. 
        ///     В засисимости от переданного значения act - удаляем или добавляем сотрудника.
        ///     Также возвращаем json ответ, что бы принять его уже в браузере.
        /// </summary>
        [HttpPost]
        [Route("/update/{act:alpha}")]
        public IActionResult UpdateInfo(CEmployee emp, string act)
        {
            if (act != "add" && act != "rem") return Json(null);

            EmpsModel.UpdateEmps(act);
            GiftsModel.UpdateGifts(act);

            var tickets = SantaModel.GetTickets(EmpsModel.GetEmployees(), GiftsModel.GetGifts());

            return Json(tickets.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}