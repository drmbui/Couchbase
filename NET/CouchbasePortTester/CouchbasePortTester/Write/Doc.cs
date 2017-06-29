using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Configuration.Client;


namespace CouchbasePortTester.Write
{
    public class Doc
    {
        private static readonly Cluster cluster = new Cluster();
        public void Create()
        {

            var config = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri("http://vcsmc2rqaalnd04.ual.com:8091"),
                    new Uri("http://vcsmc2rqaalnd05.ual.com:8091"),
                    new Uri("http://vcsmc2rqaalnd06.ual.com:8091"),
                    new Uri("http://vcsmc2rqaalnd07.ual.com:8091")
                }
            };

            var cluster = new Cluster(config);
            var bucket = cluster.OpenBucket("minhTest", "123456");

            //using (var bucket = cluster.OpenBucket("minhTest", "123456"))
            // {
            var document = new Document<dynamic>
            {

                Id = "testKey",
                Content = new
                {
                    name = "Minh Bui test insert document"
                }
            };


            var upsert = bucket.Insert(document);
            if (upsert.Success)
            {
                var get = bucket.GetDocument<dynamic>(document.Id);
                document = get.Document;
                var msg = string.Format("{0} {1}!", document.Id, document.Content.name);
                Console.WriteLine(msg);

            };

            cluster.Dispose();
            bucket.Dispose();

        }

  
    }
}
