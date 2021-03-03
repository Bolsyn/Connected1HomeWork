using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Connected1HomeWork.Service
{
    public static class ConfigService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Init()
        {
            if (DbProviderFactories.GetFactory("ConnectedLessonProvider") == null)
            {
                DbProviderFactories.RegisterFactory("ConnectedLessonProvider", SqlClientFactory.Instance);
            }

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();
            }
        }
    }
}
