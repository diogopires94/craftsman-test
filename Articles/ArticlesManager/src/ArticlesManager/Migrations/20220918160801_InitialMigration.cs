using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticlesManager.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "collections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "families",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_families", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    filter = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promotions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true),
                    permission = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sub_families",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sub_families", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "urls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url_value = table.Column<string>(type: "text", nullable: true),
                    page_title = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    meta_name = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_urls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    internal_reference = table.Column<string>(type: "text", nullable: true),
                    sku = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: true),
                    price_with_promotion = table.Column<double>(type: "double precision", nullable: true),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: true),
                    family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sub_family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: true),
                    generic1 = table.Column<string>(type: "text", nullable: true),
                    row_number = table.Column<string>(type: "text", nullable: true),
                    main_article_image_url = table.Column<string>(type: "text", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    meta_name = table.Column<string>(type: "text", nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    is_low_stock = table.Column<bool>(type: "boolean", nullable: false),
                    is_out_of_stock = table.Column<bool>(type: "boolean", nullable: false),
                    is_published = table.Column<bool>(type: "boolean", nullable: false),
                    is_outlet = table.Column<bool>(type: "boolean", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_articles", x => x.id);
                    table.ForeignKey(
                        name: "fk_articles_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_articles_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_articles_families_family_id",
                        column: x => x.family_id,
                        principalTable: "families",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_articles_sub_families_sub_family_id",
                        column: x => x.sub_family_id,
                        principalTable: "sub_families",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "size_tables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sub_family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_size_tables", x => x.id);
                    table.ForeignKey(
                        name: "fk_size_tables_families_family_id",
                        column: x => x.family_id,
                        principalTable: "families",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_size_tables_sub_families_sub_family_id",
                        column: x => x.sub_family_id,
                        principalTable: "sub_families",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "url_filters",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url_id = table.Column<Guid>(type: "uuid", nullable: true),
                    family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sub_family_id = table.Column<Guid>(type: "uuid", nullable: true),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: true),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_url_filters", x => x.id);
                    table.ForeignKey(
                        name: "fk_url_filters_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_url_filters_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_url_filters_families_family_id",
                        column: x => x.family_id,
                        principalTable: "families",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_url_filters_sub_families_sub_family_id",
                        column: x => x.sub_family_id,
                        principalTable: "sub_families",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_url_filters_urls_url_id",
                        column: x => x.url_id,
                        principalTable: "urls",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "article_images",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_id = table.Column<Guid>(type: "uuid", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_images_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "article_promotions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_id = table.Column<Guid>(type: "uuid", nullable: true),
                    discount = table.Column<int>(type: "integer", nullable: false),
                    promotion_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_article_promotions", x => x.id);
                    table.ForeignKey(
                        name: "fk_article_promotions_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_article_promotions_promotions_promotion_id",
                        column: x => x.promotion_id,
                        principalTable: "promotions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "barcodes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    barcode_value = table.Column<string>(type: "text", nullable: true),
                    article_id = table.Column<Guid>(type: "uuid", nullable: true),
                    size = table.Column<string>(type: "text", nullable: true),
                    size_description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: true),
                    color_code = table.Column<string>(type: "text", nullable: true),
                    color_description = table.Column<string>(type: "text", nullable: true),
                    stock_quantity = table.Column<int>(type: "integer", nullable: true),
                    reserved_quantity = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_barcodes", x => x.id);
                    table.ForeignKey(
                        name: "fk_barcodes_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "home_page_highlights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_id = table.Column<Guid>(type: "uuid", nullable: true),
                    brand_id = table.Column<Guid>(type: "uuid", nullable: true),
                    collection_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    order = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_home_page_highlights", x => x.id);
                    table.ForeignKey(
                        name: "fk_home_page_highlights_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_home_page_highlights_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_home_page_highlights_collections_collection_id",
                        column: x => x.collection_id,
                        principalTable: "collections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_charts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    article_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_charts", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_charts_articles_article_id",
                        column: x => x.article_id,
                        principalTable: "articles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "size_table_lines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    size_table_id = table.Column<Guid>(type: "uuid", nullable: true),
                    eu = table.Column<string>(type: "text", nullable: true),
                    us = table.Column<string>(type: "text", nullable: true),
                    uk = table.Column<string>(type: "text", nullable: true),
                    cm = table.Column<string>(type: "text", nullable: true),
                    created_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_size_table_lines", x => x.id);
                    table.ForeignKey(
                        name: "fk_size_table_lines_size_tables_size_table_id",
                        column: x => x.size_table_id,
                        principalTable: "size_tables",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_article_images_article_id",
                table: "article_images",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_article_promotions_article_id",
                table: "article_promotions",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_article_promotions_promotion_id",
                table: "article_promotions",
                column: "promotion_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_brand_id",
                table: "articles",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_collection_id",
                table: "articles",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_family_id",
                table: "articles",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "ix_articles_sub_family_id",
                table: "articles",
                column: "sub_family_id");

            migrationBuilder.CreateIndex(
                name: "ix_barcodes_article_id",
                table: "barcodes",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_home_page_highlights_article_id",
                table: "home_page_highlights",
                column: "article_id");

            migrationBuilder.CreateIndex(
                name: "ix_home_page_highlights_brand_id",
                table: "home_page_highlights",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_home_page_highlights_collection_id",
                table: "home_page_highlights",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_size_table_lines_size_table_id",
                table: "size_table_lines",
                column: "size_table_id");

            migrationBuilder.CreateIndex(
                name: "ix_size_tables_family_id",
                table: "size_tables",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "ix_size_tables_sub_family_id",
                table: "size_tables",
                column: "sub_family_id");

            migrationBuilder.CreateIndex(
                name: "ix_url_filters_brand_id",
                table: "url_filters",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_url_filters_collection_id",
                table: "url_filters",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "ix_url_filters_family_id",
                table: "url_filters",
                column: "family_id");

            migrationBuilder.CreateIndex(
                name: "ix_url_filters_sub_family_id",
                table: "url_filters",
                column: "sub_family_id");

            migrationBuilder.CreateIndex(
                name: "ix_url_filters_url_id",
                table: "url_filters",
                column: "url_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_charts_article_id",
                table: "user_charts",
                column: "article_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_images");

            migrationBuilder.DropTable(
                name: "article_promotions");

            migrationBuilder.DropTable(
                name: "barcodes");

            migrationBuilder.DropTable(
                name: "home_page_highlights");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "size_table_lines");

            migrationBuilder.DropTable(
                name: "url_filters");

            migrationBuilder.DropTable(
                name: "user_charts");

            migrationBuilder.DropTable(
                name: "promotions");

            migrationBuilder.DropTable(
                name: "size_tables");

            migrationBuilder.DropTable(
                name: "urls");

            migrationBuilder.DropTable(
                name: "articles");

            migrationBuilder.DropTable(
                name: "brands");

            migrationBuilder.DropTable(
                name: "collections");

            migrationBuilder.DropTable(
                name: "families");

            migrationBuilder.DropTable(
                name: "sub_families");
        }
    }
}
