using Couchbase;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CouchbasePortTester
{
    class Program
    {
     
        static void Main(string[] args)
        {

            Write.Doc d = new Write.Doc();
            d.Create();
       
        }
    }
}
