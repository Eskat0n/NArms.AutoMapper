namespace NArms.AutoMapper.Tests
{
    using System;
    using global::AutoMapper;
    using NUnit.Framework;
    using Stubs;
    using Stubs.Profiles;

    [TestFixture]
    public class UnwrapExceptionTests
    {
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Mapper.AddProfile<StubProfile>();            
        }

        [SetUp]
        public void SetUp()
        {
            MapExtensions.Configuration.UnwrapExceptions = true;
        }

        [TearDown]
        public void TearDown()
        {
            MapExtensions.Configuration.UnwrapExceptions = false;
        }

        [Test]
        public void ShouldUnwrapExceptionGeneratedByUserCode()
        {
            Assert.Throws<NotSupportedException>(() => new SourceObject().MapTo<ExceptionObject>());
        }
        
        [Test]
        public void ShouldNotUnwrapExceptionGeneratedByAutoMapper()
        {
            Assert.Throws<AutoMapperMappingException>(() => new DestinationObject().MapTo<SourceObject>());
        }

        [Test]
        public void ShouldNotUnwrapExceptionIfNotOrderedTo()
        {
            MapExtensions.Configuration.UnwrapExceptions = false;
            Assert.Throws<AutoMapperMappingException>(() => new SourceObject().MapTo<ExceptionObject>());
        }
    }
}