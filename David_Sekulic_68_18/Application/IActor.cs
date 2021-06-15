using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IActor
    {
        int Id { get; }
        string Identity { get; }
        IEnumerable<int> AllowedUseCases { get; }
    }
}
