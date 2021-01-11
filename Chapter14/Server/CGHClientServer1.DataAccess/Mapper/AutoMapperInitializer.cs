using AutoMapper;

namespace CGHClientServer1.DataAccess.Mapper
{
    public static class AutoMapperInitializer
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add all Profiles from the Assembly containing this Type
                cfg.AddMaps(typeof(AutoMapperInitializer));
            });

            configuration.AssertConfigurationIsValid();
            IMapper mapper = configuration.CreateMapper();

            return mapper;
        }
    }
}