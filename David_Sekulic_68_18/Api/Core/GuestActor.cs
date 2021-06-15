using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core
{
    public class GuestActor : IActor
    {
        public int Id => 0;

        public string Identity => "Guest";
        //{2,3,6};
        public IEnumerable<int> AllowedUseCases => new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
    }
}
