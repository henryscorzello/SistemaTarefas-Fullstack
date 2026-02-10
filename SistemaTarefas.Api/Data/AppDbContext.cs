using Microsoft.EntityFrameworkCore;
using SistemaTarefas.Shared;
using System.Collections.Generic;

namespace SistemaTarefas.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Isso aqui diz ao EF para criar uma tabela chamada "Tarefas" baseada no modelo Tarefa
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}