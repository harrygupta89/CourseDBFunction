--DB name : appdb
create Table Course 
(
	CourseID int,
	CourseName varchar(1000),
	Rating numeric(2,1)
)

select * from Course;

insert into Course values(1,'AZ-204',4.5)
insert into Course values(2,'AZ-303',4.6)
insert into Course values(3,'AZ-304',4.7)


