using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TestsES5.Extensions;
using TestsES5.Models.Entity;

namespace TestsES5.Models
{
    [Serializable]
    public class SessionQuestionStorage : QuestionStorage
    {
        public static QuestionStorage GetStorage(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionQuestionStorage storage = session?.GetQuestionStorageFromSession<SessionQuestionStorage>("QuestionStorage")
                ?? new SessionQuestionStorage();
            storage.Session = session;
            return storage;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(QuestionEntity question)
        {
            if (question != null)
            {
                base.AddItem(question);
                Session.SetQuestionStorageToSession("QuestionStorage", this);
            }
        }

        public override void RemoveLine(QuestionEntity question)
        {
            if (question != null)
            {
                base.RemoveLine(question);
                Session.SetQuestionStorageToSession("QuestionStorage", this);
            }
        }
        public override QuestionEntity GetAndRemoveNextQuestionFromSession()
        {
           var storage= Session.GetQuestionStorageFromSession<QuestionStorage>("QuestionStorage");
            return base.GetAndRemoveNextQuestionFromSession();
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("QuestionStorage");
        }
    }
}