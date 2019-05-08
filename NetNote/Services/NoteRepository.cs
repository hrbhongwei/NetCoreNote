using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetNote.Models;

namespace NetNote.Services
{
    public class NoteRepository : INoteRepository
    {
        private NoteContext _context;
        public NoteRepository(NoteContext context)
        {
            _context = context;
        }
        public Task AddAsync(Note note)
        {
            _context.Notes.Add(note);
            return _context.SaveChangesAsync();
        }

        public Task<Note> GetByIdAsync(Guid id)
        {
            return _context.Notes.Include(type=>type.Type).FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<Note>> ListAsync()
        {
            return _context.Notes.Include(type=>type.Type).ToListAsync();
        }

        public Tuple<List<Note>, int> PageList(int pageindex, int pagesize)
        {
            var query = _context.Notes.Include(type => type.Type).AsQueryable();
            var count = query.Count();
            var pagecount = count % pagesize == 0 ? count / pagesize : count / pagesize + 1;
            var notes = query.OrderByDescending(r => r.Create)
                .Skip((pageindex - 1) * pagesize)
                .Take(pagesize)
                .ToList();
            return new Tuple<List<Note>, int>(notes, count);
        }

        public Task UpdateAsync(Note note)
        {
            _context.Entry(note).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
