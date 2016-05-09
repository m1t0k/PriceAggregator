SELECT EXISTS (
    SELECT 1 
    FROM   pg_catalog.pg_class c
    JOIN   pg_catalog.pg_namespace n ON n.oid = c.relnamespace
    WHERE  n.nspname = 'public'
    AND    c.relname = 'brand1'
);



create table public.brand
(
id serial primary key,
name varchar(512) not null,
description varchar(1024)
)


create table public.category
(
id serial primary key,
name varchar(512) not null,
description varchar(1024)
)


create table public.user
(
id serial primary key,
email varchar(512) not null unique,
firstname varchar(512),
lastname varchar(512),
description varchar(1024),
createdAt timestamp not  null,
lastUpdated timestamp,
loggedAt timestamp,
userType smallint not null default 0 

)


create table public.productList
(
  id serial primary key,
  ownerId integer not null,
  brandId integer not null,
  createdAt timestamp not  null,
  lastUpdated timestamp,
  description varchar(1024)
)

alter table public.productList
add column name varchar(512) not null 

create table public.product
(
  id serial primary key,
  sku varchar(512) not null,
  rrp money,
  brandId integer not null,
  categoryId integer not null,
  productListId integer not null,
  createdAt timestamp not  null,
  lastUpdated timestamp 
)

alter table public.product
add column name varchar(512) not null 

create table public.retailer
(
 id serial primary key,
 name varchar(512) not null,
 url varchar(512) not null,
 description varchar(512) not null,
)


create table public.pricelist
(
 id serial primary key,
 retailerId integer not null,
 productId integer not null,
 price money,
 createdAt timestamp,
 status smallint not null default 0 
)

create table public.pricelistLog
(
 id serial primary key,
 retailerId integer not null,
 productId integer not null,
 price money,
 createdAt timestamp 
)
