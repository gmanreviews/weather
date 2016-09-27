use weather; -- my database name locally

-- THIS SECTION CREATES TABLES NEEDED FOR DATABASE

CREATE TABLE 
employee_type(
id int not null unique identity,
emp_type varchar(20) not null
PRIMARY KEY (id)
);

CREATE TABLE
employee_location(
id int not null unique identity,
location varchar(50) not null,
PRIMARY KEY (id)
);

CREATE TABLE employee_personal_details(
id int not null unique identity,
first_name varchar(100) not null,
last_name varchar(100) not null,
dob date not null,
email varchar(150) null,
PRIMARY KEY (id)
);

CREATE TABLE employee_assignment(
id int not null unique identity,
employee_id int not null unique,
employee_location_id int not null,
employee_type_id int not null,
PRIMARY KEY (id),
FOREIGN KEY (employee_id) REFERENCES employee_personal_details(id),
FOREIGN KEY (employee_location_id) REFERENCES employee_location(id),
FOREIGN KEY (employee_type_id) REFERENCES employee_type(id)
);

CREATE TABLE employee_attendance(
id int not null unique identity,
employee_id int not null,
employee_location_id int not null,
date_of_attendance date not null,
time_of_arrival datetime not null,
time_of_exit datetime not null,
PRIMARY KEY (id),
FOREIGN KEY (employee_id) REFERENCES employee_personal_details(id),
FOREIGN KEY (employee_location_id) REFERENCES employee_location(id)
);

CREATE TABLE notifications(
id int not null unique identity,
confirm_sent bit not null default 0,
confirm_read bit not null default 0,
note_subject varchar(200) not null,
note_content varchar(MAX) not null,
PRIMARY KEY (id)
);

ALTER TABLE employee_personal_details ADD adddress VARCHAR(MAX) NULL;
ALTER TABLE employee_personal_details ADD city VARCHAR(100) NULL;
ALTER TABLE employee_personal_details ADD country VARCHAR(100) NULL;
ALTER TABLE employee_personal_details ADD telephone VARCHAR(20) NULL;
ALTER TABLE employee_location ADD city_id int not null;
ALTER TABLE notifications ADD note_to varchar(150) NOT NULL;

--- CREATE DEFINED DATA FROM REQUIREMENTS DOC
 
-- city ids correlate to that of the open weather map api
INSERT INTO employee_location (location, city_id) VALUES ('Ocho Rios',3489239), ('Kingston',3489854), ('Motego Bay',3489460), ('Falmouth',2649715);
INSERT INTO employee_type (emp_type) VALUES ('canners'),('packers'),('technicians');

--- DUMMY DATA

INSERT INTO employee_personal_details
(first_name, last_name, dob, email, adddress, city, country, telephone)
VALUES
('Andrew', 'Robinson', '1986-03-26','andrewneilrobinson@gmail.com','69 Lady Musgrave Rd.','Kingston','Jamaica','18764439794'),
('Ryan', 'Giggs', '1954-02-11','giggs@gamail.com','Somewhere','Montego Bay','Jamaica','18764539794'),
('Park', 'Ji-Sung', '1976-01-13','apark@gamail.com','Korea','Lucea','Jamaica','18764439794'),
('Eric', 'Cantona', '1966-07-07','cancan@gamail.com','Lille','Ocho Rios','Jamaica','18764489794');



--- STORED PROCEDURES