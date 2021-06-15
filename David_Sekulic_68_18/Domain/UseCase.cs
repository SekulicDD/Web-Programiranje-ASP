using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UseCase:Entity
    {
        public string Name { get; set; }
        public ICollection<UserUseCase> UseCaseUsers { get; set; } = new HashSet<UserUseCase>();
    }
}
