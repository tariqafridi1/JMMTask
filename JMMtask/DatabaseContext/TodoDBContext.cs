using JMMtask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JMMtask.DatabaseContext
{
    public class TodoDBContext:IdentityDbContext
    {
        
        public TodoDBContext(DbContextOptions<TodoDBContext> Options) : base(Options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
    }
}

