using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using TestsES5.Models;
using TestsES5.Models.Entity;
using TestsES5.Models.Interfaces;
using TestsES5.Extensions;
using System.IO;

namespace TestsES5.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _repository;
        public QuestionController(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View("MainPage");
        }
        [HttpPost]
        public int TestInit(int query = 5)
        {
            var result = _repository.GetRandomElements(query);
            QuestionStorage storage = HttpContext.Session.GetQuestionStorageFromSession<QuestionStorage>("QuestionStorage") ?? new QuestionStorage();
            foreach (var question in result)
            {
                storage.AddItem(question);
            }
            HttpContext.Session.SetQuestionStorageToSession("QuestionStorage", storage);
            return query;
        }

        [HttpGet]
        public QuestionEntity GetNextQuestion()
        {
            var storage = HttpContext.Session.GetQuestionStorageFromSession<QuestionStorage>("QuestionStorage");
            QuestionEntity result = null;
            if (storage != null)
            {
                result = storage.GetAndRemoveNextQuestionFromSession();
            }
            HttpContext.Session.SetQuestionStorageToSession("QuestionStorage", storage);
            return result;
        }
        [HttpGet]
        public IEnumerable<QuestionEntity> getAllQuestions()
        {
            return _repository.GetRandomElements(10);
        }

        [HttpPost]
        public ViewResult Result()
        {
            var body = "";
            using (var reader = new StreamReader(Request.Body))
            {
                body = reader.ReadToEnd();
            }
            ViewBag.result = Int32.Parse(body);
            return View("Result");
        }
    }
}