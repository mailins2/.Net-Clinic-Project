create database QLPK
go
USE QLPK 

CREATE TABLE NHANVIEN(
	MANV CHAR(10) NOT NULL PRIMARY KEY,
	HOTEN NVARCHAR(50),
	NGAYSINH DATE,
	DIACHI NVARCHAR(100),
	SDT CHAR(10)CHECK (LEN(SDT) = 10) ,
	CCCD CHAR(12)CHECK (LEN(CCCD) = 12),
	EMAIL VARCHAR(100),
	GIOITINH NVARCHAR(5) CHECK (GIOITINH IN (N'Nam', N'Nữ'))
)


CREATE TABLE BACSI(
	MABS CHAR(10) NOT NULL PRIMARY KEY,
	HOTEN NVARCHAR(50),
	NGAYSINH DATE,
	DIACHI NVARCHAR(100),
	SDT CHAR(10) CHECK (LEN(SDT) = 10) ,
	CCCD CHAR(12)CHECK (LEN(CCCD) = 12),
	EMAIL VARCHAR(100),
	HOCHAM NVARCHAR(20),
	GIOITINH NVARCHAR(5) CHECK (GIOITINH IN (N'Nam', N'Nữ'))
)
CREATE TABLE TAIKHOAN (
    TENDANGNHAP NVARCHAR(50) NOT NULL PRIMARY KEY,
    MATKHAU NVARCHAR(50) NOT NULL,
    VAITRO NVARCHAR(20) CHECK (VAITRO IN (N'Admin', N'Bacsi',N'Nhanvien')),
    MANV CHAR(10),
    MABS CHAR(10),
    CONSTRAINT FK_TAIKHOAN_NHANVIEN FOREIGN KEY (MANV) REFERENCES NHANVIEN(MANV),
    CONSTRAINT FK_TAIKHOAN_BACSI FOREIGN KEY (MABS) REFERENCES BACSI(MABS)
)


CREATE TABLE BENHNHAN (
	MABN CHAR(10) NOT NULL PRIMARY KEY,
	HOTEN NVARCHAR(50),
	NGAYSINH DATE,
	DIACHI NVARCHAR(100),
	SDT CHAR(10)CHECK (LEN(SDT) = 10) ,
	CCCD CHAR(12)CHECK (LEN(CCCD) = 12),
	GIOITINH NVARCHAR(5) CHECK (GIOITINH IN (N'Nam', N'Nữ'))
)
CREATE TABLE LICHHEN
(
	malh varchar(10) not null primary key,
	mabn char(10),
	mabs char(10),
	ngayhen datetime,
	trangthai nvarchar(100),
	constraint fk_lh_bn foreign key(mabn) references BENHNHAN(mabn),
	constraint fk_lh_bs foreign key(mabs) references BACSI(mabs)
)

CREATE TABLE THUOC(
	mat varchar(10) not null,
	tenthuoc nvarchar(max),
	hansudung date,
	nhasx nvarchar(60),
	soluongtonkho int CHECK (SOLUONGTONKHO >= 0),
	dongia int NOT NULL CHECK (DONGIA >= 0),
	constraint pk_Thuoc primary key(mat)
)

-- MỖI DÒNG LÀ MỘT LẦN ĐẾN KHÁM CỦA BỆNH NHÂN
CREATE TABLE LSKHAM (
	maba varchar(10) not null PRIMARY KEY,
	mabn char(10),
	mabs char(10),
	ngaykham date,
	chuandoan nvarchar(max),
	dieutri nvarchar(max),
	CONSTRAINT FK_LSKHAM_BENHNHAN FOREIGN KEY(MABN) REFERENCES BENHNHAN(MABN),
	CONSTRAINT FK_LSKHAM_BACSI FOREIGN KEY(MABS) REFERENCES BACSI(MABS)
)

CREATE TABLE DONTHUOC(
	madt varchar(10) not null PRIMARY KEY,
	maba varchar(10),--là mã mà mỗi lần bệnh nhân đến khám (trong bảng LSKham)
	ngaykedon DATE, 
	tongtien int CHECK (TONGTIEN >= 0),
	CONSTRAINT FK_DONTHUOC_LSKHAM FOREIGN KEY(MABA) REFERENCES LSKHAM(MABA)
)


CREATE TABLE CHITIETDONTHUOC(
	madt varchar(10),
	mat varchar(10),
	lieuluong nvarchar(20),
	soluong int NOT NULL CHECK (SOLUONG > 0),
	constraint pk_ctdt primary key(madt,mat),
	constraint fk_chitietdonthuoc_thuoc foreign key(mat) references thuoc(mat),
	constraint fk_chitietdonthuoc_donthuoc foreign key(madt) references donthuoc(madt)
)

CREATE TABLE DICHVU
(
	madv varchar(10) not null primary key,
	tendv nvarchar(60),
	dongia int CHECK (DONGIA >= 0),
)
CREATE TABLE HOADONDV
(
	mahddv varchar(10) not null primary key,
	maba varchar(10),-- sua mabn thanh maba bởi vì một bệnh nhân có nhiều lần đến khám nếu dùng mabn thì nó trả về nhiều dòng 
	tongtien int CHECK (TONGTIEN >= 0),
	ngay date ,
	manv char(10) NOT NULL,
	constraint fk_hddv_lskham foreign key(maba) references LSKHAM(maba),
	constraint fk_hddv_nv foreign key(manv) references NHANVIEN(manv)
)



