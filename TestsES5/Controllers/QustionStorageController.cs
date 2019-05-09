using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TestsES5.Models.Entity;
using TestsES5.Models.Interfaces;
using TestsES5.Extensions;

namespace TestsES5.Controllers
{
    public class QustionStorageController : Controller
    {
        public class QuestionStorageController : Controller
        {
            private IQuestionRepository repository;

            public QuestionStorageController(IQuestionRepository repo)
            {
                repository = repo;
            }

            public ViewResult Index()
            {
                return View();
            }

            public RedirectToActionResult AddToStorage(int questionId)
            {
               QuestionEntity question  = repository.Questions
                    .FirstOrDefault(p => p.Id == questionId);

                if (question != null)
                {
                    QuestionStorage storage = GetStorage();
                    storage.AddItem(question);
                    SaveCart(storage);

                }
                return RedirectToAction();
            }

            public RedirectToActionResult RemoveFromStorage(int bookId,
                    string returnUrl)
            {
                QuestionEntity question = repository.Questions
                    .FirstOrDefault(q => q.Id == bookId);
                if (question != null)
                {
                    QuestionStorage storage = GetStorage();
                    storage.RemoveLine(question);
                    SaveCart(storage);
                }
                return RedirectToAction();
            }

            private QuestionStorage GetStorage()
            {
                QuestionStorage storage = HttpContext.Session.GetQuestionStorageFromSession<QuestionStorage>("QuestionStorage") ?? new QuestionStorage();
                return storage;
            }

            private void SaveCart(QuestionStorage storage)
            {
                HttpContext.Session.SetQuestionStorageToSession("QuestionStorage", storage);
            }
            public ActionResult RedirectToCart()
            {
                return View("Index");
            }
        }
    }
}