CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE TABLE brands (
    id uuid NOT NULL,
    code text UNIQUE,
    description text NULL,
    image_url text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_brands PRIMARY KEY (id)
);

CREATE TABLE collections (
    id uuid NOT NULL,
    code text UNIQUE,
    description text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_collections PRIMARY KEY (id)
);

CREATE TABLE families (
    id uuid NOT NULL,
    code text UNIQUE,
    description text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_families PRIMARY KEY (id)
);

CREATE TABLE promotions (
    id uuid NOT NULL,
    name text NULL,
    filter text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_promotions PRIMARY KEY (id)
);

CREATE TABLE role_permissions (
    id uuid NOT NULL,
    role text NULL,
    permission text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_role_permissions PRIMARY KEY (id)
);

CREATE TABLE sub_families (
    id uuid NOT NULL,
    code text UNIQUE,
    description text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_sub_families PRIMARY KEY (id)
);

CREATE TABLE urls (
    id uuid NOT NULL,
    url_value text NULL,
    page_title text NULL,
    meta_description text NULL,
    meta_name text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_urls PRIMARY KEY (id)
);

CREATE TABLE articles (
    id uuid NOT NULL,
    internal_reference text UNIQUE,
    sku text NULL,
    description text NULL,
    price double precision NULL,
    price_with_promotion double precision NULL,
    brand_id uuid NULL,
    family_id uuid NULL,
    sub_family_id uuid NULL,
    collection_id uuid NULL,
    generic1 text NULL,
    row_number text NULL,
    main_article_image_url text NULL,
    url text NULL,
    meta_name text NULL,
    meta_description text NULL,
    is_low_stock boolean NOT NULL,
    is_out_of_stock boolean NOT NULL,
    is_published boolean NOT NULL,
    is_outlet boolean NOT NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_articles PRIMARY KEY (id),
    CONSTRAINT fk_articles_brands_brand_id FOREIGN KEY (brand_id) REFERENCES brands (id),
    CONSTRAINT fk_articles_collections_collection_id FOREIGN KEY (collection_id) REFERENCES collections (id),
    CONSTRAINT fk_articles_families_family_id FOREIGN KEY (family_id) REFERENCES families (id),
    CONSTRAINT fk_articles_sub_families_sub_family_id FOREIGN KEY (sub_family_id) REFERENCES sub_families (id)
);

CREATE TABLE size_tables (
    id uuid NOT NULL,
    name text NULL,
    family_id uuid NULL,
    sub_family_id uuid NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_size_tables PRIMARY KEY (id),
    CONSTRAINT fk_size_tables_families_family_id FOREIGN KEY (family_id) REFERENCES families (id),
    CONSTRAINT fk_size_tables_sub_families_sub_family_id FOREIGN KEY (sub_family_id) REFERENCES sub_families (id)
);

CREATE TABLE url_filters (
    id uuid NOT NULL,
    url_id uuid NULL,
    family_id uuid NULL,
    sub_family_id uuid NULL,
    brand_id uuid NULL,
    collection_id uuid NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_url_filters PRIMARY KEY (id),
    CONSTRAINT fk_url_filters_brands_brand_id FOREIGN KEY (brand_id) REFERENCES brands (id),
    CONSTRAINT fk_url_filters_collections_collection_id FOREIGN KEY (collection_id) REFERENCES collections (id),
    CONSTRAINT fk_url_filters_families_family_id FOREIGN KEY (family_id) REFERENCES families (id),
    CONSTRAINT fk_url_filters_sub_families_sub_family_id FOREIGN KEY (sub_family_id) REFERENCES sub_families (id),
    CONSTRAINT fk_url_filters_urls_url_id FOREIGN KEY (url_id) REFERENCES urls (id)
);

CREATE TABLE article_images (
    id uuid NOT NULL,
    article_id uuid NULL,
    url text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_article_images PRIMARY KEY (id),
    CONSTRAINT fk_article_images_articles_article_id FOREIGN KEY (article_id) REFERENCES articles (id)
);

