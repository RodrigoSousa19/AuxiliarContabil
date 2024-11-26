using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.Infrastructure.Context;

public class ModelConfiguration
{
    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComposicaoSalario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ComposicaoSalario");
                
                entity.ToTable("ComposicaoSalario");

            entity.HasIndex(e => new { e.FimPeriodo, e.InicioPeriodo },
                "IX_ComposicaoSalario_FimPeriodo_InicioPeriodo");

            entity.Property(e => e.Das).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.FimPeriodo).HasColumnType("datetime");
            entity.Property(e => e.Gps).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.InicioPeriodo).HasColumnType("datetime");
            entity.Property(e => e.MensalidadeContabilidade).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.ProLabore).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.SalarioBruto).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.SalarioDia).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.SalarioHora).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.ComposicaoAtual).HasColumnType("bit");
        });

        modelBuilder.Entity<DAS>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DAS");

            entity.ToTable("DAS");
            
            entity.Property(e => e.Faixa).HasColumnType("varchar(50)");
            entity.Property(e => e.Aliquota).HasColumnType("numeric(18, 9)");
            entity.Property(e => e.ReceitaBrutaAnual).HasColumnType("numeric(18, 9)");
        });

        modelBuilder.Entity<ExtratoBancarioPessoaJuridica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExtratoB__3214EC07C55C0E52");

            entity.ToTable("ExtratoBancarioPessoaJuridica");

            entity.Property(e => e.NomeBanco).HasMaxLength(255);
            entity.Property(e => e.TipoTransacao).HasMaxLength(50);
            entity.Property(e => e.ValorTransacao).HasColumnType("numeric(18, 9)");
        });

        modelBuilder.Entity<Feriado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Feriado");

            entity.Property(e => e.Nome).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });
    }
}