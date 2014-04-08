namespace NArms.AutoMapper.Tests.Stubs.Profiles
{
    using System;
    using global::AutoMapper;

    public class StubProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<SourceObject, DestinationObject>()
                .ForMember(x => x.Property, x => x.MapFrom(z => z.Property));

            CreateMap<SourceObject, ExceptionObject>()
                .AfterMap((so, eo) =>
                {
                    throw new NotSupportedException();
                });
        }
    }
}