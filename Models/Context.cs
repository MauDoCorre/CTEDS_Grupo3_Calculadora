using Microsoft.EntityFrameworkCore;
using System;

namespace Calculadora.Models;

public class Context : DbContext //essa referencia faz com q contex tenha tudo da DbContext (algo completo de fora)
{
	public Context(DbContextOptions<Context> options) : base(options)
	{
		Database.EnsureCreated(); //garantir q o banco de dados esteja criado
								  //vai criar a tabela e banco caso eles não existam
	}

	public DbSet<Operation> Operations { get; set; }


	//sobrescrever um modelo padrão --> vai preencher o banco de dados com coisas padrão
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Operation>().HasData(GetOperations()); //hasData --> inserir dados
															   //chamou o GetProducts q é um método q vai preencher a tabela
		base.OnModelCreating(modelBuilder);
	}

	private static Operation[] GetOperations()
	{
		return new Operation[]
		{
			new Operation
			{
				FullOperation = "5 + 5 = 10",
				Time = "20:15",
			},
			new Operation
			{
				FullOperation = "5 * 8 = 40",
				Time = "16:15",
			},
		};
	}
}

