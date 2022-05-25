using System;
using System.Collections.Generic;

namespace WebCbt_Backend.Models
{
    public partial class ScoreSheet
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public int Answer1 { get; set; }
        public int Answer2 { get; set; }
        public int Answer3 { get; set; }
        public int Answer4 { get; set; }
        public int Answer5 { get; set; }

        public virtual Evaluation Evaluation { get; set; } = null!;
    }
}
