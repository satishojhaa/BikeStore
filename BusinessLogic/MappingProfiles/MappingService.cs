using System.Collections.Generic;

namespace BusinessLogic.MappingProfiles
{
    public interface IMappingService
    {
        TDest Map<TSrc, TDest>(TSrc source);
        void AssertConfigurationIsValid();
        IList<TDest> Map<TSrc, TDest>(IList<TSrc> source);
    }
    public class MappingService : IMappingService
    {
        public void AssertConfigurationIsValid()
        {
            AutoMapper.Mapper.AssertConfigurationIsValid();
        }

        public IList<TDest> Map<TSrc, TDest>(IList<TSrc> source)
        {
            return AutoMapper.Mapper.Map<IList<TSrc>, IList<TDest>>(source);
        }

        public TDest Map<TSrc, TDest>(TSrc source)
        {
            return AutoMapper.Mapper.Map<TSrc, TDest>(source);
        }

        public MappingService()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BikeMappingProfile>();
                cfg.AddProfile<BrandMappingProfile>();
                cfg.AddProfile<CategoryMappingProfile>();
            });
        }
    }
}
