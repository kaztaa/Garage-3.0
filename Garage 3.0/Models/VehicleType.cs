namespace Garage_3._0.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum VehicleTypes
    {
        Car,
        Motorcycle,
        Truck,
        Bus,
        Van,
        Bicycle
    }
}
