using System;
using System.Collections.Generic;

namespace WebCbt_Backend.Models
{
    public partial class Evaluation
    {
        public int EvaluationId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; } = null!;
        public int Question1 { get; set; }
        public int Question2 { get; set; }
        public int Question3 { get; set; }
        public int Question4 { get; set; }
        public int Question5 { get; set; }
    }
}
