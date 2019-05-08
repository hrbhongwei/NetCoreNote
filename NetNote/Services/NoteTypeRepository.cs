using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetNote.Models;

namespace NetNote.Services
{
    public class NoteTypeRepository: INoteTypeRepository
    {
        private NoteContext _context;
        public NoteTypeRepository(NoteContext context)
        {
            _context = context;
        }

        public Task<List<NoteType>> ListAsync()
        {
            return _context.NoteTypes.ToListAsync();
        }
    }
}
