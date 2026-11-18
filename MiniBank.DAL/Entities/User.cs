using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.DAL.Entities;
public class User:BaseModel
{
    public string FullName { get; set; }
    public string FinCode { get; set; }
    public string PhoneNumber { get; set; }
    public List<Card> Cards { get; set; }


}
