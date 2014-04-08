namespace NArms.AutoMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::AutoMapper;

    public static class MapExtensions
    {
        public static IConfiguration Configuration { get; private set; }

        static MapExtensions()
        {
            Configuration = new Configuration();
        }

        public static object MapTo(this object source)
        {
            var typeMap = Mapper.GetAllTypeMaps()
                .SingleOrDefault(x => x.SourceType == source.GetType());

            if (typeMap == null)
                throw new InvalidOperationException(string.Format("There are two or more mappings for source type {0}", source.GetType()));

            return Execute(() => Mapper.Map(source, source.GetType(), typeMap.DestinationType));
        }

        public static TDest MapTo<TDest>(this object source)
        {
            return Execute(() => (TDest) Mapper.Map(source, source.GetType(), typeof (TDest)));
        }

        public static TDest MapTo<TSource, TDest>(this TSource source, TDest dest)
        {
            return Execute(() => Mapper.Map(source, dest));
        }

        public static IEnumerable<TDest> MapEachTo<TDest>(this IEnumerable<object> source)
        {
            return Execute(() => (IEnumerable<TDest>) Mapper.Map(source, source.GetType(), typeof (IEnumerable<TDest>)));
        }

        private static TReturn Execute<TReturn>(Func<TReturn> func)
        {
            try
            {
                return func();
            }
            catch (AutoMapperMappingException ex)
            {
                if (Configuration.UnwrapExceptions && ex.InnerException != null)
                    throw ex.InnerException;
                throw;
            }
        }
    }
}