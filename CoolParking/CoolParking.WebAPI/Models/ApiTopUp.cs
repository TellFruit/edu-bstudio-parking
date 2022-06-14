namespace CoolParking.WebAPI.Models
{
    public class ApiTopUp
    {
        public string Id { get; }
        
        public decimal Sum { get; }

        public ApiTopUp(string id, decimal sum)
        {
            Id = id;
            Sum = sum;
        }
    }
}
