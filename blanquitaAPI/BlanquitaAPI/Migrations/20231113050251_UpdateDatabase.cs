using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlanquitaAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combo",
                columns: table => new
                {
                    IdCombo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Combo__D65BF2C8230EB487", x => x.IdCombo);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    IdPerfil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Perfil__C7BD5CC117BA96DC", x => x.IdPerfil);
                });

            migrationBuilder.CreateTable(
                name: "TipoProducto",
                columns: table => new
                {
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoProd__A974F9200A83A316", x => x.IdTipoProducto);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    IdPerfil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__5B65BF97F5C1BA4D", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK__Usuario__IdPerfi__440B1D61",
                        column: x => x.IdPerfil,
                        principalTable: "Perfil",
                        principalColumn: "IdPerfil");
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoProducto = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__098892104075B11C", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK__Producto__IdTipo__398D8EEE",
                        column: x => x.IdTipoProducto,
                        principalTable: "TipoProducto",
                        principalColumn: "IdTipoProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orden",
                columns: table => new
                {
                    IdOrden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden__C38F300D645A4506", x => x.IdOrden);
                    table.ForeignKey(
                        name: "FK__Orden__IdUsuario__46E78A0C",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateTable(
                name: "ProductoCombo",
                columns: table => new
                {
                    IdProductoCombo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdCombo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__87A1E832A9D2970E", x => x.IdProductoCombo);
                    table.ForeignKey(
                        name: "FK__ProductoC__IdCom__3E52440B",
                        column: x => x.IdCombo,
                        principalTable: "Combo",
                        principalColumn: "IdCombo");
                    table.ForeignKey(
                        name: "FK__ProductoC__IdPro__3F466844",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "OrdenCombo",
                columns: table => new
                {
                    IdOrdenCombo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrden = table.Column<int>(type: "int", nullable: false),
                    IdCombo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrdenCom__6BEFCB8B41CA470A", x => x.IdOrdenCombo);
                    table.ForeignKey(
                        name: "FK__OrdenComb__IdCom__4AB81AF0",
                        column: x => x.IdCombo,
                        principalTable: "Combo",
                        principalColumn: "IdCombo");
                    table.ForeignKey(
                        name: "FK__OrdenComb__IdOrd__49C3F6B7",
                        column: x => x.IdOrden,
                        principalTable: "Orden",
                        principalColumn: "IdOrden");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orden_IdUsuario",
                table: "Orden",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenCombo_IdCombo",
                table: "OrdenCombo",
                column: "IdCombo");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenCombo_IdOrden",
                table: "OrdenCombo",
                column: "IdOrden");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_IdTipoProducto",
                table: "Producto",
                column: "IdTipoProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCombo_IdCombo",
                table: "ProductoCombo",
                column: "IdCombo");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCombo_IdProducto",
                table: "ProductoCombo",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdPerfil",
                table: "Usuario",
                column: "IdPerfil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenCombo");

            migrationBuilder.DropTable(
                name: "ProductoCombo");

            migrationBuilder.DropTable(
                name: "Orden");

            migrationBuilder.DropTable(
                name: "Combo");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoProducto");

            migrationBuilder.DropTable(
                name: "Perfil");
        }
    }
}
