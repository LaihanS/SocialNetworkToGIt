using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Common;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<Comment> Comentarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<User_FriendTable> Amigos { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.created = DateTime.Now;
                        item.Entity.createdBy = "Laihusmanguplus";
                        break;

                    case EntityState.Modified:
                        item.Entity.modifiedAt = DateTime.Now;
                        item.Entity.modifiedBy = "Laihusmanguplus";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Nombres(tablas)
            modelBuilder.Entity<Comment>().ToTable("Comentarios");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Publicacion>().ToTable("Publicaciones");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Comment>().HasKey(comment => comment.id);
            modelBuilder.Entity<Publicacion>().HasKey(post => post.id);
            modelBuilder.Entity<Usuario>().HasKey(usuario => usuario.id);
            #endregion

            #region Relaciones

            modelBuilder.Entity<Publicacion>().HasMany<Comment>(post => post.Comentarios).
            WithOne(comment => comment.Publicacion).
            HasForeignKey(comment => comment.PostId).
            OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>().HasMany<Comment>(user => user.Comentarios).
            WithOne(comment => comment.Usuario).
            HasForeignKey(comment => comment.UserId).
            OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>().HasMany<Publicacion>(user => user.Publicaciones).
           WithOne(post => post.Usuario).
           HasForeignKey(comment => comment.UserID).
           OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>().HasMany<User_FriendTable>(user => user.Amigos).
           WithOne(friend => friend.Usuario).
          HasForeignKey(friend => friend.idAmigo).
          OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Config

            #region Post
            modelBuilder.Entity<Publicacion>().Property(post => post.id).IsRequired().HasMaxLength(150);
            #endregion

            #region Usuario
            modelBuilder.Entity<Usuario>().Property(Usuario => Usuario.id).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(Usuario => Usuario.Nombre).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(Usuario => Usuario.Apellido).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(Usuario => Usuario.Correo).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(Usuario => Usuario.UserName).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Usuario>().Property(Usuario => Usuario.Telefono).IsRequired().HasMaxLength(150);

            #endregion


            #region Comment
            modelBuilder.Entity<Comment>().Property(comment => comment.id).IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Comment>().Property(comment => comment.text).IsRequired().HasMaxLength(150);

            #endregion

            #endregion
        }
    }
}
