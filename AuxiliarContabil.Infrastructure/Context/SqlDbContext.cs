using System;
using System.Collections.Generic;
using AuxiliarContabil.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.Infrastructure.Context;

public partial class SqlDbContext : DbContext
{
    public SqlDbContext()
    {
    }

    public SqlDbContext(DbContextOptions<SqlDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComposicaoSalario> ComposicaoSalarios { get; set; }

    public virtual DbSet<DAS> DAS { get; set; }

    public virtual DbSet<ExtratoBancarioPessoaJuridica> ExtratoBancarioPessoaJuridicas { get; set; }

    public virtual DbSet<Feriado> Feriados { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelConfiguration.ConfigureModel(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
