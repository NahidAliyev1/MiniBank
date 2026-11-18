using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.BL.DTOS
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string FinCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
