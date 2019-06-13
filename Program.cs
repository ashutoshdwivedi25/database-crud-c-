using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AEPLCore.DataAccess;
using AEPLCore.DataAccess.Contracts;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Database
{
    class Program
    {
        public readonly static ManualResetEvent Shutdown = new ManualResetEvent (false);
        static void Main (string[] args)
        {
            Startup startUp = new Startup ();
            ServiceProvider serviceProvider = startUp.InitialiseServices ();
            var connectionFactory = serviceProvider.GetService<ConnectionFactory<IOptions<ConnectionStringOption>>> ();
            var data = GetModel (connectionFactory);
            var data1 = GetModel1 (connectionFactory);
            foreach (var item in data1)
            {
                Console.WriteLine("{0}", item.place);
            }
         //   Shutdown.WaitOne ();
           // Console.WriteLine(data);
        }

        private static int GetModel (ConnectionFactory<IOptions<ConnectionStringOption>> connectionFactory)
        {
            string cmd = @"insert into training.Classxy(place,payscale)
                         VALUES('irute',56465);";
            using (var con = connectionFactory.GetMasterConnection ().Result)
            {
                return (con.Connection.Execute(cmd));
            }
        }
        private static List<Model> GetModel1 (ConnectionFactory<IOptions<ConnectionStringOption>> connectionFactory)
        {
            string cmd = @"select place from training.Classxy where classid = 6";
            using (var con = connectionFactory.GetReadOnlyConnection ().Result)
            {
                return (con.Connection.Query<Model> (cmd).ToList () ?? new List<Model> ());
            }
        }
    }
}