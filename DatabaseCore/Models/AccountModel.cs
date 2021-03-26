using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCore.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }


        public decimal Balance { get; set; }


        public UserModel User { get; set; }

        public decimal Debet { get; set; }
        public decimal Credet { get; set; }
    }
}
