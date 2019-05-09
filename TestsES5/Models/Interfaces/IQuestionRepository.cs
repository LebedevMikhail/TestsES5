using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsES5.Models.Entity;

namespace TestsES5.Models.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<QuestionEntity> Questions { get; }
        IEnumerable<QuestionEntity> GetRandomElements(int number);
        QuestionEntity GetQuestionById(int questionId);
    }
}
