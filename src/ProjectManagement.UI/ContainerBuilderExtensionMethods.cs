using Autofac;
using AutoMapper;
using System;
using System.Linq;

namespace ProjectManagement.UI
{
    public static class ContainerBuilderExtensionMethods
    {
        /// <summary>
        /// Collects all classes of <see cref="AutoMapper"/>`s <see cref="Profile"/> from the whole solution and creates <see cref="AutoMapper"/>`s <see cref="IMapper"/>.
        /// </summary>
        /// <param name="builder"><see cref="Autofac"/>`s container builder.</param>
        /// <returns></returns>
        public static ContainerBuilder UseAutoMapper(this ContainerBuilder builder)
        {
            if (builder == null) throw new NullReferenceException();

            var autoMapperProfileTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a =>
                        a.GetTypes().Where(p =>
                            typeof(Profile).IsAssignableFrom(p) &&
                            p.IsPublic &&
                            !p.IsAbstract));

            var autoMapperProfiles =
                autoMapperProfileTypes
                    .Select(p => (Profile)Activator.CreateInstance(p));

            builder
                .Register(ctx => new MapperConfiguration(cfg =>
                {
                    foreach (var profile in autoMapperProfiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }));

            builder
                .Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>();

            return builder;
        }
    }
}
