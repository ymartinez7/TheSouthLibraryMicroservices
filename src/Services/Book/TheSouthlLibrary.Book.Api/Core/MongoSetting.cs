using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSouthlLibrary.Book.Api.Core.Entities
{
    public class MongoSetting
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
