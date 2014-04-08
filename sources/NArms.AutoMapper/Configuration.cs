namespace NArms.AutoMapper
{
    internal class Configuration : IConfiguration
    {
        public Configuration()
        {
            UnwrapExceptions = false;
        }

        public bool UnwrapExceptions { get; set; }
    }
}