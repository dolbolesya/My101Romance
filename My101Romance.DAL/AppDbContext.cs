﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My101Romance.Domain;
using My101Romance.Domain.Entity;

namespace My101Romance.DAL;

public sealed class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Card> Card { get; set; }
    
    public DbSet<User> User { get; set; }
}