CREATE TABLE article_promotions (
    id uuid NOT NULL,
    article_id uuid NULL,
    discount integer NOT NULL,
    promotion_id uuid NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_article_promotions PRIMARY KEY (id),
    CONSTRAINT fk_article_promotions_articles_article_id FOREIGN KEY (article_id) REFERENCES articles (id),
    CONSTRAINT fk_article_promotions_promotions_promotion_id FOREIGN KEY (promotion_id) REFERENCES promotions (id)
);

CREATE TABLE barcodes (
    id uuid NOT NULL,
    barcode_value text UNIQUE,
    article_id uuid NULL,
    size text NULL,
    size_description text NULL,
    price double precision NULL,
    color_code text NULL,
    color_description text NULL,
    stock_quantity integer NULL,
    reserved_quantity integer NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_barcodes PRIMARY KEY (id),
    CONSTRAINT fk_barcodes_articles_article_id FOREIGN KEY (article_id) REFERENCES articles (id)
);

CREATE TABLE home_page_highlights (
    id uuid NOT NULL,
    article_id uuid NULL,
    brand_id uuid NULL,
    collection_id uuid NULL,
    name text NULL,
    "order" integer NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_home_page_highlights PRIMARY KEY (id),
    CONSTRAINT fk_home_page_highlights_articles_article_id FOREIGN KEY (article_id) REFERENCES articles (id),
    CONSTRAINT fk_home_page_highlights_brands_brand_id FOREIGN KEY (brand_id) REFERENCES brands (id),
    CONSTRAINT fk_home_page_highlights_collections_collection_id FOREIGN KEY (collection_id) REFERENCES collections (id)
);

CREATE TABLE user_charts (
    id uuid NOT NULL,
    user_id uuid NULL,
    article_id uuid NULL,
    quantity integer NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_user_charts PRIMARY KEY (id),
    CONSTRAINT fk_user_charts_articles_article_id FOREIGN KEY (article_id) REFERENCES articles (id)
);

CREATE TABLE size_table_lines (
    id uuid NOT NULL,
    size_table_id uuid NULL,
    eu text NULL,
    us text NULL,
    uk text NULL,
    cm text NULL,
    created_on timestamp with time zone NOT NULL,
    created_by text NULL,
    last_modified_on timestamp with time zone NULL,
    last_modified_by text NULL,
    is_deleted boolean NOT NULL,
    CONSTRAINT pk_size_table_lines PRIMARY KEY (id),
    CONSTRAINT fk_size_table_lines_size_tables_size_table_id FOREIGN KEY (size_table_id) REFERENCES size_tables (id)
);

CREATE INDEX ix_article_images_article_id ON article_images (article_id);

CREATE INDEX ix_article_promotions_article_id ON article_promotions (article_id);

CREATE INDEX ix_article_promotions_promotion_id ON article_promotions (promotion_id);

CREATE INDEX ix_articles_brand_id ON articles (brand_id);

CREATE INDEX ix_articles_collection_id ON articles (collection_id);

CREATE INDEX ix_articles_family_id ON articles (family_id);

CREATE INDEX ix_articles_sub_family_id ON articles (sub_family_id);

CREATE INDEX ix_barcodes_article_id ON barcodes (article_id);

CREATE INDEX ix_home_page_highlights_article_id ON home_page_highlights (article_id);

CREATE INDEX ix_home_page_highlights_brand_id ON home_page_highlights (brand_id);

CREATE INDEX ix_home_page_highlights_collection_id ON home_page_highlights (collection_id);

CREATE INDEX ix_size_table_lines_size_table_id ON size_table_lines (size_table_id);

CREATE INDEX ix_size_tables_family_id ON size_tables (family_id);

CREATE INDEX ix_size_tables_sub_family_id ON size_tables (sub_family_id);

CREATE INDEX ix_url_filters_brand_id ON url_filters (brand_id);

CREATE INDEX ix_url_filters_collection_id ON url_filters (collection_id);

CREATE INDEX ix_url_filters_family_id ON url_filters (family_id);

CREATE INDEX ix_url_filters_sub_family_id ON url_filters (sub_family_id);

CREATE INDEX ix_url_filters_url_id ON url_filters (url_id);

CREATE INDEX ix_user_charts_article_id ON user_charts (article_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20220918160801_InitialMigration', '6.0.4');

COMMIT;

