
-- Bảng Khách hàng
CREATE TABLE KhachHang (
    makh NVARCHAR(20) PRIMARY KEY,
    tenkh NVARCHAR(50),
    cmnd NVARCHAR(50),
    quoctich NVARCHAR(50),
    sdt NVARCHAR(20),
    tuoikh INT,
    gioitinh NVARCHAR(20)
);

-- Bảng Dịch Vụ
CREATE TABLE DichVu (
    madichvu NVARCHAR(20) PRIMARY KEY,
    tendv NVARCHAR(50),
    gia INT,
    soluong INT, 
);


-- Bảng Nhân Viên
CREATE TABLE NhanVien (
    manv NVARCHAR(20) PRIMARY KEY,
    tennv NVARCHAR(50),
    chucvu NVARCHAR(50),
    ngaysinh DATE,
    ngaylam DATE,
    gioitinh NVARCHAR(20),
    taikhoan NVARCHAR(50),
    matkhau NVARCHAR(500)
);

-- Bảng Phòng
CREATE TABLE Phong (
    maphong NVARCHAR(20) PRIMARY KEY,
    tenphong NVARCHAR(50),
    loaiphong NVARCHAR(50),
    kieuphong NVARCHAR(50),
    tinhtrang NVARCHAR(50),
    gia INT,
    
);


-- Bảng Đặt Phòng
CREATE TABLE DatPhong (
    madp NVARCHAR(20) PRIMARY KEY,
    makh NVARCHAR(20),
    ngayden DATE,
    ngaydi DATE,
    maphong NVARCHAR(20),
    tinhtrang NVARCHAR(20),
    manv NVARCHAR(20),
    FOREIGN KEY (makh) REFERENCES KhachHang(makh),
    FOREIGN KEY (maphong) REFERENCES Phong(maphong),
    FOREIGN KEY (manv) REFERENCES NhanVien(manv)
);

CREATE TABLE NhanPhong (
    manp NVARCHAR(20) PRIMARY KEY,    -- Mã nhận phòng
    madp NVARCHAR(20),                -- Mã đặt phòng
    ngaynhan DATE,                    -- Ngày nhận phòng
    manv NVARCHAR(20),                -- Mã nhân viên phụ trách
    ghichu NVARCHAR(255),             -- Ghi chú thêm
    FOREIGN KEY (madp) REFERENCES DatPhong(madp),
	FOREIGN KEY (manv) REFERENCES NhanVien(manv)
);



-- Bảng Danh Sách Dịch Vụ (Sử dụng trong Đặt Phòng)
CREATE TABLE DSDichVu (
    madp NVARCHAR(20),
    madichvu NVARCHAR(20),
    tendv NVARCHAR(50),
    gia INT,
    soluong INT,
	tongtien int,

    FOREIGN KEY (madp) REFERENCES DatPhong(madp),
    FOREIGN KEY (madichvu) REFERENCES DichVu(madichvu)
);

-- Bảng Hóa Đơn
CREATE TABLE HoaDon (
    mahd NVARCHAR(20) PRIMARY KEY,
    madp NVARCHAR(20),
    tienphong FLOAT(16),
    tongtiendv FLOAT(16),
    tongtienthanhtoan FLOAT(16),
    ngaytao DATE,
    tinhtrang NVARCHAR(50),
    FOREIGN KEY (madp) REFERENCES DatPhong(madp)
);
-- Xóa dữ liệu bảng phụ
DELETE FROM DSDichVu;
DELETE FROM NhanPhong;
DELETE FROM HoaDon;
DELETE FROM DatPhong;
DELETE FROM DichVu;
DELETE FROM Phong;
DELETE FROM KhachHang;
DELETE FROM NhanVien;

-- Bảng Khách Hàng
INSERT INTO KhachHang (makh, tenkh, cmnd, quoctich, sdt, tuoikh, gioitinh)
VALUES ('KH001', N'Nguyễn Văn A', N'123456789', N'Việt Nam', N'0901234567', 30, N'Nam'),
('KH002', N'Nguyễn Thị B', N'123459876', N'Việt Nam', N'0901237654', 30, N'Nữ');

-- Bảng Dịch Vụ
INSERT INTO DichVu (madichvu, tendv, gia, soluong)
VALUES ('DV001', N'Ăn sáng', 100, 100),
('DV002', N'Bể bơi', 50, 100);

-- Bảng Nhân Viên
INSERT INTO NhanVien (manv, tennv, chucvu, ngaysinh, ngaylam, gioitinh, taikhoan, matkhau)
VALUES 
    ('QL001', N'Lưu Nguyên Trường', N'Quản lý', '1980-11-20', '2015-09-10', N'Nam', N'truongln', N'123456'),
    ('QL002', N'Hoàng Mạnh Cường', N'Quản lý', '1980-11-20', '2015-09-10', N'Nam', N'cuonghm', N'123456'),
    ('NV001', N'Nguyễn Lam Sơn', N'Nhân viên', '1993-02-14', '2023-04-01', N'Nam', N'sonnl', N'123456'),
    ('NV002', N'Trần Đỗ Minh Hải', N'Nhân viên', '1980-11-20', '2015-09-10', N'Nam', N'haitdm', N'123456'),
	('NV003', N'Lê Ngọc Bảo Khánh', N'Nhân viên', '1980-11-20', '2015-09-10', N'Nam', N'khanhlnb', N'123456');

-- Bảng Phòng
INSERT INTO Phong (maphong, tenphong, loaiphong, kieuphong, tinhtrang, gia)
VALUES ('P001', N'Phòng VIP 1', N'VIP', N'Đơn', N'Trống', 500),
('P002', N'Phòng VIP 2', N'VIP', N'Đơn', N'Trống', 500);




CREATE PROCEDURE InHoaDon
    @mahd NVARCHAR(20)
AS
BEGIN
    SELECT 
        P.tenphong AS TenPhong,
		P.gia AS Gia,
        DP.ngayden AS NgayDen,
        DP.ngaydi AS NgayDi,
        DV.tendv AS TenDichVu,
        DSDV.soluong AS SoLuongDichVu,
        DV.gia AS GiaDichVu,
        (DSDV.soluong * DV.gia) AS ThanhTienDichVu,
        P.gia AS GiaPhongMoiNgay,
        DATEDIFF(DAY, DP.ngayden, DP.ngaydi) AS SoNgayO,
        DATEDIFF(DAY, DP.ngayden, DP.ngaydi) * P.gia AS ThanhTienPhong,
        SUM(DSDV.soluong * DV.gia) AS TongTienDichVu,
        DATEDIFF(DAY, DP.ngayden, DP.ngaydi) * P.gia + SUM(SUM(DSDV.soluong * DV.gia)) OVER() AS TongTienThanhToan
    FROM 
        HoaDon HD
        JOIN DatPhong DP ON HD.madp = DP.madp
        JOIN Phong P ON DP.maphong = P.maphong
        JOIN DSDichVu DSDV ON DP.madp = DSDV.madp
        JOIN DichVu DV ON DSDV.madichvu = DV.madichvu
    WHERE 
        HD.mahd = @mahd
    GROUP BY 
        P.tenphong,P.gia, DP.ngayden, DP.ngaydi, DV.tendv, DSDV.soluong, DV.gia, P.gia;
END; 