CREATE TABLE CHITIETHDDV
(
	mahddv varchar(10),
	madv varchar(10),
	soluong int,
	constraint pk_cthddv primary key(mahddv,madv),
	constraint fk_cthddv_dv foreign key(madv) references DICHVU(madv),
	constraint fk_cthddv_hddv foreign key(mahddv) references HOADONDV(mahddv)
)

ALTER TABLE NHANVIEN ADD CONSTRAINT uq_nv_cccd UNIQUE(CCCD);
ALTER TABLE NHANVIEN ADD CONSTRAINT uq_nv_sdt UNIQUE(SDT);
ALTER TABLE NHANVIEN ADD CONSTRAINT uq_nv_email UNIQUE(EMAIL);

ALTER TABLE BENHNHAN ADD CONSTRAINT uq_bn_cccd UNIQUE(CCCD);
ALTER TABLE BENHNHAN ADD CONSTRAINT uq_bn_sdt UNIQUE(SDT);

ALTER TABLE BACSI ADD CONSTRAINT uq_bs_cccd UNIQUE(CCCD);
ALTER TABLE BACSI ADD CONSTRAINT uq_bs_sdt UNIQUE(SDT);
ALTER TABLE BACSI ADD CONSTRAINT uq_bs_email UNIQUE(EMAIL);
------------------------------------------------------------------------------------
go 
--kiểm tra ngaykedon phải trùng với ngày khám của bệnh nhân 
create trigger check_ngay_donthuoc
	on DONTHUOC
	after insert, update 
	as 
	begin 
		if exists(select 1
				  from inserted i 
				  join LSKHAM ls on ls.maba=i.maba
				  where i.ngaykedon<>ls.ngaykham
				  ) 
				  begin 
					 RAISERROR ('Ngày trong DONTHUOC phải trùng với ngày khám trong LSKHAM.', 16, 1);
					 ROLLBACK TRANSACTION;
				  end 
	end 
go
--kiểm tra xem là ngày trong hoadondv có cùng ngày với lskham của bệnh nhân hay không?
	create trigger check_ngay_hddv
	on HOADONDV
	after insert, update 
	as 
	begin 
		if exists(select 1
				  from inserted i 
				  join LSKHAM ls on ls.maba=i.maba
				  where i.ngay<>ls.ngaykham
				  ) 
				  begin 
					 RAISERROR ('Ngày trong HOADONDV phải trùng với ngày khám trong LSKHAM.', 16, 1);
					 ROLLBACK TRANSACTION;
				  end 
	end 
go
 --trigger tính tổng tiền đơn thuốc 

  create trigger Tinh_TongTienDonThuoc
  on CHITIETDONTHUOC
  after insert , delete, update 
  as
  begin 
	update DONTHUOC 
	set tongtien=(select sum (ctdt.soluong*THUOC.dongia)
				 from CHITIETDONTHUOC ctdt 
				 join THUOC on THUOC.mat=ctdt.mat
				 where ctdt.madt=DONTHUOC.madt)
	where DONTHUOC.madt in (
		SELECT DISTINCT madt FROM inserted
		UNION
		SELECT DISTINCT madt FROM deleted
	)

  end 
go
  --trigger tinh tổng tiền dịch vụ 

 create trigger Tinh_TongTienDV
  on CHITIETHDDV
  after insert , delete, update 
  as
  begin 
	update HOADONDV
	set tongtien=(select sum (cthd.soluong*dv.dongia)
				 from CHITIETHDDV cthd 
				 join DICHVU dv on dv.madv=cthd.madv
				 where cthd.mahddv=HOADONDV.mahddv)
	where HOADONDV.mahddv in (
		SELECT DISTINCT mahddv FROM inserted
		UNION
		SELECT DISTINCT mahddv FROM deleted
	)
	end 
go
----------------------------------------Nhập liệu ------------------------------------------------------------------------------------------------------

--Bảng NHANVIEN
insert into NHANVIEN values ('NV0001',N'Nguyễn Minh Khánh','1999-12-01',N'Long An','0236000341','074304002118','minhkhanh@gmail.com',N'Nam')
insert into NHANVIEN values ('NV0002',N'Trần Ngọc Anh','1979-01-11',N'Hà Tĩnh','0236000342','074304002119','ngocanh@gmail.com',N'Nữ')
insert into NHANVIEN values ('NV0003',N'Nguyễn Linh Chi','2000-07-18',N'TPHCM','0236000343','074304002120','linhchi@gmail.com',N'Nữ')
insert into NHANVIEN values ('NV0004',N'Lê Khánh Duy','1998-02-20',N'Bình Thuận','0236000344','074304002121','khanhduy@gmail.com',N'Nam')
insert into NHANVIEN values ('NV0005',N'Lâm Thu Trà','2001-11-06',N'Long An','0236000345','074304002122','thutra@gmail.com',N'Nữ')

-- Bảng BACSI
insert into BACSI values ('BS0001',N'Nguyễn Thị Phan Thúy','1978-12-01',N'Long An','0236000331','074304003118','phanthuy@gmail.com',N'Bác sĩ - CK2',N'Nữ')
insert into BACSI values ('BS0002',N'Phạm Đăng Trọng Trường','1970-03-01',N'Đồng Nai','0236000332','074304003119','truong@gmail.com',N'Bác sĩ - CK2',N'Nam')
insert into BACSI values ('BS0003',N'Phạm Thị Uyển Nhi','1980-11-23',N'TPHCM','0236000333','074304003120','uyennhi@gmail.com',N'Thạc sĩ - Bác sĩ',N'Nữ')
insert into BACSI values ('BS0004',N'Trần Minh Trí','1990-10-13',N'Bình Dương','0236000334','074304003121','minhtri@gmail.com',N'Bác sĩ - CK1',N'Nam')
insert into BACSI values ('BS0005',N'Lê Hữu Bách','1968-04-01',N'Nghệ An','0236000335','074304003122','huubach@gmail.com',N'Bác sĩ - CK1',N'Nam')
-- Bảng TAIKHOAN

