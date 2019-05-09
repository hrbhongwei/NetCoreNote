using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote
{
    public class NoteContext:IdentityDbContext<NoteUser>
    {
        public NoteContext(DbContextOptions<NoteContext> options) :base(options)
        {

        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteType> NoteTypes { get; set; }
    }
}
