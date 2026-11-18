using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.DAL.Entities;
public class Transactionn:BaseModel
{
    public string FromCardNumber { get; set; }
    public string ToCardNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public bool IsSucces { get; set; }

}
