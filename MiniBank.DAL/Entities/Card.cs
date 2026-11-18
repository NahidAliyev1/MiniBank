using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.DAL.Entities;
public class Card:BaseModel
{
    public string CardNumber { get; set; }
	public DateTime ExpireDate { get; set; }

    public decimal Balance { get; set; }	
	public bool IsBlocked { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    private int _cvv;

	public int Cvv
	{
		get { return _cvv; }
		set { _cvv = value; }
	}


}