INSERT INTO TAIKHOAN VALUES (N'admin1', N'admin1123', N'Admin', NULL, NULL)
INSERT INTO TAIKHOAN VALUES (N'bacsi01', N'bacsi123', N'Bacsi', NULL, 'BS0001')
INSERT INTO TAIKHOAN VALUES (N'bacsi02', N'bacsi123', N'Bacsi', NULL, 'BS0002')
INSERT INTO TAIKHOAN VALUES (N'nhanvien01', N'nhanvien123', N'Nhanvien', 'NV0001', NULL)
INSERT INTO TAIKHOAN VALUES (N'nhanvien02', N'nhanvien123', N'Nhanvien', 'NV0002', NULL)
--Bảng BENHNHAN
insert into BENHNHAN values ('BN0001',N'Hồ Mỹ Châu','2003-12-01',N'Long An','0136000341','084304002118',N'Nữ')
insert into BENHNHAN values ('BN0002',N'Võ Thanh Phương','1999-01-11',N'Hà Tĩnh','0136000342','084304002119',N'Nam')
insert into BENHNHAN values ('BN0003',N'Nguyễn Linh Chi','2000-07-18',N'TPHCM','0136000343','084304002120',N'Nữ')
insert into BENHNHAN values ('BN0004',N'Lê Khánh Duy','1998-02-20',N'Bình Thuận','0136000344','084304002121',N'Nam')
insert into BENHNHAN values ('BN0005',N'Lâm Thu Trà','2001-11-06',N'Long An','0136000345','084304002122',N'Nữ')
insert into BENHNHAN values ('BN0006',N'Phạm Văn Đảng','2006-12-01',N'Long An','0136000346','084304002123',N'Nam')
insert into BENHNHAN values ('BN0007',N'Phạm Hiếu Liêm','1999-01-11',N'Hà Tĩnh','0136000347','084304002124',N'Nam')
insert into BENHNHAN values ('BN0008',N'Đào Thị Kim Duyên','2000-07-18',N'Tiền Giang','0136000348','084304002125',N'Nữ')
insert into BENHNHAN values ('BN0009',N'Nguyễn Đắc Tuấn','1998-02-20',N'Bình Thuận','0136000349','084304002126',N'Nam')
insert into BENHNHAN values ('BN0010',N'Lê Thị Thanh Trúc','2001-11-06',N'Trà Vinh','0136000310','084304002127',N'Nữ')
insert into BENHNHAN values ('BN0011',N'Bùi Quốc Hiếu','2004-02-01',N'Thái Nguyên','0136000311','084304002128',N'Nam')
insert into BENHNHAN values ('BN0012',N'Nguyễn Thị Thu Hằng','1999-01-11',N'Hà Tĩnh','0136000312','084304002129',N'Nữ')
insert into BENHNHAN values ('BN0013',N'Vũ Thị Kim Khánh','2000-07-18',N'Ninh Bình','0136000313','084304002130',N'Nữ')
insert into BENHNHAN values ('BN0014',N'Nguyễn Tuyết Hạnh','1998-12-20',N'Bình Thuận','0136000314','084304002131',N'Nữ')
insert into BENHNHAN values ('BN0015',N'Trần Kim Phượng','2001-09-06',N'Long An','0136000315','084304002132',N'Nữ')

