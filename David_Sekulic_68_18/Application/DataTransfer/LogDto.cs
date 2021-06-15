using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransfer
{
    public class LogDto
    {
        public string Actor { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UseCaseName { get; set; }
        public DateTime Date { get; set; }
    }
}
