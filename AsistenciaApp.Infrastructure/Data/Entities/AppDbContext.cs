using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AsistenciaApp.Infrastructure.Data.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencium> Asistencia { get; set; }
    public virtual DbSet<Curso> Cursos { get; set; }
    public virtual DbSet<Docente> Docentes { get; set; }
    public virtual DbSet<Estudiante> Estudiantes { get; set; }
    public virtual DbSet<EstudianteTutor> EstudianteTutors { get; set; }
    public virtual DbSet<Grado> Grados { get; set; }
    public virtual DbSet<Notificacion> Notificacions { get; set; }
    public virtual DbSet<Prediccion> Prediccions { get; set; }
    public virtual DbSet<Seccion> Seccions { get; set; }
    public virtual DbSet<Tutor> Tutors { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia);
            entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");
            entity.Property(e => e.Estado).HasMaxLength(20).HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso);
            entity.ToTable("Curso");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.AnioEscolar).HasMaxLength(10).HasColumnName("anio_escolar");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdDocente).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdGrado).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdSeccion).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.IdDocente);
            entity.ToTable("Docente");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdUsuario).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante);
            entity.ToTable("Estudiante");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellido).HasMaxLength(100).HasColumnName("apellido");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.IdCurso).HasColumnName("id_curso");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdCurso).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<EstudianteTutor>(entity =>
        {
            entity.HasKey(e => e.IdEstudianteTutor);
            entity.ToTable("Estudiante_Tutor");
            entity.Property(e => e.IdEstudianteTutor).HasColumnName("id_EstudianteTutor");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");
            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.EstudianteTutors)
                .HasForeignKey(d => d.IdEstudiante).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.IdTutorNavigation).WithMany(p => p.EstudianteTutors)
                .HasForeignKey(d => d.IdTutor).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.IdGrado);
            entity.ToTable("Grado");
            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(50).HasColumnName("nombre");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion);
            entity.ToTable("Notificacion");
            entity.Property(e => e.IdNotificacion).HasColumnName("id_notificacion");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Estado).HasMaxLength(20).HasColumnName("estado");
            entity.Property(e => e.FechaEnvio).HasColumnName("fecha_envio");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");
            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdEstudiante).OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.IdTutorNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdTutor).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Prediccion>(entity =>
        {
            entity.HasKey(e => e.IdPrediccion);
            entity.ToTable("Prediccion");
            entity.Property(e => e.IdPrediccion).HasColumnName("id_prediccion");
            entity.Property(e => e.FechaPrediccion).HasColumnName("fecha_prediccion");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.ProbabilidadAbandono)
                .HasColumnType("numeric(5,2)")
                .HasColumnName("probabilidad_abandono");
            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Prediccions)
                .HasForeignKey(d => d.IdEstudiante).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.IdSeccion);
            entity.ToTable("Seccion");
            entity.Property(e => e.IdSeccion).HasColumnName("id_seccion");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.IdGrado).HasColumnName("id_grado");
            entity.Property(e => e.Nombre).HasMaxLength(10).HasColumnName("nombre");
            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Seccions)
                .HasForeignKey(d => d.IdGrado).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.IdTutor);
            entity.ToTable("Tutor");
            entity.Property(e => e.IdTutor).HasColumnName("id_tutor");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellido).HasMaxLength(100).HasColumnName("apellido");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Parentesco).HasMaxLength(50).HasColumnName("parentesco");
            entity.Property(e => e.TelefonoWhatsapp).HasMaxLength(20).HasColumnName("telefono_whatsapp");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);
            entity.ToTable("Usuario");
            entity.HasIndex(e => e.Correo).IsUnique();
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Contrasena).HasMaxLength(255).HasColumnName("contrasena");
            entity.Property(e => e.Correo).HasMaxLength(100).HasColumnName("correo");
            entity.Property(e => e.Nombre).HasMaxLength(100).HasColumnName("nombre");
            entity.Property(e => e.Rol).HasMaxLength(20).HasColumnName("rol");
        });

        modelBuilder.Entity<Usuario>().HasQueryFilter(e => e.Activo);
        modelBuilder.Entity<Grado>().HasQueryFilter(e => e.Activo);
        modelBuilder.Entity<Seccion>().HasQueryFilter(e => e.Activo);
        modelBuilder.Entity<Curso>().HasQueryFilter(e => e.Activo);
        modelBuilder.Entity<Estudiante>().HasQueryFilter(e => e.Activo);
        modelBuilder.Entity<Tutor>().HasQueryFilter(e => e.Activo);
        modelBuilder.Entity<Notificacion>().HasQueryFilter(e => e.Activo);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}