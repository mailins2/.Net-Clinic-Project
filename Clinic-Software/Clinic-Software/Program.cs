using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic_Software
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static string LoginID { get; set; }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Homepage());
            //Application.Run(new BacSi());
            //Application.Run(new KeDonThuoc());
            //Application.Run(new KhamBenh());
            //Application.Run(new ThongTinKhamBenh());
            //Application.Run(new KeDichVu());
            Application.Run(new Dangnhap());
            Application.Run(new Doimatkhau());
            //Application.Run(new LichHen());
            //Application.Run(new XemLichHen());
            //Application.Run(new DangKyLichHen());
            //Application.Run(new Nhansu());
            //Application.Run(new Baocaodoanhthu());
            //Application.Run(new BCdanhsachkhambenh());
            //Application.Run(new BCdoanhthudichvu());
        }
    }
}
