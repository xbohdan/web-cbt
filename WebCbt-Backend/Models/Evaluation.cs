using System;
using System.Collections.Generic;

namespace WebCbt_Backend.Models
{
    public partial class Evaluation
    {
        public Evaluation()
        {
            ScoreSheets = new HashSet<ScoreSheet>();
        }

        public int EvaluationId { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int EvaluationType { get; set; }
        public int TotalScore { get; set; }

        public virtual ICollection<ScoreSheet> ScoreSheets { get; set; }
    }
}