--Bảng DICHVU
insert into DICHVU values ('DV0001',N'KHÁM DA LIỄU (BHYT)', 42000)
insert into DICHVU values ('DV0002',N'AFB trực tiếp nhuộm Ziehl-Neelsen', 70000)
insert into DICHVU values ('DV0003',N'Định lượng Bilirubin toàn phần [Máu]', 21000)
insert into DICHVU values ('DV0004',N'Định lượng Bilirubin trực tiếp [Máu]', 21000)
insert into DICHVU values ('DV0005',N'CHLAMYDIA TEST NHANH', 74000)
insert into DICHVU values ('DV0006',N'Định lượng Cholesterol toàn phần (máu)', 27000)
insert into DICHVU values ('DV0007',N'Tổng phân tích tế bào máu ngoại vi (bằng máy đếm tổng trở)', 41000)
insert into DICHVU values ('DV0008',N'Điện giải đồ (Na, K, Cl) [Máu]', 29000)
insert into DICHVU values ('DV0009',N'Định nhóm máu hệ ABO (Kỹ thuật phiến đá)', 40000)
insert into DICHVU values ('DV0010',N'Định nhóm máu hệ Rh(D) (Kỹ thuật phiến đá)', 32000)
insert into DICHVU values ('DV0011',N'Định lượng Albumin [Máu]', 21000)
insert into DICHVU values ('DV0012',N'Định lượng Creatinin (máu)', 21000)
insert into DICHVU values ('DV0013',N'Nhuộm hai màu Hematoxyline- Eosin', 350000)
insert into DICHVU values ('DV0014',N'Nhuộm Periodic Acide Schiff (PAS)', 95000)
insert into DICHVU values ('DV0015',N'Nhuộm phiến đồ tế bào theo Papanicolaou', 374000)
--Thuoc
INSERT INTO THUOC VALUES
('T01', 'Hydrocortisone', '2025-12-31', 'Pfizer', 50, 10800),
('T02', 'Mometasone', '2026-06-30', 'Bayer', 30, 20400),
('T03', 'Clobetasol', '2024-10-31', 'GlaxoSmithKline', 20, 31500),
('T04', 'Fluocinonide', '2025-03-31', 'Taro Pharmaceuticals', 25, 42600),
('T05', 'Betamethasone', '2024-12-31', 'Sandoz', 40, 53700),
('T06', 'Triamcinolone', '2025-05-31', 'Mylan', 35, 64800),
('T07', 'Calcipotriene', '2026-01-31', 'LEO Pharma', 15, 75900),
('T08', 'Tazarotene', '2025-09-30', 'Allergan', 18, 87500),
('T09', 'Adapalene', '2024-11-30', 'Galderma', 20, 96400),
('T10', 'Isotretinoin', '2026-07-31', 'Hoffmann-La Roche', 10, 100000),
('T11', 'Hydroquinone', '2024-08-31', 'Obagi', 12, 20900),
('T12', 'Kojic Acid', '2025-02-28', 'AMA', 22, 30100),
('T13', 'Retinoids', '2025-04-30', 'Neutrogena', 25, 49500),
('T14', 'Alpha Hydroxy Acids (AHA)', '2026-03-31', 'Derma E', 27, 50300),
('T15', 'Beta Hydroxy Acids (BHA)', '2025-01-31', 'Paula''s Choice', 19, 65900),
('T16', 'Salicylic Acid', '2024-09-30', 'Neutrogena', 33, 70400),
('T17', 'Urea', '2025-10-31', 'Eucerin', 28, 84700),
('T18', 'Lactic Acid', '2026-02-28', 'The Ordinary', 21, 92900),
('T19', 'Glycolic Acid', '2025-11-30', 'Pixi', 35, 50600),
('T20', 'Ascorbic Acid (Vitamin C)', '2026-05-31', 'Skinceuticals', 15, 106500);
--Lich Hen 
insert into LICHHEN values ('LH0001', 'BN0001', 'BS0001', '2024-11-10 09:00', N'đã xác nhận')
insert into LICHHEN values ('LH0002', 'BN0002', 'BS0001', '2024-11-10 11:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0003', 'BN0003', 'BS0001', '2024-11-10 14:00', N'đã hủy')
insert into LICHHEN values ('LH0004', 'BN0004', 'BS0002', '2024-11-11 08:30', N'đã xác nhận')
insert into LICHHEN values ('LH0005', 'BN0005', 'BS0002', '2024-11-11 10:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0006', 'BN0006', 'BS0002', '2024-11-11 15:00', N'đã hủy')
insert into LICHHEN values ('LH0007', 'BN0007', 'BS0003', '2024-11-12 09:45', N'đã xác nhận')
insert into LICHHEN values ('LH0008', 'BN0008', 'BS0003', '2024-11-12 13:15', N'chưa xác nhận')
insert into LICHHEN values ('LH0009', 'BN0009', 'BS0003', '2024-11-12 16:00', N'đã hủy')
insert into LICHHEN values ('LH0010', 'BN0010', 'BS0004', '2024-11-13 08:15', N'đã xác nhận')
insert into LICHHEN values ('LH0011', 'BN0011', 'BS0004', '2024-11-13 11:45', N'chưa xác nhận')
insert into LICHHEN values ('LH0012', 'BN0012', 'BS0004', '2024-11-13 14:30', N'đã hủy')
insert into LICHHEN values ('LH0013', 'BN0013', 'BS0005', '2024-11-14 10:00', N'đã xác nhận')
insert into LICHHEN values ('LH0014', 'BN0014', 'BS0005', '2024-11-14 12:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0015', 'BN0015', 'BS0005', '2024-11-14 15:45', N'đã hủy')
insert into LICHHEN values ('LH0016', 'BN0001', 'BS0001', '2024-12-01 09:30', N'đã xác nhận')
insert into LICHHEN values ('LH0017', 'BN0002', 'BS0002', '2024-12-02 13:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0018', 'BN0003', 'BS0003', '2024-12-02 16:30', N'đã hủy')
insert into LICHHEN values ('LH0019', 'BN0004', 'BS0004', '2024-12-03 08:45', N'đã xác nhận')
insert into LICHHEN values ('LH0020', 'BN0005', 'BS0005', '2024-12-04 10:45', N'chưa xác nhận')
insert into LICHHEN values ('LH0021', 'BN0006', 'BS0001', '2024-12-05 15:15', N'đã hủy')
insert into LICHHEN values ('LH0022', 'BN0007', 'BS0002', '2024-12-06 09:15', N'đã xác nhận')
insert into LICHHEN values ('LH0023', 'BN0008', 'BS0003', '2024-12-07 14:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0024', 'BN0009', 'BS0004', '2024-12-08 16:15', N'đã hủy')
insert into LICHHEN values ('LH0025', 'BN0010', 'BS0005', '2024-12-09 08:00', N'đã xác nhận')
insert into LICHHEN values ('LH0026', 'BN0011', 'BS0001', '2024-12-10 10:15', N'chưa xác nhận')
insert into LICHHEN values ('LH0027', 'BN0012', 'BS0002', '2024-12-11 15:00', N'đã hủy')
insert into LICHHEN values ('LH0028', 'BN0013', 'BS0003', '2024-12-12 09:00', N'đã xác nhận')
insert into LICHHEN values ('LH0029', 'BN0014', 'BS0004', '2024-12-13 11:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0030', 'BN0015', 'BS0005', '2024-12-14 14:45', N'đã hủy')
--t11
insert into LICHHEN values ('LH0061', 'BN0001', 'BS0001', '2024-11-22 09:00', N'đã xác nhận')
insert into LICHHEN values ('LH0062', 'BN0002', 'BS0002', '2024-11-22 11:15', N'chưa xác nhận')
insert into LICHHEN values ('LH0063', 'BN0003', 'BS0003', '2024-11-23 14:30', N'đã hủy')
insert into LICHHEN values ('LH0064', 'BN0004', 'BS0004', '2024-11-23 16:45', N'đã xác nhận')
insert into LICHHEN values ('LH0065', 'BN0005', 'BS0005', '2024-11-24 08:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0066', 'BN0006', 'BS0001', '2024-11-24 10:15', N'đã hủy')
insert into LICHHEN values ('LH0067', 'BN0007', 'BS0002', '2024-11-25 13:00', N'đã xác nhận')
insert into LICHHEN values ('LH0068', 'BN0008', 'BS0003', '2024-11-25 15:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0069', 'BN0009', 'BS0004', '2024-11-26 09:00', N'đã hủy')
insert into LICHHEN values ('LH0070', 'BN0010', 'BS0005', '2024-11-26 11:45', N'đã xác nhận')
insert into LICHHEN values ('LH0071', 'BN0011', 'BS0001', '2024-11-27 14:15', N'chưa xác nhận')
insert into LICHHEN values ('LH0072', 'BN0012', 'BS0002', '2024-11-27 16:30', N'đã hủy')
insert into LICHHEN values ('LH0073', 'BN0013', 'BS0003', '2024-11-28 08:30', N'đã xác nhận')
insert into LICHHEN values ('LH0074', 'BN0014', 'BS0004', '2024-11-28 10:45', N'chưa xác nhận')
insert into LICHHEN values ('LH0075', 'BN0015', 'BS0005', '2024-11-29 13:00', N'đã hủy')
insert into LICHHEN values ('LH0076', 'BN0001', 'BS0001', '2024-11-29 15:30', N'đã xác nhận')
insert into LICHHEN values ('LH0077', 'BN0002', 'BS0002', '2024-11-30 09:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0078', 'BN0003', 'BS0003', '2024-11-30 11:15', N'đã hủy')
insert into LICHHEN values ('LH0079', 'BN0004', 'BS0004', '2024-11-30 14:30', N'đã xác nhận')
insert into LICHHEN values ('LH0080', 'BN0005', 'BS0005', '2024-11-30 16:45', N'chưa xác nhận')
insert into LICHHEN values ('LH0081', 'BN0006', 'BS0001', '2024-11-22 08:00', N'đã hủy')
insert into LICHHEN values ('LH0082', 'BN0007', 'BS0002', '2024-11-23 10:15', N'đã xác nhận')
insert into LICHHEN values ('LH0083', 'BN0008', 'BS0003', '2024-11-24 13:45', N'chưa xác nhận')
insert into LICHHEN values ('LH0084', 'BN0009', 'BS0004', '2024-11-25 16:00', N'đã hủy')
insert into LICHHEN values ('LH0085', 'BN0010', 'BS0005', '2024-11-26 09:15', N'đã xác nhận')
insert into LICHHEN values ('LH0086', 'BN0011', 'BS0001', '2024-11-27 11:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0087', 'BN0012', 'BS0002', '2024-11-28 14:00', N'đã hủy')
insert into LICHHEN values ('LH0088', 'BN0013', 'BS0003', '2024-11-29 16:30', N'đã xác nhận')
insert into LICHHEN values ('LH0089', 'BN0014', 'BS0004', '2024-11-30 08:15', N'chưa xác nhận')
insert into LICHHEN values ('LH0090', 'BN0015', 'BS0005', '2024-11-30 10:45', N'đã hủy')

