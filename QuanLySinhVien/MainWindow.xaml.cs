using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace QuanLySinhVien
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnKetnoi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-30D4PD3; Database=QuanLyCanBo; Integrated Security=SSPI"))
                {
                    connection.Open();
                }
                MessageBox.Show("Mo va dong co so du lieu thanh cong.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi: " + ex.Message);
            }
        }

        private void btnDulieu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SinhVien> DanhSachSinhVien = new List<SinhVien>();
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanLyCanBo;Integrated Security=SSPI"))
                using (SqlCommand command =
                new SqlCommand("SELECT PhongBanID,TenPhongBan FROM PhongBan; ", connection))
 {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sv = new SinhVien();
                            sv.PhongBanID = reader.GetString(0);
                            sv.TenPhongBan = reader.GetString(1);
                            DanhSachSinhVien.Add(sv);
                        }
                    }
                }
                MessageBox.Show("Mo va dong co so du lieu thanh cong.");
                dulieu.ItemsSource = DanhSachSinhVien;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi: " + ex.Message);
            }
        }
        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.PhongBanID = "4";
            sv.TenPhongBan = "CTT13";
            if (Them_sinh_vien(sv) > 0)
                MessageBox.Show("Du lieu duoc them thanh cong!");
        }
        private int Them_sinh_vien(SinhVien sinhvien)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanLyCanBo; Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("INSERT INTO PhongBan(PhongBanID,TenPhongBan) VALUES(@PhongBanID,@TenPhongBan);", connection))
                {
                    command.Parameters.Add("PhongBanID", SqlDbType.NVarChar, 50).Value =
                    sinhvien.PhongBanID;
                    object dbTenPhongBan = sinhvien.TenPhongBan;
                    if (dbTenPhongBan == null)
                    {
                        dbTenPhongBan = DBNull.Value;
                    }
                    command.Parameters.Add("TenPhongBan", SqlDbType.NVarChar, 50).Value =
                    dbTenPhongBan;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi:" + ex.Message);
                return -1;
            }
        }
        private void BtnXoa_Click_1(object sender, RoutedEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.PhongBanID = txtMaSV.Text;
            if (Xoa_sinh_vien(sv) > 0)
                MessageBox.Show("Du lieu duoc xoa thanh cong!");
        }
        private int Xoa_sinh_vien(SinhVien sinhvien)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-30D4PD3;Database=QuanLyCanBo;Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("DELETE FROM PhongBan WHERE PhongBanID = @PhongBanID", connection))
                {
                    command.Parameters.Add("PhongBanID", SqlDbType.NVarChar,50).Value = sinhvien.PhongBanID;
                connection.Open();
                return command.ExecuteNonQuery();
            }
 }
 catch (Exception ex)
 {
 MessageBox.Show("Loi khi mo ket noi:" + ex.Message);
 return -1;
 }
}

        private void BtnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            SinhVien sv = new SinhVien();
            sv.PhongBanID = txtMaSV.Text;
            sv.TenPhongBan = txtTenSV.Text;
            if (Cap_nhat_sinh_vien(sv) > 0)
                MessageBox.Show("Du lieu duoc cap nhat thanh cong!");
        }

        private int Cap_nhat_sinh_vien(SinhVien sinhvien)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-30D4PD3; Database=QuanLyCanBo; Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("UPDATE PhongBan SET PhongBanID = @PhongBanID,TenPhongBan = @TenPhongBan WHERE PhongBanID = @PhongBanID", connection))
                {
                    command.Parameters.Add("PhongBanID", SqlDbType.NVarChar, 50).Value =
                    sinhvien.PhongBanID;
                    command.Parameters.Add("TenPhongBan", SqlDbType.NVarChar, 50).Value =
                    sinhvien.TenPhongBan;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi:" + ex.Message);
                return -1;
            }
        }
    }
    }
