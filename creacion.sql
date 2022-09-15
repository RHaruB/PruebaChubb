Create DataBase SegurosChubb;
use SegurosChubb;
Create Table Seguros
(
Codigo_Seguro int not null,
Descripcion varchar(200) not null,
Valor_Asegurado float,
Prima float,
Estado char

Primary key (Codigo_Seguro)
);
Create Table Persona
(
Codigo_Persona int not null,
Cedula varchar(10) not null,
Nombre_Cliente varchar(200)not null,
Telefono varchar(20),
Edad int,
Estado char

Primary key (Codigo_Persona)
)
Create Table Poliza
(
Codigo_Poliza int not null,
Codigo_Persona int not null,
Codigo_Seguro int not null,
Estado char

Primary Key (Codigo_Poliza)
FOREIGN KEY (Codigo_Persona) REFERENCES Persona(Codigo_Persona),
FOREIGN KEY (Codigo_Seguro) REFERENCES Seguros(Codigo_Seguro)

)
