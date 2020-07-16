using AutoMapper;
using ServerSideApp.Application.Mapping;
using ServerSideApp.Infrastructure.Persistence;
using System;

namespace UnitTests
{
    /// <summary>
    /// Base tests fixture.
    /// </summary>
    public class BaseTestsFixture : IDisposable
    {
        /// <summary>
        /// Context of sample database.
        /// </summary>
        public ApplicationDbContext Context { get; }

        /// <summary>
        /// AutoMapper for DTO and main entities.
        /// </summary>
        public IMapper Mapper { get; }

        /// <summary>
        /// Define base tests fixture.
        /// </summary>
        public BaseTestsFixture()
        {
            Context = ApplicationContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SaleProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        /// <summary>
        /// Разрушить контекст.
        /// </summary>
        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }
}