[assembly: WebActivator.PreApplicationStartMethod(typeof(Leonni.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Leonni.App_Start.NinjectWebCommon), "Stop")]

namespace Leonni.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Data.Entity;
    using Leonni.Interfaces;

    using Ninject.Web.Mvc;
    using System.Web.Mvc;
    using Leonni.Repositories;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<LeonniDbContext>();

            kernel.Bind<IPageContentRepository>().To<PageContentRepository>();
            kernel.Bind<ILanguageRepository>().To<LanguageRepository>();
            kernel.Bind<ITranslationRepository>().To<TranslationRepository>();
            //kernel.Bind<IAspnetUserRepository>().To<AspnetUserRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IDisciplineRepository>().To<DisciplineRepository>();
            //kernel.Bind<ICountryRepository>().To<CountryRepository>();
            kernel.Bind<IYearRepository>().To<YearRepository>();
            kernel.Bind<IMonthRepository>().To<MonthRepository>();
            kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            kernel.Bind<IFileRepository>().To<FileRepository>();
            kernel.Bind<IGroupRepository>().To<GroupRepository>();
            kernel.Bind<IEntityFileRepository>().To<EntityFileRepository>();
            kernel.Bind<ICountryRepository>().To<CountryRepository>();
            kernel.Bind<IProvinceRepository>().To<ProvinceRepository>();
            kernel.Bind<IAdvertisementRepository>().To<AdvertisementRepository>();
            kernel.Bind<ISectionRepository>().To<SectionRepository>();
            kernel.Bind<IProjectRepository>().To<ProjectRepository>();
            kernel.Bind<IVideoLinkRepository>().To<VideoLinkRepository>();
            kernel.Bind<IFrontEntityFileRepository>().To<FrontEntityFileRepository>();
            kernel.Bind<IFrontContentRepository>().To<FrontContentRepository>();
            kernel.Bind<IMessageRepository>().To<MessageRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<ICommentThreadRepository>().To<CommentThreadRepository>();
            //kernel.Bind<IProjectRepository>().To<ProjectRepository>();
            kernel.Bind<IPublicationRepository>().To<PublicationRepository>();
            kernel.Bind<ILikeRepository>().To<LikeRepository>();
            kernel.Bind<IFavouriteRepository>().To<FavouriteRepository>();
            // Tell ASP.NET MVC 3 to use our Ninject DI Container 
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //ModelValidatorProviders.Providers.Clear();
            // ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new NinjectValidatorFactory(kernel)));
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }
    }
}
