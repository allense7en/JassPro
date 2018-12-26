using JassPro.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JassPro.DAL.Extension;

namespace JassPro.DAL
{
    public class BaseContext : DbContext
    {
        public BaseContext() :
            base("connString")
        {

        }
    }
}
