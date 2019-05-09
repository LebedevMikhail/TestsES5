using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestsES5.Models.Entity;
using TestsES5.Models.Interfaces;

namespace TestsES5.Models
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<QuestionEntity> Questions => _context.QuestionEntities;

        public QuestionEntity GetQuestionById(int questionId) => Questions.FirstOrDefault(q => q.Id == questionId);

        public IEnumerable<QuestionEntity> GetRandomElements(int number) => Questions.OrderBy(q => Guid.NewGuid()).Take(number);

    }
}
