using System.Collections.Generic;


namespace DatabaseCore.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }


        public AccountModel Account { get; set; }
        public List<TransactionModel> Transactions { get; set; }

    }
}
