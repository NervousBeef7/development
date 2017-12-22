using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Context: DbContext
    {
        public DbSet<SearchResult> Results { get; set; }
        public DbSet<Query> Quieries { get; set; }

        public Context() : base("images_DB")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Context>());
        }
    }
}
