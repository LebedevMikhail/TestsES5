using System;
using System.Collections.Generic;
using TestsES5.Models;
using TestsES5.Models.Entity;

namespace TestsES5
{
    [Serializable]
    public class QuestionStorage
    {
        public List<QuestionEntity> Questions = new List<QuestionEntity>();

        public virtual void Clear() => Questions.Clear();

        public virtual void AddItem(QuestionEntity question)
        {
            if (question != null)
            {
                Questions.Add(question);
            }

        }

        public virtual void RemoveLine(QuestionEntity question) =>
            Questions?.RemoveAll(q => q?.Id == question.Id);
        public virtual QuestionEntity GetAndRemoveNextQuestionFromSession()
        {
            if (Questions.Count > 1)
            {
                var lastIndex = Questions.Count - 1;
                var result = Questions[lastIndex];
                Questions.RemoveAt(Questions.Count - 1);
                return result;
            }
            return null;
        }
    }
}