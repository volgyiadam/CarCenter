using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCenter.Models
{
    public class NHibernateSession
    {

        public static ISession OpenSession()
        {
            var configuration = new Configuration();
            var configurationPath = HttpContext.Current.Server.MapPath(@"~\Models\hibernate.cfg.xml");
            configuration.Configure(configurationPath);
            var carConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Car.hbm.xml");
            configuration.AddFile(carConfigurationFile);
            var siteConfigurationFile = HttpContext.Current.Server.MapPath(@"~\Models\Site.hbm.xml");
            configuration.AddFile(siteConfigurationFile);

            ISessionFactory sessionFactory = configuration.BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}