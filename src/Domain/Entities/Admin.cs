namespace PastasAPI.Domain.Entities;

public class Admin : User
{
    public Admin()
    {
        Rol = PastasAPI.Domain.Enums.RolEnum.Admin;
    }
}