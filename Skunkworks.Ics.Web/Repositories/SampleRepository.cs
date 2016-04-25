using Skunkworks.Ics.Web.Interfaces;
using Skunkworks.Ics.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skunkworks.Ics.Web.Repositories
{
    public class SampleRepository<T> : IRepository<T> where T : SampleModel
    {
        public bool Insert(T model)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}