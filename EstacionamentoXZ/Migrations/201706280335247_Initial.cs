namespace EstacionamentoXZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carros",
                c => new
                    {
                        CarroId = c.Int(nullable: false, identity: true),
                        Modelo = c.String(nullable: false),
                        Placa = c.String(nullable: false),
                        EstaEstacionado = c.Boolean(nullable: false),
                        Cliente_ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.CarroId)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ClienteId)
                .Index(t => t.Cliente_ClienteId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 70),
                        Cpf = c.String(maxLength: 11),
                        DataDeNascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Estadias",
                c => new
                    {
                        EstadiaId = c.Int(nullable: false, identity: true),
                        Entrada = c.DateTime(nullable: false),
                        Saida = c.DateTime(nullable: false),
                        Carro_CarroId = c.Int(),
                    })
                .PrimaryKey(t => t.EstadiaId)
                .ForeignKey("dbo.Carros", t => t.Carro_CarroId)
                .Index(t => t.Carro_CarroId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Estadias", "Carro_CarroId", "dbo.Carros");
            DropForeignKey("dbo.Carros", "Cliente_ClienteId", "dbo.Clientes");
            DropIndex("dbo.Estadias", new[] { "Carro_CarroId" });
            DropIndex("dbo.Carros", new[] { "Cliente_ClienteId" });
            DropTable("dbo.Estadias");
            DropTable("dbo.Clientes");
            DropTable("dbo.Carros");
        }
    }
}
