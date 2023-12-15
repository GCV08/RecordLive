using Application.Interfaces;
using Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly RLContext _context;



        public UnitOfWork(RLContext context)
        {
            _context = context;
        }


        //DECLARAR REPOSITORIOS



        //FUNCIONES DEL SERVICIO
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
