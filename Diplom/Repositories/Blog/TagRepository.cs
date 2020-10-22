using Diplom.Interfaces;
using Diplom.Models.DataModel.Blog;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Repositories.Blog
{
    public class TagRepository : GenericRepository<Tag>
    {
        public TagRepository(IConfiguration configuration) : base(configuration) { }
    }
}
