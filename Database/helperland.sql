--Create database
create database helperland

use helperland
--creating tables

--creating users table
create table users (
	users_id int not null identity(1, 1) Primary key,
	fname varchar(30) not null,
	lname varchar(30) not null,
	email varchar(40) not null,
	phone nvarchar(15) not null,
	dob date not null,
	languages varchar(35) not null,
	pwd varchar(32) not null,
	user_status varchar(15) not null, 
	user_role int not null,
	nationality varchar(20),
	gender varchar(6)
)

--create address table
create table addresses (
	address_id int not null identity(1, 1) Primary key,
	addr varchar(150) not null,
	city varchar(25) not null,
	postalcode int,
	users_id int Foreign key references users(users_id),
)

--create services table
create table serviceTable (
	service_id int not null identity(1, 1) primary key,
	service_date date not null,
	service_time datetime not null,
	sp_id int not null foreign key references users(users_id),
	cust_id int not null foreign key references users(users_id),
	duration int not null,
	payment int not null, 
	service_status varchar(15),
	postalcode int not null,
	extra int,
	comment varchar(1000), 
	have_pet bit,
	address_id int foreign key references addresses(address_id)
)

--create sp-rating table
create table sp_rating (
	rating_id int not null identity(1, 1) primary key,
	sp_id int not null foreign key references users(users_id),
	cust_id int not null foreign key references users(users_id),
	ontime_rate int,
	friendly_rate int,
	quality_rate int,
	rate_date date not null,
	rate_time time not null,
)

--create favourite_sp table
create table favourite_sp (
	fav_id int not null identity(1, 1) primary key,
	sp_id int not null foreign key references users(users_id),
	cust_id int not null foreign key references users(users_id)
)

--create blocklist table
create table blockList(
	block_id int not null identity(1, 1) primary key,
	sp_id int not null foreign key references users(users_id),
	cust_id int not null foreign key references users(users_id)
)

--create roles table 
create table roles(
	role_id int not null primary key,
	roles varchar(20) not null
)

--insert values into roles table
insert into roles (role_id, roles) values (1 , 'customer')
insert into roles (role_id, roles) values (2 , 'service provider')
insert into roles (role_id, roles) values (3 , 'admin')

--set role as foreign key in users table
alter table users 
add constraint FK_user_role foreign key (user_role) references roles(role_id)