--t12
insert into LICHHEN values ('LH0031', 'BN0001', 'BS0001', '2024-12-15 09:30', N'đã xác nhận')
insert into LICHHEN values ('LH0032', 'BN0002', 'BS0002', '2024-12-15 11:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0033', 'BN0003', 'BS0003', '2024-12-16 14:45', N'đã hủy')
insert into LICHHEN values ('LH0034', 'BN0004', 'BS0004', '2024-12-16 16:30', N'đã xác nhận')
insert into LICHHEN values ('LH0035', 'BN0005', 'BS0005', '2024-12-17 08:15', N'chưa xác nhận')
insert into LICHHEN values ('LH0036', 'BN0006', 'BS0001', '2024-12-17 10:45', N'đã hủy')
insert into LICHHEN values ('LH0037', 'BN0007', 'BS0002', '2024-12-18 13:15', N'đã xác nhận')
insert into LICHHEN values ('LH0038', 'BN0008', 'BS0003', '2024-12-18 15:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0039', 'BN0009', 'BS0004', '2024-12-19 09:45', N'đã hủy')
insert into LICHHEN values ('LH0040', 'BN0010', 'BS0005', '2024-12-19 11:30', N'đã xác nhận')
insert into LICHHEN values ('LH0041', 'BN0011', 'BS0001', '2024-12-20 14:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0042', 'BN0012', 'BS0002', '2024-12-20 16:15', N'đã hủy')
insert into LICHHEN values ('LH0043', 'BN0013', 'BS0003', '2024-12-21 08:30', N'đã xác nhận')
insert into LICHHEN values ('LH0044', 'BN0014', 'BS0004', '2024-12-21 10:45', N'chưa xác nhận')
insert into LICHHEN values ('LH0045', 'BN0015', 'BS0005', '2024-12-22 13:15', N'đã hủy')
insert into LICHHEN values ('LH0046', 'BN0001', 'BS0001', '2024-12-22 15:30', N'đã xác nhận')
insert into LICHHEN values ('LH0047', 'BN0002', 'BS0002', '2024-12-23 09:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0048', 'BN0003', 'BS0003', '2024-12-23 11:15', N'đã hủy')
insert into LICHHEN values ('LH0049', 'BN0004', 'BS0004', '2024-12-24 14:45', N'đã xác nhận')
insert into LICHHEN values ('LH0050', 'BN0005', 'BS0005', '2024-12-24 16:30', N'chưa xác nhận')
insert into LICHHEN values ('LH0051', 'BN0006', 'BS0001', '2024-12-25 08:15', N'đã hủy')
insert into LICHHEN values ('LH0052', 'BN0007', 'BS0002', '2024-12-25 10:30', N'đã xác nhận')
insert into LICHHEN values ('LH0053', 'BN0008', 'BS0003', '2024-12-26 13:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0054', 'BN0009', 'BS0004', '2024-12-26 15:45', N'đã hủy')
insert into LICHHEN values ('LH0055', 'BN0010', 'BS0005', '2024-12-27 09:30', N'đã xác nhận')
insert into LICHHEN values ('LH0056', 'BN0011', 'BS0001', '2024-12-27 11:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0057', 'BN0012', 'BS0002', '2024-12-28 14:30', N'đã hủy')
insert into LICHHEN values ('LH0058', 'BN0013', 'BS0003', '2024-12-28 16:15', N'đã xác nhận')
insert into LICHHEN values ('LH0059', 'BN0014', 'BS0004', '2024-12-29 08:00', N'chưa xác nhận')
insert into LICHHEN values ('LH0060', 'BN0015', 'BS0005', '2024-12-29 10:15', N'đã hủy')
--tg now
INSERT INTO LICHHEN VALUES ('LH0090', 'BN0013', 'BS0001', '2024-11-22 15:18', N'đã xác nhận');
INSERT INTO LICHHEN VALUES ('LH0091', 'BN0014', 'BS0001', '2024-11-22 15:20', N'chưa xác nhận');
INSERT INTO LICHHEN VALUES ('LH0092', 'BN0015', 'BS0001', '2024-11-22 15:22', N'đã xác nhận');
INSERT INTO LICHHEN VALUES ('LH0093', 'BN0010', 'BS0002', '2024-11-22 15:23', N'chưa xác nhận');
INSERT INTO LICHHEN VALUES ('LH0094', 'BN0007', 'BS0003', '2024-11-22 15:25', N'đã xác nhận');
INSERT INTO LICHHEN VALUES ('LH0095', 'BN0008', 'BS0004', '2024-11-22 15:30', N'chưa xác nhận');
INSERT INTO LICHHEN VALUES ('LH0096', 'BN0009', 'BS0005', '2024-11-22 15:35', N'đã xác nhận');
INSERT INTO LICHHEN VALUES ('LH0097', 'BN0010', 'BS0003', '2024-11-22 15:50', N'chưa xác nhận');
INSERT INTO LICHHEN VALUES ('LH0098', 'BN0011', 'BS0002', '2024-11-22 15:58', N'đã xác nhận');
INSERT INTO LICHHEN VALUES ('LH0099', 'BN0012', 'BS0002', '2024-11-22 16:00', N'chưa xác nhận');
INSERT INTO LICHHEN VALUES ('LH0100', 'BN0012', 'BS0002', '2024-11-22 17:12', N'chưa xác nhận');
INSERT INTO LICHHEN VALUES ('LH0101', 'BN0012', 'BS0002', '2024-11-22 17:14', N'chưa xác nhận');

