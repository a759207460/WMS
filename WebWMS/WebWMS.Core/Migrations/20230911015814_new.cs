using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebWMS.Core.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    asn_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    asn_status = table.Column<byte>(type: "tinyint", nullable: false),
                    spu_id = table.Column<int>(type: "int", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    asn_qty = table.Column<int>(type: "int", nullable: false),
                    actual_qty = table.Column<int>(type: "int", nullable: false),
                    sorted_qty = table.Column<int>(type: "int", nullable: false),
                    shortage_qty = table.Column<int>(type: "int", nullable: false),
                    more_qty = table.Column<int>(type: "int", nullable: false),
                    damage_qty = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    supplier_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    goods_owner_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asnsort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    asn_id = table.Column<int>(type: "int", nullable: false),
                    sorted_qty = table.Column<int>(type: "int", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asnsort", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MoblePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRemove = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispatchlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dispatch_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dispatch_status = table.Column<byte>(type: "tinyint", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    damage_qty = table.Column<int>(type: "int", nullable: false),
                    lock_qty = table.Column<int>(type: "int", nullable: false),
                    picked_qty = table.Column<int>(type: "int", nullable: false),
                    intrasit_qty = table.Column<int>(type: "int", nullable: false),
                    package_qty = table.Column<int>(type: "int", nullable: false),
                    weighing_qty = table.Column<int>(type: "int", nullable: false),
                    actual_qty = table.Column<int>(type: "int", nullable: false),
                    sign_qty = table.Column<int>(type: "int", nullable: false),
                    package_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    package_person = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    package_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    weighing_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weighing_person = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weighing_weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    waybill_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    carrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    freightfee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatchlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Freightfees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    carrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departure_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arrival_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price_per_weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price_per_volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    min_payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Freightfees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goodslocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    warehouse_id = table.Column<int>(type: "int", nullable: false),
                    warehouse_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    warehouse_area_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    warehouse_area_property = table.Column<byte>(type: "tinyint", nullable: false),
                    location_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location_length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    location_width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    location_heigth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    location_volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    location_load = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    roadway_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shelf_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    layer_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tag_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    warehouse_area_id = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goodslocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goodsowners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    goods_owner_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goodsowners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NavigateController = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NavigateActioin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasChildren = table.Column<bool>(type: "bit", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadStyle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rolemenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userrole_id = table.Column<int>(type: "int", nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    authority = table.Column<byte>(type: "tinyint", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rolemenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    spu_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    spu_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    spu_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bar_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    supplier_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    length_unit = table.Column<byte>(type: "tinyint", nullable: false),
                    volume_unit = table.Column<byte>(type: "tinyint", nullable: false),
                    weight_unit = table.Column<byte>(type: "tinyint", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stockadjusts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    goods_location_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    is_update_stock = table.Column<bool>(type: "bit", nullable: false),
                    job_type = table.Column<byte>(type: "tinyint", nullable: false),
                    source_table_id = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockadjusts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stockfreezes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_type = table.Column<bool>(type: "bit", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    goods_location_id = table.Column<int>(type: "int", nullable: false),
                    handler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockfreezes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stockmoves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    move_status = table.Column<byte>(type: "tinyint", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    orig_goods_location_id = table.Column<int>(type: "int", nullable: false),
                    dest_googs_location_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    handler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    handle_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockmoves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stockprocesss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_type = table.Column<bool>(type: "bit", nullable: false),
                    process_status = table.Column<bool>(type: "bit", nullable: false),
                    processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    process_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockprocesss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    goods_location_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    is_freeze = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocktakings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    job_status = table.Column<bool>(type: "bit", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    goods_location_id = table.Column<int>(type: "int", nullable: false),
                    book_qty = table.Column<int>(type: "int", nullable: false),
                    counted_qty = table.Column<int>(type: "int", nullable: false),
                    difference_qty = table.Column<int>(type: "int", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    handler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    handle_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocktakings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouseareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    warehouse_id = table.Column<int>(type: "int", nullable: false),
                    area_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    area_property = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouseareas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    warehouse_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    manager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact_tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_valid = table.Column<bool>(type: "bit", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dispatchpicklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    goods_location_id = table.Column<int>(type: "int", nullable: false),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    pick_qty = table.Column<int>(type: "int", nullable: false),
                    is_update_stock = table.Column<bool>(type: "bit", nullable: false),
                    dispatchlistId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dispatchpicklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dispatchpicklists_Dispatchlists_dispatchlistId",
                        column: x => x.dispatchlistId,
                        principalTable: "Dispatchlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpuId = table.Column<int>(type: "int", nullable: false),
                    sku_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sku_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    lenght = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skus_Spus_SpuId",
                        column: x => x.SpuId,
                        principalTable: "Spus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stockprocessdetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sku_id = table.Column<int>(type: "int", nullable: false),
                    goods_owner_id = table.Column<int>(type: "int", nullable: false),
                    goods_location_id = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    tenant_id = table.Column<long>(type: "bigint", nullable: false),
                    is_source = table.Column<bool>(type: "bit", nullable: false),
                    is_update_stock = table.Column<bool>(type: "bit", nullable: false),
                    StockprocessId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stockprocessdetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stockprocessdetails_Stockprocesss_StockprocessId",
                        column: x => x.StockprocessId,
                        principalTable: "Stockprocesss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dispatchpicklists_dispatchlistId",
                table: "Dispatchpicklists",
                column: "dispatchlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Skus_SpuId",
                table: "Skus",
                column: "SpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Stockprocessdetails_StockprocessId",
                table: "Stockprocessdetails",
                column: "StockprocessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asns");

            migrationBuilder.DropTable(
                name: "Asnsort");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Dispatchpicklists");

            migrationBuilder.DropTable(
                name: "Freightfees");

            migrationBuilder.DropTable(
                name: "Goodslocations");

            migrationBuilder.DropTable(
                name: "Goodsowners");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Rolemenus");

            migrationBuilder.DropTable(
                name: "Skus");

            migrationBuilder.DropTable(
                name: "Stockadjusts");

            migrationBuilder.DropTable(
                name: "Stockfreezes");

            migrationBuilder.DropTable(
                name: "Stockmoves");

            migrationBuilder.DropTable(
                name: "Stockprocessdetails");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Stocktakings");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "Warehouseareas");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Dispatchlists");

            migrationBuilder.DropTable(
                name: "Spus");

            migrationBuilder.DropTable(
                name: "Stockprocesss");
        }
    }
}
