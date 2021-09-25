using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;

namespace TestIdentity.Identity.Mapping
{
    public static class MapperRegistration
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(Configure);
        }

        private static void Configure(IMapperConfigurationExpression obj)
        {
            var profiles = GetProfiles();

            foreach (var profile in profiles.Select(profile => (Profile)Activator.CreateInstance(profile)))
            {
                obj.AddProfile(profile);
            }
        }

        private static List<Type> GetProfiles()
        {
           return (from t in typeof(Startup).GetTypeInfo().Assembly.GetTypes()
                     where typeof(IAutoMapper).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract
                     select t).ToList();

        //    return (from t in typeof(Startup).GetTypeInfo().Assembly.GetTypes()
        //            where typeof(IAutomapper).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract
        //            select t).ToList();
        }
    }
}
