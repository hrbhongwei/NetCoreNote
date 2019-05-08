using NetNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Services
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(Guid id);
        Task<List<Note>> ListAsync();
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
        Tuple<List<Note>, int> PageList(int pageindex, int pagesize);
    }
}
