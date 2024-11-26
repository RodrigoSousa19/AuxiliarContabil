using Microsoft.EntityFrameworkCore;

namespace AuxiliarContabil.Infrastructure.Context;

public class ModelConfiguration
{
    public static void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComposicaoSalario>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ComposicaoSalario");

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
        });

        modelBuilder.Entity<DAS>(entity =>
        {
            entity.HasKey(e => e.Faixa).HasName("PK__DAS__9AB3E305C48C62E0");

            entity.ToTable("DAS");

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
            entity.HasNoKey();

            entity.Property(e => e.Nome).HasMaxLength(255);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<ScheduledService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3214EC075142CD7F");

            entity.Property(e => e.ClassName).HasMaxLength(255);
            entity.Property(e => e.CronExpression).HasMaxLength(255);
            entity.Property(e => e.ServiceName).HasMaxLength(255);
        });

        modelBuilder.Entity<ServiceExecutionLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__ServiceE__5E548648E75C0989");

            entity.ToTable("ServiceExecutionLog");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceExecutionLogs)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceEx__Servi__403A8C7D");
        });
    }
}