using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestsES5.Models.Entity
{
    [Serializable]
    public class QuestionEntity : IComparable
    {
        [Key]
        public int Id { get; set; }

        [Column("Answers")]
        [Required]
        public string Answers { get; set; }

        [Column("Options")]
        [Required]
        public string Options { get; set; }

        [Column("Text")]
        [Required]
        public string Text { get; set; }

        [Column("Timeout")]
        public int? Timeout { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            QuestionEntity entity = obj as QuestionEntity;
            if (entity != null)
                return this.Id.CompareTo(entity.Id);
            else
                throw new ArgumentException("Object is not a QuestionEntity");
        }
    }
}

