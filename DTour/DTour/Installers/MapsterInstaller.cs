namespace DTour.Installers;

public class MapsterInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        TypeAdapterConfig<CustomRole, RoleUserDto>.NewConfig()
            .Map(des => des.Users, src => src.UserRoles!.Count);
        TypeAdapterConfig<SelectListDto, RoleUserDto>.NewConfig()
            .Map(des => des.id, src => src.id)
            .Map(des => des.text, src => src.text);
        TypeAdapterConfig<CustomRole, RoleUserDto>.NewConfig()
            .Map(des => des.id, src => src.Id);
        TypeAdapterConfig<StoreBooking, StoreBookingDto>.NewConfig()
            .Map(des => des.TranDate, src => src.CreatedOn);
    }
}