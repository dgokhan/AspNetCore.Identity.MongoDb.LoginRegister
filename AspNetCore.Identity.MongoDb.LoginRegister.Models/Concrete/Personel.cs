using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace AspNetCore.Identity.MongoDb.LoginRegister.Models.Concrete
{
    [CollectionName("Personel")]
    public class Personel : MongoIdentityUser
    {
        public Personel()
        {
            CreatedDate = DateTime.Now;
            Status = 1;
        }

        public PersonelContact PersonelContact { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public short Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
