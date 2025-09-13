namespace LeafByte.Parking.API.Models;

public enum UserRole
{
    Admin,
    User
}

public enum Status
{
    Active,
    Inactive,
    Deleted 
}

public enum AccessType
{
    Person,
    Car,
    Motorcycle
}

public enum DeviceType
{
    RfidReader,
    Camera,
    MotionSensor,
    GateCrossbar
}
