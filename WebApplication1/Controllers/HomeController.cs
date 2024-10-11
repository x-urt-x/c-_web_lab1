using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult EntryView() => View();
        public IActionResult OpenInputView() => View("InputView");

        public async Task<IActionResult> SubmitPersonRew(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View("InputView");
            }
            
            await Person.CreateAsync(person);
            return View("ThanksView", person);
        }
        public async Task<IActionResult> OpenListView()
        {
            List<Person> persons = await Person.GetAllAsync();
            return View("ListView", persons);
        }
    }
}
