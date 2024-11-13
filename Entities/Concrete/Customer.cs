using Core.Entities;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public string CustomerId { get; set; } //Northwinde customerId string olduğu için
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }

    }
}
