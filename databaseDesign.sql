create database QuanLySinhVien
go
use QuanLySinhVien
go

create table SinhVien (
MaSV varchar(13),
TenSV nvarchar(30),
MaKhoa varchar(13),
NgaySinh datetime,
DiaChi nvarchar(50),
constraint PK_SinhVien primary key (MaSV)
)
go
create table Khoa(
MaKhoa varchar(13),
TenKhoa nvarchar(30),
constraint PK_Khoa primary key (MaKhoa)
)
go
create table MonHoc(
MaMH varchar(13),
TenMH nvarchar(30),
MaKhoa varchar(13),
SoTinChi int,
GV nvarchar(30),
constraint PK_MonHoc primary key (MaMH)
)
go
create table KetQua(
MaSV varchar(13),
MaMH varchar(13),
Diem float
)
go
create table NguoiDung(
TenDangNhap varchar(30),
MatKhau nvarchar(32),
Moderator int,
constraint PK_NguoiDung primary key (TenDangNhap)
)
go

alter table SinhVien 
add constraint FK_SinhVien_MaMH foreign key (MaKhoa) references Khoa(MaKhoa)
go
alter table MonHoc
add constraint FK_MonHoc_MaMH foreign key (MaKhoa) references Khoa(MaKhoa)
go
alter table KetQua add 
constraint FK_KetQua_MaSV foreign key (MaSV) references SinhVien(MaSV),
constraint FK_KetQua_MaMH foreign key (MaMH) references MonHoc(MaMH)
go

insert into SinhVien (MaSV, TenSV, MaKhoa, NgaySinh, DiaChi) values 
('43.01.104.200', N'Lê Văn Tèo', 'CNTT', 1999-02-28, N'279 ADV'),
('43.01.101.200', N'Trần Thị Tâm', 'TOAN', 1999-02-02, N'281 ADV')
go
insert into Khoa values 
('CNTT', N'Công nghệ thông tin'),
('TOAN', N'Toán'),
('VLY', N'Vật lý')
go
insert into MonHoc values 
('COMP1001', N'Lập trình cơ bản', 'CNTT', 3, N'Tamtd'),
('MATH1003', N'Đại số tuyến tính', 'TOAN',3 , N'Lanvd'),
('MATH1004', N'Lý thuyết đồ thị', 'CNTT', 2, N'thầy Hưng')
go
insert into KetQua values
('43.01.104.200', 'COMP1001', 4.75),
('43.01.101.200', 'MATH1003', 9.5)
go
insert into NguoiDung values
('thanhlt', convert(nvarchar(32), HashBytes('md5', '123456'),2), 1),
('chaunhk', convert(nvarchar(32), HashBytes('md5', '654321'),2), 1),
('datnt'  , convert(nvarchar(32), HashBytes('md5', '654321'),2), 0)
go