INSERT INTO LICHHEN VALUES ('LH0102', 'BN0009', 'BS0005', '2024-11-22 17:17', N'đã xác nhận');
---LSKHAM
INSERT INTO LSKHAM  VALUES
('BA0001', 'BN0001', 'BS0001', '2024-01-10', N'Viêm da cơ địa', N'Dùng kem bôi Hydrocortisone 2 lần/ngày'),
('BA0002', 'BN0002', 'BS0002', '2024-01-15', N'Mụn trứng cá', N'Uống Isotretinoin 10mg/ngày và dùng gel Adapalene buổi tối'),
('BA0003', 'BN0003', 'BS0003', '2024-01-20', N'Vẩy nến', N'Sử dụng kem bôi Calcipotriene 2 lần/ngày'),
('BA0004', 'BN0004', 'BS0004', '2024-01-25', N'Nấm da', N'Dùng kem bôi Clotrimazole 2 lần/ngày trong 4 tuần'),
('BA0005', 'BN0005', 'BS0005', '2024-02-01', N'Chàm', N'Dùng kem bôi Betamethasone 2 lần/ngày'),
('BA0006', 'BN0001', 'BS0002', '2024-02-05', N'Mụn trứng cá', N'Dùng gel bôi Benzoyl Peroxide 2.5% buổi sáng'),
('BA0007', 'BN0002', 'BS0003', '2024-02-10', N'Chàm', N'Sử dụng kem bôi Triamcinolone 2 lần/ngày'),
('BA0008', 'BN0003', 'BS0004', '2024-02-15', N'Nấm móng', N'Dùng thuốc uống Terbinafine 250mg/ngày trong 6 tuần'),
('BA0009', 'BN0004', 'BS0005', '2024-02-20', N'Viêm nang lông', N'Sử dụng kem bôi Clindamycin 2 lần/ngày'),
('BA0010', 'BN0005', 'BS0001', '2024-02-25', N'Chàm', N'Dùng kem bôi Hydrocortisone 2 lần/ngày'),
('BA0011', 'BN0001', 'BS0003', '2024-03-01', N'Mụn trứng cá', N'Dùng gel bôi Adapalene buổi tối'),
('BA0012', 'BN0002', 'BS0004', '2024-03-05', N'Viêm da cơ địa', N'Sử dụng kem bôi Mometasone 2 lần/ngày'),
('BA0013', 'BN0003', 'BS0005', '2024-03-10', N'Chàm', N'Dùng kem bôi Triamcinolone 2 lần/ngày'),
('BA0014', 'BN0004', 'BS0001', '2024-03-15', N'Nấm da', N'Sử dụng kem bôi Clotrimazole 2 lần/ngày trong 4 tuần'),
('BA0015', 'BN0005', 'BS0002', '2024-03-20', N'Vẩy nến', N'Sử dụng kem bôi Calcipotriene 2 lần/ngày'),
('BA0016', 'BN0001', 'BS0004', '2024-03-25', N'Mụn trứng cá', N'Uống Isotretinoin 10mg/ngày và dùng gel Adapalene buổi tối'),
('BA0017', 'BN0002', 'BS0005', '2024-04-01', N'Chàm', N'Sử dụng kem bôi Betamethasone 2 lần/ngày'),
('BA0018', 'BN0003', 'BS0001', '2024-04-05', N'Nấm móng', N'Dùng thuốc uống Terbinafine 250mg/ngày trong 6 tuần'),
('BA0019', 'BN0004', 'BS0002', '2024-04-10', N'Viêm nang lông', N'Sử dụng kem bôi Clindamycin 2 lần/ngày'),
('BA0020', 'BN0005', 'BS0003', '2024-04-15', N'Viêm da cơ địa', N'Dùng kem bôi Hydrocortisone 2 lần/ngày');
-- Đơn thuốc
INSERT INTO DONTHUOC (madt, maba, ngaykedon, tongtien)
VALUES  ('DT01', 'BA0001', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0001'), 0),  -- Tổng tiền dựa trên THUOC 'T01'
		('DT02', 'BA0003', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0003'), 0),  -- Tổng tiền dựa trên THUOC 'T02'
		('DT03', 'BA0004', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0004'), 0),  -- Tổng tiền dựa trên THUOC 'T03'
		('DT04', 'BA0005', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0005'), 0),  -- Tổng tiền dựa trên THUOC 'T04'
		('DT05', 'BA0007', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0007'), 0),  -- Tổng tiền dựa trên THUOC 'T05'
		('DT06', 'BA0008', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0008'), 0),  -- Tổng tiền dựa trên THUOC 'T06'
		('DT07', 'BA0010', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0010'), 0),  -- Tổng tiền dựa trên THUOC 'T07'
		('DT08', 'BA0011', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0011'), 0),  -- Tổng tiền dựa trên THUOC 'T08'
		('DT09', 'BA0012', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0012'), 0),  -- Tổng tiền dựa trên THUOC 'T09'
		('DT10', 'BA0015', (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0015'), 0); -- Tổng tiền dựa trên THUOC 'T10'

--nhập CHITHIETDONTHUOC
INSERT INTO CHITIETDONTHUOC (madt, mat, lieuluong, soluong)
VALUES 
    ('DT01', 'T01', N'2 lần/ngày', 1),-- Thuốc 'Hydrocortisone' cho đơn thuốc 'DT01'
	('DT01', 'T02', N'10mg/ngày', 3),--Thuốc 'Mometasone' cho đơn thuốc 'DT01'
    ('DT02', 'T02', N'10mg/ngày', 30),-- Thuốc 'Mometasone' cho đơn thuốc 'DT02'
    ('DT03', 'T03', N'1 lần/ngày', 20),-- Thuốc 'Clobetasol' cho đơn thuốc 'DT03'
	('DT03', 'T01', N'2 lần/ngày', 5),-- Thuốc 'Hydrocortisone' cho đơn thuốc 'DT03'
	('DT03', 'T04', N'2 lần/ngày', 2), -- Thuốc 'Fluocinonide' cho đơn thuốc 'DT03'
    ('DT04', 'T04', N'2 lần/ngày', 25),  -- Thuốc 'Fluocinonide' cho đơn thuốc 'DT04'
    ('DT05', 'T05', N'2 lần/ngày', 40),  -- Thuốc 'Betamethasone' cho đơn thuốc 'DT05'
    ('DT06', 'T06', N'1 lần/ngày', 35),-- Thuốc 'Triamcinolone' cho đơn thuốc 'DT06'
	('DT06', 'T05', N'2 lần/ngày', 15),-- Thuốc 'Betamethasone' cho đơn thuốc 'DT06'
    ('DT07', 'T07', N'2 lần/ngày', 15),  -- Thuốc 'Calcipotriene' cho đơn thuốc 'DT07'
    ('DT08', 'T08', N'2 lần/ngày', 18),  -- Thuốc 'Tazarotene' cho đơn thuốc 'DT08'
    ('DT09', 'T09', N'1 lần/ngày', 20),  -- Thuốc 'Adapalene' cho đơn thuốc 'DT09'
    ('DT10', 'T10', N'10mg/ngày', 10),  -- Thuốc 'Isotretinoin' cho đơn thuốc 'DT10'
	('DT10', 'T09', N'1 lần/ngày', 6),  -- Thuốc 'Adapalene' cho đơn thuốc 'DT10'
	('DT10', 'T08', N'2 lần/ngày', 8),  -- Thuốc 'Tazarotene' cho đơn thuốc 'DT10'
	('DT10', 'T05', N'2 lần/ngày', 8)-- Thuốc 'Betamethasone' cho đơn thuốc 'DT10'

 



	

--HOADONDV
INSERT INTO HOADONDV 
VALUES ('HD0001', 'BA0001', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0001'), 'NV0001'),
	   ('HD0002', 'BA0002', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0002'), 'NV0001'),
	   ('HD0003', 'BA0003', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0003'), 'NV0002'),
	   ('HD0004', 'BA0004', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0004'), 'NV0002'),
	   ('HD0005', 'BA0006', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0006'), 'NV0003'),
	   ('HD0006', 'BA0008', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0008'), 'NV0003'),
	   ('HD0007', 'BA0009', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0009'), 'NV0004'),
	   ('HD0008', 'BA0010', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0010'), 'NV0005'),
	   ('HD0009', 'BA0012', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0012'), 'NV0005'),
	   ('HD0010', 'BA0013', 0, (SELECT ngaykham FROM LSKHAM WHERE maba = 'BA0013'), 'NV0005')


	


	

--CHITIETHDDV
INSERT INTO CHITIETHDDV (mahddv, madv, soluong)
VALUES	('HD0001', 'DV0001', 1),
		('HD0001', 'DV0002', 2),
		('HD0002', 'DV0003', 1),
		('HD0002', 'DV0004', 1),
		('HD0003', 'DV0005', 1),
		('HD0003', 'DV0006', 2),
		('HD0004', 'DV0007', 1),
		('HD0005', 'DV0008', 1),
		('HD0006', 'DV0009', 1),
		('HD0006', 'DV0010', 2)
INSERT INTO CHITIETHDDV (mahddv, madv, soluong)
VALUES	('HD0007', 'DV0001', 1),
	('HD0008', 'DV0002', 2),
	('HD0009', 'DV0003', 1),
	('HD0010', 'DV0004', 1)

    
--SELECT 
--    BENHNHAN.mabn, 
--    BENHNHAN.hoten, 
--    COUNT(LSKHAM.maba) AS solankham
--FROM 
--    BENHNHAN
--LEFT JOIN 
--    LSKHAM ON BENHNHAN.mabn = LSKHAM.mabn
--GROUP BY 
--    BENHNHAN.mabn, BENHNHAN.hoten;
--tính tổng tiền hóa đơn dịch vụ 
--update trước 
 --update HOADONDV
 --set tongtien =(select sum (cthd.soluong*dv.dongia)
	--			 from CHITIETHDDV cthd 
	--			 join DICHVU dv on dv.madv=cthd.madv
	--			 where cthd.mahddv=HOADONDV.mahddv)


----tính tổng tiền đơn thuốc cho mỗi lần bệnh nhân đến khám 
-- select sum (ctdt.soluong*THUOC.dongia)
-- from CHITIETDONTHUOC ctdt 
-- join THUOC on THUOC.mat=ctdt.mat
-- where ctdt.madt='DT01'

-- --
-- update DONTHUOC
-- set tongtien =(select sum (ctdt.soluong*THUOC.dongia)
--				 from CHITIETDONTHUOC ctdt 
--				 join THUOC on THUOC.mat=ctdt.mat
--				 where ctdt.madt=DONTHUOC.madt)

-- --


--select * from NHANVIEN
--select * from BACSI
--select * from BENHNHAN
--select * from DICHVU
--select * from CHITIETHDDV
--select * from HOADONDV
--select * from THUOC
--select * from DONTHUOC
--select * from CHITIETDONTHUOC
select * from LICHHEN
--select * from LSKHAM
SELECT MALH,HOTEN,FORMAT(NGAYHEN,'HH:mm dd/MM/yyyy') AS 'NGAYHEN'
FROM LICHHEN,BENHNHAN 
WHERE BENHNHAN.MABN = LICHHEN.MABN AND MABS = 'BS0005' 
order by cast(ngayhen as date), cast(ngayhen as time)

select * from LICHHEN

select distinct trangthai
from LICHHEN
--
select mabs,hoten
from BACSI
--
SELECT malh,lh.mabn,bn.HOTEN,lh.mabs,FORMAT(NGAYHEN,'HH:mm dd-MM-yyyy') AS 'NGAYHEN',trangthai 
FROM LichHen lh
join BENHNHAN bn on bn.MABN=lh.mabn
where malh='LH0001'

--
declare @TrangThai nvarchar(100), @MaLichHen char(10),@MaBenhNhan char(10),@HoTen NVARCHAR(50),@TuNgay DateTime, @DenNgay Datetime
set @TrangThai=N'Chưa xác nhận '
set @MaLichHen=null
set @MaBenhNhan=null
set @HoTen=null
set @TuNgay=null
set @DenNgay=null
SELECT malh,lh.mabn,bn.HOTEN,lh.mabs,FORMAT(NGAYHEN,'HH:mm:ss dd-MM-yyyy') AS 'NGAYHEN',trangthai 
FROM LichHen lh
join BENHNHAN bn on bn.MABN=lh.mabn
WHERE (@TrangThai IS NULL OR trangthai = @TrangThai) 
AND (@MaLichHen IS NULL OR malh = @MaLichHen)
AND (@MaBenhNhan IS NULL OR lh.mabn = @MaBenhNhan)
AND (@HoTen is null or HOTEN=@HoTen)
--AND (ngayhen BETWEEN @TuNgay  AND @DenNgay)
order by cast(ngayhen as date), cast(ngayhen as time)
--
select FORMAT(ngaysinh,'dd-MM-yyyy') as Ngaysinh,DIACHI,SDT,GIOITINH
from BENHNHAN bn
where mabn='BN0001'

select distinct GIOITINH 
from BENHNHAN
