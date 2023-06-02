using DapperExtensions.Mapper;
using PaymentAPI.Domain.Entities;
using PaymentAPI.Infastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.Infastructure.Data.Mappers
{
    public class PaymentMapper : ClassMapper<PaymentDTO>
    {
        public PaymentMapper()
        {
            // Use the Table method to specify the table name if different than the class name.
            Table("Payment");

            // Use the Map method to map properties to columns.
            // Here we specify that the Id column is the key.
            Map(x => x.Id).Column("Id").Key(KeyType.Assigned);

            // AutoMap remaining properties
            AutoMap();
        }
    }
}
