namespace Garage_3._0.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public  VehicleType Type { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        //public enum VehicleTypes
        //{
        //    Car,
        //    Motorcycle,
        //    Truck,
        //    Bus,
        //    Van,
        //    Bicycle
        //}
    }
}
