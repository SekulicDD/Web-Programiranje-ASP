using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UseCaseLog : Entity
    {
        public DateTime Date { get; set; }
        public string Data { get; set; }
        public string Actor { get; set; }
        public int UseCaseId { get; set; }
        public int? UserId { get; set; }
        public UseCase UseCase { get; set; }
        public User User { get; set; }
    }
}
