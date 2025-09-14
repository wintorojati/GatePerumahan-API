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

public enum HouseStatus
{
    Filled,
    Empty
}

public enum DeviceType
{
    RfidReader,
    Camera,
    MotionSensor,
    GateCrossbar
}

public enum IdentityType
{
    KTP,
    SIM,
    Passport
}

public enum ResidentType
{
    Resident,
    NonResident
}

public enum Gender
{
    Male,
    Female,
    Other
}

public enum ActionType{
    Add,
    Edit,
    Delete,
}

public enum PhotoType
{
    Profile,
    EntryIn,
    EntryOut,
    Identity,
    Other
}
