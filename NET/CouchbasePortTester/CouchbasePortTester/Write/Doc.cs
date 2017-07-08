using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;
using System.Threading;

namespace CouchbasePortTester.Write
{
    public class Doc
    {
        private static readonly Cluster cluster = new Cluster();
        /// <summary>
        /// 
        /// </summary>
        public void Create()
        {

            var config = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri("http://vcsmc2rqbfpnd01.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd02.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd03.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd04.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd05.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd06.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd07.ual.com:8091"),
                    new Uri("http://vcsmc2rqbfpnd08.ual.com:8091")
                }
            };

            var cluster = new Cluster(config);
            var bucket = cluster.OpenBucket("minhTest");

            //using (var bucket = cluster.OpenBucket("minhTest", "123456"))
            // {
            for (int i = 0; i < 100; i++)
            {
                var document = new Document<dynamic>
                {

                    Id = "testKey" + Guid.NewGuid(),
                    Content = new
                    {
                        name = "Minh Bui test insert document"
                    }
                };

                try
                {
                    var upsert = bucket.Upsert(document);

                    if (upsert.Success)
                    {
                        Thread.Sleep(50);
                      
                            var get = bucket.GetDocument<dynamic>(document.Id);
                            document = get.Document;
                            var msg = string.Format("{0} {1}!", document.Id, document.Content.name);
                            msg = i.ToString() + " " + msg;
                            //Console.WriteLine(msg);

                    }
                    else
                    {
                        string ms = upsert.Message;
                        ms = i.ToString() + " message failed!";
                        Console.WriteLine(ms);
                        
                    }

                }
                catch (Exception e)
                {
                    string m = e.ToString();
                }

            }

            Console.ReadLine();

            cluster.Dispose();
            bucket.Dispose();

        }
    }
}
