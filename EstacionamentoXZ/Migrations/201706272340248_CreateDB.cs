namespace EstacionamentoXZ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carros",
                c => new
                    {
                        CarroId = c.Int(nullable: false, identity: true),
                        ModeloCarro = c.String(nullable: false),
                        PlacaCarro = c.String(maxLength: 8),
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
                        NomeCliente = c.String(nullable: false, maxLength: 70),
                        Cpf = c.String(maxLength: 11),
                        DataDeNascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Movimentacoes",
                c => new
                    {
                        MovimentacaoId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        DataDeEntrada = c.DateTime(nullable: false),
                        DataDeSaida = c.DateTime(nullable: false),
                        Carro_CarroId = c.Int(),
                        Cliente_ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.MovimentacaoId)
                .ForeignKey("dbo.Carros", t => t.Carro_CarroId)
                .ForeignKey("dbo.Clientes", t => t.Cliente_ClienteId)
                .Index(t => t.Carro_CarroId)
                .Index(t => t.Cliente_ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movimentacoes", "Cliente_ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.Movimentacoes", "Carro_CarroId", "dbo.Carros");
            DropForeignKey("dbo.Carros", "Cliente_ClienteId", "dbo.Clientes");
            DropIndex("dbo.Movimentacoes", new[] { "Cliente_ClienteId" });
            DropIndex("dbo.Movimentacoes", new[] { "Carro_CarroId" });
            DropIndex("dbo.Carros", new[] { "Cliente_ClienteId" });
            DropTable("dbo.Movimentacoes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Carros");
        }
    }
}
