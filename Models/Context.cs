using Microsoft.EntityFrameworkCore;
using System;

namespace Calculadora.Models;

public class Context : DbContext 
{
	public Context(DbContextOptions<Context> options) : base(options)
	{
		Database.EnsureCreated(); 
	}

	public DbSet<Operation> Operations { get; set; }



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Operation>().HasData(GetOperations()); 
		base.OnModelCreating(modelBuilder);
	}

	private static Operation[] GetOperations()
	{
		return new Operation[]
		{
			new Operation
			{
				FullOperation = "5 + 5 = 10",
				Time = "28/09/2022 21:10:06"
			},
			new Operation
			{
				FullOperation = "5 * 8 = 40",
				Time = "05/09/2022 05:50:06"
			},
		};
	}
}

